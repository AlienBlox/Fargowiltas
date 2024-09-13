// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.OmniBridgifierProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class OmniBridgifierProj : ModProjectile
  {
    protected virtual int TileHeight => 4;

    protected virtual int Placeable
    {
      get
      {
        return (double) this.Projectile.ai[0] != 0.0 ? ModContent.TileType<OmnistationSheet2>() : ModContent.TileType<OmnistationSheet>();
      }
    }

    protected virtual bool Replaceable(int TileType)
    {
      return TileType == ModContent.TileType<OmnistationSheet>() || TileType == ModContent.TileType<OmnistationSheet2>();
    }

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 36;
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
      PlaceInDirection(1);
      PlaceInDirection(-1);

      bool TryPlaceOmni(int xPos, int yPos)
      {
        for (int index1 = -1; index1 <= 0; ++index1)
        {
          for (int index2 = -this.TileHeight; index2 < 0; ++index2)
          {
            int num1 = xPos + index1;
            int num2 = yPos + index2;
            if (WorldGen.InWorld(num1, num2, 0))
            {
              Tile tileSafely = Framing.GetTileSafely(num1, num2);
              if (Tile.op_Equality(tileSafely, (ArgumentException) null) || ((Tile) ref tileSafely).TileType != (ushort) 0)
                return false;
            }
          }
        }
        WorldGen.PlaceTile(xPos, yPos - this.TileHeight / 2, this.Placeable, false, false, -1, 0);
        if (Main.netMode == 2)
          NetMessage.SendTileSquare(-1, xPos - 1, yPos - this.TileHeight, 2, this.TileHeight, (TileChangeType) 0);
        return true;
      }

      void PlaceInDirection(int direction)
      {
        Vector2 center = ((Entity) this.Projectile).Center;
        int xPos = (int) ((double) center.X / 16.0);
        int yPos = (int) ((double) center.Y / 16.0);
        int num = 64;
        bool flag = true;
        while (WorldGen.InWorld(xPos, yPos, 0))
        {
          Tile tileSafely1 = Framing.GetTileSafely(xPos, yPos);
          if (Tile.op_Inequality(tileSafely1, (ArgumentException) null))
          {
            if (((Tile) ref tileSafely1).TileType == (ushort) 19)
            {
              flag = true;
              Tile tileSafely2 = Framing.GetTileSafely(xPos, yPos - 1);
              if (Tile.op_Inequality(tileSafely2, (ArgumentException) null) && this.Replaceable((int) ((Tile) ref tileSafely2).TileType))
                num = 128;
              if (num <= 0 && TryPlaceOmni(xPos, yPos))
                num = 128;
            }
            else
            {
              if (((Tile) ref tileSafely1).TileType != (ushort) 0 || !flag)
                break;
              flag = false;
            }
          }
          xPos += direction;
          --num;
        }
      }
    }
  }
}
