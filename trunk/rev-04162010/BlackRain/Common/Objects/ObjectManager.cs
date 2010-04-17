using System;
using System.Collections.Generic;
using System.Diagnostics;
using BlackRain.Constants;
using Magic;

namespace BlackRain.Common.Objects
{
    public static class ObjectManager
    {
        public static BlackMagic Memory { get; set; }
        public static Process WowProcess { get; set; }
        public static WowPlayer Me { get; private set; }
        public static bool Initialized { get; private set; }

        #region <Initialization Fields>
        private static uint CConnectionPointer { get; set; }
        private static uint CConnection { get; set; }
        private static uint CConnectionOffset { get; set; }
        private static string TLSPattern { get { return "EB 02 33 C0 8B D 00 00 00 00 64 8B 15 00 00 00 00 8B 34 8A 8B D 00 00 00 00 89 81 00 00 00 00"; } }
        private static string TLSMask { get { return "xxxxxx????xxx????xxxxx????xx????"; } }
        private static uint ThreadLocalStorage { get; set; }
        private static uint CurrentManager { get; set; }
        #endregion
        #region <Properties>
        public static ulong LocalGUID { get; set; }
        #endregion

        public static List<WowObject> Objects  = new List<WowObject>();

        /// <summary>
        /// Initializes the ObjectManager, attaching it to the selected process.
        /// </summary>
        /// <param name="pid">The World of Warcraft process ID.</param>
        public static void Initialize(int pid)
        {
            if (Memory != null) return;
                Memory = new BlackMagic(pid); // Assign Memory to the selected Wow instance.

            try
            {
                ThreadLocalStorage = Memory.FindPattern(TLSPattern, TLSMask);

                if (ThreadLocalStorage != uint.MaxValue) // Ensure we have found the TLS.
                {
                    CConnection = Memory.ReadUInt(Memory.ReadUInt(ThreadLocalStorage + 0x16));
                    CConnectionOffset = Memory.ReadUInt(ThreadLocalStorage + 0x1C);
                    CurrentManager = Memory.ReadUInt(CConnection + CConnectionOffset);

                    Logging.Write(string.Format("CurrentManager found at {0:X}", CurrentManager));

                    // Retrieve the local player's GUID for later use.
                    LocalGUID = Memory.ReadUInt64(CurrentManager + (uint)Offsets.ObjectManager.LocalGUID);

                    if (CurrentManager != uint.MinValue && CurrentManager != uint.MaxValue)
                    {
                        Initialized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Write(ex.Message);
            }
        }

        /// <summary>
        /// Pulses the ObjectManager, refreshing any objects it holds.
        /// <!-- This is a slow way to go around things, but it's accurate, and that's what counts. -->
        /// </summary>
        public static void Pulse()
        {
            if (Objects.Count != int.MinValue)
                Objects.Clear();

            var currentObject = new WowObject(Memory.ReadUInt(CurrentManager + (uint) Offsets.ObjectManager.FirstObject));

            while (currentObject.BaseAddress != uint.MinValue && currentObject.BaseAddress % 2 == uint.MinValue)
            {
                if (currentObject.Type == (int) Offsets.WowObjectType.Unit)
                    Objects.Add(new WowUnit(currentObject.BaseAddress));
                if (currentObject.Type == (int) Offsets.WowObjectType.Item)
                    Objects.Add(new WowItem(currentObject.BaseAddress));
                if (currentObject.Type == (int) Offsets.WowObjectType.Container)
                    Objects.Add(new WowContainer(currentObject.BaseAddress));
                if (currentObject.Type == (int) Offsets.WowObjectType.Corpse)
                    Objects.Add(new WowCorpseObject(currentObject.BaseAddress));
                if (currentObject.Type == (int) Offsets.WowObjectType.GameObject)
                    Objects.Add(new WowGameObject(currentObject.BaseAddress));
                if (currentObject.Type == (int)Offsets.WowObjectType.DynamicObject)
                    Objects.Add(new WowDynamicObject(currentObject.BaseAddress));
                if (currentObject.Type == (int)Offsets.WowObjectType.Player)
                    Objects.Add(new WowPlayer(currentObject.BaseAddress));

                if (currentObject.GUID == LocalGUID)
                    Me = new WowPlayer(currentObject.BaseAddress);

                currentObject.BaseAddress = Memory.ReadUInt(currentObject.BaseAddress + (uint)Offsets.ObjectManager.NextObject);
            }
        }
    }
}
