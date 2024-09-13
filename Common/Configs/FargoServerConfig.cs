// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Configs.FargoServerConfig
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Config;

#nullable disable
namespace Fargowiltas.Common.Configs
{
  public sealed class FargoServerConfig : ModConfig
  {
    public static FargoServerConfig Instance;
    [Header("$Mods.Fargowiltas.Configs.FargoServerConfig.Headers.TownNPCs")]
    [DefaultValue(true)]
    public bool Mutant;
    [DefaultValue(true)]
    public bool Abom;
    [DefaultValue(true)]
    public bool Devi;
    [DefaultValue(true)]
    public bool Lumber;
    [DefaultValue(true)]
    public bool Squirrel;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool CatchNPCs;
    [DefaultValue(true)]
    public bool NPCSales;
    [DefaultValue(true)]
    public bool SaferBoundNPCs;
    [Header("$Mods.Fargowiltas.Configs.FargoServerConfig.Headers.Seasons")]
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections Halloween;
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections Christmas;
    [Header("$Mods.Fargowiltas.Configs.FargoServerConfig.Headers.Seeds")]
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections DrunkWorld;
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections BeeWorld;
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections WorthyWorld;
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections CelebrationWorld;
    [DefaultValue(0)]
    [DrawTicks]
    public SeasonSelections ConstantWorld;
    [Header("$Mods.Fargowiltas.Configs.FargoServerConfig.Headers.Unlimited")]
    [DefaultValue(true)]
    public bool UnlimitedAmmo;
    [DefaultValue(true)]
    public bool UnlimitedConsumableWeapons;
    [DefaultValue(true)]
    public bool UnlimitedPotionBuffsOn120;
    private const uint maxExtraBuffSlots = 99;
    [Header("$Mods.Fargowiltas.Configs.FargoServerConfig.Headers.StatMultipliers")]
    [Range(1f, 10f)]
    [Increment(0.1f)]
    [DefaultValue(1f)]
    public float EnemyHealth;
    [Range(1f, 10f)]
    [Increment(0.1f)]
    [DefaultValue(1f)]
    public float BossHealth;
    [Range(1f, 10f)]
    [Increment(0.1f)]
    [DefaultValue(1f)]
    public float EnemyDamage;
    [Range(1f, 10f)]
    [Increment(0.1f)]
    [DefaultValue(1f)]
    public float BossDamage;
    [DefaultValue(true)]
    public bool BossApplyToAllWhenAlive;
    [Header("$Mods.Fargowiltas.Configs.FargoServerConfig.Headers.Misc")]
    [Range(0, 99)]
    [DefaultValue(22)]
    [ReloadRequired]
    public uint ExtraBuffSlots;
    [DefaultValue(true)]
    public bool AnglerQuestInstantReset;
    [DefaultValue(true)]
    public bool ExtraLures;
    [DefaultValue(true)]
    public bool StalkerMoneyTrough;
    [DefaultValue(true)]
    public bool RottenEggs;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool BannerRecipes;
    [DefaultValue(true)]
    public bool IncreaseMaxStack;
    [DefaultValue(true)]
    public bool ExtractSpeed;
    [DefaultValue(true)]
    public bool Fountains;
    [DefaultValue(true)]
    public bool BossZen;
    [DefaultValue(true)]
    public bool PiggyBankAcc;
    [DefaultValue(true)]
    public bool FasterLavaFishing;
    [DefaultValue(true)]
    public bool TorchGodEX;
    [DefaultValue(true)]
    public bool PylonsIgnoreEvents;

    public virtual void OnLoaded() => FargoServerConfig.Instance = this;

    public virtual ConfigScope Mode => (ConfigScope) 0;

    public virtual bool AcceptClientChanges(
      ModConfig pendingConfig,
      int whoAmI,
      ref NetworkText message)
    {
      return false;
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
      this.ExtraBuffSlots = Utils.Clamp<uint>(this.ExtraBuffSlots, 0U, 99U);
    }
  }
}
