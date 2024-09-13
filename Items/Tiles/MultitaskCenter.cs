// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.MultitaskCenter
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Systems.Recipes;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class MultitaskCenter : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 28;
      ((Entity) this.Item).height = 14;
      this.Item.maxStack = 99;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 15;
      this.Item.useTime = 10;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.value = Item.buyPrice(0, 30, 0, 0);
      this.Item.createTile = ModContent.TileType<MultitaskCenterSheet>();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(36, 1).AddIngredient(2172, 1).AddIngredient(33, 1).AddRecipeGroup(RecipeGroups.AnyAnvil, 1).AddIngredient(31, 1).AddIngredient(363, 1).AddIngredient(332, 1).AddRecipeGroup(RecipeGroups.AnyWoodenTable, 1).AddRecipeGroup(RecipeGroups.AnyWoodenChair, 1).AddRecipeGroup(RecipeGroups.AnyCookingPot, 1).AddRecipeGroup(RecipeGroups.AnyWoodenSink, 1).AddIngredient(352, 1).Register();
    }
  }
}
