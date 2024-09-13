// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Systems.Recipes.RecipeGroups
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Ammos.Bullets;
using Fargowiltas.Items.Tiles;
using Fargowiltas.Utilities;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Common.Systems.Recipes
{
  public class RecipeGroups : ModSystem
  {
    internal static int AnyGoldBar;
    internal static int AnyDemonAltar;
    internal static int AnyAnvil;
    internal static int AnyHMAnvil;
    internal static int AnyForge;
    internal static int AnyBookcase;
    internal static int AnyCookingPot;
    internal static int AnyTombstone;
    internal static int AnyWoodenTable;
    internal static int AnyWoodenChair;
    internal static int AnyWoodenSink;
    internal static int AnyButterfly;
    internal static int AnySquirrel;
    internal static int AnyCommonFish;
    internal static int AnyDragonfly;
    internal static int AnyBird;
    internal static int AnyDuck;
    internal static int AnyFoodT2;
    internal static int AnyFoodT3;
    internal static int AnyGemRobe;

    public virtual void AddRecipeGroups()
    {
      RecipeGroup.RegisterGroup("Fargowiltas:AnySilverPouch", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(ModContent.ItemType<SilverPouch>())), new int[2]
      {
        ModContent.ItemType<SilverPouch>(),
        ModContent.ItemType<TungstenPouch>()
      }));
      RecipeGroups.AnyGoldBar = RecipeGroup.RegisterGroup("Fargowiltas:AnyGoldBar", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(19)), new int[2]
      {
        19,
        706
      }));
      RecipeGroups.AnyDemonAltar = RecipeGroup.RegisterGroup("Fargowiltas:AnyDemonAltar", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(ModContent.ItemType<DemonAltar>())), new int[2]
      {
        ModContent.ItemType<DemonAltar>(),
        ModContent.ItemType<CrimsonAltar>()
      }));
      RecipeGroups.AnyAnvil = RecipeGroup.RegisterGroup("Fargowiltas:AnyAnvil", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(35)), new int[2]
      {
        35,
        716
      }));
      RecipeGroups.AnyHMAnvil = RecipeGroup.RegisterGroup("Fargowiltas:AnyHMAnvil", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(525)), new int[2]
      {
        525,
        1220
      }));
      RecipeGroups.AnyForge = RecipeGroup.RegisterGroup("Fargowiltas:AnyForge", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(524)), new int[2]
      {
        524,
        1221
      }));
      RecipeGroups.AnyBookcase = RecipeGroup.RegisterGroup("Fargowiltas:AnyBookcase", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(354)), new int[32]
      {
        354,
        1414,
        2138,
        2554,
        2020,
        3917,
        2233,
        2021,
        2022,
        2031,
        2025,
        2137,
        1512,
        3167,
        1415,
        2023,
        2135,
        3166,
        3165,
        2540,
        1463,
        2536,
        2027,
        1416,
        2670,
        2026,
        2136,
        2029,
        2569,
        2028,
        2024,
        5192
      }));
      RecipeGroups.AnyCookingPot = RecipeGroup.RegisterGroup("Fargowiltas:AnyCookingPot", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(345)), new int[2]
      {
        345,
        1791
      }));
      RecipeGroups.AnyButterfly = RecipeGroup.RegisterGroup("Fargowiltas:AnyButterfly", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("LegacyMisc.87", true)), new int[9]
      {
        2001,
        1994,
        1995,
        1996,
        1998,
        1999,
        1997,
        2000,
        4845
      }));
      RecipeGroups.AnySquirrel = RecipeGroup.RegisterGroup("Fargowiltas:AnySquirrel", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(2018)), new int[2]
      {
        2018,
        3563
      }));
      RecipeGroups.AnyCommonFish = RecipeGroup.RegisterGroup("Fargowiltas:AnyCommonFish", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("CommonFish")), new int[6]
      {
        2299,
        2290,
        2297,
        2301,
        2298,
        2300
      }));
      RecipeGroups.AnyDragonfly = RecipeGroup.RegisterGroup("Fargowiltas:AnyDragonfly", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("LegacyMisc.105", true)), new int[6]
      {
        4334,
        4335,
        4336,
        4337,
        4338,
        4339
      }));
      RecipeGroups.AnyBird = RecipeGroup.RegisterGroup("Fargowiltas:AnyBird", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(2015)), new int[7]
      {
        2015,
        2016,
        2017,
        2123,
        2122,
        4374,
        4359
      }));
      RecipeGroups.AnyDuck = RecipeGroup.RegisterGroup("Fargowiltas:AnyDuck", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(2123)), new int[3]
      {
        2123,
        2122,
        4374
      }));
      RecipeGroups.AnyTombstone = RecipeGroup.RegisterGroup("Fargowiltas:AnyTombstone", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(321)), new int[11]
      {
        321,
        1174,
        1175,
        1173,
        1176,
        1177,
        3229,
        3230,
        3231,
        3232,
        3233
      }));
      RecipeGroups.AnyWoodenTable = RecipeGroup.RegisterGroup("Fargowiltas:AnyWoodenTable", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(32)), new int[12]
      {
        32,
        677,
        5207,
        639,
        829,
        640,
        1816,
        638,
        917,
        2532,
        2259,
        4583
      }));
      RecipeGroups.AnyWoodenChair = RecipeGroup.RegisterGroup("Fargowiltas:AnyWoodenChair", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(34)), new int[12]
      {
        34,
        2557,
        5196,
        629,
        806,
        630,
        1814,
        628,
        915,
        2524,
        2228,
        4572
      }));
      RecipeGroups.AnyWoodenSink = RecipeGroup.RegisterGroup("Fargowiltas:AnyWoodenSink", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText(2827)), new int[12]
      {
        2827,
        2852,
        5205,
        2829,
        2833,
        2830,
        2847,
        2828,
        2835,
        2850,
        2849,
        4581
      }));
      RecipeGroups.AnyFoodT2 = RecipeGroup.RegisterGroup("Fargowiltas:AnyFoodT2", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("FoodT2")), new int[12]
      {
        357,
        2426,
        1787,
        2427,
        4019,
        5093,
        3195,
        4403,
        5092,
        4623,
        4032,
        4034
      }));
      RecipeGroups.AnyFoodT3 = RecipeGroup.RegisterGroup("Fargowiltas:AnyFoodT3", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("FoodT3")), new int[2]
      {
        4022,
        4615
      }));
      RecipeGroups.AnyGemRobe = RecipeGroup.RegisterGroup("Fargowiltas:AnyGemRobe", new RecipeGroup((Func<string>) (() => RecipeHelper.GenerateAnyItemRecipeGroupText("GemRobe")), new int[7]
      {
        4256,
        1282,
        1287,
        1285,
        1286,
        1284,
        1283
      }));
    }
  }
}
