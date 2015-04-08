
```
using BlackRain.Constants;

namespace BlackRain.Common.Objects
{
    /// <summary>
    /// Contains all information for a WowObject.
    /// </summary>
    public class WowObject
    {
        public uint BaseAddress { get; set; }

        public WowObject(uint baseAddress)
        {
            BaseAddress = baseAddress;
        }

        /// <summary>
        /// The object's GUID.
        /// </summary>
        public virtual ulong GUID
        {
            get { return GetStorageField<ulong>((uint)Offsets.WowObjectFields.OBJECT_FIELD_GUID); }
        }

        /// <summary>
        /// The object's Type.
        /// </summary>
        public int Type
        {
            get { return ObjectManager.Memory.ReadInt(BaseAddress + 0x14); }
        }

        /// <summary>
        /// The object's Entry.
        /// </summary>
        public int Entry
        {
            get { return GetStorageField<int>((uint)Offsets.WowObjectFields.OBJECT_FIELD_ENTRY); }
        }

        /// <summary>
        /// The object's level.
        /// </summary>
        public int Level
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_LEVEL); }
        }

        /// <summary>
        /// Returns the object's X position.
        /// </summary>
        public virtual float X
        {
            get { return ObjectManager.Memory.ReadFloat(BaseAddress + (uint) Offsets.WowObject.X); }
        }

        /// <summary>
        /// Returns the object's Y position.
        /// </summary>
        public virtual float Y
        {
            get { return ObjectManager.Memory.ReadFloat(BaseAddress + (uint) Offsets.WowObject.Y); }
        }

        /// <summary>
        /// Returns the object's Z position.
        /// </summary>
        public virtual float Z
        {
            get { return ObjectManager.Memory.ReadFloat(BaseAddress + (uint) Offsets.WowObject.Z); }
        }

        /// <summary>
        /// Determines if the unit is our local player.
        /// </summary>
        public bool IsMe
        {
            get { return GUID == ObjectManager.LocalGUID ? true : false; }
        }

        #region <Storage Field Methods>

        /// <summary>
        /// Gets the descriptor struct.
        /// </summary>
        /// <typeparam name="T">struct</typeparam>
        /// <param name="field">Descriptor field</param>
        /// <returns>Descriptor field</returns>
        protected T GetStorageField<T>(uint field) where T : struct
        {
            field = field *4; // He's anal.
            var m_pStorage = ObjectManager.Memory.ReadUInt(BaseAddress + 0x08);

            return (T)ObjectManager.Memory.ReadObject(m_pStorage + field, typeof(T));
        }

        /// <summary>
        /// Gets the descriptor struct.
        /// Overload for when not casting uint.
        /// </summary>
        /// <typeparam name="T">struct</typeparam>
        /// <param name="field">Descriptor field</param>
        /// <returns>Descriptor field</returns>
        protected T GetStorageField<T>(Offsets.WowObjectFields field) where T : struct
        {
            return GetStorageField<T>((uint)field);
        }

        /// <summary>
        /// Sets the descriptors for the selected field and obect.
        /// * Writes to memory.
        /// </summary>
        /// <typeparam name="T">struct</typeparam>
        /// <param name="field">WowObject.DescriptorField</param>
        /// <param name="value">Object</param>
        protected void SetStorageField<T>(uint field, object value) where T : struct
        {
            field = field * 4;
            var m_pStorage = ObjectManager.Memory.ReadUInt(BaseAddress + 0x08);

            ObjectManager.Memory.WriteObject(ObjectManager.Memory.ReadUInt(m_pStorage + field), value, typeof(T));
        }

        /// <summary>
        /// Sets the descriptors for the selected field and obect.
        /// * Writes to memory.
        /// </summary>
        /// <typeparam name="T">struct</typeparam>
        /// <param name="field">WowObject.DescriptorField</param>
        /// <param name="value">Object</param>
        protected void SetStorageField<T>(Offsets.WowObjectFields field, object value) where T : struct
        {
            var _field = (uint)field * 4;
            SetStorageField<T>(_field, value);
        }

        #endregion

        #region <Conversion Members>

        /***
         * Only contains some basic conversions, which are commonly used as troubleshoots,
         * or used in Object Searching.
         **/

        #region <ToUnit()>
        public WowUnit ToUnit(WowObject obj)
        {
            return new WowUnit(obj.BaseAddress);
        }

        public WowUnit ToUnit(WowItem obj)
        {
            return new WowUnit(obj.BaseAddress);
        }
        #endregion

        #region <ToPlayer()>

        public WowPlayer ToPlayer(WowItem obj)
        {
            return new WowPlayer(obj.BaseAddress);
        }

        public WowPlayer ToPlayer(WowUnit obj)
        {
            return new WowPlayer(obj.BaseAddress);
        }

        public WowPlayer ToPlayer(WowObject obj)
        {
            return new WowPlayer(obj.BaseAddress);
        }

        public WowPlayer ToPlayer(WowPlayer obj)
        {
            return new WowPlayer(obj.BaseAddress);
        }

        #endregion

        #region <ToGameObject()>
        
        public WowGameObject ToGameObject(WowObject obj)
        {
            return new WowGameObject(obj.BaseAddress);
        }

        public WowGameObject ToGameObject(WowDynamicObject obj)
        {
            return new WowGameObject(obj.BaseAddress);
        }

        #endregion

        #endregion
    }
}
```