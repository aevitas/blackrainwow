using BlackRain.Constants;

namespace BlackRain.Common.Objects
{
    public class WowCorpseObject : WowUnit
    {
        public WowCorpseObject(uint baseAddress)
            : base(baseAddress)
        {
        }

        /// <summary>
        /// The corpse object's owner's GUID.
        /// </summary>
        public ulong Owner
        {
            get { return GetStorageField<ulong>((uint) Offsets.WowCorpseFields.CORPSE_FIELD_OWNER); }
        }

        /// <summary>
        /// The corpse's Display ID.
        /// </summary>
        public int CorpseDisplayID
        {
            get { return GetStorageField<int>((uint) Offsets.WowCorpseFields.CORPSE_FIELD_DISPLAY_ID); }
        }
    }
}
