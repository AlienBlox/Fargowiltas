// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Explosives.OmniBridgifier
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Tiles;
using Fargowiltas.Projectiles.Explosives;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Explosives
{
  public class OmniBridgifier : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 10;
      ((Entity) this.Item).height = 32;
      this.Item.maxStack = 1;
      this.Item.consumable = false;
      this.Item.useStyle = 1;
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
      this.Item.value = Item.buyPrice(0, 0, 3, 0);
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<OmniBridgifierProj>();
      this.Item.shootSpeed = 5f;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      position = ((Entity) player).Bottom;
      position.Y += 8f;
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
      float num = -1f;
      if (((IEnumerable<Item>) player.inventory).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.type == ModContent.ItemType<Omnistation>())))
        num = 0.0f;
      if (((IEnumerable<Item>) player.inventory).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.type == ModContent.ItemType<Omnistation2>())))
        num = (double) num == 0.0 ? (float) Main.rand.Next(2) : 1f;
      if ((double) num == -1.0)
        num = (float) Main.rand.Next(2);
      Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, num, 0.0f, 0.0f);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<InstaBridge>(), 1).AddTile(ModContent.TileType<OmnistationSheet>()).Register();
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<InstaBridge>(), 1).AddTile(ModContent.TileType<OmnistationSheet2>()).Register();
    }
  }
}
