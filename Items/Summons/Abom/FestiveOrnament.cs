// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Abom.FestiveOrnament
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons.Abom
{
  public class FestiveOrnament : BaseSummon
  {
    public override int NPCType => 344;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player) => !Main.dayTime;
  }
}
