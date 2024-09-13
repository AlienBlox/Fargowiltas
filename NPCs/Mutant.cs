// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.Mutant
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Content.Biomes;
using Fargowiltas.Items.Misc;
using Fargowiltas.Items.Summons.Mutant;
using Fargowiltas.Items.Summons.SwarmSummons;
using Fargowiltas.Items.Tiles;
using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  [AutoloadHead]
  public class Mutant : ModNPC
  {
    private static int shopNum = 1;
    internal bool spawned;
    private bool canSayDefeatQuote = true;
    private int defeatQuoteTimer = 900;
    public const string ShopName1 = "Pre Hardmode Shop";
    public const string ShopName2 = "Hardmode Shop";
    public const string ShopName3 = "Post Moon Lord Shop";
    private const int SquirrelFrameCount = 6;
    private int SquirrelFrame;

    public virtual ITownNPCProfile TownNPCProfile() => (ITownNPCProfile) new MutantProfile();

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 25;
      NPCID.Sets.ExtraFramesCount[this.NPC.type] = 9;
      NPCID.Sets.AttackFrameCount[this.NPC.type] = 4;
      NPCID.Sets.DangerDetectRange[this.NPC.type] = 700;
      NPCID.Sets.AttackType[this.NPC.type] = 0;
      NPCID.Sets.AttackTime[this.NPC.type] = 90;
      NPCID.Sets.AttackAverageChance[this.NPC.type] = 30;
      NPCID.Sets.ShimmerTownTransform[this.NPC.type] = true;
      NPCID.Sets.ShimmerTownTransform[this.Type] = true;
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
      ((NPCHappiness) ref happiness2).SetBiomeAffection<ForestBiome>((AffectionLevel) 50);
      NPCHappiness happiness3 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness3).SetBiomeAffection<HallowBiome>((AffectionLevel) -50);
      NPCHappiness happiness4 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness4).SetNPCAffection<Abominationn>((AffectionLevel) 100);
      NPCHappiness happiness5 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness5).SetNPCAffection<Deviantt>((AffectionLevel) 50);
      NPCHappiness happiness6 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness6).SetNPCAffection<LumberJack>((AffectionLevel) -50);
      this.NPC.AddDebuffImmunities(new List<int>() { 68 });
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.Fargowiltas.Bestiary.Mutant")
      });
    }

    public virtual void SetDefaults()
    {
      this.NPC.townNPC = true;
      this.NPC.friendly = true;
      ((Entity) this.NPC).width = 18;
      ((Entity) this.NPC).height = 40;
      this.NPC.aiStyle = 7;
      this.NPC.damage = 10;
      this.NPC.defense = NPC.downedMoonlord ? 50 : 15;
      this.NPC.lifeMax = NPC.downedMoonlord ? 5000 : 250;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.knockBackResist = 0.5f;
      this.AnimationType = 22;
      if (FargoServerConfig.Instance.CatchNPCs)
        Main.npcCatchable[this.NPC.type] = true;
      this.NPC.buffImmune[68] = true;
      if (!Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
        return;
      if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
      {
        (object) "DownedMutant"
      }))
        return;
      this.NPC.lifeMax = 77000;
      this.NPC.defense = 360;
    }

    public virtual bool CanGoToStatue(bool toKingStatue) => true;

    public virtual void AI()
    {
      this.NPC.breath = 200;
      if (this.defeatQuoteTimer > 0)
        --this.defeatQuoteTimer;
      else
        this.canSayDefeatQuote = false;
      if (!this.spawned)
      {
        this.spawned = true;
        if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
        {
          if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
          {
            (object) "DownedMutant"
          }))
          {
            this.NPC.lifeMax = 77000;
            this.NPC.life = this.NPC.lifeMax;
            this.NPC.defense = 360;
          }
        }
      }
      this.AnimationType = this.NPC.IsShimmerVariant ? -1 : 22;
      NPCID.Sets.CannotSitOnFurniture[this.NPC.type] = NPC.ShimmeredTownNPCs[this.NPC.type];
    }

    public virtual bool UsesPartyHat() => !this.NPC.IsShimmerVariant;

    public virtual bool CanTownNPCSpawn(int numTownNPCs)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "MutantAlive"
        }))
          return false;
      }
      return FargoServerConfig.Instance.Mutant && FargoWorld.DownedBools["boss"] && !FargoGlobalNPC.AnyBossAlive();
    }

    public virtual List<string> SetNPCNameList()
    {
      return new List<string>((IEnumerable<string>) new string[13]
      {
        "Flacken",
        "Dorf",
        "Bingo",
        "Hans",
        "Fargo",
        "Grim",
        "Mike",
        "Fargu",
        "Terrance",
        "Catty N. Pem",
        "Tom",
        "Weirdus",
        "Polly"
      });
    }

    public virtual string GetChat()
    {
      if (this.NPC.homeless && this.canSayDefeatQuote && Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedMutant"
        }))
        {
          this.canSayDefeatQuote = false;
          return (bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
          {
            (object) "EternityMode"
          }) ? Fargowiltas.NPCs.Mutant.MutantChat("EternityDefeat") : Fargowiltas.NPCs.Mutant.MutantChat("Defeat");
        }
      }
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] && Utils.NextBool(Main.rand, 4))
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "MutantArmor"
        }))
          return Fargowiltas.NPCs.Mutant.MutantChat("MutantArmor");
      }
      List<string> list = ((IEnumerable<LocalizedText>) Language.FindAll(Lang.CreateDialogFilter("Mods.Fargowiltas.NPCs.Mutant.Chat.Normal"))).Select<LocalizedText, string>((Func<LocalizedText, string>) (item => item.Value)).ToList<string>();
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("SoulsPostML"), NPC.downedMoonlord);
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedMutant"
        }))
        {
          list.Add(Fargowiltas.NPCs.Mutant.MutantChat("DefeatCommon"));
        }
        else
        {
          if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
          {
            (object) "DownedFishronEX"
          }))
          {
            if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
            {
              (object) "DownedAbom"
            }))
              goto label_15;
          }
          list.Add(Fargowiltas.NPCs.Mutant.MutantChat("DefeatAbom"));
        }
      }
      else
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("SoulsModDisabled"));
label_15:
      list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("PumpkinMoon"), Main.pumpkinMoon);
      list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("FrostMoon"), Main.snowMoon);
      list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("SlimeRain"), Main.slimeRain);
      list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("BloodMoon1"), Main.bloodMoon);
      list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("BloodMoon2"), Main.bloodMoon);
      list.AddWithCondition<string>(Fargowiltas.NPCs.Mutant.MutantChat("NightTime"), !Main.dayTime);
      int firstNpc1 = NPC.FindFirstNPC(208);
      if (BirthdayParty.PartyIsUp)
      {
        if (firstNpc1 >= 0)
          list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Party", (object) Main.npc[firstNpc1].GivenName));
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("PartyWithoutPartyGirl"));
      }
      int firstNpc2 = NPC.FindFirstNPC(18);
      if (firstNpc2 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Nurse", (object) Main.npc[firstNpc2].GivenName));
      int firstNpc3 = NPC.FindFirstNPC(228);
      if (firstNpc3 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("WitchDoctor", (object) Main.npc[firstNpc3].GivenName));
      int firstNpc4 = NPC.FindFirstNPC(20);
      if (firstNpc4 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Dryad", (object) Main.npc[firstNpc4].GivenName));
      int firstNpc5 = NPC.FindFirstNPC(353);
      if (firstNpc5 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Stylist", (object) Main.npc[firstNpc5].GivenName));
      int firstNpc6 = NPC.FindFirstNPC(160);
      if (firstNpc6 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Truffle"));
      int firstNpc7 = NPC.FindFirstNPC(441);
      if (firstNpc7 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("TaxCollector", (object) Main.npc[firstNpc7].GivenName));
      int firstNpc8 = NPC.FindFirstNPC(22);
      if (firstNpc8 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Guide", (object) Main.npc[firstNpc8].GivenName));
      int firstNpc9 = NPC.FindFirstNPC(209);
      if (firstNpc6 >= 0 && firstNpc3 >= 0 && firstNpc9 >= 0 && Utils.NextBool(Main.rand, 52))
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("WitchDoctorTruffleCyborg", (object) Main.npc[firstNpc3].GivenName, (object) Main.npc[firstNpc6].GivenName, (object) Main.npc[firstNpc9].GivenName));
      if (firstNpc1 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("PartyGirl", (object) Main.npc[firstNpc1].GivenName));
      int firstNpc10 = NPC.FindFirstNPC(38);
      if (firstNpc10 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Demolitionist", (object) Main.npc[firstNpc10].GivenName));
      int firstNpc11 = NPC.FindFirstNPC(550);
      if (firstNpc11 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("Tavernkeep", (object) Main.npc[firstNpc11].GivenName));
      int firstNpc12 = NPC.FindFirstNPC(207);
      if (firstNpc12 >= 0)
        list.Add(Fargowiltas.NPCs.Mutant.MutantChat("DyeTrader", (object) Main.npc[firstNpc12].GivenName));
      return Utils.Next<string>(Main.rand, (IList<string>) list);
    }

    private bool AnyHardmodeSummon
    {
      get
      {
        return Main.hardMode || Fargowiltas.Fargowiltas.summonTracker.SortedSummons.Any<MutantSummonInfo>((Func<MutantSummonInfo, bool>) (s => (double) s.progression >= 7.0 && s.downed()));
      }
    }

    private bool AnyPostMLSummon
    {
      get
      {
        return NPC.downedMoonlord || Fargowiltas.Fargowiltas.summonTracker.SortedSummons.Any<MutantSummonInfo>((Func<MutantSummonInfo, bool>) (s => (double) s.progression >= 18.0 && s.downed()));
      }
    }

    private static string GetLocalization(string line)
    {
      return Language.GetTextValue("Mods.Fargowiltas.NPCs.Mutant." + line);
    }

    public virtual void SetChatButtons(ref string button, ref string button2)
    {
      if (this.AnyHardmodeSummon)
        button2 = Fargowiltas.NPCs.Mutant.GetLocalization("CycleShop");
      else
        Fargowiltas.NPCs.Mutant.shopNum = 1;
      switch (Fargowiltas.NPCs.Mutant.shopNum)
      {
        case 1:
          button = Fargowiltas.NPCs.Mutant.GetLocalization("PreHM");
          break;
        case 2:
          button = Fargowiltas.NPCs.Mutant.GetLocalization("HM");
          break;
        default:
          button = Fargowiltas.NPCs.Mutant.GetLocalization("PostML");
          break;
      }
      if (this.AnyPostMLSummon)
      {
        if (Fargowiltas.NPCs.Mutant.shopNum < 4)
          return;
        Fargowiltas.NPCs.Mutant.shopNum = 1;
      }
      else
      {
        if (Fargowiltas.NPCs.Mutant.shopNum < 3)
          return;
        Fargowiltas.NPCs.Mutant.shopNum = 1;
      }
    }

    public virtual void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
      if (firstButton)
      {
        switch (Fargowiltas.NPCs.Mutant.shopNum)
        {
          case 1:
            shopName = "Pre Hardmode Shop";
            break;
          case 2:
            shopName = "Hardmode Shop";
            break;
          default:
            shopName = "Post Moon Lord Shop";
            break;
        }
      }
      else
      {
        if (firstButton || !this.AnyHardmodeSummon)
          return;
        ++Fargowiltas.NPCs.Mutant.shopNum;
      }
    }

    public virtual void AddShops()
    {
      NPCShop npcShop1 = new NPCShop(this.Type, "Pre Hardmode Shop");
      Item obj1 = new Item(ModContent.ItemType<Overloader>(), 1, 0);
      obj1.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 400000));
      Condition[] conditionArray1 = new Condition[1]
      {
        Condition.InExpertMode
      };
      NPCShop npcShop2 = npcShop1.Add(obj1, conditionArray1).Add(new Item(ModContent.ItemType<ModeToggle>(), 1, 0), Array.Empty<Condition>());
      ModItem modItem;
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] && ModContent.TryFind<ModItem>("FargowiltasSouls", "Masochist", ref modItem))
      {
        NPCShop npcShop3 = npcShop2;
        Item obj2 = new Item(modItem.Type, 1, 0);
        obj2.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
        Condition[] conditionArray2 = Array.Empty<Condition>();
        npcShop3.Add(obj2, conditionArray2);
      }
      foreach (MutantSummonInfo sortedSummon in Fargowiltas.Fargowiltas.summonTracker.SortedSummons)
      {
        if ((double) sortedSummon.progression <= 7.0)
        {
          NPCShop npcShop4 = npcShop2;
          Item obj3 = new Item(sortedSummon.itemId, 1, 0);
          obj3.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, sortedSummon.price));
          Condition[] conditionArray3 = new Condition[1]
          {
            new Condition("Mods.Fargowiltas.Conditions.DownedTheBoss", sortedSummon.downed)
          };
          npcShop4.Add(obj3, conditionArray3);
        }
      }
      NPCShop npcShop5 = new NPCShop(this.Type, "Hardmode Shop");
      foreach (MutantSummonInfo sortedSummon in Fargowiltas.Fargowiltas.summonTracker.SortedSummons)
      {
        if ((double) sortedSummon.progression > 7.0 && (double) sortedSummon.progression <= 18.0)
        {
          NPCShop npcShop6 = npcShop5;
          Item obj4 = new Item(sortedSummon.itemId, 1, 0);
          obj4.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, sortedSummon.price));
          Condition[] conditionArray4 = new Condition[1]
          {
            new Condition("Mods.Fargowiltas.Conditions.DownedTheBoss", sortedSummon.downed)
          };
          npcShop6.Add(obj4, conditionArray4);
        }
      }
      NPCShop npcShop7 = new NPCShop(this.Type, "Post Moon Lord Shop");
      foreach (MutantSummonInfo sortedSummon in Fargowiltas.Fargowiltas.summonTracker.SortedSummons)
      {
        if ((double) sortedSummon.progression > 18.0)
        {
          NPCShop npcShop8 = npcShop7;
          Item obj5 = new Item(sortedSummon.itemId, 1, 0);
          obj5.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, sortedSummon.price));
          Condition[] conditionArray5 = new Condition[1]
          {
            new Condition("Mods.Fargowiltas.Conditions.DownedTheBoss", sortedSummon.downed)
          };
          npcShop8.Add(obj5, conditionArray5);
        }
      }
      NPCShop npcShop9 = npcShop7;
      Item obj6 = new Item(ModContent.ItemType<AncientSeal>(), 1, 0);
      obj6.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000000));
      Condition[] conditionArray6 = Array.Empty<Condition>();
      npcShop9.Add(obj6, conditionArray6);
      npcShop2.Add(new Item(ModContent.ItemType<SiblingPylon>(), 1, 0), new Condition[3]
      {
        Condition.HappyEnoughToSellPylons,
        Condition.NpcIsPresent(ModContent.NPCType<Abominationn>()),
        Condition.NpcIsPresent(ModContent.NPCType<Deviantt>())
      });
      npcShop5.Add(new Item(ModContent.ItemType<SiblingPylon>(), 1, 0), new Condition[3]
      {
        Condition.HappyEnoughToSellPylons,
        Condition.NpcIsPresent(ModContent.NPCType<Abominationn>()),
        Condition.NpcIsPresent(ModContent.NPCType<Deviantt>())
      });
      npcShop7.Add(new Item(ModContent.ItemType<SiblingPylon>(), 1, 0), new Condition[3]
      {
        Condition.HappyEnoughToSellPylons,
        Condition.NpcIsPresent(ModContent.NPCType<Abominationn>()),
        Condition.NpcIsPresent(ModContent.NPCType<Deviantt>())
      });
      ((AbstractNPCShop) npcShop2).Register();
      ((AbstractNPCShop) npcShop5).Register();
      ((AbstractNPCShop) npcShop7).Register();
    }

    public virtual void ModifyActiveShop(string shopName, Item[] items)
    {
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasSouls", ref mod))
        return;
      string str1 = shopName;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
      interpolatedStringHandler.AppendFormatted(((ModType) this).Mod.Name);
      interpolatedStringHandler.AppendLiteral("/");
      interpolatedStringHandler.AppendFormatted<LocalizedText>(this.DisplayName);
      interpolatedStringHandler.AppendLiteral("/");
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      string str2 = str1.Replace(stringAndClear, "");
      if (!(str2 == "Pre Hardmode Shop") && !(str2 == "Hardmode Shop") && !(str2 == "Post Moon Lord Shop"))
        return;
      foreach (Item obj in items)
      {
        if (obj != null && !obj.IsAir)
        {
          float num = 1f;
          if ((bool) mod.Call(new object[1]
          {
            (object) "MutantDiscountCard"
          }))
            num -= 0.2f;
          if ((bool) mod.Call(new object[1]
          {
            (object) "MutantPact"
          }))
            num -= 0.3f;
          obj.shopCustomPrice = new int?((int) ((double) (obj.shopCustomPrice ?? obj.GetStoreValue()) * (double) num));
        }
      }
    }

    public virtual void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedMutant"
        }))
        {
          damage = 700;
          knockback = 7f;
          return;
        }
      }
      if (NPC.downedMoonlord)
      {
        damage = 250;
        knockback = 6f;
      }
      else if (Main.hardMode)
      {
        damage = 60;
        knockback = 5f;
      }
      else
      {
        damage = 20;
        knockback = 4f;
      }
    }

    public virtual void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
      if (NPC.downedMoonlord)
        cooldown = 1;
      else if (Main.hardMode)
      {
        cooldown = 20;
        randExtraCooldown = 25;
      }
      else
      {
        cooldown = 30;
        randExtraCooldown = 30;
      }
    }

    public virtual void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        ModProjectile modProjectile;
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedMutant"
        }) && ModContent.TryFind<ModProjectile>("FargowiltasSouls", "MutantSpearThrownFriendly", ref modProjectile))
        {
          projType = modProjectile.Type;
          goto label_4;
        }
      }
      projType = !NPC.downedMoonlord ? (!Main.hardMode ? ModContent.ProjectileType<EyeProjectile>() : ModContent.ProjectileType<MechEyeProjectile>()) : ModContent.ProjectileType<PhantasmalEyeProjectile>();
label_4:
      attackDelay = 1;
    }

    public virtual void TownNPCAttackProjSpeed(
      ref float multiplier,
      ref float gravityCorrection,
      ref float randomOffset)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedMutant"
        }))
        {
          multiplier = 25f;
          randomOffset = 0.0f;
          return;
        }
      }
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
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_1, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "MutantGore3").Type, 1f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_2, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "MutantGore2").Type, 1f);
        Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_3, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "MutantGore1").Type, 1f);
      }
      else
      {
        for (int index = 0; (double) index < (double) (((NPC.HitInfo) ref hit).Damage / this.NPC.lifeMax) * 50.0; ++index)
          Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, (float) hit.HitDirection, -1f, 0, new Color(), 0.6f);
      }
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = Asset<Texture2D>.op_Explicit(base.TownNPCProfile().GetTextureNPCShouldUse(this.NPC));
      Rectangle frame = this.NPC.frame;
      if (this.NPC.IsShimmerVariant)
      {
        this.NPC.spriteDirection = ((Entity) this.NPC).direction;
        int num = 56;
        if ((double) ((Entity) this.NPC).velocity.X == 0.0)
        {
          this.SquirrelFrame = 0;
        }
        else
        {
          ++this.NPC.frameCounter;
          if (this.NPC.frameCounter >= 5.0)
          {
            this.NPC.frameCounter = 0.0;
            ++this.SquirrelFrame;
            if (this.SquirrelFrame >= 6)
              this.SquirrelFrame = 1;
          }
        }
        frame.X = 0;
        frame.Y = num * this.SquirrelFrame;
        frame.Width = texture2D.Width;
        frame.Height = num;
      }
      Vector2 vector2 = Vector2.op_Division(Utils.Size(this.NPC.frame), 2f);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY - 4f)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }

    private static string MutantChat(string key, params object[] args)
    {
      return Language.GetTextValue("Mods.Fargowiltas.NPCs.Mutant.Chat." + key, args);
    }
  }
}
