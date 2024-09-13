// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Ammos.Rockets.MiniNuke2Box
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

#nullable disable
namespace Fargowiltas.Items.Ammos.Rockets
{
  internal class MiniNuke2Box : RocketBox
  {
    public override int AmmunitionItem => 4458;

    public override int RocketProjectile => 796;

    public override int SnowmanProjectile => 809;

    public override int GrenadeProjectile => 797;

    public override int MineProjectile => 798;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.maxStack = 2;
    }
  }
}
