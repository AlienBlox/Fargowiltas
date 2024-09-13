// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Renewals.BaseRenewalItem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Renewals
{
  public abstract class BaseRenewalItem : ModItem
  {
    private readonly string name;
    private readonly string tooltip;
    private readonly int material;
    private readonly bool supreme;
    private readonly int supremeMaterial;

    protected BaseRenewalItem(
      string name,
      string tooltip,
      int material,
      bool supreme = false,
      int supremeMaterial = -1)
    {
      this.name = name;
      this.tooltip = tooltip;
      this.material = material;
      this.supreme = supreme;
      this.supremeMaterial = supremeMaterial;
    }

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 10;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 26;
      this.Item.maxStack = 99;
      this.Item.consumable = true;
      this.Item.useStyle = 1;
      this.Item.rare = 3;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
      this.Item.value = Item.buyPrice(0, 0, 3, 0);
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.shoot = 1;
      this.Item.shootSpeed = 5f;
    }

    public virtual void AddRecipes()
    {
      Recipe recipe = Recipe.Create(this.Type, 1);
      if (this.supreme)
      {
        recipe.AddIngredient(this.supremeMaterial, 10);
        recipe.AddIngredient(1006, 5);
        recipe.AddTile(355);
      }
      else
      {
        recipe.AddIngredient(31, 1);
        recipe.AddIngredient(this.material, 100);
        recipe.AddTile(13);
      }
      recipe.Register();
    }
  }
}
