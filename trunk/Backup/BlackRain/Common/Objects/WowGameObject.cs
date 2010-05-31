using BlackRain.Constants;

namespace BlackRain.Common.Objects
{
    public class WowGameObject : WowObject
    {
        public WowGameObject(uint baseAddress)
            : base(baseAddress)
        {
        }

        /// <summary>
        /// The GameObject's Display ID.
        /// </summary>
        public int DisplayID
        {
            get { return GetStorageField<int>((uint)Offsets.WowGameObjectFields.GAMEOBJECT_DISPLAYID); }
        }

        /// <summary>
        /// The GameObject's faction.
        /// </summary>
        public int Faction
        {
            get { return GetStorageField<int>((uint) Offsets.WowGameObjectFields.GAMEOBJECT_FACTION); }
        }

        /// <summary>
        /// The GameObject's level.
        /// </summary>
        public int Level
        {
            get { return GetStorageField<int>((uint) Offsets.WowGameObjectFields.GAMEOBJECT_LEVEL); }
        }

        /// <summary>
        /// The GUID of the object this object was created by.
        /// <!-- Presumably, hasn't been double-checked. -->
        /// </summary>
        public ulong CreatedBy
        {
            get { return GetStorageField<ulong>((uint) Offsets.WowGameObjectFields.OBJECT_FIELD_CREATED_BY); }
        }
    }
}
