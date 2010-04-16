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
            get { return GetStorageField<int>((uint)Offsets.WowObjectFields.OBJECT_FIELD_TYPE); }
        }

        /// <summary>
        /// The object's Entry.
        /// </summary>
        public int Entry
        {
            get { return GetStorageField<int>((uint)Offsets.WowObjectFields.OBJECT_FIELD_ENTRY); }
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
            field = field * 4;
            var m_pStorage = ObjectManager.Memory.ReadUInt(BaseAddress + 0x08);
            return (T)ObjectManager.Memory.ReadObject(m_pStorage + field, typeof(T));
        }

        /// <summary>
        /// Gets the descriptor struct.
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
            uint _field = (uint)field * 4;
            SetStorageField<T>(_field, value);
        }

        #endregion
    }
}
