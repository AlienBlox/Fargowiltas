// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Explosives.InstaPond
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles.Explosives;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Explosives
{
  public class InstaPond : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 10;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 46;
      ((Entity) this.Item).height = 48;
      this.Item.maxStack = 99;
      this.Item.consumable = true;
      this.Item.useStyle = 1;
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
      this.Item.value = Item.buyPrice(0, 0, 3, 0);
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<InstaPondProj>();
      this.Item.shootSpeed = 5f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(167, 25).AddIngredient(4824, 3).AddTile(16).Register();
    }
  }
}
