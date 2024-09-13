// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Tiles.FargoGlobalTile
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

#nullable disable
namespace Fargowiltas.Tiles
{
  public class FargoGlobalTile : GlobalTile
  {
    private static uint LastTorchUpdate;
    private readonly int[] TorchesToReplace = new int[9]
    {
      7,
      20,
      18,
      19,
      9,
      21,
      16,
      17,
      0
    };

    public virtual int[] AdjTiles(int type)
    {
      if (type != 283)
        return base.AdjTiles(type);
      return new int[2]{ 18, 283 };
    }

    public virtual void MouseOver(int i, int j, int type)
    {
      if (type != 219 && type != 642)
        return;
      Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().extractSpeed = true;
    }

    public virtual void KillTile(
      int i,
      int j,
      int type,
      ref bool fail,
      ref bool effectOnly,
      ref bool noItem)
    {
      if (WorldGen.gen)
        return;
      bool flag;
      if (type == 5 || type == 634 && !fail && !(FargoWorld.DownedBools.TryGetValue("lumberjack", out flag) & flag))
        ++FargoWorld.WoodChopped;
      if (type != 567 || fail)
        return;
      FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "gnome");
    }

    public virtual void NearbyEffects(int i, int j, int type, bool closer)
    {
      if (closer && TileID.Sets.Torch[type] && !Main.dedServ && Main.LocalPlayer.UsingBiomeTorches && (FargoGlobalTile.LastTorchUpdate < Main.GameUpdateCount - 60U || (int) FargoGlobalTile.LastTorchUpdate == (int) Main.GameUpdateCount))
      {
        FargoGlobalTile.LastTorchUpdate = Main.GameUpdateCount;
        if (FargoServerConfig.Instance.TorchGodEX && Main.LocalPlayer.ShoppingZone_BelowSurface && !Main.LocalPlayer.ZoneDungeon && !Main.LocalPlayer.ZoneLihzhardTemple)
        {
          Tile tileSafely = Framing.GetTileSafely(i, j);
          int num1 = (int) ((Tile) ref tileSafely).TileFrameY / 22;
          bool flag = ((IEnumerable<int>) this.TorchesToReplace).Contains<int>(num1);
          if (flag && (num1 == 20 && Main.LocalPlayer.ZoneHallow || num1 == 18 && Main.LocalPlayer.ZoneCorrupt || num1 == 19 && Main.LocalPlayer.ZoneCrimson || num1 == 16 && (Main.LocalPlayer.ZoneDesert || Main.LocalPlayer.ZoneUndergroundDesert) || num1 == 21 && Main.LocalPlayer.ZoneJungle || num1 == 17 && Main.LocalPlayer.ZoneBeach))
            flag = false;
          if (flag)
          {
            int num2 = 0;
            int num3 = Main.LocalPlayer.BiomeTorchPlaceStyle(ref type, ref num2);
            if (num3 == 7)
              num3 = 13;
            else if (Main.LocalPlayer.ZoneBeach)
              num3 = 17;
            else if (num3 == 0)
              num3 = 13;
            if (num1 != num3 && ((IEnumerable<int>) this.TorchesToReplace).Contains<int>(num1))
            {
              WorldGen.KillTile(i, j, false, false, true);
              WorldGen.PlaceTile(i, j, 4, false, false, ((Entity) Main.LocalPlayer).whoAmI, num3);
              if (Main.netMode == 1)
                NetMessage.SendData(17, -1, -1, (NetworkText) null, 1, (float) i, (float) j, 4f, 0, 0, 0);
            }
          }
        }
      }
      switch (type)
      {
        case 125:
          if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            break;
          Main.LocalPlayer.AddBuff(29, 2, true, false);
          break;
        case 287:
          if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            break;
          Main.LocalPlayer.AddBuff(93, 2, true, false);
          break;
        case 354:
          if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            break;
          Main.LocalPlayer.AddBuff(150, 2, true, false);
          break;
        case 377:
          if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            break;
          Main.LocalPlayer.AddBuff(159, 2, true, false);
          break;
        case 464:
          if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
            break;
          Main.LocalPlayer.AddBuff(348, 2, true, false);
          break;
      }
    }

    internal static void DestroyChest(int x, int y)
    {
      int num1 = 1;
      int chest = Chest.FindChest(x, y);
      Tile tile1;
      if (chest != -1)
      {
        for (int index = 0; index < 40; ++index)
          Main.chest[chest].item[index] = new Item();
        Main.chest[chest] = (Chest) null;
        Tile tile2 = ((Tilemap) ref Main.tile)[x, y];
        if (((Tile) ref tile2).TileType == (ushort) 467)
          num1 = 5;
        tile1 = ((Tilemap) ref Main.tile)[x, y];
        if ((int) ((Tile) ref tile1).TileType >= (int) TileID.Count)
          num1 = 101;
      }
      for (int index1 = x; index1 < x + 2; ++index1)
      {
        for (int index2 = y; index2 < y + 2; ++index2)
        {
          tile1 = ((Tilemap) ref Main.tile)[index1, index2];
          ((Tile) ref tile1).TileType = (ushort) 0;
          tile1 = ((Tilemap) ref Main.tile)[index1, index2];
          ((Tile) ref tile1).TileFrameX = (short) 0;
          tile1 = ((Tilemap) ref Main.tile)[index1, index2];
          ((Tile) ref tile1).TileFrameY = (short) 0;
        }
      }
      if (Main.netMode == 0)
        return;
      if (chest != -1)
      {
        int num2 = num1;
        double num3 = (double) x;
        double num4 = (double) y;
        int num5 = chest;
        tile1 = ((Tilemap) ref Main.tile)[x, y];
        int num6 = (int) ((Tile) ref tile1).TileType;
        NetMessage.SendData(34, -1, -1, (NetworkText) null, num2, (float) num3, (float) num4, 0.0f, num5, num6, 0);
      }
      NetMessage.SendTileSquare(-1, x, y, 3, (TileChangeType) 0);
    }

    internal static Point16 FindChestTopLeft(int x, int y, bool destroy)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      if (!TileID.Sets.BasicChest[(int) ((Tile) ref tile).TileType])
        return Point16.NegativeOne;
      TileObjectData tileData = TileObjectData.GetTileData((int) ((Tile) ref tile).TileType, 0, 0);
      x -= (int) ((Tile) ref tile).TileFrameX / 18 % tileData.Width;
      y -= (int) ((Tile) ref tile).TileFrameY / 18 % tileData.Height;
      if (destroy)
        FargoGlobalTile.DestroyChest(x, y);
      return new Point16(x, y);
    }

    internal static void ClearTileAndLiquid(int x, int y, bool sendData = true)
    {
      FargoGlobalTile.FindChestTopLeft(x, y, true);
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      bool flag = ((Tile) ref tile).LiquidAmount > (byte) 0;
      WorldGen.KillTile(x, y, false, false, true);
      ((Tile) ref tile).Clear((TileDataType) 1);
      ((Tile) ref tile).Clear((TileDataType) 16);
      if (Main.netMode != 2)
        return;
      if (flag)
        NetMessage.sendWater(x, y);
      if (!sendData)
        return;
      NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
    }

    internal static void ClearEverything(int x, int y, bool sendData = true)
    {
      FargoGlobalTile.FindChestTopLeft(x, y, true);
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      bool flag = ((Tile) ref tile).LiquidAmount > (byte) 0;
      WorldGen.KillTile(x, y, false, false, true);
      ((Tile) ref tile).ClearEverything();
      if (Main.netMode != 2)
        return;
      if (flag)
        NetMessage.sendWater(x, y);
      if (!sendData)
        return;
      NetMessage.SendTileSquare(-1, x, y, 1, (TileChangeType) 0);
    }

    private enum TorchStyle
    {
      None = 0,
      Demon = 7,
      Ice = 9,
      Bone = 13, // 0x0000000D
      Desert = 16, // 0x00000010
      Coral = 17, // 0x00000011
      Corrupt = 18, // 0x00000012
      Crimson = 19, // 0x00000013
      Hallow = 20, // 0x00000014
      Jungle = 21, // 0x00000015
    }
  }
}
