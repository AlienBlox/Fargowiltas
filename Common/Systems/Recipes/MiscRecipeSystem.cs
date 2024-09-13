// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Common.Systems.Recipes.MiscRecipeSystem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Summons;
using Fargowiltas.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Common.Systems.Recipes
{
  public class MiscRecipeSystem : ModSystem
  {
    public virtual void AddRecipes()
    {
      MiscRecipeSystem.AddStatueRecipes();
      MiscRecipeSystem.AddMiscRecipes();
    }

    public virtual void PostAddRecipes()
    {
      foreach (Recipe recipe in ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe => recipe.HasIngredient(2218))))
      {
        Item obj1;
        if (recipe.TryGetIngredient(1316, ref obj1))
        {
          recipe.RemoveIngredient(obj1);
          recipe.AddIngredient(1001, 1);
        }
        Item obj2;
        if (recipe.TryGetIngredient(1317, ref obj2))
        {
          recipe.RemoveIngredient(obj2);
          recipe.AddIngredient(1004, 1);
        }
        Item obj3;
        if (recipe.TryGetIngredient(1318, ref obj3))
        {
          recipe.RemoveIngredient(obj3);
          recipe.AddIngredient(1005, 1);
        }
      }
      foreach (Recipe recipe in ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe => recipe.createItem.ModItem != null && recipe.createItem.ModItem is BaseSummon)))
        recipe.DisableDecraft();
    }

    private static void AddStatueRecipes()
    {
      MiscRecipeSystem.AddStatueRecipe(443, 1621);
      MiscRecipeSystem.AddStatueRecipe(463, 1630);
      MiscRecipeSystem.AddStatueRecipe(454, 1634);
      MiscRecipeSystem.AddStatueRecipe(459, 1665);
      MiscRecipeSystem.AddStatueRecipe(478, 1675);
      MiscRecipeSystem.AddStatueRecipe(2672, 1680);
      MiscRecipeSystem.AddStatueRecipe(446, 1681);
      MiscRecipeSystem.AddStatueRecipe(3712, 1681);
      MiscRecipeSystem.AddStatueRecipe(440, 1683);
      MiscRecipeSystem.AddStatueRecipe(3708, 1685);
      MiscRecipeSystem.AddStatueRecipe(3709, 1691);
      MiscRecipeSystem.AddStatueRecipe(3710, 3410);
      MiscRecipeSystem.AddStatueRecipe(3711, 1699);
      MiscRecipeSystem.AddStatueRecipe(3713, 2988);
      MiscRecipeSystem.AddStatueRecipe(3714, 3405);
      MiscRecipeSystem.AddStatueRecipe(3715, 1658);
      MiscRecipeSystem.AddStatueRecipe(3716, 1674);
      MiscRecipeSystem.AddStatueRecipe(3717, 3406);
      MiscRecipeSystem.AddStatueRecipe(3718, 3408);
      MiscRecipeSystem.AddStatueRecipe(3720, 3409);
      MiscRecipeSystem.AddStatueRecipe(453, 166, 99);
      MiscRecipeSystem.AddStatueRecipe(473, 29, 6);
      MiscRecipeSystem.AddStatueRecipe(438, 109, 6);
      MiscRecipeSystem.AddStatueRecipe(3719, 1701);
      MiscRecipeSystem.AddStatueRecipe(466, 1641);
      MiscRecipeSystem.AddStatueRecipe(471, 1639);
      MiscRecipeSystem.AddStatueRecipe(441, 1654);
      MiscRecipeSystem.AddStatueRecipe(452, 1661);
      MiscRecipeSystem.AddStatueRecipe(449, 1664);
      MiscRecipeSystem.AddStatueRecipe(442);
      MiscRecipeSystem.AddStatueRecipe(468);
      MiscRecipeSystem.AddStatueRecipe(465);
      MiscRecipeSystem.AddStatueRecipe(461);
      MiscRecipeSystem.AddStatueRecipe(462);
      MiscRecipeSystem.AddStatueRecipe(460);
      MiscRecipeSystem.AddStatueRecipe(455);
      MiscRecipeSystem.AddStatueRecipe(469);
      MiscRecipeSystem.AddStatueRecipe(457);
      MiscRecipeSystem.AddStatueRecipe(475);
      MiscRecipeSystem.AddStatueRecipe(439);
      MiscRecipeSystem.AddStatueRecipe(456);
      MiscRecipeSystem.AddStatueRecipe(52);
      MiscRecipeSystem.AddStatueRecipe(458);
      MiscRecipeSystem.AddStatueRecipe(450);
      MiscRecipeSystem.AddStatueRecipe(451);
      MiscRecipeSystem.AddStatueRecipe(472);
      MiscRecipeSystem.AddStatueRecipe(474);
      MiscRecipeSystem.AddStatueRecipe(447);
      MiscRecipeSystem.AddStatueRecipe(448);
      MiscRecipeSystem.AddStatueRecipe(467);
      MiscRecipeSystem.AddStatueRecipe(1154, 1667, isLihzahrdStatue: true);
      MiscRecipeSystem.AddStatueRecipe(1152, 1667, isLihzahrdStatue: true);
      MiscRecipeSystem.AddStatueRecipe(1153, 1667, isLihzahrdStatue: true);
      Recipe recipe1 = Recipe.Create(476, 1);
      recipe1.AddIngredient(355, 1);
      recipe1.AddIngredient(2351, 1);
      recipe1.AddIngredient(3, 50);
      recipe1.AddTile(283);
      recipe1.DisableDecraft();
      recipe1.Register();
      Recipe recipe2 = Recipe.Create(477, 1);
      recipe2.AddIngredient(355, 1);
      recipe2.AddIngredient(2351, 1);
      recipe2.AddIngredient(3, 50);
      recipe2.AddTile(283);
      recipe2.DisableDecraft();
      recipe2.Register();
    }

    private static void AddStatueRecipe(
      int statue,
      int extraIngredient = -1,
      int extraIngredientAmount = 1,
      bool isLihzahrdStatue = false)
    {
      Recipe recipe = Recipe.Create(statue, 1);
      if (extraIngredient != -1)
        recipe.AddIngredient(extraIngredient, extraIngredientAmount);
      recipe.AddIngredient(isLihzahrdStatue ? 1101 : 3, 50);
      recipe.AddTile(283);
      recipe.DisableDecraft();
      recipe.Register();
    }

    private static void AddMiscRecipes()
    {
      RecipeHelper.CreateSimpleRecipe(724, 989, 125, 1, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(1725, 1799, 304, 500, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(2338, 753, 304, 5, 1, true, false);
      RecipeHelper.CreateSimpleRecipe(316, 5114, 85, 5, 1, true, false, Condition.InGraveyard);
      RecipeHelper.CreateSimpleRecipe(989, 4144, 125, 1, 1, true, false, Condition.Hardmode);
      Recipe recipe1 = Recipe.Create(4837, 1);
      recipe1.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe1.AddIngredient(999, 5);
      recipe1.AddTile(220);
      recipe1.DisableDecraft();
      recipe1.Register();
      Recipe recipe2 = Recipe.Create(4831, 1);
      recipe2.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe2.AddIngredient(181, 5);
      recipe2.AddTile(220);
      recipe2.DisableDecraft();
      recipe2.Register();
      Recipe recipe3 = Recipe.Create(4836, 1);
      recipe3.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe3.AddIngredient(182, 5);
      recipe3.AddTile(220);
      recipe3.DisableDecraft();
      recipe3.Register();
      Recipe recipe4 = Recipe.Create(4834, 1);
      recipe4.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe4.AddIngredient(179, 5);
      recipe4.AddTile(220);
      recipe4.DisableDecraft();
      recipe4.Register();
      Recipe recipe5 = Recipe.Create(4835, 1);
      recipe5.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe5.AddIngredient(178, 5);
      recipe5.AddTile(220);
      recipe5.DisableDecraft();
      recipe5.Register();
      Recipe recipe6 = Recipe.Create(4833, 1);
      recipe6.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe6.AddIngredient(177, 5);
      recipe6.AddTile(220);
      recipe6.DisableDecraft();
      recipe6.Register();
      Recipe recipe7 = Recipe.Create(4832, 1);
      recipe7.AddRecipeGroup(RecipeGroups.AnySquirrel, 1);
      recipe7.AddIngredient(180, 5);
      recipe7.AddTile(220);
      recipe7.DisableDecraft();
      recipe7.Register();
      Recipe recipe8 = Recipe.Create(3017, 1);
      recipe8.AddIngredient(54, 1);
      recipe8.AddIngredient(313, 1);
      recipe8.AddIngredient(315, 1);
      recipe8.AddIngredient(2358, 1);
      recipe8.AddIngredient(314, 1);
      recipe8.AddIngredient(317, 1);
      recipe8.AddIngredient(316, 1);
      recipe8.AddIngredient(318, 1);
      recipe8.AddTile(304);
      recipe8.DisableDecraft();
      recipe8.Register();
      Recipe recipe9 = Recipe.Create(2196, 1);
      recipe9.AddIngredient(332, 1);
      recipe9.AddIngredient(210, 10);
      recipe9.AddTile(18);
      recipe9.DisableDecraft();
      recipe9.Register();
      Recipe recipe10 = Recipe.Create(4281, 1);
      recipe10.AddIngredient(9, 10);
      recipe10.AddIngredient(2015, 1);
      recipe10.AddTile(304);
      recipe10.DisableDecraft();
      recipe10.Register();
      Recipe recipe11 = Recipe.Create(29, 1);
      recipe11.AddIngredient(183, 15);
      recipe11.AddIngredient(188, 3);
      recipe11.AddIngredient(331, 3);
      recipe11.AddTile(26);
      recipe11.DisableDecraft();
      recipe11.Register();
      Recipe recipe12 = Recipe.Create(208, 1);
      recipe12.AddIngredient(223, 1);
      recipe12.AddIngredient(1115, 1);
      recipe12.AddTile(304);
      recipe12.DisableDecraft();
      recipe12.Register();
      Recipe recipe13 = Recipe.Create(223, 1);
      recipe13.AddIngredient(208, 1);
      recipe13.AddIngredient(1116, 1);
      recipe13.AddTile(304);
      recipe13.DisableDecraft();
      recipe13.Register();
      Recipe recipe14 = Recipe.Create(1242, 1);
      recipe14.AddIngredient(999, 15);
      recipe14.AddIngredient(1992, 1);
      recipe14.AddTile(96);
      recipe14.DisableDecraft();
      recipe14.Register();
      Recipe recipe15 = Recipe.Create(223, 1);
      recipe15.AddIngredient(314, 15);
      recipe15.AddIngredient(109, 1);
      recipe15.AddTile(355);
      recipe15.DisableDecraft();
      recipe15.Register();
      Recipe recipe16 = Recipe.Create(857, 1);
      recipe16.AddIngredient(169, 50);
      recipe16.AddIngredient(31, 1);
      recipe16.AddTile(355);
      recipe16.DisableDecraft();
      recipe16.Register();
      Recipe recipe17 = Recipe.Create(1552, 1);
      recipe17.AddIngredient(1006, 1);
      recipe17.AddIngredient(783, 1);
      recipe17.AddTile(247);
      recipe17.DisableDecraft();
      recipe17.Register();
      Recipe recipe18 = Recipe.Create(939, 1);
      recipe18.AddIngredient(84, 1);
      recipe18.AddIngredient(3080, 8);
      recipe18.AddTile(96);
      recipe18.DisableDecraft();
      recipe18.Register();
      Recipe recipe19 = Recipe.Create(857, 1);
      recipe19.AddIngredient(848, 1);
      recipe19.AddIngredient(866, 1);
      recipe19.AddIngredient(73, 10);
      recipe19.AddTile(114);
      recipe19.DisableDecraft();
      recipe19.Register();
      Recipe recipe20 = Recipe.Create(934, 1);
      recipe20.AddIngredient(848, 1);
      recipe20.AddIngredient(866, 1);
      recipe20.AddIngredient(73, 10);
      recipe20.AddTile(114);
      recipe20.DisableDecraft();
      recipe20.Register();
      Recipe recipe21 = Recipe.Create(1863, 1);
      recipe21.AddIngredient(399, 1);
      recipe21.AddIngredient(215, 1);
      recipe21.AddTile(114);
      recipe21.DisableDecraft();
      recipe21.Register();
      Recipe recipe22 = Recipe.Create(3250, 1);
      recipe22.AddIngredient(1250, 1);
      recipe22.AddIngredient(215, 1);
      recipe22.AddTile(114);
      recipe22.DisableDecraft();
      recipe22.Register();
      Recipe recipe23 = Recipe.Create(4951, 1);
      recipe23.AddIngredient(4919, 1);
      recipe23.AddIngredient(4916, 1);
      recipe23.AddIngredient(4875, 1);
      recipe23.AddIngredient(4921, 1);
      recipe23.AddIngredient(4918, 1);
      recipe23.AddIngredient(4876, 1);
      recipe23.AddIngredient(4920, 1);
      recipe23.AddIngredient(4917, 1);
      recipe23.AddIngredient(74, 1);
      recipe23.AddTile(26);
      recipe23.DisableDecraft();
      recipe23.Register();
      Recipe recipe24 = Recipe.Create(4368, 1);
      recipe24.AddIngredient(4367, 1);
      recipe24.AddIngredient(4371, 1);
      recipe24.AddTile(220);
      recipe24.DisableDecraft();
      recipe24.Register();
      Recipe recipe25 = Recipe.Create(4370, 1);
      recipe25.AddIngredient(4369, 1);
      recipe25.AddIngredient(4371, 1);
      recipe25.AddTile(220);
      recipe25.DisableDecraft();
      recipe25.Register();
    }
  }
}
