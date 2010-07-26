using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Magic;

namespace BlackRain.Common.Objects
{
    /// <summary>
    /// Manages all the BlackRain WowObjects, and maintains them.
    /// </summary>
    public static class ObjectManager
    {
        #region <Enums>

        // ReSharper disable UnusedMember.Local
        private enum ObjectType : uint
        {
            Object = 0,
            Item = 1,
            Container = 2,
            Unit = 3,
            Player = 4,
            GameObject = 5,
            DynamicObject = 6,
            Corpse = 7,
            AiGroup = 8,
            AreaTrigger = 9
        }
        // ReSharper restore UnusedMember.Local

        #endregion

        /// <summary>
        /// The instance of BlackMagic used for World of Warcraft memory editing.
        /// </summary>
        public static BlackMagic Memory { get; set; }

        /// <summary>
        /// Is the ObjectManager initialized?
        /// </summary>
        public static bool Initialized { get { return Memory != null; } }

        /// <summary>
        /// A list of all Objects.
        /// </summary>
        public static List<WowObject> Objects = new List<WowObject>();

        /// <summary>
        /// A list of all Corpses.
        /// </summary>
        public static List<WowCorpse> Corpses { get { return GetObjectsOfType<WowCorpse>(false, true); } }

        /// <summary>
        /// A list of all units.
        /// </summary>
        public static List<WowUnit> Units { get { return GetObjectsOfType<WowUnit>(false, true); } }

        /// <summary>
        /// A list of all Game Objects.
        /// </summary>
        public static List<WowGameObject> GameObjects { get { return GetObjectsOfType<WowGameObject>(false, false); } }

        /// <summary>
        /// The local player.
        /// </summary>
        public static WowPlayer Me { get; set; }

        internal static uint CurrentManager { get; set; }

        /// <summary>
        /// The local player's GUID.
        /// </summary>
        public static ulong PlayerGUID { get; set; }

        /// <summary>
        /// Initialized the ObjectManager, and attaches it to the selected process ID.
        /// </summary>
        /// <param name="pID"></param>
        public static void Initialize(int pID)
        {

            if (Initialized) // Nothing to do if we're already initialized.
                return;

            Memory = new BlackMagic(pID);

            try
            {
                var TLS =
                    Memory.FindPattern(
                        "EB 02 33 C0 8B D 00 00 00 00 64 8B 15 00 00 00 00 8B 34 8A 8B D 00 00 00 00 89 81 00 00 00 00",
                        "xxxxxx????xxx????xxxxx????xx????");

                if (TLS != uint.MaxValue)
                {
                    var ClientConnection = Memory.ReadUInt(Memory.ReadUInt(TLS + 0x16));
                    var ClientConnectionOffset = Memory.ReadUInt(TLS + 0x1C);
                    CurrentManager = Memory.ReadUInt(ClientConnection + ClientConnectionOffset);

                    PlayerGUID = Memory.ReadUInt64(CurrentManager + 0xC0); // Store the player's GUID.
                }
            }
            catch (Exception ex)
            {
                Logging.WriteException(Color.Red, ex);
            }
        }

        /// <summary>
        /// Pulses the ObjectManager, refreshing any objects it holds.
        /// </summary>
        public static void Pulse()
        {
            if (!Initialized) // Can't pulse if we're not onto something!
                return;

            if (Objects.Count > 0)
                Objects.Clear();

            try
            {
                var currentObject = new WowObject(Memory.ReadUInt(CurrentManager + 0xAC));

                while (currentObject.BaseAddress != uint.MinValue && currentObject.BaseAddress % 2 == uint.MinValue)
                {
                    switch (currentObject.Type)
                    {
                        case (int)ObjectType.Unit:
                            Objects.Add(new WowUnit(currentObject.BaseAddress));
                            break;
                        case (int)ObjectType.Item:
                            Objects.Add(new WowItem(currentObject.BaseAddress));
                            break;
                        case (int)ObjectType.Container:
                            Objects.Add(new WowContainer(currentObject.BaseAddress));
                            break;
                        case (int)ObjectType.Corpse:
                            Objects.Add(new WowCorpse(currentObject.BaseAddress));
                            break;
                        case (int)ObjectType.GameObject:
                            Objects.Add(new WowGameObject(currentObject.BaseAddress));
                            break;
                        case (int)ObjectType.DynamicObject:
                            Objects.Add(new WowDynamicObject(currentObject.BaseAddress));
                            break;
                        case (int)ObjectType.Player:
                            Objects.Add(new WowPlayer(currentObject.BaseAddress));
                            break;
                    }

                    if (currentObject.GUID == PlayerGUID)
                        Me = new WowPlayer(currentObject.BaseAddress);

                    currentObject.BaseAddress = Memory.ReadUInt(currentObject.BaseAddress + 0x3C);
                }


            }
            catch (Exception ex)
            {
                Logging.WriteException(Color.Red, ex);
            }

        }

        /// <summary>
        /// Reads the memory at the specified address.
        /// </summary>
        /// <typeparam name="T">The type of data to be read.</typeparam>
        /// <param name="address">The memory location.</param>
        /// <returns>(T) value</returns>
        public static T Read<T>(uint address)
        {
            object ret;
            Type t = typeof (T);

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Int16:
                    ret = Memory.ReadUShort(address);
                    break;
                case TypeCode.Int32:
                    ret = Memory.ReadInt(address);
                    break;
                case TypeCode.Int64:
                    ret = Memory.ReadInt64(address);
                    break;
                case TypeCode.String:
                    ret = Memory.ReadASCIIString(address, 40);
                    break;
                case TypeCode.UInt16:
                    ret = Memory.ReadUShort(address);
                    break;
                case TypeCode.UInt32:
                    ret = Memory.ReadUInt(address);
                    break;
                case TypeCode.UInt64:
                    ret = Memory.ReadUInt64(address);
                    break;
                case TypeCode.Single:
                    ret = Memory.ReadShort(address);
                    break;
                case TypeCode.Byte:
                    ret = Memory.ReadByte(address);
                    break;
                case TypeCode.Object:
                    ret = Memory.ReadObject(address, t);
                    break;
                case TypeCode.Double:
                    ret = Memory.ReadDouble(address);
                    break;
                default: throw new NotSupportedException(string.Format("Type {0} is not currently supported.", typeof(T).FullName));
            }

            return (T) ret;
        }

        #region <Search Members>

        /// <summary>
        /// Returns all objects of type T.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <returns></returns>
        public static List<T> ObjectsOfType<T>()
        {
            var objects = (from o in Objects.OfType<T>()
                          select o).ToList();

            return objects;
        }

        /// <summary>
        /// Gets object of the specified type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allowInheritance">Indicates whether to also get objects that derives from the specified type (ie. WoWPlayer derives from WoWUnit, so specifying WoWUnit and true would also return all players).</param>
        /// <param name="includeMeIfFound">Indicates whether to include the local player.</param>
        /// <returns></returns>
        public static List<T> GetObjectsOfType<T>(bool allowInheritance, bool includeMeIfFound) where T : WowObject
        {
            Type upperType = typeof(T);
            List<WowObject> objects = Objects;
            var tempObjects = new List<T>();

            for (int i = 0; i < objects.Count; i++)
            {
                Type t = objects[i].GetType();

                if (t == upperType || allowInheritance && t.IsSubclassOf(upperType))
                {
                    if (!includeMeIfFound && objects[i] == Me)
                        continue;

                    var obj = objects[i] as T;

                    if (obj != null)
                        tempObjects.Add(obj);
                }
            }

            return tempObjects;
        }

        #endregion
    }
}
