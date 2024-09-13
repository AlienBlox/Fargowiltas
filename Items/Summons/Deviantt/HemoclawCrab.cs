// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.HemoclawCrab
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Systems.Recipes;
using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class HemoclawCrab : BaseSummon
  {
    public override int NPCType => 620;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanUseItem(Player player) => !Main.dayTime && Main.bloodMoon;

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4271, 1).AddIngredient(1085, 1).AddRecipeGroup(RecipeGroups.AnyFoodT3, 1).AddTile(134).Register();
    }
  }
}
