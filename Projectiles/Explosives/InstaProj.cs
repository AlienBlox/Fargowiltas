// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.InstaProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class InstaProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 36;
      this.Projectile.aiStyle = 16;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 170;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.Kill();
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      Vector2 center = ((Entity) this.Projectile).Center;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      if (Main.netMode == 1)
        return;
      for (int index = -3; index <= 3; ++index)
      {
        for (int y = (int) (1.0 + (double) center.Y / 16.0); y <= Main.maxTilesY - 40; ++y)
        {
          int x = (int) ((double) index + (double) center.X / 16.0);
          if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY && !Tile.op_Equality(((Tilemap) ref Main.tile)[x, y], (ArgumentException) null) && FargoGlobalProjectile.OkayToDestroyTileAt(x, y))
          {
            FargoGlobalTile.ClearEverything(x, y, false);
            WorldGen.PlaceWall(x, y, 1, false);
            if (index == -3 || index == 3)
              WorldGen.PlaceTile(x, y, 38, false, false, -1, 0);
            else if ((index == -2 || index == 2) && y % 10 == 0)
              WorldGen.PlaceTile(x, y, 4, false, false, -1, 0);
            else if (index == 0)
              WorldGen.PlaceTile(x, y, 213, false, false, -1, 0);
            NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
          }
        }
      }
    }
  }
}
