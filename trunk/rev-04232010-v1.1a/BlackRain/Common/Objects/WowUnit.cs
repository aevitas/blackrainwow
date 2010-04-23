using BlackRain.Constants;

namespace BlackRain.Common.Objects
{
    public class WowUnit : WowObject
    {
        public WowUnit(uint baseAddress)
            : base(baseAddress)
        {
        }

        /// <summary>
        /// Is this unit a critter?
        /// </summary>
        public bool Critter
        {
            get { return GetStorageField<int>((uint) Offsets.WowUnitFields.UNIT_FIELD_CRITTER) == 1 ? true : false; }
        }

        /// <summary>
        /// The GUID of the object this unit is charmed by.
        /// </summary>
        public ulong CharmedBy
        {
            get { return GetStorageField<ulong>((uint) Offsets.WowUnitFields.UNIT_FIELD_CHARMEDBY); }
        }

        /// <summary>
        /// The GUID of the object this unit is summoned by.
        /// </summary>
        public ulong SummonedBy
        {
            get { return GetStorageField<ulong>((uint) Offsets.WowUnitFields.UNIT_FIELD_SUMMONEDBY); }
        }

        /// <summary>
        /// The GUID of the object this unit was created by.
        /// </summary>
        public ulong CreatedBy
        {
            get { return GetStorageField<ulong>((uint) Offsets.WowUnitFields.UNIT_FIELD_CREATEDBY); }
        }

        /// <summary>
        /// The unit's health.
        /// </summary>
        public int Health
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_HEALTH); }
        }

        /// <summary>
        /// The unit's maximum health.
        /// </summary>
        public int MaximumHealth
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_MAXHEALTH); }
        }

        /// <summary>
        /// The unit's health as a percentage of its total.
        /// </summary>
        public int HealthPercentage
        {
            get { return (100 * Health) / MaximumHealth; }
        }

        /// <summary>
        /// The unit's base health.
        /// </summary>
        public int BaseHealth
        {
            get { return GetStorageField<int>((uint) Offsets.WowUnitFields.UNIT_FIELD_BASE_HEALTH); }
        }

        /// <summary>
        /// The unit's base health.
        /// </summary>
        public int BaseMana
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_BASE_MANA); }
        }

        /// <summary>
        /// The unit's mana.
        /// </summary>
        public int Mana
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER1); }
        }

        /// <summary>
        /// The unit's rage.
        /// </summary>
        public int Rage
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER2); }
        }

        /// <summary>
        /// The unit's focus.
        /// </summary>
        public int Focus
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER3); }
        }

        /// <summary>
        /// The unit's energy.
        /// </summary>
        public int Energy
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER4); }
        }

        /// <summary>
        /// The unit's happinnes.
        /// </summary>
        public int Happinnes
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER4); }
        }

        /// <summary>
        /// The unit's runic power.
        /// </summary>
        public int RunicPower
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER5); }
        }

        /// <summary>
        /// The unit's runes.
        /// </summary>
        public int Runes
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_POWER6); }
        }

        /// <summary>
        /// The unit's maximum mana.
        /// </summary>
        public int MaximumMana
        {
            get { return GetStorageField<int>((uint) Offsets.WowUnitFields.UNIT_FIELD_MAXPOWER1); }
        }

        /// <summary>
        /// The unit's maximum rage.
        /// </summary>
        public int MaximumRage
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_MAXPOWER2); }
        }

        /// <summary>
        /// The unit's maximum focus.
        /// </summary>
        public int MaximumFocus
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_MAXPOWER3); }
        }

        /// <summary>
        /// The unit's maximum energy.
        /// </summary>
        public int MaximumEnergy
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_MAXPOWER4); }
        }

        /// <summary>
        /// The unit's maximum runic power.
        /// </summary>
        public int MaximumRunicPower
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_MAXPOWER5); }
        }

        /// <summary>
        /// The unit's maximum runes.
        /// <!-- This is presumably always 6, two blood, two frost, two unholy. But may be different on DK based bosses/mobs? -->
        /// </summary>
        public int MaximumRunes
        {
            get { return GetStorageField<int>((uint)Offsets.WowUnitFields.UNIT_FIELD_MAXPOWER6); }
        }

        /// <summary>
        /// The unit's level.
        /// </summary>
        public new int Level
        {
            get { return GetStorageField<int>((uint) Offsets.WowUnitFields.UNIT_FIELD_LEVEL); }
        }

        /// <summary>
        /// The unit's DisplayID.
        /// </summary>
        public int DisplayID
        {
            get { return GetStorageField<int>((uint) Offsets.WowUnitFields.UNIT_FIELD_DISPLAYID); }
        }

        /// <summary>
        /// The mount display of the mount the unit is mounted on.
        /// </summary>
        public int MountDisplayID
        {
            get { return GetStorageField<int>((uint) Offsets.WowUnitFields.UNIT_FIELD_MOUNTDISPLAYID); }
        }

        /// <summary>
        /// The GUID of the object this unit is targeting.
        /// </summary>
        public ulong Target
        {
            get { return GetStorageField<ulong>((uint) Offsets.WowUnitFields.UNIT_FIELD_TARGET); }
        }
    }
}
