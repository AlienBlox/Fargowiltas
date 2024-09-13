// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.MiniInstabridgeProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class MiniInstabridgeProj : ModProjectile
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
      int num1 = (double) ((Entity) this.Projectile).Center.X < (double) ((Entity) Main.player[this.Projectile.owner]).Center.X ? 1 : 0;
      int num2 = num1 != 0 ? -400 : 0;
      int num3 = num1 != 0 ? 0 : 400;
      int[] source = new int[5]{ 80, 5, 32, 352, 69 };
      for (int index = num2; index < num3; ++index)
      {
        int x = (int) ((double) index + (double) center.X / 16.0);
        int y = (int) ((double) center.Y / 16.0);
        if (x >= 0 && x < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
        {
          Tile tile = ((Tilemap) ref Main.tile)[x, y];
          if (!Tile.op_Equality(tile, (ArgumentException) null))
          {
            if (((IEnumerable<int>) source).Contains<int>((int) ((Tile) ref tile).TileType))
              FargoGlobalTile.ClearEverything(x, y);
            WorldGen.PlaceTile(x, y, 19, false, false, -1, 0);
            NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
          }
        }
      }
    }
  }
}
