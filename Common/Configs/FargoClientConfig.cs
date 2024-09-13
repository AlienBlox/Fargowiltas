// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Configs.FargoClientConfig
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader.Config;

#nullable disable
namespace Fargowiltas.Common.Configs
{
  public sealed class FargoClientConfig : ModConfig
  {
    public static FargoClientConfig Instance;
    [DefaultValue(true)]
    public bool ExpandedTooltips;
    [DefaultValue(false)]
    public bool HideUnlimitedBuffs;
    [DefaultValue(false)]
    public bool DoubleTapDashDisabled;
    [DefaultValue(false)]
    public bool DoubleTapSetBonusDisabled;
    [DefaultValue(1f)]
    [Slider]
    public float TransparentFriendlyProjectiles;
    [DefaultValue(0.75f)]
    [Slider]
    public float DebuffOpacity;
    [DefaultValue(0.75f)]
    [Slider]
    public float DebuffFaderRatio;

    public virtual void OnLoaded() => FargoClientConfig.Instance = this;

    public virtual ConfigScope Mode => (ConfigScope) 1;

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
      this.TransparentFriendlyProjectiles = Utils.Clamp<float>(this.TransparentFriendlyProjectiles, 0.0f, 1f);
    }
  }
}
