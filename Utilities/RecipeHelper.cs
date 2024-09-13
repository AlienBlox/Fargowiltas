// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Utilities.RecipeHelper
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Localization;

#nullable disable
namespace Fargowiltas.Utilities
{
  public static class RecipeHelper
  {
    public static void CreateSimpleRecipe(
      int ingredientID,
      int resultID,
      int tileID,
      int ingredientAmount = 1,
      int resultAmount = 1,
      bool disableDecraft = false,
      bool usesRecipeGroup = false,
      params Condition[] conditions)
    {
      Recipe recipe = Recipe.Create(resultID, resultAmount);
      if (usesRecipeGroup)
        recipe.AddRecipeGroup(ingredientID, ingredientAmount);
      else
        recipe.AddIngredient(ingredientID, ingredientAmount);
      recipe.AddTile(tileID);
      foreach (Condition condition in conditions)
        recipe.AddCondition(condition);
      if (disableDecraft)
        recipe.DisableDecraft();
      recipe.Register();
    }

    public static string GenerateAnyItemRecipeGroupText(int vanillaId)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("LegacyMisc.37"));
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<LocalizedText>(Lang.GetItemName(vanillaId));
      return interpolatedStringHandler.ToStringAndClear();
    }

    public static string GenerateAnyItemRecipeGroupText(string localizationKey, bool isVanillaKey = false)
    {
      return Language.GetTextValue("LegacyMisc.37") + " " + Language.GetTextValue(isVanillaKey ? localizationKey : "Mods.Fargowiltas.RecipeGroups." + localizationKey);
    }

    public static string GenerateAnyBannerRecipeGroupText(string vanillaKey)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("LegacyMisc.37"));
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue(vanillaKey));
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted(Language.GetTextValue("MapObject.Banner"));
      return interpolatedStringHandler.ToStringAndClear();
    }
  }
}
