// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Explosives.SemiBridgifier
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Tiles;
using Fargowiltas.Projectiles.Explosives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Explosives
{
  public class SemiBridgifier : OmniBridgifier
  {
    public override void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.shoot = ModContent.ProjectileType<SemiBridgifierProj>();
    }

    public override bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      return false;
    }

    public override void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<InstaBridge>(), 1).AddTile(ModContent.TileType<SemistationSheet>()).Register();
    }
  }
}
