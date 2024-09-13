// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.CoreoftheFrostCore
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class CoreoftheFrostCore : BaseSummon
  {
    public override int NPCType => 243;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 6, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
    }
  }
}
