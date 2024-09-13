// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.SuspiciousLookingLure
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Systems.Recipes;
using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class SuspiciousLookingLure : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual bool CanUseItem(Player player) => !Main.dayTime;

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
      this.Item.shoot = ModContent.ProjectileType<SpawnProj>();
    }

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) ((int) ((Entity) player).position.X + Main.rand.Next(-800, 800)), (float) ((int) ((Entity) player).position.Y + Main.rand.Next(-1000, -250)));
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, 587f, 0.0f, 0.0f);
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) ((int) ((Entity) player).position.X + Main.rand.Next(-800, 800)), (float) ((int) ((Entity) player).position.Y + Main.rand.Next(-1000, -250)));
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, 586f, 0.0f, 0.0f);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4271, 1).AddIngredient(1085, 1).AddRecipeGroup(RecipeGroups.AnyFoodT2, 1).AddTile(16).Register();
    }
  }
}
