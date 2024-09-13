// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Systems.Recipes.ConversionRecipeSystem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Summons;
using Fargowiltas.Items.Summons.Mutant;
using Fargowiltas.Items.Summons.VanillaCopy;
using Fargowiltas.Utilities;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Common.Systems.Recipes
{
  public class ConversionRecipeSystem : ModSystem
  {
    public virtual void AddRecipes()
    {
      ConversionRecipeSystem.AddSummonConversions();
      ConversionRecipeSystem.AddEvilConversions();
      ConversionRecipeSystem.AddMetalConversions();
    }

    private static void AddSummonConversions()
    {
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<FleshyDoll>(), 267, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<LihzahrdPowerCell2>(), 1293, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<TruffleWorm2>(), 2673, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<CelestialSigil2>(), 3601, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<MechEye>(), 544, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<MechWorm>(), 556, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<MechSkull>(), 557, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<GoreySpine>(), 1331, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<SlimyCrown>(), 560, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<Abeemination2>(), 1133, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<DeerThing2>(), 5120, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<WormyFood>(), 70, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<SuspiciousEye>(), 43, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<PrismaticPrimrose>(), 4961, 18, 1, 1, false, false);
      RecipeHelper.CreateSimpleRecipe(ModContent.ItemType<JellyCrystal>(), 4988, 18, 1, 1, false, false);
    }

    private static void AddEvilConversions()
    {
      ConversionRecipeSystem.AddConvertRecipe(1330, 68);
      ConversionRecipeSystem.AddConvertRecipe(86, 1329);
      ConversionRecipeSystem.AddConvertRecipe(782, 784);
      ConversionRecipeSystem.AddConvertRecipe(1332, 522);
      ConversionRecipeSystem.AddConvertRecipe(3016, 3015);
      ConversionRecipeSystem.AddConvertRecipe(3007, 3008);
      ConversionRecipeSystem.AddConvertRecipe(3023, 3020);
      ConversionRecipeSystem.AddConvertRecipe(3012, 3013);
      ConversionRecipeSystem.AddConvertRecipe(3014, 3006);
      ConversionRecipeSystem.AddConvertRecipe(115, 3062);
      ConversionRecipeSystem.AddConvertRecipe(96, 800);
      ConversionRecipeSystem.AddConvertRecipe(1290, 111);
      ConversionRecipeSystem.AddConvertRecipe(162, 802);
      ConversionRecipeSystem.AddConvertRecipe(1256, 64);
      ConversionRecipeSystem.AddConvertRecipe(836, 61);
      ConversionRecipeSystem.AddConvertRecipe(911, 619);
      ConversionRecipeSystem.AddConvertRecipe(60, 2887);
      ConversionRecipeSystem.AddConvertRecipe(3211, 3210);
      ConversionRecipeSystem.AddConvertRecipe(1569, 1571);
      ConversionRecipeSystem.AddConvertRecipe(2318, 2305);
      ConversionRecipeSystem.AddConvertRecipe(2319, 2318);
      ConversionRecipeSystem.AddConvertRecipe(3060, 994);
      ConversionRecipeSystem.AddConvertRecipe(2171, 59);
      ConversionRecipeSystem.AddConvertRecipe(1492, 1488);
      ConversionRecipeSystem.AddConvertRecipe(4284, 4285);
    }

    private static void AddMetalConversions()
    {
      ConversionRecipeSystem.AddConvertRecipe(12, 699);
      ConversionRecipeSystem.AddConvertRecipe(20, 703);
      ConversionRecipeSystem.AddConvertRecipe(11, 700);
      ConversionRecipeSystem.AddConvertRecipe(22, 704);
      ConversionRecipeSystem.AddConvertRecipe(14, 701);
      ConversionRecipeSystem.AddConvertRecipe(21, 705);
      ConversionRecipeSystem.AddConvertRecipe(13, 702);
      ConversionRecipeSystem.AddConvertRecipe(19, 706);
      ConversionRecipeSystem.AddConvertRecipe(364, 1104);
      ConversionRecipeSystem.AddConvertRecipe(381, 1184);
      ConversionRecipeSystem.AddConvertRecipe(365, 1105);
      ConversionRecipeSystem.AddConvertRecipe(382, 1191);
      ConversionRecipeSystem.AddConvertRecipe(366, 1106);
      ConversionRecipeSystem.AddConvertRecipe(391, 1198);
      ConversionRecipeSystem.AddConvertRecipe(56, 880);
      ConversionRecipeSystem.AddConvertRecipe(57, 1257);
    }

    private static void AddConvertRecipe(int itemID, int otherItemID)
    {
      RecipeHelper.CreateSimpleRecipe(itemID, otherItemID, 26, 1, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(otherItemID, itemID, 26, 1, 1, true, false);
    }
  }
}
