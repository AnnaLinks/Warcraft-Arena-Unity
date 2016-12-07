﻿using UnityEngine;
using System.Collections.Generic;

public enum MageSpells
{
    FrostBolt,
    FireBall,
    FrostNova,
    Blink,
}

public class WarcraftDatabase : MonoBehaviour
{ 
    public static WarcraftDatabase Instanse { get; private set; }

    #region Spell Info

    public static Dictionary<int, TrinitySpellInfo> SpellInfos { get; private set; }
    
    public static Dictionary<int, SpellChargeCategory> SpellChargeCategories { get; private set; }
    public static Dictionary<int, SpellCastTimes> SpellCastTimes { get; private set; }
    public static Dictionary<int, SpellDuration> SpellDurations { get; private set; }
    public static Dictionary<int, SpellRange> SpellRanges { get; private set; }
    public static Dictionary<int, SpellPowerCost> SpellPowerCosts { get; private set; }
    #endregion

    void Awake()
    {
        if (Instanse == null)
            Instanse = this;
        else
        {
            Destroy(this);
            return;
        }

        Load();

        System.GC.Collect();
    }


    void Load()
    {
        LoadSpells();
    }

    // #TODO : Read from json
    void LoadSpells()
    {
        LoadSpellPowerCosts();

        LoadSpellRanges();

        LoadSpellDurations();

        LoadCastTimes();

        LoadSpellChargeCategories();

        LoadSpellInfo();
    }


    #region Spell Info Loading

    void LoadSpellPowerCosts()
    {
        SpellPowerCosts = new Dictionary<int, SpellPowerCost>();

        // Standard mana minor cost : Referenced in spell helper
        var spellPowerCost = new SpellPowerCost()
        {
            Id = 1,
            ManaCost = 0,
            ManaCostPercentage = 2.0f,
            HealthCostPercentage = 0.0f,
            ManaCostPercentagePerSecond = 0.0f,

            RequiredAura = -1,
        };
        SpellPowerCosts.Add(spellPowerCost.Id, spellPowerCost);
    }

    void LoadSpellRanges()
    {
        SpellRanges = new Dictionary<int, SpellRange>();

        // Self cast : Referenced in spell helper
        var spellRange = new SpellRange()
        {
            Id = 1,
            Flags = SpellRangeFlag.DEFAULT,
        };
        SpellRanges.Add(spellRange.Id, spellRange);
    }

    void LoadSpellDurations()
    {
        SpellDurations = new Dictionary<int, SpellDuration>();

        for(int i = 0; i <= 120; i++)
        {
            SpellDurations.Add(i, new SpellDuration()
            {
                Id = i,
                Duration = i,
                DurationPerLevel = 0,
                MaxDuration = i,
            });
        }
    }

    void LoadCastTimes()
    {
        SpellCastTimes = new Dictionary<int, SpellCastTimes>();

        // Instant cast : Referenced in spell helper
        var spellCastTime = new SpellCastTimes()
        {
            Id = 1,
            CastTime = 0.0f,
            CastTimePerLevel = 0.0f,
            MinCastTime = 0.0f,
        };
        SpellCastTimes.Add(spellCastTime.Id, spellCastTime);
    }

    void LoadSpellChargeCategories()
    {
        SpellChargeCategories = new Dictionary<int, SpellChargeCategory>();

        // No charges : Referenced in spell helper
        var spellCategory = new SpellChargeCategory()
        {
            Id = 1,
            ChargeRecoveryTime = 0,
            MaxCharges = 0,
            ChargeCategoryType = 0,
        };
        SpellChargeCategories.Add(spellCategory.Id, spellCategory);
    }

    void LoadSpellInfo()
    {
        SpellInfos = new Dictionary<int, TrinitySpellInfo>();

        // #1 Frost Nova
        var spellInfo = new TrinitySpellInfo()
        {
            Id = 1,
            Dispel = DispelType.MAGIC,
            Mechanic = Mechanics.FREEZE,
            Attributes = SpellAttributes.DAMAGE_DOESNT_BREAK_AURAS | SpellAttributes.CANT_BE_REFLECTED,
            Targets = Targets.TARGET_UNIT_SRC_AREA_ENEMY,
            SchoolMask = SpellSchoolMask.FROST,
            DamageClass = SpellDamageClass.MAGIC,
            PreventionType = SpellPreventionType.SILENCE | SpellPreventionType.PACIFY,
            ExplicitTargetMask = SpellCastTargetFlags.UNIT_ENEMY,
            InterruptFlags = SpellInterruptFlags.NONE,
            FamilyName = SpellFamilyNames.MAGE,

            ChargeCategory = SpellHelper.ZeroChargeCategory,
            CastTime = SpellHelper.InstantCastTime,
            Range = SpellHelper.SelfCastRange,
            Duration = SpellDurations[8],
            PowerCosts = new List<SpellPowerCost>() { SpellHelper.BasicManaCost },

            RecoveryTime = 0.5f,
            StartRecoveryTime = 0.0f,
            StartRecoveryCategory = 0,

            Speed = 0.0f,

            StackAmount = 0,
            MaxAffectedTargets = 0,

            SpellIconId = 1,
            ActiveIconId = 2,
        };
    }

    #endregion
}