// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Explosives.BoomShuriken
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
  public class BoomShuriken : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 999;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 11;
      ((Entity) this.Item).height = 11;
      this.Item.damage = 16;
      this.Item.noMelee = true;
      this.Item.consumable = true;
      this.Item.noUseGraphic = true;
      this.Item.scale = 0.75f;
      this.Item.useStyle = 1;
      this.Item.useTime = 10;
      this.Item.useAnimation = 10;
      this.Item.knockBack = 3f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
      this.Item.maxStack = 999;
      this.Item.rare = 1;
      this.Item.shoot = ModContent.ProjectileType<ShurikenProj>();
      this.Item.shootSpeed = 11f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(20).AddIngredient(42, 20).AddIngredient(167, 1).AddTile(16).Register();
    }
  }
}
