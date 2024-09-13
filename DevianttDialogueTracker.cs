// Decompiled with JetBrains decompiler
// Type: Fargowiltas.DevianttDialogueTracker
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

#nullable disable
namespace Fargowiltas
{
  internal class DevianttDialogueTracker
  {
    public List<DevianttDialogueTracker.HelpDialogue> PossibleDialogue;
    private int lastDialogueType;

    public DevianttDialogueTracker()
    {
      this.PossibleDialogue = new List<DevianttDialogueTracker.HelpDialogue>();
    }

    public void AddDialogue(
      string messageKey,
      byte type,
      Predicate<string> predicate,
      string keyPath = "Fargowiltas.NPCs.Deviantt.HelpDialogue")
    {
      this.PossibleDialogue.Add(new DevianttDialogueTracker.HelpDialogue(Language.GetText("Mods." + keyPath + "." + messageKey), type, predicate));
    }

    public string GetDialogue(string deviName)
    {
      WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
      (List<DevianttDialogueTracker.HelpDialogue> sortedDialogue, int type) = this.SortDialogue(deviName);
      foreach (DevianttDialogueTracker.HelpDialogue helpDialogue in sortedDialogue)
        weightedRandom.Add(helpDialogue.Message.Value, 1.0);
      this.lastDialogueType = type;
      return WeightedRandom<string>.op_Implicit(weightedRandom);
    }

    private (List<DevianttDialogueTracker.HelpDialogue> sortedDialogue, int type) SortDialogue(
      string deviName)
    {
      List<DevianttDialogueTracker.HelpDialogue> helpDialogueList = new List<DevianttDialogueTracker.HelpDialogue>();
      int typeChoice = 0;
      int num = 0;
      List<DevianttDialogueTracker.HelpDialogue> list;
      do
      {
        ++num;
        typeChoice = Main.rand.Next(3);
        if (typeChoice != this.lastDialogueType || typeChoice == (int) DevianttDialogueTracker.HelpDialogueType.Misc)
        {
          list = this.PossibleDialogue.Where<DevianttDialogueTracker.HelpDialogue>((Func<DevianttDialogueTracker.HelpDialogue, bool>) (dialogue => (int) dialogue.Type == typeChoice && dialogue.CanDisplay(deviName))).ToList<DevianttDialogueTracker.HelpDialogue>();
          if (list.Count != 0)
            goto label_5;
        }
      }
      while (num != 100);
      typeChoice = (int) DevianttDialogueTracker.HelpDialogueType.BossOrEvent;
      list = this.PossibleDialogue.Where<DevianttDialogueTracker.HelpDialogue>((Func<DevianttDialogueTracker.HelpDialogue, bool>) (dialogue => (int) dialogue.Type == typeChoice && dialogue.CanDisplay(deviName))).ToList<DevianttDialogueTracker.HelpDialogue>();
label_5:
      return (list, typeChoice);
    }

    public void AddVanillaDialogue()
    {
      this.AddDialogue("DownedMutant", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => (bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
      {
        (object) "DownedMutant"
      }) ?? (object) false)));
      this.AddDialogue("DownedAbom", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name =>
      {
        if (!(bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedAbom"
        }) ?? (object) false))
          return false;
        return !(bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedMutant"
        }) ?? (object) false);
      }));
      this.AddDialogue("DownedEridanus", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name =>
      {
        if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedEridanus"
        }))
          return false;
        return !(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedAbom"
        });
      }));
      this.AddDialogue("PostML", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name =>
      {
        if (!NPC.downedMoonlord)
          return false;
        return !(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedEridanus"
        });
      }));
      this.AddDialogue("MoonLord", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedAncientCultist && !NPC.downedMoonlord));
      this.AddDialogue("Cultist", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedFishron && !NPC.downedAncientCultist));
      this.AddDialogue("Fishron", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => FargoWorld.DownedBools["betsy"] && !NPC.downedFishron));
      this.AddDialogue("Betsy", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedGolemBoss && !FargoWorld.DownedBools["betsy"]));
      this.AddDialogue("Golem", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedPlantBoss && !NPC.downedGolemBoss));
      this.AddDialogue("Plantera", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.downedPlantBoss));
      this.AddDialogue("Destroyer", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => Main.hardMode && !NPC.downedMechBoss1));
      this.AddDialogue("Twins", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => Main.hardMode && !NPC.downedMechBoss2));
      this.AddDialogue("Prime", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => Main.hardMode && !NPC.downedMechBoss3));
      this.AddDialogue("WOF", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => (bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
      {
        (object) "DownedDevi"
      }) && !Main.hardMode));
      this.AddDialogue("Deviantt", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name =>
      {
        if (!NPC.downedBoss3)
          return false;
        return !(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedDevi"
        });
      }));
      this.AddDialogue("Skeletron", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedQueenBee && !NPC.downedBoss3));
      this.AddDialogue("QueenBee", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedBoss2 && !NPC.downedQueenBee));
      this.AddDialogue("Brain", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedBoss1 && !NPC.downedBoss2 && WorldGen.crimson));
      this.AddDialogue("Eater", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedBoss1 && !NPC.downedBoss2 && !WorldGen.crimson));
      this.AddDialogue("GoblinsCrimson", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => !NPC.downedGoblins && WorldGen.crimson));
      this.AddDialogue("GoblinsCorruption", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => !NPC.downedGoblins && !WorldGen.crimson));
      this.AddDialogue("EOC", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => NPC.downedSlimeKing && !NPC.downedBoss1));
      this.AddDialogue("Slime", DevianttDialogueTracker.HelpDialogueType.BossOrEvent, (Predicate<string>) (name => !NPC.downedSlimeKing));
      this.AddDialogue("Auras", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name => true));
      this.AddDialogue("Debuffs", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name => true));
      this.AddDialogue("SoulToggle", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name => true));
      this.AddDialogue("CactusDamage", DevianttDialogueTracker.HelpDialogueType.Environment, (Predicate<string>) (name => true));
      this.AddDialogue("RainLightning", DevianttDialogueTracker.HelpDialogueType.Environment, (Predicate<string>) (name => true));
      this.AddDialogue("LifeCrystals", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name => Main.LocalPlayer.statLifeMax < 400));
      this.AddDialogue("Fish", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name => !Main.hardMode));
      this.AddDialogue("Underwater", DevianttDialogueTracker.HelpDialogueType.Environment, (Predicate<string>) (name =>
      {
        if (((ExtraJumpState) ref Main.LocalPlayer.GetJumpState<ExtraJump>(ExtraJump.Flipper)).Enabled || Main.LocalPlayer.gills)
          return false;
        return !(bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "MutantAntibodies"
        }) ?? (object) false);
      }));
      this.AddDialogue("Underworld", DevianttDialogueTracker.HelpDialogueType.Environment, (Predicate<string>) (name =>
      {
        if (Main.LocalPlayer.fireWalk || Main.LocalPlayer.lavaMax > 0 || Main.LocalPlayer.buffImmune[24])
          return false;
        return !(bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "PureHeart"
        }) ?? (object) false);
      }));
      this.AddDialogue("SpaceSuffocation", DevianttDialogueTracker.HelpDialogueType.Environment, (Predicate<string>) (name =>
      {
        if (Main.LocalPlayer.buffImmune[68])
          return false;
        return !(bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "PureHeart"
        }) ?? (object) false);
      }));
      this.AddDialogue("UndergroundHallow", DevianttDialogueTracker.HelpDialogueType.Environment, (Predicate<string>) (name =>
      {
        if (!Main.hardMode)
          return false;
        return !(bool) (Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "PureHeart"
        }) ?? (object) false);
      }));
      this.AddDialogue("LifeFruit", DevianttDialogueTracker.HelpDialogueType.Misc, (Predicate<string>) (name => Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.LocalPlayer.statLifeMax2 < 500));
    }

    public static class HelpDialogueType
    {
      public static readonly byte BossOrEvent = 0;
      public static readonly byte Environment = 1;
      public static readonly byte Misc = 2;
    }

    public struct HelpDialogue
    {
      public readonly LocalizedText Message;
      public readonly byte Type;
      public readonly Predicate<string> Predicate;

      public HelpDialogue(LocalizedText message, byte type, Predicate<string> predicate)
      {
        this.Message = message;
        this.Type = type;
        this.Predicate = predicate;
      }

      public bool CanDisplay(string deviName) => this.Predicate(deviName);
    }
  }
}
