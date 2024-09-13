// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.BaseSummon
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
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
  public abstract class BaseSummon : ModItem
  {
    public abstract int NPCType { get; }

    public virtual string NPCName { get; }

    public virtual bool ResetTimeWhenUsed => false;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

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
      if (this.ResetTimeWhenUsed)
      {
        Main.time = 0.0;
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      Vector2 center;
      // ISSUE: explicit constructor call
      ((Vector2) ref center).\u002Ector((float) ((int) ((Entity) player).position.X + Main.rand.Next(-800, 800)), (float) ((int) ((Entity) player).position.Y + Main.rand.Next(-800, -250)));
      if (this.NPCType == 245)
      {
        center = ((Entity) player).Center;
        for (int index = 0; index < 30; ++index)
        {
          center.Y -= 16f;
          if ((double) center.Y <= 0.0 || WorldGen.SolidTile((int) center.X / 16, (int) center.Y / 16, false))
          {
            center.Y += 16f;
            break;
          }
        }
      }
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), center, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, (float) this.NPCType, 0.0f, 0.0f);
      LocalizedText text = Language.GetText("Announcement.HasAwoken");
      string str = this.NPCName ?? (ModContent.GetModNPC(this.NPCType) == null ? Lang.GetNPCNameValue(this.NPCType) : ModContent.GetModNPC(this.NPCType).DisplayName.Value);
      if (Main.netMode == 2)
        ChatHelper.BroadcastChatMessage(text.ToNetworkText(new object[1]
        {
          (object) str
        }), new Color(175, 75, (int) byte.MaxValue), -1);
      else if (this.NPCType != 50)
        Main.NewText((object) text.Format(new object[1]
        {
          (object) str
        }), new Color?(new Color(175, 75, (int) byte.MaxValue)));
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return false;
    }
  }
}
