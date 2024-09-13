// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.MechEye
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons
{
  public class MechEye : ModItem
  {
    public virtual string Texture => "Terraria/Images/Item_544";

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = 1000;
      this.Item.rare = 3;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = true;
      this.Item.shoot = ModContent.ProjectileType<SpawnProj>();
    }

    public virtual bool CanUseItem(Player player) => !Main.dayTime;

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
      if (!Main.dayTime)
      {
        if (!NPC.downedMechBoss2)
        {
          Main.dayTime = false;
          Main.time = 0.0;
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, 125f, 0.0f, 0.0f);
        Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, 126f, 0.0f, 0.0f);
        if (Main.netMode == 2)
          ChatHelper.BroadcastChatMessage(NetworkText.FromKey("LegacyMisc.48", Array.Empty<object>()), new Color(175, 75, (int) byte.MaxValue), -1);
        else
          Main.NewText((object) Language.GetTextValue("LegacyMisc.48"), new Color?(new Color(175, 75, (int) byte.MaxValue)));
      }
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(544, 1).AddTile(18).Register();
    }
  }
}
