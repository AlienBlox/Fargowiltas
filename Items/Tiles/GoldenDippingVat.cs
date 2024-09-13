// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.GoldenDippingVat
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
  public class GoldenDippingVat : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 99;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.rare = 8;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.useAnimation = 15;
      this.Item.useTime = 15;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.createTile = ModContent.TileType<GoldenDippingVatSheet>();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup(RecipeGroups.AnyCookingPot, 1).AddRecipeGroup(RecipeGroups.AnyGoldBar, 25).AddIngredient(73, 25).AddIngredient(1348, 250).AddTile(134).Register();
      GoldenDippingVat.AddCritter(2015, 2889);
      GoldenDippingVat.AddCritter(2019, 2890);
      GoldenDippingVat.AddCritter(2121, 2892);
      GoldenDippingVat.AddCritter(261, 4274);
      GoldenDippingVat.AddCritter(2740, 2893);
      GoldenDippingVat.AddCritter(4361, 4362);
      GoldenDippingVat.AddCritter(2003, 2894);
      GoldenDippingVat.AddCritter(4480, 4482);
      GoldenDippingVat.AddCritter(4418, 4419);
      GoldenDippingVat.AddCritter(2002, 2895);
      GoldenDippingVat.AddCritterFromGroup(RecipeGroups.AnySquirrel, 3564);
      GoldenDippingVat.AddCritterFromGroup(RecipeGroups.AnyButterfly, 2891);
      GoldenDippingVat.AddCritterFromGroup(RecipeGroups.AnyCommonFish, 2308);
      GoldenDippingVat.AddCritterFromGroup(RecipeGroups.AnyDragonfly, 4340);
    }

    private static void AddCritter(int critterID, int goldCritterID)
    {
      Recipe.Create(goldCritterID, 1).AddIngredient(critterID, 1).AddIngredient(1348, 100).AddTile(ModContent.TileType<GoldenDippingVatSheet>()).Register();
    }

    private static void AddCritterFromGroup(int critterGroup, int goldCritterID)
    {
      Recipe.Create(goldCritterID, 1).AddRecipeGroup(critterGroup, 1).AddIngredient(1348, 100).AddTile(ModContent.TileType<GoldenDippingVatSheet>()).Register();
    }
  }
}
