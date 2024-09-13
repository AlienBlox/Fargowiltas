// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.Deviantt
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Content.Biomes;
using Fargowiltas.Items.Summons.Deviantt;
using Fargowiltas.Items.Tiles;
using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  [AutoloadHead]
  public class Deviantt : ModNPC
  {
    private bool canSayDefeatQuote = true;
    private int defeatQuoteTimer = 900;
    private int trolling;
    public const string ShopName = "Shop";

    public virtual ITownNPCProfile TownNPCProfile() => (ITownNPCProfile) new DevianttProfile();

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 23;
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
      ((NPCHappiness) ref happiness2).SetBiomeAffection<JungleBiome>((AffectionLevel) 50);
      NPCHappiness happiness3 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness3).SetBiomeAffection<SnowBiome>((AffectionLevel) -50);
      NPCHappiness happiness4 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness4).SetBiomeAffection<DesertBiome>((AffectionLevel) -100);
      NPCHappiness happiness5 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness5).SetNPCAffection<Mutant>((AffectionLevel) 100);
      NPCHappiness happiness6 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness6).SetNPCAffection<Abominationn>((AffectionLevel) 50);
      NPCHappiness happiness7 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness7).SetNPCAffection(633, (AffectionLevel) -50);
      NPCHappiness happiness8 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness8).SetNPCAffection(369, (AffectionLevel) -100);
      NPCID.Sets.SpecificDebuffImmunity[this.Type][68] = new bool?(true);
      NPCID.Sets.SpecificDebuffImmunity[this.Type][119] = new bool?(true);
      NPCID.Sets.SpecificDebuffImmunity[this.Type][120] = new bool?(true);
      NPCID.Sets.SpecificDebuffImmunity[this.Type][24] = new bool?(true);
      this.NPC.AddDebuffImmunities(new List<int>()
      {
        68,
        119,
        120,
        24
      });
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.Fargowiltas.Bestiary.Deviantt")
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
      this.NPC.lifeMax = NPC.downedMoonlord ? 2500 : 250;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.knockBackResist = 0.5f;
      this.AnimationType = 369;
      this.NPC.buffImmune[68] = true;
    }

    public virtual bool CanTownNPCSpawn(int numTownNPCs)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DevianttAlive"
        }))
          return false;
      }
      if (!FargoServerConfig.Instance.Devi || FargoGlobalNPC.AnyBossAlive())
        return false;
      bool flag;
      if (FargoWorld.DownedBools.TryGetValue("rareEnemy", out flag) & flag)
        return true;
      if (!Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
        return false;
      return (bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
      {
        (object) "EternityMode"
      });
    }

    public virtual bool CanGoToStatue(bool toKingStatue) => !toKingStatue;

    public virtual void AI()
    {
      this.NPC.breath = 200;
      if (this.defeatQuoteTimer > 0)
        --this.defeatQuoteTimer;
      else
        this.canSayDefeatQuote = false;
      if (++this.trolling <= 10800)
        return;
      this.trolling = -Main.rand.Next(1800);
      this.DoALittleTrolling();
    }

    private void DoALittleTrolling()
    {
      if (Main.netMode == 1 || FargoGlobalNPC.AnyBossAlive() || ((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.damage > 0 && !n.friendly && (double) ((Entity) this.NPC).Distance(((Entity) n).Center) < 1200.0)) || this.NPC.life < this.NPC.lifeMax || (double) this.NPC.ai[0] == 10.0)
        return;
      Vector2 targetPos = new Vector2();
      float targetDistance = 600f;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && Main.npc[index].friendly && Main.npc[index].townNPC && Main.npc[index].life == Main.npc[index].lifeMax && index != ((Entity) this.NPC).whoAmI)
          TryUpdateTarget(((Entity) Main.npc[index]).Center);
      }
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (((Entity) Main.player[index]).active && !Main.player[index].dead && !Main.player[index].ghost && Main.player[index].statLife == Main.player[index].statLifeMax2)
          TryUpdateTarget(((Entity) Main.player[index]).Center);
      }
      if (!Vector2.op_Inequality(targetPos, new Vector2()))
        return;
      float num1 = targetDistance / 600f;
      targetPos.Y += 16f;
      targetPos.Y -= 60f * num1 * num1;
      Vector2 vector2 = Vector2.op_Multiply((float) (8.0 + 12.0 * (double) num1), ((Entity) this.NPC).DirectionTo(targetPos));
      int num2 = Utils.NextBool(Main.rand) ? 370 : 371;
      int index1 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, num2, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      Main.projectile[index1].npcProj = true;
      this.NPC.spriteDirection = ((Entity) this.NPC).direction = (double) targetPos.X < (double) ((Entity) this.NPC).Center.X ? -1 : 1;
      this.NPC.ai[0] = 10f;
      this.NPC.ai[1] = (float) (NPCID.Sets.AttackTime[this.NPC.type] - 1);
      this.NPC.localAI[3] = 300f;
      this.NPC.netUpdate = true;

      void TryUpdateTarget(Vector2 possibleTarget)
      {
        if ((double) targetDistance <= (double) ((Entity) this.NPC).Distance(possibleTarget) || !Collision.CanHitLine(((Entity) this.NPC).Center, 0, 0, possibleTarget, 0, 0))
          return;
        Tile tileSafely = Framing.GetTileSafely(Vector2.op_Addition(possibleTarget, Vector2.op_Multiply(32f, Vector2.UnitY)));
        if (!((Tile) ref tileSafely).HasUnactuatedTile || !Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType])
          return;
        targetPos = possibleTarget;
        targetDistance = ((Entity) this.NPC).Distance(possibleTarget);
      }
    }

    public virtual List<string> SetNPCNameList()
    {
      return new List<string>((IEnumerable<string>) new string[13]
      {
        "Akira",
        "Remi",
        "Saku",
        "Seira",
        "Koi",
        "Elly",
        "Lori",
        "Calia",
        "Teri",
        "Artt",
        "Flan",
        "Shion",
        "Tewi"
      });
    }

    public virtual string GetChat()
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "EternityMode"
        }))
        {
          if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
          {
            (object) "GiftsReceived"
          }))
          {
            Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
            {
              (object) "GiveDevianttGifts"
            });
            return Main.npcChatText = Fargowiltas.NPCs.Deviantt.DeviChat("GiveGifts");
          }
        }
      }
      if (Main.notTheBeesWorld)
      {
        string str = Fargowiltas.NPCs.Deviantt.DeviChat("NTB");
        int num = Main.rand.Next(10, 50);
        for (int index = 0; index < num; ++index)
          str += Fargowiltas.NPCs.Deviantt.DeviChat("NTB" + Utils.Next<string>(Main.rand, new string[6]
          {
            "HA",
            "HA",
            "HEE",
            "HOO",
            "HEH",
            "HAH"
          }));
        return str + Language.GetTextValue("Mods.Fargowiltas.MessageInfo.Common.Exclamation");
      }
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] && Utils.NextBool(Main.rand))
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "EridanusArmor"
        }))
          return Fargowiltas.NPCs.Deviantt.DeviChat("EridanusArmor");
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "NekomiArmor"
        }))
          return Fargowiltas.NPCs.Deviantt.DeviChat("NekomiArmor");
      }
      if (this.NPC.homeless && this.canSayDefeatQuote && Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "DownedDevi"
        }))
        {
          this.canSayDefeatQuote = false;
          return Fargowiltas.NPCs.Deviantt.DeviChat("Defeat");
        }
      }
      if (Utils.NextBool(Main.rand))
      {
        if (Main.LocalPlayer.stinky)
          return Fargowiltas.NPCs.Deviantt.DeviChat("Stinky");
        if (Main.LocalPlayer.loveStruck)
          return Fargowiltas.NPCs.Deviantt.DeviChat("LoveStruck", (object) Main.rand.Next(2, 8));
        if (Main.bloodMoon)
          return Fargowiltas.NPCs.Deviantt.DeviChat("BloodMoon");
      }
      List<string> list = ((IEnumerable<LocalizedText>) Language.FindAll(Lang.CreateDialogFilter("Mods.Fargowiltas.NPCs.Deviantt.Chat.Normal"))).Select<LocalizedText, string>((Func<LocalizedText, string>) (item => item.Value)).ToList<string>();
      list.Add(Fargowiltas.NPCs.Deviantt.DeviChat("Formattable1", (object) Main.LocalPlayer.name));
      if (Main.hardMode)
        list.Add(Fargowiltas.NPCs.Deviantt.DeviChat("HM"));
      int firstNpc1 = NPC.FindFirstNPC(ModContent.NPCType<Mutant>());
      if (firstNpc1 != -1)
      {
        list.Add(Fargowiltas.NPCs.Deviantt.DeviChat("Mutant1", (object) Main.npc[firstNpc1].GivenName));
        list.Add(Fargowiltas.NPCs.Deviantt.DeviChat("Mutant2", (object) Main.npc[firstNpc1].GivenName));
      }
      int firstNpc2 = NPC.FindFirstNPC(ModContent.NPCType<LumberJack>());
      if (firstNpc2 != -1)
        list.Add(Fargowiltas.NPCs.Deviantt.DeviChat("Lumber", (object) Main.npc[firstNpc2].GivenName));
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if ((bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "EternityMode"
        }))
          list.Add(Fargowiltas.NPCs.Deviantt.DeviChat("EternityMode"));
      }
      return Utils.Next<string>(Main.rand, (IList<string>) list);
    }

    public virtual void SetChatButtons(ref string button, ref string button2)
    {
      button = Language.GetTextValue("LegacyInterface.28");
      if (!Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
        return;
      if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
      {
        (object) "EternityMode"
      }))
        return;
      button2 = Language.GetTextValue("Mods.Fargowiltas.NPCs.Deviantt.HelpButton");
    }

    public virtual void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
      if (firstButton)
      {
        shopName = "Shop";
      }
      else
      {
        if (!Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
          return;
        if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "EternityMode"
        }))
          return;
        Main.npcChatText = Fargowiltas.Fargowiltas.dialogueTracker.GetDialogue(this.NPC.GivenName);
      }
    }

    public virtual void AddShops() => this.AddVanillaShop();

    public void AddVanillaShop()
    {
      NPCShop npcShop1 = new NPCShop(this.Type, "Shop");
      ModItem modItem;
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSoulsDLC"] && ModContent.TryFind<ModItem>("FargowiltasSoulsDLC", "PandorasBox", ref modItem))
        npcShop1.Add(new Item(modItem.Type, 1, 0), Array.Empty<Condition>());
      NPCShop npcShop2 = npcShop1;
      Item obj1 = new Item(ModContent.ItemType<WormSnack>(), 1, 0);
      obj1.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 20000));
      Condition[] conditionArray1 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.WormDown", (Func<bool>) (() => FargoWorld.DownedBools["worm"]))
      };
      NPCShop npcShop3 = npcShop2.Add(obj1, conditionArray1);
      Item obj2 = new Item(ModContent.ItemType<PinkSlimeCrown>(), 1, 0);
      obj2.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray2 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.PinkyDown", (Func<bool>) (() => FargoWorld.DownedBools["pinky"]))
      };
      NPCShop npcShop4 = npcShop3.Add(obj2, conditionArray2);
      Item obj3 = new Item(ModContent.ItemType<GoblinScrap>(), 1, 0);
      obj3.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray3 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.ScoutDown", (Func<bool>) (() => FargoWorld.DownedBools["goblinScout"]))
      };
      NPCShop npcShop5 = npcShop4.Add(obj3, conditionArray3);
      Item obj4 = new Item(ModContent.ItemType<Eggplant>(), 1, 0);
      obj4.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 20000));
      Condition[] conditionArray4 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.DoctorDown", (Func<bool>) (() => FargoWorld.DownedBools["doctorBones"]))
      };
      NPCShop npcShop6 = npcShop5.Add(obj4, conditionArray4);
      Item obj5 = new Item(ModContent.ItemType<AttractiveOre>(), 1, 0);
      obj5.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 30000));
      Condition[] conditionArray5 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MinerDown", (Func<bool>) (() => FargoWorld.DownedBools["undeadMiner"]))
      };
      NPCShop npcShop7 = npcShop6.Add(obj5, conditionArray5);
      Item obj6 = new Item(ModContent.ItemType<HolyGrail>(), 1, 0);
      obj6.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray6 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.TimDown", (Func<bool>) (() => FargoWorld.DownedBools["tim"]))
      };
      NPCShop npcShop8 = npcShop7.Add(obj6, conditionArray6);
      Item obj7 = new Item(ModContent.ItemType<GnomeHat>(), 1, 0);
      obj7.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray7 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.GnomeDown", (Func<bool>) (() => FargoWorld.DownedBools["gnome"]))
      };
      NPCShop npcShop9 = npcShop8.Add(obj7, conditionArray7);
      Item obj8 = new Item(ModContent.ItemType<GoldenSlimeCrown>(), 1, 0);
      obj8.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 600000));
      Condition[] conditionArray8 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.GoldSlimeDown", (Func<bool>) (() => FargoWorld.DownedBools["goldenSlime"]))
      };
      NPCShop npcShop10 = npcShop9.Add(obj8, conditionArray8);
      Item obj9 = new Item(ModContent.ItemType<SlimyLockBox>(), 1, 0);
      obj9.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray9 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.DungeonSlimeDown", (Func<bool>) (() => NPC.downedBoss3 && FargoWorld.DownedBools["dungeonSlime"]))
      };
      NPCShop npcShop11 = npcShop10.Add(obj9, conditionArray9);
      Item obj10 = new Item(ModContent.ItemType<AthenianIdol>(), 1, 0);
      obj10.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray10 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MedusaDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["medusa"]))
      };
      NPCShop npcShop12 = npcShop11.Add(obj10, conditionArray10);
      Item obj11 = new Item(ModContent.ItemType<ClownLicense>(), 1, 0);
      obj11.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray11 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.ClownDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["clown"]))
      };
      NPCShop npcShop13 = npcShop12.Add(obj11, conditionArray11);
      Item obj12 = new Item(ModContent.ItemType<HeartChocolate>(), 1, 0);
      obj12.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray12 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.NymphDown", (Func<bool>) (() => FargoWorld.DownedBools["nymph"]))
      };
      NPCShop npcShop14 = npcShop13.Add(obj12, conditionArray12);
      Item obj13 = new Item(ModContent.ItemType<MothLamp>(), 1, 0);
      obj13.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray13 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MothDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["moth"]))
      };
      NPCShop npcShop15 = npcShop14.Add(obj13, conditionArray13);
      Item obj14 = new Item(ModContent.ItemType<DilutedRainbowMatter>(), 1, 0);
      obj14.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray14 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.RainbowSlimeDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["rainbowSlime"]))
      };
      NPCShop npcShop16 = npcShop15.Add(obj14, conditionArray14);
      Item obj15 = new Item(ModContent.ItemType<CloudSnack>(), 1, 0);
      obj15.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray15 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.WyvernDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["wyvern"]))
      };
      NPCShop npcShop17 = npcShop16.Add(obj15, conditionArray15);
      Item obj16 = new Item(ModContent.ItemType<RuneOrb>(), 1, 0);
      obj16.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray16 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.RuneDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["runeWizard"]))
      };
      NPCShop npcShop18 = npcShop17.Add(obj16, conditionArray16);
      Item obj17 = new Item(ModContent.ItemType<SuspiciousLookingChest>(), 1, 0);
      obj17.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray17 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MimicDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["mimic"]))
      };
      NPCShop npcShop19 = npcShop18.Add(obj17, conditionArray17);
      Item obj18 = new Item(ModContent.ItemType<HallowChest>(), 1, 0);
      obj18.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray18 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MimicHallowDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["mimicHallow"]))
      };
      NPCShop npcShop20 = npcShop19.Add(obj18, conditionArray18);
      Item obj19 = new Item(ModContent.ItemType<CorruptChest>(), 1, 0);
      obj19.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray19 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MimicCorruptDown", (Func<bool>) (() =>
        {
          if (!Main.hardMode)
            return false;
          return FargoWorld.DownedBools["mimicCorrupt"] || FargoWorld.DownedBools["mimicCrimson"];
        }))
      };
      NPCShop npcShop21 = npcShop20.Add(obj19, conditionArray19);
      Item obj20 = new Item(ModContent.ItemType<CrimsonChest>(), 1, 0);
      obj20.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray20 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MimicCrimsonDown", (Func<bool>) (() =>
        {
          if (!Main.hardMode)
            return false;
          return FargoWorld.DownedBools["mimicCorrupt"] || FargoWorld.DownedBools["mimicCrimson"];
        }))
      };
      NPCShop npcShop22 = npcShop21.Add(obj20, conditionArray20);
      Item obj21 = new Item(ModContent.ItemType<JungleChest>(), 1, 0);
      obj21.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray21 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MimicJungleDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["mimicJungle"]))
      };
      NPCShop npcShop23 = npcShop22.Add(obj21, conditionArray21);
      Item obj22 = new Item(ModContent.ItemType<CoreoftheFrostCore>(), 1, 0);
      obj22.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray22 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.IceGolemDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["iceGolem"]))
      };
      NPCShop npcShop24 = npcShop23.Add(obj22, conditionArray22);
      Item obj23 = new Item(ModContent.ItemType<ForbiddenForbiddenFragment>(), 1, 0);
      obj23.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray23 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.SandDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["sandElemental"]))
      };
      NPCShop npcShop25 = npcShop24.Add(obj23, conditionArray23);
      Item obj24 = new Item(ModContent.ItemType<DemonicPlushie>(), 1, 0);
      obj24.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray24 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.DevilDown", (Func<bool>) (() => NPC.downedMechBossAny && FargoWorld.DownedBools["redDevil"]))
      };
      NPCShop npcShop26 = npcShop25.Add(obj24, conditionArray24);
      Item obj25 = new Item(ModContent.ItemType<SuspiciousLookingLure>(), 1, 0);
      obj25.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray25 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.BloodFishDown", (Func<bool>) (() => FargoWorld.DownedBools["eyeFish"] || FargoWorld.DownedBools["zombieMerman"]))
      };
      NPCShop npcShop27 = npcShop26.Add(obj25, conditionArray25);
      Item obj26 = new Item(ModContent.ItemType<BloodUrchin>(), 1, 0);
      obj26.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray26 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.BloodEelDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["bloodEel"]))
      };
      NPCShop npcShop28 = npcShop27.Add(obj26, conditionArray26);
      Item obj27 = new Item(ModContent.ItemType<HemoclawCrab>(), 1, 0);
      obj27.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray27 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.BloodGoblinDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["goblinShark"]))
      };
      NPCShop npcShop29 = npcShop28.Add(obj27, conditionArray27);
      Item obj28 = new Item(ModContent.ItemType<BloodSushiPlatter>(), 1, 0);
      obj28.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 200000));
      Condition[] conditionArray28 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.BloodNautDown", (Func<bool>) (() => Main.hardMode && FargoWorld.DownedBools["dreadnautilus"]))
      };
      NPCShop npcShop30 = npcShop29.Add(obj28, conditionArray28);
      Item obj29 = new Item(ModContent.ItemType<ShadowflameIcon>(), 1, 0);
      obj29.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray29 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.SummonerDown", (Func<bool>) (() => Main.hardMode && NPC.downedGoblins && FargoWorld.DownedBools["goblinSummoner"]))
      };
      NPCShop npcShop31 = npcShop30.Add(obj29, conditionArray29);
      Item obj30 = new Item(ModContent.ItemType<PirateFlag>(), 1, 0);
      obj30.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray30 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.PirateDown", (Func<bool>) (() => Main.hardMode && NPC.downedPirates && FargoWorld.DownedBools["pirateCaptain"]))
      };
      NPCShop npcShop32 = npcShop31.Add(obj30, conditionArray30);
      Item obj31 = new Item(ModContent.ItemType<Pincushion>(), 1, 0);
      obj31.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray31 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.NailheadDown", (Func<bool>) (() => NPC.downedPlantBoss && FargoWorld.DownedBools["nailhead"]))
      };
      NPCShop npcShop33 = npcShop32.Add(obj31, conditionArray31);
      Item obj32 = new Item(ModContent.ItemType<MothronEgg>(), 1, 0);
      obj32.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray32 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.MothronDown", (Func<bool>) (() => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && FargoWorld.DownedBools["mothron"]))
      };
      NPCShop npcShop34 = npcShop33.Add(obj32, conditionArray32);
      Item obj33 = new Item(ModContent.ItemType<LeesHeadband>(), 1, 0);
      obj33.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray33 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.LeeDown", (Func<bool>) (() => NPC.downedPlantBoss && FargoWorld.DownedBools["boneLee"]))
      };
      NPCShop npcShop35 = npcShop34.Add(obj33, conditionArray33);
      Item obj34 = new Item(ModContent.ItemType<GrandCross>(), 1, 0);
      obj34.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 150000));
      Condition[] conditionArray34 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.PaladinDown", (Func<bool>) (() => NPC.downedPlantBoss && FargoWorld.DownedBools["paladin"]))
      };
      NPCShop npcShop36 = npcShop35.Add(obj34, conditionArray34);
      Item obj35 = new Item(ModContent.ItemType<AmalgamatedSkull>(), 1, 0);
      obj35.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray35 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.SkeleGunDown", (Func<bool>) (() => NPC.downedPlantBoss && FargoWorld.DownedBools["skeletonGun"]))
      };
      NPCShop npcShop37 = npcShop36.Add(obj35, conditionArray35);
      Item obj36 = new Item(ModContent.ItemType<AmalgamatedSpirit>(), 1, 0);
      obj36.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 300000));
      Condition[] conditionArray36 = new Condition[1]
      {
        new Condition("Mods.Fargowiltas.Conditions.SkeleMagesDown", (Func<bool>) (() => NPC.downedPlantBoss && FargoWorld.DownedBools["skeletonMage"]))
      };
      npcShop37.Add(obj36, conditionArray36).Add(new Item(ModContent.ItemType<SiblingPylon>(), 1, 0), new Condition[3]
      {
        Condition.HappyEnoughToSellPylons,
        Condition.NpcIsPresent(ModContent.NPCType<Mutant>()),
        Condition.NpcIsPresent(ModContent.NPCType<Abominationn>())
      });
      ((AbstractNPCShop) npcShop1).Register();
    }

    public virtual void ModifyActiveShop(string shopName, Item[] items)
    {
    }

    public virtual void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
      if (NPC.downedMoonlord)
      {
        damage = 80;
        knockback = 4f;
      }
      else if (Main.hardMode)
      {
        damage = 40;
        knockback = 4f;
      }
      else
      {
        damage = 20;
        knockback = 2f;
      }
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
      projType = NPC.downedMoonlord ? ModContent.ProjectileType<FakeHeartMarkDeviantt>() : ModContent.ProjectileType<FakeHeartDeviantt>();
      attackDelay = 1;
    }

    public virtual void TownNPCAttackProjSpeed(
      ref float multiplier,
      ref float gravityCorrection,
      ref float randomOffset)
    {
      multiplier = 10f;
      randomOffset = 0.0f;
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
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_1, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "DevianttGore3").Type, 1f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_2, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "DevianttGore2").Type, 1f);
        Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_3, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "DevianttGore1").Type, 1f);
      }
      else
      {
        for (int index = 0; (double) index < (double) (((NPC.HitInfo) ref hit).Damage / this.NPC.lifeMax) * 50.0; ++index)
          Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, (float) hit.HitDirection, -1f, 0, new Color(), 0.6f);
      }
    }

    public virtual void OnKill()
    {
      ModNPC modNpc;
      if (!Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] || !ModContent.TryFind<ModNPC>("FargowiltasSouls", "CosmosChampion", ref modNpc) || !NPC.AnyNPCs(modNpc.Type))
        return;
      Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).Hitbox, ModContent.ItemType<WalkingRick>(), 1, false, 0, false, false);
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
      {
        if (!(bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "GiftsReceived"
        }))
        {
          Texture2D texture2D = Asset<Texture2D>.op_Explicit(base.TownNPCProfile().GetTextureNPCShouldUse(this.NPC));
          Rectangle frame = this.NPC.frame;
          Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
          SpriteEffects spriteEffects1 = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
          Color discoColor = Main.DiscoColor;
          ((Color) ref discoColor).A = (byte) 0;
          float num1 = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.5 + 1.0) * this.NPC.scale;
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition), new Vector2(0.0f, this.NPC.gfxOffY - 4f));
          Rectangle? nullable = new Rectangle?(frame);
          Color color = discoColor;
          double rotation = (double) this.NPC.rotation;
          Vector2 vector2_3 = vector2_1;
          double num2 = (double) num1;
          SpriteEffects spriteEffects2 = spriteEffects1;
          Main.EntitySpriteDraw(texture2D, vector2_2, nullable, color, (float) rotation, vector2_3, (float) num2, spriteEffects2, 0.0f);
        }
      }
      return true;
    }

    private static string DeviChat(string key, params object[] args)
    {
      return Language.GetTextValue("Mods.Fargowiltas.NPCs.Deviantt.Chat." + key, args);
    }
  }
}
