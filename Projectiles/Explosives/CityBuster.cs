// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.CityBuster
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
  public class CityBuster : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 26;
      ((Entity) this.Projectile).height = 26;
      this.Projectile.aiStyle = 16;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 300;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity.X = 0.0f;
      return base.OnTileCollide(oldVelocity);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item15, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (Main.netMode == 1)
        return;
      Vector2 center = ((Entity) this.Projectile).Center;
      int num = 64;
      for (int index1 = -num; index1 <= num; ++index1)
      {
        for (int index2 = -num * 2; index2 <= 0; ++index2)
        {
          int x = (int) ((double) index1 + (double) center.X / 16.0);
          int y = (int) ((double) index2 + (double) center.Y / 16.0);
          if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
          {
            Tile tile = ((Tilemap) ref Main.tile)[x, y];
            if (!Tile.op_Equality(tile, (ArgumentException) null) && FargoGlobalProjectile.OkayToDestroyTileAt(x, y) && !FargoGlobalProjectile.TileIsLiterallyAir(tile))
              FargoGlobalTile.ClearTileAndLiquid(x, y);
          }
        }
      }
      Main.refreshMap = true;
    }
  }
}
