// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.DoubleObsInstaBridgeProj
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
  public class DoubleObsInstaBridgeProj : ModProjectile
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
      this.Projectile.timeLeft = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnKill(int timeLeft)
    {
      Vector2 center = ((Entity) this.Projectile).Center;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      if (Main.netMode == 1)
        return;
      for (int index1 = 1; index1 < Main.maxTilesX; ++index1)
      {
        for (int index2 = -40; index2 <= 0; ++index2)
        {
          int x = index1;
          int y = (int) ((double) index2 + (double) center.Y / 16.0);
          if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
          {
            Tile tile = ((Tilemap) ref Main.tile)[x, y];
            if (!Tile.op_Equality(tile, (ArgumentException) null) && FargoGlobalProjectile.OkayToDestroyTileAt(x, y))
            {
              if (index2 == -20 || index2 == 0)
              {
                FargoGlobalTile.ClearEverything(x, y, false);
                WorldGen.PlaceTile(x, y, 19, false, false, -1, 13);
                if (Main.netMode == 2)
                  NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
              }
              else if (!FargoGlobalProjectile.TileIsLiterallyAir(tile))
                FargoGlobalTile.ClearEverything(x, y);
            }
          }
        }
      }
    }
  }
}
