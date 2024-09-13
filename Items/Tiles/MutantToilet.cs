// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.MutantToilet
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class MutantToilet : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.DefaultToPlaceableTile(ModContent.TileType<MutantToiletSheet>(), 0);
      this.Item.maxStack = 99;
      ((Entity) this.Item).width = 16;
      ((Entity) this.Item).height = 24;
      this.Item.value = Item.sellPrice(1, 50, 0, 0);
      this.Item.rare = 11;
    }

    public virtual void AddRecipes()
    {
      ModItem modItem;
      if (!ModContent.TryFind<ModItem>("Fargowiltas/Mutant", ref modItem))
        return;
      this.CreateRecipe(1).AddIngredient(3467, 6).AddIngredient(modItem.Type, 1).AddTile(ModContent.TileType<CrucibleCosmosSheet>()).Register();
    }
  }
}
