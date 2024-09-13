// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.Semistation
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class Semistation : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 15;
      this.Item.useTime = 10;
      this.Item.useStyle = 1;
      this.Item.rare = 1;
      this.Item.value = Item.buyPrice(0, 1, 0, 0);
      this.Item.createTile = ModContent.TileType<SemistationSheet>();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(63, 5).AddIngredient(966, 5).AddIngredient(1859, 5).AddIngredient(1431, 5).AddTile(16).Register();
    }
  }
}
