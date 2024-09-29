// Decompiled with JetBrains decompiler
// Type: Fargowiltas.FargoWorld
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Items.Tiles;
using Fargowiltas.NPCs;
using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

#nullable disable
namespace Fargowiltas
{
  public class FargoWorld : ModSystem
  {
    internal static int AbomClearCD;
    internal static int WoodChopped;
    internal static bool OverloadGoblins;
    internal static bool OverloadPirates;
    internal static bool OverloadPumpkinMoon;
    internal static bool OverloadFrostMoon;
    internal static bool OverloadMartians;
    internal static bool OverloadedSlimeRain;
    internal static bool Matsuri;
    internal static bool[] CurrentSpawnRateTile;
    internal static Dictionary<string, bool> DownedBools = new Dictionary<string, bool>();
    private readonly string[] tags = new string[52]
    {
      "lumberjack",
      "betsy",
      "boss",
      "rareEnemy",
      "pinky",
      "undeadMiner",
      "tim",
      "doctorBones",
      "mimic",
      "wyvern",
      "runeWizard",
      "nymph",
      "moth",
      "rainbowSlime",
      "paladin",
      "medusa",
      "clown",
      "iceGolem",
      "sandElemental",
      "mothron",
      "mimicHallow",
      "mimicCorrupt",
      "mimicCrimson",
      "mimicJungle",
      "goblinSummoner",
      "flyingDutchman",
      "dungeonSlime",
      "pirateCaptain",
      "skeletonGun",
      "skeletonMage",
      "boneLee",
      "darkMage",
      "ogre",
      "headlessHorseman",
      "babyGuardian",
      "squirrel",
      "worm",
      "nailhead",
      "zombieMerman",
      "eyeFish",
      "bloodEel",
      "goblinShark",
      "dreadnautilus",
      "gnome",
      "redDevil",
      "goldenSlime",
      "goblinScout",
      "pumpking",
      "mourningWood",
      "iceQueen",
      "santank",
      "everscream"
    };

    public virtual void PreWorldGen()
    {
      this.SetWorldBool(FargoServerConfig.Instance.DrunkWorld, ref Main.drunkWorld);
      this.SetWorldBool(FargoServerConfig.Instance.BeeWorld, ref Main.notTheBeesWorld);
      this.SetWorldBool(FargoServerConfig.Instance.WorthyWorld, ref Main.getGoodWorld);
      this.SetWorldBool(FargoServerConfig.Instance.CelebrationWorld, ref Main.tenthAnniversaryWorld);
      this.SetWorldBool(FargoServerConfig.Instance.ConstantWorld, ref Main.dontStarveWorld);
      foreach (string tag in this.tags)
        FargoWorld.DownedBools[tag] = false;
      FargoWorld.WoodChopped = 0;
    }

    private void SetWorldBool(SeasonSelections toggle, ref bool flag)
    {
      switch (toggle)
      {
        case SeasonSelections.AlwaysOn:
          flag = true;
          break;
        case SeasonSelections.AlwaysOff:
          flag = false;
          break;
      }
    }

    private void ResetFlags()
    {
      FargoWorld.AbomClearCD = 0;
      FargoWorld.OverloadGoblins = false;
      FargoWorld.OverloadPirates = false;
      FargoWorld.OverloadPumpkinMoon = false;
      FargoWorld.OverloadFrostMoon = false;
      FargoWorld.OverloadMartians = false;
      FargoWorld.OverloadedSlimeRain = false;
      FargoWorld.Matsuri = false;
      foreach (string tag in this.tags)
        FargoWorld.DownedBools[tag] = false;
      FargoWorld.CurrentSpawnRateTile = new bool[Main.netMode == 2 ? (int) byte.MaxValue : 1];
    }

    public virtual void OnWorldLoad() => this.ResetFlags();

    public virtual void OnWorldUnload()
    {
      FargoGlobalProjectile.CannotDestroyRectangle.Clear();
      this.ResetFlags();
    }

    public virtual void SaveWorldData(TagCompound tag)
    {
      List<string> list = new List<string>();
      foreach (string tag1 in this.tags)
      {
        bool condition;
        if (FargoWorld.DownedBools.TryGetValue(tag1, out condition) & condition)
          list.AddWithCondition<string>(tag1, condition);
      }
      tag.Add("downed", (object) list);
      tag.Add("matsuri", (object) FargoWorld.Matsuri);
    }

    public virtual void LoadWorldData(TagCompound tag)
    {
      IList<string> list = tag.GetList<string>("downed");
      foreach (string tag1 in this.tags)
        FargoWorld.DownedBools[tag1] = list.Contains(tag1);
      FargoWorld.Matsuri = tag.Get<bool>("matsuri");
    }

    public virtual void NetReceive(BinaryReader reader)
    {
      foreach (string tag in this.tags)
        FargoWorld.DownedBools[tag] = reader.ReadBoolean();
      FargoWorld.AbomClearCD = reader.ReadInt32();
      FargoWorld.WoodChopped = reader.ReadInt32();
      FargoWorld.Matsuri = reader.ReadBoolean();
      Fargowiltas.Fargowiltas.SwarmActive = reader.ReadBoolean();
    }

    public virtual void NetSend(BinaryWriter writer)
    {
      foreach (string tag in this.tags)
        writer.Write(FargoWorld.DownedBools[tag]);
      writer.Write(FargoWorld.AbomClearCD);
      writer.Write(FargoWorld.WoodChopped);
      writer.Write(FargoWorld.Matsuri);
      writer.Write(Fargowiltas.Fargowiltas.SwarmActive);
    }

    public virtual void PostUpdateWorld()
    {
      this.SetWorldBool(FargoServerConfig.Instance.Halloween, ref Main.halloween);
      this.SetWorldBool(FargoServerConfig.Instance.Christmas, ref Main.xMas);
      this.SetWorldBool(FargoServerConfig.Instance.DrunkWorld, ref Main.drunkWorld);
      this.SetWorldBool(FargoServerConfig.Instance.BeeWorld, ref Main.notTheBeesWorld);
      this.SetWorldBool(FargoServerConfig.Instance.WorthyWorld, ref Main.getGoodWorld);
      this.SetWorldBool(FargoServerConfig.Instance.CelebrationWorld, ref Main.tenthAnniversaryWorld);
      this.SetWorldBool(FargoServerConfig.Instance.ConstantWorld, ref Main.dontStarveWorld);
      if (FargoWorld.Matsuri)
        LanternNight.NextNightIsLanternNight = true;
      if (Main.netMode != 1 && Fargowiltas.Fargowiltas.SwarmActive && this.NoBosses() && !NPC.AnyNPCs(13) && !NPC.AnyNPCs(68) && !NPC.AnyNPCs(564))
      {
        Fargowiltas.Fargowiltas.SwarmActive = false;
        FargoGlobalNPC.LastWoFIndex = -1;
        FargoGlobalNPC.WoFDirection = 0;
        if (Main.netMode == 2)
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (FargoWorld.AbomClearCD > 0)
        --FargoWorld.AbomClearCD;
      if (FargoWorld.OverloadGoblins && Main.invasionType != 1)
        FargoWorld.OverloadGoblins = false;
      if (FargoWorld.OverloadPirates && Main.invasionType != 3)
        FargoWorld.OverloadPirates = false;
      if (FargoWorld.OverloadPumpkinMoon && !Main.pumpkinMoon)
        FargoWorld.OverloadPumpkinMoon = false;
      if (FargoWorld.OverloadFrostMoon && !Main.snowMoon)
        FargoWorld.OverloadFrostMoon = false;
      if (FargoWorld.OverloadMartians && Main.invasionType != 4)
        FargoWorld.OverloadMartians = false;
      if (!FargoWorld.OverloadedSlimeRain || Main.slimeRain)
        return;
      FargoWorld.OverloadedSlimeRain = false;
    }

    public virtual void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
    {
      ref bool local = ref FargoWorld.CurrentSpawnRateTile[0];
      bool flag = local;
      local = tileCounts[ModContent.TileType<RegalStatueSheet>()] > 0;
      if (Main.netMode != 1 || local == flag)
        return;
      ModPacket packet = Fargowiltas.Fargowiltas.Instance.GetPacket(256);
      ((BinaryWriter) packet).Write((byte) 1);
      ((BinaryWriter) packet).Write(local);
      packet.Send(-1, -1);
    }

    public virtual void PreUpdateWorld()
    {
      bool flag = false;
      for (int index = 0; index < FargoWorld.CurrentSpawnRateTile.Length; ++index)
      {
        if (FargoWorld.CurrentSpawnRateTile[index])
        {
          Player player = Main.player[index];
          if (((Entity) player).active)
          {
            if (!player.dead)
              flag = true;
          }
          else
            FargoWorld.CurrentSpawnRateTile[index] = false;
        }
      }
      if (!flag)
        return;
      Main.checkForSpawns += 81;
    }

    private bool NoBosses()
    {
      return ((IEnumerable<NPC>) Main.npc).All<NPC>((Func<NPC, bool>) (i => !((Entity) i).active || !i.boss));
    }

    public virtual void UpdateUI(GameTime gameTime)
    {
      base.UpdateUI(gameTime);
      Fargowiltas.Fargowiltas.UserInterfaceManager.UpdateUI(gameTime);
    }

    public virtual void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
      base.ModifyInterfaceLayers(layers);
      Fargowiltas.Fargowiltas.UserInterfaceManager.ModifyInterfaceLayers(layers);
    }

    public virtual void AddRecipes() => Fargowiltas.Fargowiltas.summonTracker.FinalizeSummonData();
  }
}
