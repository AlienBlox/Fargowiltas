// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.CrucibleCosmos
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Systems.Recipes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class CrucibleCosmos : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void ModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
      }
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 28;
      ((Entity) this.Item).height = 14;
      this.Item.rare = 10;
      this.Item.maxStack = 99;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 15;
      this.Item.useTime = 10;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.value = Item.buyPrice(2, 0, 0, 0);
      this.Item.createTile = ModContent.TileType<CrucibleCosmosSheet>();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<MultitaskCenter>(), 1).AddIngredient(ModContent.ItemType<ElementalAssembler>(), 1).AddIngredient(ModContent.ItemType<GoldenDippingVat>(), 1).AddRecipeGroup(RecipeGroups.AnyForge, 1).AddRecipeGroup(RecipeGroups.AnyHMAnvil, 1).AddRecipeGroup(RecipeGroups.AnyBookcase, 1).AddIngredient(487, 1).AddIngredient(1551, 1).AddIngredient(995, 1).AddIngredient(996, 1).AddIngredient(2203, 1).AddIngredient(4142, 1).AddIngredient(2193, 1).AddIngredient(2195, 1).AddIngredient(3549, 1).AddIngredient(3467, 25).AddTile(26).Register();
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("MagicStorage", ref mod))
        return;
      this.CreateRecipe(1).AddIngredient(mod.Find<ModItem>("CombinedStations4Item").Type, 1).AddIngredient(3467, 25).AddTile(26).Register();
    }
  }
}
