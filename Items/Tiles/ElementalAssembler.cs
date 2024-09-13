// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.ElementalAssembler
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
  public class ElementalAssembler : ModItem
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
      this.Item.value = Item.buyPrice(0, 50, 0, 0);
      this.Item.createTile = ModContent.TileType<ElementalAssemblerSheet>();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(221, 1).AddIngredient(3000, 1).AddIngredient(398, 1).AddIngredient(1430, 1).AddIngredient(1120, 1).AddIngredient(2196, 1).AddIngredient(2194, 1).AddIngredient(2198, 1).AddIngredient(2204, 1).AddIngredient(2197, 1).AddIngredient(998, 1).AddIngredient(2192, 1).AddIngredient(207, 1).AddIngredient(1128, 1).AddIngredient(5008, 1).AddRecipeGroup(RecipeGroups.AnyTombstone, 1).AddRecipeGroup(RecipeGroups.AnyDemonAltar, 1).AddTile(18).Register();
    }
  }
}
