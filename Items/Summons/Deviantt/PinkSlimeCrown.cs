// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.PinkSlimeCrown
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class PinkSlimeCrown : ModItem
  {
    public virtual void SetStaticDefaults() => ((ModType) this).SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 4;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
    }

    public virtual bool? UseItem(Player player)
    {
      int index = NPC.NewNPC(NPC.GetBossSpawnSource(((Entity) player).whoAmI), (int) ((Entity) player).position.X + Main.rand.Next(-800, 800), (int) ((Entity) player).position.Y + Main.rand.Next(-800, -250), 1, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      Main.npc[index].SetDefaults(-4, new NPCSpawnParams());
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      LocalizedText text = Language.GetText("Announcement.HasAwoken");
      string npcNameValue = Lang.GetNPCNameValue(-4);
      if (Main.netMode == 2)
        ChatHelper.BroadcastChatMessage(text.ToNetworkText(new object[1]
        {
          (object) npcNameValue
        }), new Color(175, 75, (int) byte.MaxValue), -1);
      else
        Main.NewText((object) text.Format(new object[1]
        {
          (object) npcNameValue
        }), new Color?(new Color(175, 75, (int) byte.MaxValue)));
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(560, 1).AddIngredient(1018, 1).AddTile(228).Register();
    }
  }
}
