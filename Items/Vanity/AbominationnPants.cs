// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Vanity.AbominationnPants
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Vanity
{
  [AutoloadEquip]
  public class AbominationnPants : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.vanity = true;
      this.Item.rare = 1;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1942, 1).AddIngredient(3363, 1).AddTile(114).Register();
    }
  }
}
