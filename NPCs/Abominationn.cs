// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.Abominationn
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Content.Biomes;
using Fargowiltas.Items.Summons.Abom;
using Fargowiltas.Items.Summons.Deviantt;
using Fargowiltas.Items.Tiles;
using Fargowiltas.Items.Vanity;
using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  [AutoloadHead]
  public class Abominationn : ModNPC
  {
    private bool canSayDefeatQuote = true;
    private bool canSayMutantShimmerQuote;
    private int defeatQuoteTimer = 900;
    public const string ShopName = "Shop";

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 25;
      NPCID.Sets.ExtraFramesCount[this.NPC.type] = 9;
      NPCID.Sets.AttackFrameCount[this.NPC.type] = 4;
      NPCID.Sets.DangerDetectRange[this.NPC.type] = 700;
      NPCID.Sets.AttackType[this.NPC.type] = 0;
      NPCID.Sets.AttackTime[this.NPC.type] = 90;
      NPCID.Sets.AttackAverageChance[this.NPC.type] = 30;
      NPCID.Sets.HatOffsetY[this.NPC.type] = 2;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.Velocity = -1f;
      bestiaryDrawModifiers1.Direction = new int?(-1);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      NPCID.Sets.NPCBestiaryDrawOffset.Add(this.Type, bestiaryDrawModifiers2);
      NPCHappiness happiness1 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness1).SetBiomeAffection<SkyBiome>((AffectionLevel) 100);
      NPCHappiness happiness2 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness2).SetBiomeAffection<OceanBiome>((AffectionLevel) 50);
      NPCHappiness happiness3 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness3).SetBiomeAffection<DungeonBiome>((AffectionLevel) -50);
      NPCHappiness happiness4 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness4).SetNPCAffection<Mutant>((AffectionLevel) 100);
      NPCHappiness happiness5 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness5).SetNPCAffection<Fargowiltas.NPCs.Deviantt>((AffectionLevel) 50);
      NPCHappiness happiness6 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness6).SetNPCAffection(18, (AffectionLevel) -100);
      this.NPC.AddDebuffImmunities(new List<int>() { 68 });
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.Fargowiltas.Bestiary.Abominationn")
      });
    }

    public virtual void SetDefaults()
    {
      this.NPC.townNPC = true;
      this.NPC.friendly = true;
      ((Entity) this.NPC).width = 40;
      ((Entity) this.NPC).height = 40;
      this.NPC.aiStyle = 7;
      this.NPC.damage = 10;
      this.NPC.defense = NPC.downedMoonlord ? 50 : 15;
      this.NPC.lifeMax = NPC.downedMoonlord ? 5000 : 250;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.knockBackResist = 0.5f;
      this.AnimationType = 22;
      this.NPC.buffImmune[68] = true;
    }

    public virtual bool CanTownNPCSpawn(int numTownNPCs)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "MutantAlive"
        }))
        {
          if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
          {
            (object) "AbomAlive"
          }))
            goto label_4;
        }
        return false;
      }
label_4:
      return FargoServerConfig.Instance.Abom && NPC.downedGoblins && !FargoGlobalNPC.AnyBossAlive();
    }

    public virtual bool CanGoToStatue(bool toKingStatue) => toKingStatue;

    public virtual void AI()
    {
      this.NPC.breath = 200;
      if (this.defeatQuoteTimer > 0)
        --this.defeatQuoteTimer;
      else
        this.canSayDefeatQuote = false;
      int firstNpc = NPC.FindFirstNPC(ModContent.NPCType<Mutant>());
      if (firstNpc == -1 || Main.npc[firstNpc].IsShimmerVariant)
        return;
      this.canSayMutantShimmerQuote = true;
    }

    public virtual List<string> SetNPCNameList()
    {
      return new List<string>((IEnumerable<string>) new string[12]
      {
        "Wilta",
        "Jack",
        "Harley",
        "Reaper",
        "Stevenn",
        "Doof",
        "Baroo",
        "Fergus",
        "Entev",
        "Catastrophe",
        "Bardo",
        "Betson"
      });
    }

    public virtual string GetChat()
    {
      if (this.NPC.homeless && this.canSayDefeatQuote && Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedAbom"
        }))
        {
          this.canSayDefeatQuote = false;
          return Abominationn.AbomChat("Defeat");
        }
      }
      int firstNpc1 = NPC.FindFirstNPC(ModContent.NPCType<Mutant>());
      if (firstNpc1 != -1 && Main.npc[firstNpc1].IsShimmerVariant && this.canSayMutantShimmerQuote)
      {
        this.canSayMutantShimmerQuote = false;
        return Abominationn.AbomChat("MutantShimmer");
      }
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] && Utils.NextBool(Main.rand, 3))
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "StyxArmor"
        }))
          return Abominationn.AbomChat("StyxArmor");
      }
      List<string> list = ((IEnumerable<LocalizedText>) Language.FindAll(Lang.CreateDialogFilter("Mods.Fargowiltas.NPCs.Abominationn.Chat.Normal"))).Select<LocalizedText, string>((Func<LocalizedText, string>) (item => item.Value)).ToList<string>();
      list.Add(Abominationn.AbomChat("Formattable1", !Main.hardMode ? (object) Abominationn.AbomChat("Formatter1PHM") : (object) Abominationn.AbomChat("Formatter1HM")));
      if (Main.LocalPlayer.ZoneGraveyard)
        list.Add(Abominationn.AbomChat("Graveyard"));
      int firstNpc2 = NPC.FindFirstNPC(124);
      if (firstNpc2 != -1)
        list.Add(Abominationn.AbomChat("Mechanic", (object) Main.npc[firstNpc2].GivenName));
      return Utils.Next<string>(Main.rand, (IList<string>) list);
    }

    public virtual void SetChatButtons(ref string button, ref string button2)
    {
      button = Language.GetTextValue("LegacyInterface.28");
      button2 = Language.GetTextValue("Mods.Fargowiltas.NPCs.Abominationn.CancelEvent");
    }

    public virtual void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
      if (firstButton)
      {
        shopName = "Shop";
      }
      else
      {
        if (Main.netMode == 1)
        {
          ModPacket packet = ((ModType) this).Mod.GetPacket(256);
          ((BinaryWriter) packet).Write((byte) 6);
          packet.Send(-1, -1);
        }
        if (Fargowiltas.Fargowiltas.IsEventOccurring)
        {
          if (Main.netMode == 1)
          {
            ModPacket packet = ((ModType) this).Mod.GetPacket(256);
            ((BinaryWriter) packet).Write((byte) 2);
            packet.Send(-1, -1);
          }
          string str;
          if (!Fargowiltas.Fargowiltas.TryClearEvents())
            str = Abominationn.AbomChat("CancelCD", (object) (FargoWorld.AbomClearCD / 60));
          else
            str = Abominationn.AbomChat("Canceled");
          Main.npcChatText = str;
        }
        else
          Main.npcChatText = Abominationn.AbomChat("NoEvent");
      }
    }

    public virtual void AddShops()
    {
      NPCShop npcShop1 = new NPCShop(this.Type, "Shop");
      Item obj1 = new Item(ModContent.ItemType<PartyInvite>(), 1, 0);
      obj1.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray1 = Array.Empty<Condition>();
      NPCShop npcShop2 = npcShop1.Add(obj1, conditionArray1);
      Item obj2 = new Item(ModContent.ItemType<WeatherBalloon>(), 1, 0);
      obj2.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 20000));
      Condition[] conditionArray2 = Array.Empty<Condition>();
      NPCShop npcShop3 = npcShop2.Add(obj2, conditionArray2);
      Item obj3 = new Item(ModContent.ItemType<Anemometer>(), 1, 0);
      obj3.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 30000));
      Condition[] conditionArray3 = Array.Empty<Condition>();
      NPCShop npcShop4 = npcShop3.Add(obj3, conditionArray3);
      Item obj4 = new Item(ModContent.ItemType<ForbiddenScarab>(), 1, 0);
      obj4.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 30000));
      Condition[] conditionArray4 = Array.Empty<Condition>();
      NPCShop npcShop5 = npcShop4.Add(obj4, conditionArray4);
      Item obj5 = new Item(ModContent.ItemType<SlimyBarometer>(), 1, 0);
      obj5.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 40000));
      Condition[] conditionArray5 = Array.Empty<Condition>();
      NPCShop npcShop6 = npcShop5.Add(obj5, conditionArray5);
      Item obj6 = new Item(4271, 1, 0);
      obj6.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray6 = Array.Empty<Condition>();
      NPCShop npcShop7 = npcShop6.Add(obj6, conditionArray6);
      Item obj7 = new Item(361, 1, 0);
      obj7.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 60000));
      Condition[] conditionArray7 = Array.Empty<Condition>();
      NPCShop npcShop8 = npcShop7.Add(obj7, conditionArray7);
      Item obj8 = new Item(ModContent.ItemType<MatsuriLantern>(), 1, 0);
      obj8.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray8 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.BossDown", (Func<bool>) (() => FargoWorld.DownedBools["boss"]))
      };
      NPCShop npcShop9 = npcShop8.Add(obj8, conditionArray8);
      Item obj9 = new Item(602, 1, 0);
      obj9.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray9 = new Condition[1]
      {
        Condition.Hardmode
      };
      NPCShop npcShop10 = npcShop9.Add(obj9, conditionArray9);
      Item obj10 = new Item(1315, 1, 0);
      obj10.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray10 = new Condition[1]
      {
        Condition.DownedPirates
      };
      NPCShop npcShop11 = npcShop10.Add(obj10, conditionArray10);
      Item obj11 = new Item(ModContent.ItemType<PlunderedBooty>(), 1, 0);
      obj11.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray11 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.DutchmanDown", (Func<bool>) (() => NPC.downedPirates && FargoWorld.DownedBools["flyingDutchman"]))
      };
      NPCShop npcShop12 = npcShop11.Add(obj11, conditionArray11);
      Item obj12 = new Item(2767, 1, 0);
      obj12.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray12 = new Condition[1]
      {
        Condition.DownedMechBossAny
      };
      NPCShop npcShop13 = npcShop12.Add(obj12, conditionArray12);
      Item obj13 = new Item(ModContent.ItemType<ForbiddenTome>(), 1, 0);
      obj13.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray13 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MageDown", (Func<bool>) (() => FargoWorld.DownedBools["darkMage"] || NPC.downedMechBossAny))
      };
      NPCShop npcShop14 = npcShop13.Add(obj13, conditionArray13);
      Item obj14 = new Item(ModContent.ItemType<BatteredClub>(), 1, 0);
      obj14.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray14 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.OgreDown", (Func<bool>) (() => FargoWorld.DownedBools["ogre"] || NPC.downedGolemBoss))
      };
      NPCShop npcShop15 = npcShop14.Add(obj14, conditionArray14);
      Item obj15 = new Item(ModContent.ItemType<BetsyEgg>(), 1, 0);
      obj15.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 400000));
      Condition[] conditionArray15 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.BetsyDown", (Func<bool>) (() => FargoWorld.DownedBools["betsy"]))
      };
      NPCShop npcShop16 = npcShop15.Add(obj15, conditionArray15);
      Item obj16 = new Item(1844, 1, 0);
      obj16.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 500000));
      Condition[] conditionArray16 = new Condition[1]
      {
        Condition.DownedPumpking
      };
      NPCShop npcShop17 = npcShop16.Add(obj16, conditionArray16);
      Item obj17 = new Item(ModContent.ItemType<HeadofMan>(), 1, 0);
      obj17.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray17 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.HorsemanDown", (Func<bool>) (() => FargoWorld.DownedBools["headlessHorseman"]))
      };
      NPCShop npcShop18 = npcShop17.Add(obj17, conditionArray17);
      Item obj18 = new Item(ModContent.ItemType<SpookyBranch>(), 1, 0);
      obj18.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray18 = new Condition[1]
      {
        Condition.DownedMourningWood
      };
      NPCShop npcShop19 = npcShop18.Add(obj18, conditionArray18);
      Item obj19 = new Item(ModContent.ItemType<SuspiciousLookingScythe>(), 1, 0);
      obj19.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray19 = new Condition[1]
      {
        Condition.DownedPumpking
      };
      NPCShop npcShop20 = npcShop19.Add(obj19, conditionArray19);
      Item obj20 = new Item(1958, 1, 0);
      obj20.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 500000));
      Condition[] conditionArray20 = new Condition[1]
      {
        Condition.DownedIceQueen
      };
      NPCShop npcShop21 = npcShop20.Add(obj20, conditionArray20);
      Item obj21 = new Item(ModContent.ItemType<FestiveOrnament>(), 1, 0);
      obj21.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray21 = new Condition[1]
      {
        Condition.DownedEverscream
      };
      NPCShop npcShop22 = npcShop21.Add(obj21, conditionArray21);
      Item obj22 = new Item(ModContent.ItemType<NaughtyList>(), 1, 0);
      obj22.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray22 = new Condition[1]
      {
        Condition.DownedSantaNK1
      };
      NPCShop npcShop23 = npcShop22.Add(obj22, conditionArray22);
      Item obj23 = new Item(ModContent.ItemType<IceKingsRemains>(), 1, 0);
      obj23.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray23 = new Condition[1]
      {
        Condition.DownedIceQueen
      };
      NPCShop npcShop24 = npcShop23.Add(obj23, conditionArray23);
      Item obj24 = new Item(ModContent.ItemType<RunawayProbe>(), 1, 0);
      obj24.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 500000));
      Condition[] conditionArray24 = new Condition[1]
      {
        Condition.DownedGolem
      };
      NPCShop npcShop25 = npcShop24.Add(obj24, conditionArray24);
      Item obj25 = new Item(ModContent.ItemType<MartianMemoryStick>(), 1, 0);
      obj25.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray25 = new Condition[1]
      {
        Condition.DownedMartians
      };
      NPCShop npcShop26 = npcShop25.Add(obj25, conditionArray25);
      Item obj26 = new Item(ModContent.ItemType<PillarSummon>(), 1, 0);
      obj26.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 750000));
      Condition[] conditionArray26 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.PillarsDown", (Func<bool>) (() => NPC.downedTowers))
      };
      NPCShop npcShop27 = npcShop26.Add(obj26, conditionArray26);
      Item obj27 = new Item(ModContent.ItemType<AbominationnScythe>(), 1, 0);
      obj27.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray27 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.PillarsDown", (Func<bool>) (() => NPC.downedTowers))
      };
      ((AbstractNPCShop) npcShop27.Add(obj27, conditionArray27).Add(new Item(ModContent.ItemType<SiblingPylon>(), 1, 0), new Condition[3]
      {
        Condition.HappyEnoughToSellPylons,
        Condition.NpcIsPresent(ModContent.NPCType<Mutant>()),
        Condition.NpcIsPresent(ModContent.NPCType<Fargowiltas.NPCs.Deviantt>())
      })).Register();
    }

    public virtual void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
      damage = NPC.downedMoonlord ? 150 : 20;
      knockback = NPC.downedMoonlord ? 10f : 4f;
    }

    public virtual void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
      cooldown = NPC.downedMoonlord ? 1 : 30;
      if (NPC.downedMoonlord)
        return;
      randExtraCooldown = 30;
    }

    public virtual void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
      projType = NPC.downedMoonlord ? ModContent.ProjectileType<DeathScythe>() : 274;
      attackDelay = 1;
    }

    public virtual void TownNPCAttackProjSpeed(
      ref float multiplier,
      ref float gravityCorrection,
      ref float randomOffset)
    {
      multiplier = 12f;
      randomOffset = 2f;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life <= 0)
      {
        for (int index = 0; index < 8; ++index)
          Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, 2.5f * (float) hit.HitDirection, -2.5f, 0, new Color(), 0.8f);
        if (Main.dedServ)
          return;
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_1, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "AbomGore3").Type, 1f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_2, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "AbomGore2").Type, 1f);
        Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_3, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "AbomGore1").Type, 1f);
      }
      else
      {
        for (int index = 0; (double) index < (double) (((NPC.HitInfo) ref hit).Damage / this.NPC.lifeMax) * 50.0; ++index)
          Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, (float) hit.HitDirection, -1f, 0, new Color(), 0.6f);
      }
    }

    private static string AbomChat(string key, params object[] args)
    {
      return Language.GetTextValue("Mods.Fargowiltas.NPCs.Abominationn.Chat." + key, args);
    }
  }
}
