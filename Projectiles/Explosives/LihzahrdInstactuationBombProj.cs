// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.LihzahrdInstactuationBombProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class LihzahrdInstactuationBombProj : ModProjectile
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
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (Main.netMode == 1)
        return;
      int xPos = (int) ((Entity) this.Projectile).Center.X / 16;
      int yPos = (int) ((Entity) this.Projectile).Center.Y / 16;
      int num1 = 60;
      int num2 = 60;
      int i1;
      for (i1 = 0; i1 >= -num1; --i1)
      {
        if (!WipeColumn(i1))
        {
          num2 += num1 - Math.Abs(i1);
          break;
        }
      }
      for (int i2 = 0; i2 <= num2; ++i2)
      {
        if (!WipeColumn(i2))
        {
          int num3 = num1 + (num2 - i2);
          while (i1 >= -num3 && WipeColumn(i1))
            --i1;
          break;
        }
      }

      bool WipeColumn(int i)
      {
        for (int index = 0; index >= -60; --index)
        {
          int num1 = xPos + i;
          int num2 = yPos + index;
          if (num1 < 0 || num1 > Main.maxTilesX || num2 <= 0 || num2 > Main.maxTilesY)
          {
            if (index == 0)
              return false;
          }
          else
          {
            Tile tileSafely1 = Framing.GetTileSafely(num1, num2);
            if (((Tile) ref tileSafely1).TileType != (ushort) 237)
            {
              if (((Tile) ref tileSafely1).WallType != (ushort) 87)
              {
                if (index == 0)
                  return false;
              }
              else
              {
                Tile tileSafely2 = Framing.GetTileSafely(num1, num2 - 1);
                if (TileID.Sets.BasicChest[(int) ((Tile) ref tileSafely2).TileType])
                {
                  TileObjectData tileData = TileObjectData.GetTileData((int) ((Tile) ref tileSafely2).TileType, 0, 0);
                  int num3 = num1 - (int) ((Tile) ref tileSafely1).TileFrameX / 18 % tileData.Width;
                  int num4 = num2 - 1 - (int) ((Tile) ref tileSafely1).TileFrameY / 18 % tileData.Height;
                  WorldGen.KillTile(num3, num4, false, false, false);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, num3, num4, 3, (TileChangeType) 0);
                  if (TileID.Sets.BasicChest[(int) ((Tile) ref tileSafely2).TileType])
                    continue;
                }
                if (TileID.Sets.BasicChest[(int) ((Tile) ref tileSafely1).TileType])
                {
                  TileObjectData tileData = TileObjectData.GetTileData((int) ((Tile) ref tileSafely1).TileType, 0, 0);
                  int num5 = num1 - (int) ((Tile) ref tileSafely1).TileFrameX / 18 % tileData.Width;
                  int num6 = num2 - (int) ((Tile) ref tileSafely1).TileFrameY / 18 % tileData.Height;
                  WorldGen.KillTile(num5, num6, false, false, false);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, num5, num6, 3, (TileChangeType) 0);
                }
                else if (((Tile) ref tileSafely1).TileType == (ushort) 226)
                {
                  ((Tile) ref tileSafely1).IsActuated = true;
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, num1, num2, 1, (TileChangeType) 0);
                }
                else
                {
                  WorldGen.KillTile(num1, num2, false, false, false);
                  if (Main.netMode == 2)
                    NetMessage.SendTileSquare(-1, num1, num2, 1, (TileChangeType) 0);
                }
              }
            }
          }
        }
        return true;
      }
    }
  }
}
