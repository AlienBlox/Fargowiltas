// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Systems.Recipes.ContainerRecipeSystem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Utilities;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Common.Systems.Recipes
{
  public class ContainerRecipeSystem : ModSystem
  {
    public virtual void AddRecipes()
    {
      ContainerRecipeSystem.AddPreHMTreasureBagRecipes();
      ContainerRecipeSystem.AddHMTreasureBagRecipes();
      ContainerRecipeSystem.AddEventTreasureBagRecipes();
      ContainerRecipeSystem.AddGrabBagRecipes();
      ContainerRecipeSystem.AddCrateRecipes();
      ContainerRecipeSystem.AddBiomeKeyRecipes();
      if (!Main.zenithWorld && !Main.remixWorld)
        ContainerRecipeSystem.CreateTreasureGroupRecipe(5010, 274, 3019, 218, 112, 220);
      else
        ContainerRecipeSystem.CreateTreasureGroupRecipe(5010, 274, 3019, 218, 220);
    }

    private static void AddPreHMTreasureBagRecipes()
    {
      ContainerRecipeSystem.CreateTreasureGroupRecipe(2489, 1309);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1360, 1299);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1361, 994);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1362, 3060);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1363, 1313);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3322, 2888, 1121, 1123);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1364, 2502, 1170);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(5111, 5117, 5118, 5119, 5095);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(5108, 5098, 5101, 5113);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3324, 491, 489, 2998, 490, 434, 426, 514, 4912);
    }

    private static void AddHMTreasureBagRecipes()
    {
      ContainerRecipeSystem.CreateTreasureGroupRecipe(4957, 4758, 4981, 4980);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3328, 758, 1157, 1255, 788, 1178, 3018, 1259, 1155);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1370, 1305, 1182);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3329, 1258, 1122, 899, 1248, 1294, 1295, 1296, 1297);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3330, 2611, 2624, 2622, 2621, 2623);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(2589, 2609);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(4782, 4923, 4914, 4952, 4953, 4778);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(4783, 4715, 5075, 4823);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3332, 3063, 3389, 3065, 1553, 3930, 3541, 3570, 3571, 3569);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3595, 4469);
    }

    private static void AddEventTreasureBagRecipes()
    {
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3867, 3857, 3855, 3810, 3809);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3868, 3811, 3812, 3854, 3852, 3823, 3835, 3836, 3856);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3860, 3883, 3827, 3870, 3858, 3859);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1855, 1829, 1831, 1835, 1837, 1845);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1856, 1826, 1801, 1802, 1782, 1784, 1811, 4680);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1962, 1928, 1916, 1930, 1871);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1961, 1910, 1929);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(1960, 1931, 1946, 1947, 1959, 1914);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3358, 2797, 2749, 2795, 2796, 2880, 2769, 2800, 2882, 2798);
      ContainerRecipeSystem.CreateTreasureGroupRecipe(3359, 855, 854, 905, 2584, 3033, 672, 4471);
    }

    private static void AddBiomeKeyRecipes()
    {
      RecipeHelper.CreateSimpleRecipe(1535, 1569, 134, 1, 1, true, false, Condition.DownedPlantera);
      RecipeHelper.CreateSimpleRecipe(1534, 1571, 134, 1, 1, true, false, Condition.DownedPlantera);
      RecipeHelper.CreateSimpleRecipe(1533, 1156, 134, 1, 1, true, false, Condition.DownedPlantera);
      RecipeHelper.CreateSimpleRecipe(1537, 1572, 134, 1, 1, true, false, Condition.DownedPlantera);
      RecipeHelper.CreateSimpleRecipe(1536, 1260, 134, 1, 1, true, false, Condition.DownedPlantera);
      RecipeHelper.CreateSimpleRecipe(4714, 4607, 134, 1, 1, true, false, Condition.DownedPlantera);
    }

    private static void AddGrabBagRecipes()
    {
      RecipeHelper.CreateSimpleRecipe(1869, 1927, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1869, 1923, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1869, 1921, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1869, 1870, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1869, 1909, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1869, 1915, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1869, 1918, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1774, 1810, 18, 10, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1774, 1800, 18, 25, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1774, 1809, 18, 2, 25, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 313, 18, 1, 5, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 314, 18, 1, 5, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 315, 18, 1, 5, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 317, 18, 1, 5, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 316, 18, 1, 5, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 318, 18, 1, 5, true, false);
      RecipeHelper.CreateSimpleRecipe(3093, 2358, 18, 1, 5, true, false);
    }

    private static void AddCrateRecipes()
    {
      ContainerRecipeSystem.CreateCrateRecipe(3200, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(3201, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(997, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(285, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(3068, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(946, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(953, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(3084, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(284, 2334, 3, 3979);
      if (!Main.remixWorld && !Main.zenithWorld)
        ContainerRecipeSystem.CreateCrateRecipe(3069, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(280, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(281, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(4341, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(4429, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(4427, 2334, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(2424, -1, 3, 3979);
      ContainerRecipeSystem.CreateCrateRecipe(2608, 2335, 3, 3980);
      ContainerRecipeSystem.CreateCrateRecipe(2587, 2335, 3, 3980);
      ContainerRecipeSystem.CreateCrateRecipe(2501, 2335, 3, 3980);
      ContainerRecipeSystem.CreateCrateRecipe(53, 2335, 1, 3980);
      ContainerRecipeSystem.CreateCrateRecipe(49, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(50, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(930, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(54, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(975, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(5011, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(29, 2336, 5, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(2491, -1, 5, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(989, 2336, 5, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(3064, 2336, 1, 3981);
      ContainerRecipeSystem.CreateCrateRecipe(212, 3208, 1, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(964, 3208, 1, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(211, 3208, 1, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(213, 3208, 1, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(2292, 3208, 1, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(4426, 3208, 1, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(753, 3208, 5, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(3017, 3208, 5, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(2204, 3208, 5, 3987);
      ContainerRecipeSystem.CreateCrateRecipe(159, 3206, 1, 3985);
      ContainerRecipeSystem.CreateCrateRecipe(65, 3206, 1, 3985);
      ContainerRecipeSystem.CreateCrateRecipe(4978, 3206, 1, 3985);
      ContainerRecipeSystem.CreateCrateRecipe(2197, 3206, 1, 3985);
      ContainerRecipeSystem.CreateCrateRecipe(158, 3206, 1, 3985);
      ContainerRecipeSystem.CreateCrateRecipe(2219, 3206, 1, 3985);
      ContainerRecipeSystem.CreateCrateRecipe(162, 3203, 1, 3982);
      ContainerRecipeSystem.CreateCrateRecipe(111, 3203, 1, 3982);
      ContainerRecipeSystem.CreateCrateRecipe(115, 3203, 1, 3982);
      ContainerRecipeSystem.CreateCrateRecipe(96, 3203, 1, 3982);
      ContainerRecipeSystem.CreateCrateRecipe(64, 3203, 1, 3982);
      ContainerRecipeSystem.CreateCrateRecipe(800, 3204, 1, 3983);
      ContainerRecipeSystem.CreateCrateRecipe(802, 3204, 1, 3983);
      ContainerRecipeSystem.CreateCrateRecipe(1256, 3204, 1, 3983);
      ContainerRecipeSystem.CreateCrateRecipe(1290, 3204, 1, 3983);
      ContainerRecipeSystem.CreateCrateRecipe(3062, 3204, 1, 3983);
      ContainerRecipeSystem.CreateCrateRecipe(165, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(155, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(156, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(113, 3205, 1, 3984, 327);
      if (!Main.zenithWorld && !Main.remixWorld)
        ContainerRecipeSystem.CreateCrateRecipe(157, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(3317, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(164, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(329, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(163, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(2192, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(3000, 3205, 1, 3984, 327);
      ContainerRecipeSystem.CreateCrateRecipe(2999, 3205, 1, 3984, 327);
      if (!Main.zenithWorld && !Main.remixWorld)
        ContainerRecipeSystem.CreateCrateRecipe(1319, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(987, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(724, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(950, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(3199, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(1579, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(670, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(2198, 4405, 1, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(669, 4405, 5, 4406);
      ContainerRecipeSystem.CreateCrateRecipe(4055, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4056, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4061, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4442, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4062, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4276, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4263, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4262, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4066, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(4346, 4407, 1, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(934, 4407, 3, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(857, 4407, 3, 4408);
      ContainerRecipeSystem.CreateCrateRecipe(274, 4877, 1, 4878, 329);
      ContainerRecipeSystem.CreateCrateRecipe(3019, 4877, 1, 4878, 329);
      ContainerRecipeSystem.CreateCrateRecipe(218, 4877, 1, 4878, 329);
      if (!Main.zenithWorld && !Main.remixWorld)
        ContainerRecipeSystem.CreateCrateRecipe(112, 4877, 1, 4878, 329);
      ContainerRecipeSystem.CreateCrateRecipe(220, 4877, 1, 4878, 329);
      ContainerRecipeSystem.CreateCrateRecipe(5010, 4877, 1, 4878, 329);
      ContainerRecipeSystem.CreateCrateRecipe(906, 4877, 5, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4551, 4877, 5, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4737, 4877, 5, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4828, 4877, 1, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4822, 4877, 1, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4881, 4877, 1, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4443, 4877, 1, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4824, 4877, 1, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(4819, 4877, 1, 4878);
      ContainerRecipeSystem.CreateCrateRecipe(277, 5002, 1, 5003);
      ContainerRecipeSystem.CreateCrateRecipe(186, 5002, 1, 5003);
      ContainerRecipeSystem.CreateCrateRecipe(187, 5002, 1, 5003);
      ContainerRecipeSystem.CreateCrateRecipe(4404, 5002, 1, 5003);
      ContainerRecipeSystem.CreateCrateRecipe(863, 5002, 5, 5003);
      ContainerRecipeSystem.CreateCrateRecipe(4425, 5002, 5, 5003);
    }

    private static void CreateCrateRecipe(
      int result,
      int crate,
      int crateAmount,
      int hardmodeCrate,
      int extraItem = -1)
    {
      if (crate != -1)
      {
        Recipe recipe = Recipe.Create(result, 1);
        recipe.AddIngredient(crate, crateAmount);
        if (extraItem != -1)
          recipe.AddIngredient(extraItem, 1);
        recipe.AddTile(18);
        recipe.DisableDecraft();
        recipe.Register();
      }
      if (hardmodeCrate == -1)
        return;
      Recipe recipe1 = Recipe.Create(result, 1);
      recipe1.AddIngredient(hardmodeCrate, crateAmount);
      if (extraItem != -1)
        recipe1.AddIngredient(extraItem, 1);
      recipe1.AddTile(18);
      recipe1.DisableDecraft();
      recipe1.Register();
    }

    private static void CreateTreasureGroupRecipe(int input, params int[] outputs)
    {
      foreach (int output in outputs)
        RecipeHelper.CreateSimpleRecipe(input, output, 220, 1, 1, true, false);
    }
  }
}
