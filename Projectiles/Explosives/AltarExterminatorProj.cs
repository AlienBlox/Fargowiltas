// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.AltarExterminatorProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class AltarExterminatorProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 2;
      ((Entity) this.Projectile).height = 2;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (Main.netMode == 1)
        return;
      for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
      {
        for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
        {
          if (WorldGen.InWorld(index1, index2, 0))
          {
            Tile tileSafely = Framing.GetTileSafely(index1, index2);
            if (((Tile) ref tileSafely).TileType == (ushort) 26)
            {
              WorldGen.KillTile(index1, index2, false, false, false);
              if (Main.netMode == 2)
                NetMessage.SendTileSquare(-1, index1, index2, 1, (TileChangeType) 0);
            }
          }
        }
      }
      Main.refreshMap = true;
    }
  }
}
