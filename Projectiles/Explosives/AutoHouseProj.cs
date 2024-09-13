// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.Explosives.AutoHouseProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles.Explosives
{
  public class AutoHouseProj : ModProjectile
  {
    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 1;
      ((Entity) this.Projectile).height = 1;
      this.Projectile.timeLeft = 1;
    }

    public static void PlaceHouse(int x, int y, Vector2 position, int side, Player player)
    {
      int x1 = (int) ((double) (side * -1 + x) + (double) position.X / 16.0);
      int y1 = (int) ((double) y + (double) position.Y / 16.0);
      Tile tile = ((Tilemap) ref Main.tile)[x1, y1];
      if (!FargoGlobalProjectile.OkayToDestroyTileAt(x1, y1))
        return;
      int num1 = 4;
      int num2 = 30;
      int num3 = 0;
      if (player.ZoneDesert && !player.ZoneBeach)
      {
        num1 = 72;
        num2 = 188;
        num3 = 25;
      }
      else if (player.ZoneSnow)
      {
        num1 = 149;
        num2 = 321;
        num3 = 19;
      }
      else if (player.ZoneJungle)
      {
        num1 = 42;
        num2 = 158;
        num3 = 2;
      }
      else if (player.ZoneCorrupt)
      {
        num1 = 41;
        num2 = 157;
        num3 = 1;
      }
      else if (player.ZoneCrimson)
      {
        num1 = 85;
        num2 = 208;
        num3 = 5;
      }
      else if (player.ZoneBeach)
      {
        num1 = 151;
        num2 = 322;
        num3 = 17;
      }
      else if (player.ZoneHallow)
      {
        num1 = 43;
        num2 = 159;
        num3 = 3;
      }
      else if (player.ZoneGlowshroom)
      {
        num1 = 74;
        num2 = 190;
        num3 = 18;
      }
      else if (player.ZoneSkyHeight)
      {
        num1 = 82;
        num2 = 202;
        num3 = 22;
      }
      else if (player.ZoneUnderworldHeight)
      {
        num1 = 20;
        num2 = 75;
        num3 = 13;
      }
      if (x == 10 * side || x == side)
      {
        if (y == -5 && (int) ((Tile) ref tile).TileType == num2 || (y == -4 || y == 0) && (int) ((Tile) ref tile).TileType == num2 || (y == -1 || y == -2 || y == -3) && (((Tile) ref tile).TileType == (ushort) 10 || ((Tile) ref tile).TileType == (ushort) 11))
          return;
      }
      else if (y == -5 && (((Tile) ref tile).TileType == (ushort) 19 || (int) ((Tile) ref tile).TileType == num2) || y == 0 && (((Tile) ref tile).TileType == (ushort) 19 || (int) ((Tile) ref tile).TileType == num2))
        return;
      if (x != 9 * side && x != 2 * side || y != -1 && y != -2 && y != -3 || ((Tile) ref tile).TileType != (ushort) 11)
        FargoGlobalTile.ClearEverything(x1, y1);
      if (y != -5 && y != 0 && x != 10 * side && x != side)
      {
        WorldGen.PlaceWall(x1, y1, num1, false);
        if (Main.netMode == 2)
          NetMessage.SendTileSquare(-1, x1, y1, 1, (TileChangeType) 0);
      }
      if (y == -5 && Math.Abs(x) >= 3 && Math.Abs(x) <= 5)
      {
        WorldGen.PlaceTile(x1, y1, 19, false, false, -1, num3);
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x1, (float) y1, 19f, num3, 0, 0);
      }
      else
      {
        if (y != -5 && y != 0 && x != 10 * side && (x != side || y != -4))
          return;
        WorldGen.PlaceTile(x1, y1, num2, false, false, -1, 0);
        if (Main.netMode != 2)
          return;
        NetMessage.SendTileSquare(-1, x1, y1, 1, (TileChangeType) 0);
      }
    }

    public static void PlaceFurniture(int x, int y, Vector2 position, int side, Player player)
    {
      int x1 = (int) ((double) (side * -1 + x) + (double) position.X / 16.0);
      int y1 = (int) ((double) y + (double) position.Y / 16.0);
      Tile tile = ((Tilemap) ref Main.tile)[x1, y1];
      if (!FargoGlobalProjectile.OkayToDestroyTileAt(x1, y1))
        return;
      if (y == -1)
      {
        if (Math.Abs(x) == 1)
        {
          int num = 0;
          if (player.ZoneDesert && !player.ZoneBeach)
            num = 4;
          else if (player.ZoneSnow)
            num = 30;
          else if (player.ZoneJungle)
            num = 2;
          else if (player.ZoneCorrupt)
            num = 1;
          else if (player.ZoneCrimson)
            num = 10;
          else if (player.ZoneBeach)
            num = 29;
          else if (player.ZoneHallow)
            num = 3;
          else if (player.ZoneGlowshroom)
            num = 6;
          else if (player.ZoneSkyHeight)
            num = 9;
          else if (player.ZoneUnderworldHeight)
            num = 19;
          WorldGen.PlaceTile(x1, y1, 10, false, false, -1, num);
          if (Main.netMode == 2)
            NetMessage.SendTileSquare(-1, x1, y1 - 2, 1, 3, (TileChangeType) 0);
        }
        if (x == 5 * side)
        {
          int num1 = 0;
          if (player.ZoneDesert && !player.ZoneBeach)
            num1 = 6;
          else if (player.ZoneSnow)
            num1 = 30;
          else if (player.ZoneJungle)
            num1 = 3;
          else if (player.ZoneCorrupt)
            num1 = 2;
          else if (player.ZoneCrimson)
            num1 = 11;
          else if (player.ZoneBeach)
            num1 = 29;
          else if (player.ZoneHallow)
            num1 = 4;
          else if (player.ZoneGlowshroom)
            num1 = 9;
          else if (player.ZoneSkyHeight)
            num1 = 10;
          else if (player.ZoneUnderworldHeight)
            num1 = 16;
          int num2 = x1;
          int num3 = y1;
          int num4 = side;
          int num5 = num1;
          int num6 = num4;
          WorldGen.PlaceObject(num2, num3, 15, false, num5, 0, -1, num6);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x1, (float) y1, 15f, num1, 0, 0);
        }
        if (x == 7 * side)
        {
          int num = 0;
          if (player.ZoneDesert && !player.ZoneBeach)
            num = 30;
          else if (player.ZoneSnow)
            num = 28;
          else if (player.ZoneJungle)
            num = 2;
          else if (player.ZoneCorrupt)
            num = 1;
          else if (player.ZoneCrimson)
            num = 8;
          else if (player.ZoneBeach)
            num = 26;
          else if (player.ZoneHallow)
            num = 3;
          else if (player.ZoneGlowshroom)
            num = 27;
          else if (player.ZoneSkyHeight)
            num = 7;
          else if (player.ZoneUnderworldHeight)
            num = 13;
          WorldGen.PlaceTile(x1, y1, 14, false, false, -1, num);
          if (Main.netMode == 2)
            NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x1, (float) y1, 14f, num, 0, 0);
        }
      }
      if (x != 7 * side || y != -4)
        return;
      WorldGen.PlaceTile(x1, y1, 4, false, false, -1, 5);
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) x1, (float) y1, 4f, 0, 0, 0);
    }

    public static void UpdateWall(int x, int y, Vector2 position, int side, Player player)
    {
      int num1 = (int) ((double) (side * -1 + x) + (double) position.X / 16.0);
      int num2 = (int) ((double) y + (double) position.Y / 16.0);
      WorldGen.SquareWallFrame(num1, num2, true);
      if (Main.netMode != 2)
        return;
      NetMessage.SendTileSquare(-1, num1, num2, 1, (TileChangeType) 0);
    }

    public virtual void OnKill(int timeLeft)
    {
      Vector2 center = ((Entity) this.Projectile).Center;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      Player player = Main.player[this.Projectile.owner];
      if (Main.netMode == 1)
        return;
      if ((double) ((Entity) player).Center.X < (double) center.X)
      {
        for (int index = 0; index < 3; ++index)
        {
          for (int x = 11; x > -1; --x)
          {
            if (index == 2 || x != 11 && x != 0)
            {
              for (int y = -6; y <= 1; ++y)
              {
                if (index == 2 || y != -6 && y != 1)
                {
                  switch (index)
                  {
                    case 0:
                      AutoHouseProj.PlaceHouse(x, y, center, 1, player);
                      continue;
                    case 1:
                      AutoHouseProj.PlaceFurniture(x, y, center, 1, player);
                      continue;
                    default:
                      AutoHouseProj.UpdateWall(x, y, center, 1, player);
                      continue;
                  }
                }
              }
            }
          }
        }
      }
      else
      {
        for (int index = 0; index < 3; ++index)
        {
          for (int x = -11; x < 1; ++x)
          {
            if (index == 2 || x != -11 && x != 0)
            {
              for (int y = -6; y <= 1; ++y)
              {
                if (index == 2 || y != -6 && y != 1)
                {
                  switch (index)
                  {
                    case 0:
                      AutoHouseProj.PlaceHouse(x, y, center, -1, player);
                      continue;
                    case 1:
                      AutoHouseProj.PlaceFurniture(x, y, center, -1, player);
                      continue;
                    default:
                      continue;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
