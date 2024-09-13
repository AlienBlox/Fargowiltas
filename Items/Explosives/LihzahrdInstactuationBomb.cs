// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Explosives.LihzahrdInstactuationBomb
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles.Explosives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Explosives
{
  public class LihzahrdInstactuationBomb : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 10;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 10;
      ((Entity) this.Item).height = 32;
      this.Item.maxStack = 99;
      this.Item.consumable = true;
      this.Item.useStyle = 1;
      this.Item.rare = 8;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
      this.Item.value = Item.buyPrice(0, 0, 3, 0);
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<LihzahrdInstactuationBombProj>();
    }

    private Vector2 NearbyAltar(Player player)
    {
      Vector2 bottom = ((Entity) player).Bottom;
      bottom.Y -= 8f;
      for (int index1 = 0; index1 <= 8; ++index1)
      {
        for (int index2 = -1; index2 <= 1; index2 += 2)
        {
          Vector2 vector2 = bottom;
          vector2.X += (float) (16 * index1 * index2);
          Tile tileSafely = Framing.GetTileSafely(vector2);
          if (((Tile) ref tileSafely).TileType == (ushort) 237 && ((Tile) ref tileSafely).WallType == (ushort) 87 && Collision.CanHitLine(((Entity) player).Center, 0, 0, vector2, 0, 0))
            return vector2;
        }
      }
      return new Vector2();
    }

    public virtual bool CanUseItem(Player player)
    {
      return NPC.downedPlantBoss && Vector2.op_Inequality(this.NearbyAltar(player), new Vector2());
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
      Vector2 vector2 = this.NearbyAltar(player);
      if (Vector2.op_Inequality(vector2, new Vector2()))
        Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, type, 0, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(849, 500).AddIngredient(167, 25).AddIngredient(2766, 10).AddTile(134).Register();
    }
  }
}
