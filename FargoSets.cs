// Decompiled with JetBrains decompiler
// Type: Fargowiltas.FargoSets
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items;
using Fargowiltas.Items.Misc;
using Fargowiltas.Items.Tiles;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas
{
  public class FargoSets : ModSystem
  {
    public virtual void PostSetupContent()
    {
      SetFactory factory1 = ItemID.Sets.Factory;
      FargoSets.Items.MechanicalAccessory = factory1.CreateBoolSet(false, new int[13]
      {
        3619,
        3611,
        486,
        2799,
        2216,
        3061,
        5126,
        3624,
        4346,
        4767,
        5323,
        5309,
        5095
      });
      FargoSets.Items.InfoAccessory = factory1.CreateBoolSet(false, new int[27]
      {
        15,
        707,
        16,
        708,
        17,
        709,
        393,
        18,
        395,
        3123,
        3124,
        5358,
        5359,
        5360,
        5361,
        3121,
        3119,
        3102,
        3099,
        3118,
        3120,
        3037,
        3096,
        3084,
        3095,
        3036,
        3122
      });
      FargoSets.Items.SquirrelSellsDirectly = factory1.CreateBoolSet(false, new int[19]
      {
        3124,
        5358,
        5437,
        5361,
        5360,
        5359,
        1613,
        1326,
        5000,
        5043,
        5126,
        4956,
        ModContent.ItemType<Omnistation>(),
        ModContent.ItemType<Omnistation2>(),
        ModContent.ItemType<CrucibleCosmos>(),
        ModContent.ItemType<ElementalAssembler>(),
        ModContent.ItemType<MultitaskCenter>(),
        ModContent.ItemType<PortableSundial>(),
        ModContent.ItemType<BattleCry>()
      });
      FargoSets.Items.NonBuffPotion = factory1.CreateBoolSet(false, new int[5]
      {
        2350,
        4870,
        2997,
        2351,
        ModContent.ItemType<BigSuckPotion>()
      });
      FargoSets.Items.BuffStation = factory1.CreateBoolSet(false, new int[5]
      {
        3198,
        2177,
        487,
        2999,
        3814
      });
      FargoSets.Items.RegisteredShopTooltips = factory1.CreateCustomSet<List<FargoGlobalItem.ShopTooltip>>((List<FargoGlobalItem.ShopTooltip>) null, Array.Empty<object>());
      SetFactory factory2 = TileID.Sets.Factory;
      FargoSets.Tiles.InstaCannotDestroy = factory2.CreateBoolSet(false, Array.Empty<int>());
      FargoSets.Tiles.DungeonTile = factory2.CreateBoolSet(false, new int[3]
      {
        41,
        43,
        44
      });
      FargoSets.Tiles.HardmodeOre = factory2.CreateBoolSet(false, new int[6]
      {
        107,
        221,
        108,
        222,
        111,
        223
      });
      FargoSets.Tiles.EvilAltars = factory2.CreateBoolSet(false, new int[1]
      {
        26
      });
      SetFactory factory3 = WallID.Sets.Factory;
      FargoSets.Walls.InstaCannotDestroy = factory3.CreateBoolSet(false, Array.Empty<int>());
      FargoSets.Walls.DungeonWall = factory3.CreateBoolSet(false, new int[9]
      {
        94,
        95,
        7,
        98,
        99,
        8,
        96,
        97,
        9
      });
    }

    public class Items
    {
      public static bool[] MechanicalAccessory;
      public static bool[] InfoAccessory;
      public static bool[] SquirrelSellsDirectly;
      public static bool[] NonBuffPotion;
      public static bool[] BuffStation;
      public static List<FargoGlobalItem.ShopTooltip>[] RegisteredShopTooltips;
    }

    public class Tiles
    {
      public static bool[] InstaCannotDestroy;
      public static bool[] DungeonTile;
      public static bool[] HardmodeOre;
      public static bool[] EvilAltars;
    }

    public class Walls
    {
      public static bool[] InstaCannotDestroy;
      public static bool[] DungeonWall;
    }
  }
}
