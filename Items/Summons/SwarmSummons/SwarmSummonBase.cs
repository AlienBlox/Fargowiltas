// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.SwarmSummons.SwarmSummonBase
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.SwarmSummons
{
  public abstract class SwarmSummonBase : ModItem
  {
    private int counter;
    private int npcType;
    private readonly int maxSpawn;
    private readonly string spawnMessageKey;
    private readonly string material;

    protected SwarmSummonBase(int npcType, string spawnMessageKey, int maxSpawn, string material)
    {
      this.npcType = npcType;
      this.spawnMessageKey = spawnMessageKey;
      this.maxSpawn = maxSpawn;
      this.material = material;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 100;
      this.Item.value = 10000;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
      if (this.npcType != 113)
        return;
      this.Item.useAnimation = 20;
      this.Item.useTime = 2;
      this.Item.consumable = false;
    }

    public virtual bool? UseItem(Player player)
    {
      Fargowiltas.Fargowiltas.SwarmActive = true;
      Fargowiltas.Fargowiltas.SwarmTotal = 10 * player.inventory[player.selectedItem].stack;
      Fargowiltas.Fargowiltas.SwarmKills = 0;
      Fargowiltas.Fargowiltas.SwarmSpawned = Fargowiltas.Fargowiltas.SwarmTotal >= 100 ? this.maxSpawn : 10;
      if (this.npcType == 35 && Main.dayTime)
        this.npcType = 68;
      else if (this.npcType == 125)
        Fargowiltas.Fargowiltas.SwarmTotal *= 2;
      if (this.npcType == 113)
      {
        FargoGlobalNPC.SpawnWalls(player);
        ++this.counter;
        if (this.counter < 10)
          return new bool?(true);
      }
      else
      {
        for (int index1 = 0; index1 < Fargowiltas.Fargowiltas.SwarmSpawned; ++index1)
        {
          int index2 = NPC.NewNPC(NPC.GetBossSpawnSource(((Entity) player).whoAmI), (int) ((Entity) player).position.X + Main.rand.Next(-1000, 1000), (int) ((Entity) player).position.Y + Main.rand.Next(-1000, -400), this.npcType, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          Main.npc[index2].GetGlobalNPC<FargoGlobalNPC>().SwarmActive = true;
          if (this.npcType == 125)
          {
            int index3 = NPC.NewNPC(NPC.GetBossSpawnSource(((Entity) player).whoAmI), (int) ((Entity) player).position.X + Main.rand.Next(-1000, 1000), (int) ((Entity) player).position.Y + Main.rand.Next(-1000, -400), 126, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            Main.npc[index3].GetGlobalNPC<FargoGlobalNPC>().SwarmActive = true;
          }
          else
          {
            int npcType = this.npcType;
          }
        }
      }
      player.inventory[player.selectedItem].stack = 0;
      switch (Main.netMode)
      {
        case 0:
          Main.NewText(Language.GetTextValue("Mods.Fargowiltas.MessageInfo." + this.spawnMessageKey), (byte) 175, (byte) 75, byte.MaxValue);
          break;
        case 2:
          ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo." + this.spawnMessageKey, Array.Empty<object>()), new Color(175, 75, (int) byte.MaxValue), -1);
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          break;
      }
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, this.material, 1).AddIngredient((Mod) null, "Overloader", 1).AddTile(26).Register();
    }
  }
}
