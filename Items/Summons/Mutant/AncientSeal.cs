// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Mutant.AncientSeal
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
namespace Fargowiltas.Items.Summons.Mutant
{
  public class AncientSeal : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 8, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      ItemID.Sets.ItemNoGravity[this.Type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = 1000;
      this.Item.rare = 11;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
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
      Projectile.NewProjectile(player.GetSource_ItemUse(((EntitySource_ItemUse) source).Item, (string) null), vector2, Vector2.Zero, ModContent.ProjectileType<SpawnProj>(), 0, 0.0f, Main.myPlayer, 1f, 3f, 0.0f);
      for (int count = (int) NPCID.Count; count < NPCLoader.NPCCount; ++count)
      {
        NPC npc = new NPC();
        npc.SetDefaults(count, new NPCSpawnParams());
        if (npc.boss)
        {
          string name = npc.ModNPC != null ? npc.ModNPC.DisplayName.Value : Lang.GetNPCNameValue(npc.netID);
          AncientSeal.SpawnBoss(player, count, name);
        }
      }
      if (Main.netMode == 2)
        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.AncientSeal", Array.Empty<object>()), new Color(175, 75, (int) byte.MaxValue), -1);
      else
        Main.NewText((object) Language.GetTextValue("Mods.Fargowiltas.MessageInfo.AncientSeal"), new Color?(new Color(175, 75, (int) byte.MaxValue)));
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return false;
    }

    public static int SpawnBoss(Player player, int npcID, string name)
    {
      Main.NewText((object) Language.GetTextValue("Announcement.HasAwoken", (object) name), new Color?(new Color(175, 75, (int) byte.MaxValue)));
      return NPC.NewNPC(NPC.GetBossSpawnSource(((Entity) player).whoAmI), (int) ((Entity) player).position.X + Main.rand.Next(-800, 800), (int) ((Entity) player).position.Y + Main.rand.Next(-1000, -250), npcID, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
    }
  }
}
