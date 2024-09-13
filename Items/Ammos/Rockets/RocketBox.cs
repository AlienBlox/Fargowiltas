// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Ammos.Rockets.RocketBox
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Ammos.Rockets
{
  public abstract class RocketBox : BaseAmmo
  {
    public abstract int RocketProjectile { get; }

    public abstract int SnowmanProjectile { get; }

    public abstract int GrenadeProjectile { get; }

    public abstract int MineProjectile { get; }

    public virtual void PickAmmo(
      Item weapon,
      Player player,
      ref int type,
      ref float speed,
      ref StatModifier damage,
      ref float knockback)
    {
      switch (weapon.type)
      {
        case 758:
          type = this.GrenadeProjectile;
          break;
        case 759:
          type = this.RocketProjectile;
          break;
        case 760:
          type = this.MineProjectile;
          break;
        case 1946:
          type = this.SnowmanProjectile;
          break;
      }
    }
  }
}
