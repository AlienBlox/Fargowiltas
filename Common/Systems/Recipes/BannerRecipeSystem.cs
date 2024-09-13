// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Systems.Recipes.BannerRecipeSystem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Common.Systems.Recipes
{
  public class BannerRecipeSystem : ModSystem
  {
    private static int AnyPirateBanner;
    private static int AnyArmoredBonesBanner;
    private static int AnySlimesBanner;
    private static int AnyBatBanner;
    private static int AnyHallowBanner;
    private static int AnyCorruptBanner;
    private static int AnyCrimsonBanner;
    private static int AnyJungleBanner;
    private static int AnySnowBanner;
    private static int AnyDesertBanner;

    public virtual bool IsLoadingEnabled(Mod mod) => FargoServerConfig.Instance.BannerRecipes;

    public virtual void AddRecipeGroups()
    {
      BannerRecipeSystem.AnyPirateBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyPirateBanner", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("NPCName.Pirate")), new int[4]
      {
        3442,
        3443,
        3444,
        1676
      }));
      BannerRecipeSystem.AnyArmoredBonesBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyArmoredBonesBanner", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("ArmoredBonesBanner")), new int[3]
      {
        2900,
        2930,
        2970
      }));
      BannerRecipeSystem.AnySlimesBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnySlimes", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(1683)), new int[20]
      {
        1683,
        2928,
        2968,
        2964,
        2992,
        2899,
        2935,
        3593,
        2940,
        2980,
        2981,
        2955,
        1690,
        1689,
        2908,
        2976,
        2910,
        1651,
        2938,
        2966
      }));
      BannerRecipeSystem.AnyHallowBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyHallows", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("RandomWorldName_Adjective.Hallowed")), new int[10]
      {
        1677,
        1691,
        2966,
        1651,
        3450,
        2937,
        2938,
        1629,
        1642,
        4975
      }));
      BannerRecipeSystem.AnyCorruptBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyCorrupts", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("CLI.Corrupt")), new int[10]
      {
        1641,
        2909,
        2908,
        2976,
        2913,
        1697,
        3449,
        1637,
        2905,
        4973
      }));
      BannerRecipeSystem.AnyCrimsonBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyCrimsons", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("CLI.Crimson")), new int[12]
      {
        1626,
        1644,
        1635,
        1660,
        2910,
        1625,
        1624,
        4966,
        1636,
        2936,
        1645,
        4974
      }));
      BannerRecipeSystem.AnyJungleBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyJungles", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("RandomWorldName_Location.Jungle")), new int[17]
      {
        1675,
        2977,
        2939,
        2940,
        2915,
        1615,
        1619,
        1688,
        2897,
        1640,
        2925,
        1661,
        2981,
        1666,
        1670,
        1668,
        4976
      }));
      BannerRecipeSystem.AnySnowBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnySnows", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("RandomWorldName_Noun.Snow")), new int[13]
      {
        2935,
        1643,
        1662,
        1696,
        2934,
        2933,
        1684,
        2980,
        2988,
        2898,
        3452,
        1663,
        1674
      }));
      BannerRecipeSystem.AnyDesertBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyDeserts", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("RandomWorldName_Location.Desert")), new int[23]
      {
        1693,
        1671,
        4966,
        3449,
        3450,
        3413,
        3414,
        4969,
        1618,
        3593,
        3411,
        3418,
        3419,
        3416,
        3415,
        3417,
        3412,
        3780,
        3789,
        3790,
        3791,
        3792,
        3793
      }));
      BannerRecipeSystem.AnyBatBanner = RecipeGroup.RegisterGroup("Fargowiltas:AnyBats", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyBannerRecipeGroupText("RandomWorldName_Noun.Bats")), new int[9]
      {
        1621,
        2923,
        2925,
        2933,
        2937,
        2939,
        1659,
        2943,
        4968
      }));
    }

    public virtual void AddRecipes()
    {
      BannerRecipeSystem.AddBannerToAccessoryRecipes();
      BannerRecipeSystem.AddBannerToArmorRecipes();
      BannerRecipeSystem.AddBannerToCritterRecipes();
      BannerRecipeSystem.AddBannerToFoodRecipes();
      BannerRecipeSystem.AddBannerToFurnitureRecipes();
      BannerRecipeSystem.AddBannerToMaterialRecipes();
      BannerRecipeSystem.AddBannerToMiscItemRecipes();
      BannerRecipeSystem.AddBannerToMountOrPetRecipes();
      BannerRecipeSystem.AddBannerToWeaponRecipes();
    }

    private static void AddBannerToAccessoryRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(3409, 3212, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3410, 3212, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1664, 1323, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1665, 1303, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3447, 1303, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1659, 1322, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1678, 216, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1701, 216, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1643, 216, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1680, 268, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3448, 1303, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3452, 1253, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2973, 1321, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1695, 485, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 536, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 535, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 554, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 532, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 854, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 3033, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 855, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerToItemRecipe(3395, 497, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(2943, 1322, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(1692, 900, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(2904, 963, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2904, 977, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2975, 1300, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3397, 2770, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2958, 938, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(1636, 891, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1637, 891, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2911, 891, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1642, 891, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2924, 891, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1661, 887, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4976, 887, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1689, 887, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1615, 885, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1695, 885, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4966, 888, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2908, 888, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2910, 888, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3449, 888, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1620, 886, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1631, 893, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2923, 893, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3450, 893, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2909, 892, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1645, 892, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4966, 890, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3449, 890, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3448, 890, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1677, 890, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1671, 889, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1677, 889, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1699, 889, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3405, 3781, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2970, 885, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2900, 886, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3451, 3095, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2911, 3095, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1682, 3095, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2898, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3393, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3392, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2955, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1675, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3391, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1684, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2988, 393, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1621, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3393, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2923, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3392, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2933, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2939, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3391, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4968, 18, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1694, 3102, 1, 1);
    }

    private static void AddBannerToArmorRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(3451, 959, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1641, 958, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1641, 956, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1641, 957, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3407, 3109, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3408, 3109, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3406, 3188, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3406, 3187, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3406, 3189, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1661, 961, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1661, 960, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1661, 962, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1681, 955, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1681, 954, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1682, 959, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2987, 411, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2987, 410, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2988, 879, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2898, 879, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1675, 263, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1632, 243, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4970, 243, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4965, 4761, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1690, 1243, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2907, 3757, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2907, 3758, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2907, 3759, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4972, 3757, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4972, 3758, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4972, 3759, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1678, 1136, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1678, 1135, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1643, 803, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1643, 804, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1643, 805, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1615, 263, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3417, 3770, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3416, 3786, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3416, 3785, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3416, 3784, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3416, 2801, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3416, 2802, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1671, 870, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1671, 871, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1671, 872, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4966, 870, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4966, 871, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4966, 872, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3449, 870, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3449, 871, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3449, 872, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3450, 870, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3450, 871, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3450, 872, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 1277, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 1279, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 1280, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerToItemRecipe(2984, 1514, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2994, 1943, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2994, 1944, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2994, 1945, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3400, 4740, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3400, 4741, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3400, 4742, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3396, 4738, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3396, 4739, 1, 1, Condition.DownedPlantera);
    }

    private static void AddBannerToCritterRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(1622, 2015, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(1622, 2016, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(1622, 2017, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(1628, 2019, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(1657, 261, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(2959, 2205, 1, 100);
    }

    private static void AddBannerToFoodRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(3391, 4030, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3393, 4030, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3392, 4030, 1, 1);
      BannerRecipeSystem.AddBannerSetToItemRecipe(NPCID.Sets.Skeletons, 5041);
      BannerRecipeSystem.AddBannerToItemRecipe(1646, 4021, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1658, 4016, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1618, 4012, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3413, 4012, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3414, 4012, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2911, 4018, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2935, 4026, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2933, 4026, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2980, 4026, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1668, 5042, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2977, 5042, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3793, 4028, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1680, 4035, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1634, 4035, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1685, 4020, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1651, 4017, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2925, 4023, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1640, 4023, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2897, 5042, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3789, 4028, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3790, 4028, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3791, 4028, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3792, 4028, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1623, 4020, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3419, 4020, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2904, 5042, 1, 5, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2924, 4018, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3406, 4029, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3405, 4029, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(2987, 4037, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(1627, 4025, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1674, 3532, 1, 2, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1629, 4011, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2938, 4011, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2937, 4011, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1641, 4015, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3408, 4036, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3407, 4036, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1663, 4027, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3452, 4027, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3446, 4025, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(3399, 4037, 1, 2, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(2974, 4013, 1, 2, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2975, 4013, 1, 2, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2984, 4013, 1, 2, Condition.DownedPlantera);
    }

    private static void AddBannerToFurnitureRecipes()
    {
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyCorruptBanner, 996, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyCrimsonBanner, 996, 1, 5, Condition.Hardmode);
    }

    private static void AddBannerToMaterialRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(3451, 154, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(2911, 154, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(1682, 154, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(1681, 154, 1, 100);
      BannerRecipeSystem.AddBannerToItemRecipe(4844, 999, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4838, 181, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4843, 182, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4841, 179, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4842, 178, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4840, 177, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4839, 180, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4837, 999, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4831, 181, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4836, 182, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4834, 179, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4835, 178, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4833, 177, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(4832, 180, 1, 5);
      BannerRecipeSystem.AddBannerToItemRecipe(1639, 236, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1669, 116, 1, 25);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnySlimesBanner, 23, 200, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4977, 236, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3415, 527, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4966, 527, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3449, 527, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3790, 527, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3791, 527, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1658, 1516, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3415, 528, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3450, 528, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3792, 528, 1, 5, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1677, 501, 1, 100, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1688, 1328, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3417, 3795, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyArmoredBonesBanner, 1517, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4976, 1521, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(1670, 1611, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(3446, 1518, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(1692, 1520, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(2917, 1508, 1, 50, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3397, 1570, 1, 1, Condition.DownedPlantera);
    }

    private static void AddBannerToMiscItemRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(3410, 3213, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1646, 4057, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1682, 932, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4543, 4325, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1698, 215, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4542, 4325, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4602, 4054, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1629, 1326, 4, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 437, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1691, 856, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyCorruptBanner, 1534, 1, 10, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyCrimsonBanner, 1535, 1, 10, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyDesertBanner, 4714, 1, 10, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyHallowBanner, 1536, 1, 10, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyJungleBanner, 1533, 1, 10, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnySnowBanner, 1537, 1, 10, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1627, 4610, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1628, 4612, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1632, 4670, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4970, 4671, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1657, 4674, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1665, 4649, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1668, 4648, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3447, 4650, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2968, 4369, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1680, 4651, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1683, 4367, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2992, 4371, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2897, 4675, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1674, 4613, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3789, 4669, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1691, 4684, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(4977, 4683, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1697, 4611, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1700, 4379, 1, 1, Condition.Hardmode);
    }

    private static void AddBannerToMountOrPetRecipes()
    {
      BannerRecipeSystem.AddBannerToItemRecipe(3418, 3771, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1651, 3260, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 1312, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1674, 4428, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2920, 1311, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyArmoredBonesBanner, 1183, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(1667, 1172, 1, 1, Condition.DownedGolem);
      BannerRecipeSystem.AddBannerToItemRecipe(2952, 2771, 1, 1, Condition.DownedMartians);
      BannerRecipeSystem.AddBannerToItemRecipe(2972, 2771, 1, 1, Condition.DownedMartians);
    }

    private static void AddBannerToWeaponRecipes()
    {
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyBatBanner, 5097, 1, 1);
      if (!Main.zenithWorld && !Main.remixWorld)
        BannerRecipeSystem.AddBannerToItemRecipe(1621, 1325, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1626, 5094, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3393, 3285, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1635, 5094, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1638, 272, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1641, 5094, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4543, 4381, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4543, 4273, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1644, 5094, 2, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3392, 3285, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2927, 160, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3406, 4463, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3391, 3285, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1681, 1166, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1684, 951, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4968, 4764, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2987, 1320, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(3414, 3772, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(1701, 1304, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4542, 4381, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(4542, 4273, 1, 1);
      BannerRecipeSystem.AddBannerToItemRecipe(2944, 3282, 1, 1, Condition.DownedSkeletron);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnySnowBanner, 3289, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1616, 1244, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2897, 1265, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1620, 723, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2898, 1306, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1623, 1308, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3409, 1314, 4, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1631, 1314, 4, 1, Condition.Hardmode);
      if (!Main.zenithWorld && !Main.remixWorld)
        BannerRecipeSystem.AddBannerToItemRecipe(2923, 1325, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1663, 726, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(3405, 3269, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 1264, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 676, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 725, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1630, 517, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(1674, 5096, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerToItemRecipe(2973, 682, 1, 1, Condition.Hardmode);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 672, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyPirateBanner, 2584, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerToItemRecipe(3441, 905, 1, 1, Condition.DownedPirates);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyJungleBanner, 3286, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(2943, 3290, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(3446, 683, 1, 1, Condition.DownedMechBossAny);
      BannerRecipeSystem.AddBannerToItemRecipe(1679, 1327, 1, 1, Condition.DownedMechBossAll);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyArmoredBonesBanner, 671, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyArmoredBonesBanner, 3291, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyArmoredBonesBanner, 4679, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerGroupToItemRecipe(BannerRecipeSystem.AnyArmoredBonesBanner, 1266, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3400, 3098, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3402, 3249, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2914, 1445, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3396, 3105, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2924, 4789, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3397, 3292, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3403, 3107, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2956, 1444, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2958, 1513, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(3401, 3106, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2965, 1446, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2974, 759, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2975, 1254, 1, 1, Condition.DownedPlantera);
      BannerRecipeSystem.AddBannerToItemRecipe(2984, 679, 1, 1, Condition.DownedPlantera);
    }

    private static void AddBannerGroupToItemRecipe(
      int recipeGroupID,
      int resultID,
      int resultAmount = 1,
      int groupAmount = 1,
      params Condition[] conditions)
    {
      RecipeHelper.CreateSimpleRecipe(recipeGroupID, resultID, 220, groupAmount, resultAmount, true, true, conditions);
    }

    private static void AddBannerToItemRecipe(
      int bannerItemID,
      int resultID,
      int bannerAmount = 1,
      int resultAmount = 1,
      params Condition[] conditions)
    {
      RecipeHelper.CreateSimpleRecipe(bannerItemID, resultID, 220, bannerAmount, resultAmount, true, false, conditions);
    }

    private static void AddBannerSetToItemRecipe(bool[] set, int resultID)
    {
      for (int index = 0; index < (int) NPCID.Count; ++index)
      {
        if (set[index])
        {
          int num = Item.NPCtoBanner(index);
          if (num > 0)
            RecipeHelper.CreateSimpleRecipe(Item.BannerToItem(num), resultID, 220, 1, 1, true, false);
        }
      }
    }
  }
}
