// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.LumberJack
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Items.Tiles;
using Fargowiltas.Items.Vanity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  [AutoloadHead]
  public class LumberJack : ModNPC
  {
    private bool dayOver;
    private bool nightOver;
    public const string ShopName = "Shop";

    public virtual ITownNPCProfile TownNPCProfile() => (ITownNPCProfile) new LumberJackProfile();

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
      ((NPCHappiness) ref happiness1).SetBiomeAffection<ForestBiome>((AffectionLevel) 100);
      NPCHappiness happiness2 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness2).SetNPCAffection<Squirrel>((AffectionLevel) 50);
      NPCHappiness happiness3 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness3).SetNPCAffection(20, (AffectionLevel) -50);
      NPCHappiness happiness4 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness4).SetNPCAffection(38, (AffectionLevel) -100);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.Fargowiltas.Bestiary.LumberJack")
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
      this.NPC.defense = 15;
      this.NPC.lifeMax = 250;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.knockBackResist = 0.5f;
      this.AnimationType = 22;
    }

    public virtual bool CanTownNPCSpawn(int numTownNPCs)
    {
      bool flag;
      return ((!FargoServerConfig.Instance.Lumber ? 0 : (FargoWorld.DownedBools.TryGetValue("lumberjack", out flag) ? 1 : 0)) & (flag ? 1 : 0)) != 0;
    }

    public static void OnTreeShake(On_WorldGen.orig_ShakeTree orig, int i, int j)
    {
      orig.Invoke(i, j);
      int num1;
      int num2;
      WorldGen.GetTreeBottom(i, j, ref num1, ref num2);
      Tile tile1 = ((Tilemap) ref Main.tile)[num1, num2];
      if (WorldGen.GetTreeType((int) ((Tile) ref tile1).TileType) == null)
        return;
      int num3;
      for (num3 = num2 - 1; num3 > 10; --num3)
      {
        Tile tile2 = ((Tilemap) ref Main.tile)[num1, num3];
        if (((Tile) ref tile2).HasTile)
        {
          bool[] isShakeable = TileID.Sets.IsShakeable;
          tile2 = ((Tilemap) ref Main.tile)[num1, num3];
          int index = (int) ((Tile) ref tile2).TileType;
          if (!isShakeable[index])
            break;
        }
        else
          break;
      }
      int num4 = num3 + 1;
      bool flag;
      if (!WorldGen.IsTileALeafyTreeTop(num1, num4) || Collision.SolidTiles(num1 - 2, num1 + 2, num4 - 2, num4 + 2) || !Utils.NextBool(WorldGen.genRand, 10) || FargoWorld.WoodChopped < 250 || FargoWorld.DownedBools.TryGetValue("lumberjack", out flag) & flag)
        return;
      FargoWorld.DownedBools["lumberjack"] = true;
      NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), num1 * 16, num4 * 16, ModContent.NPCType<LumberJack>(), 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
    }

    public virtual void Load()
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_WorldGen.ShakeTree += LumberJack.\u003C\u003EO.\u003C0\u003E__OnTreeShake ?? (LumberJack.\u003C\u003EO.\u003C0\u003E__OnTreeShake = new On_WorldGen.hook_ShakeTree((object) null, __methodptr(OnTreeShake)));
    }

    public virtual bool CanGoToStatue(bool toKingStatue) => toKingStatue;

    public virtual void AI()
    {
      if (!Main.dayTime)
        this.nightOver = true;
      if (!Main.dayTime)
        return;
      this.dayOver = true;
    }

    public virtual List<string> SetNPCNameList()
    {
      return new List<string>((IEnumerable<string>) new string[12]
      {
        "Griff",
        "Jack",
        "Bruce",
        "Larry",
        "Will",
        "Jerry",
        "Liam",
        "Stan",
        "Lee",
        "Woody",
        "Leif",
        "Paul"
      });
    }

    public virtual string GetChat()
    {
      List<string> list = ((IEnumerable<LocalizedText>) Language.FindAll(Lang.CreateDialogFilter("Mods.Fargowiltas.NPCs.LumberJack.Chat.Normal"))).Select<LocalizedText, string>((Func<LocalizedText, string>) (item => item.Value)).ToList<string>();
      int firstNpc = NPC.FindFirstNPC(18);
      if (firstNpc >= 0)
        list.Add(LumberJack.LumberChat("Nurse", (object) Main.npc[firstNpc].GivenName));
      if (Main.LocalPlayer.HeldItem.type == 5095)
        list.Add(LumberJack.LumberChat("LucyTheAxe"));
      return Utils.Next<string>(Main.rand, (IList<string>) list);
    }

    public virtual void SetChatButtons(ref string button, ref string button2)
    {
      button = Language.GetTextValue("LegacyInterface.28");
      button2 = Language.GetTextValue("Mods.Fargowiltas.NPCs.LumberJack.TreeTreasures");
    }

    public virtual void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
      Player localPlayer = Main.LocalPlayer;
      if (firstButton)
        shopName = "Shop";
      else if (this.dayOver && this.nightOver)
      {
        string str;
        if (localPlayer.ZoneDesert && !localPlayer.ZoneBeach)
        {
          str = LumberJack.LumberChat("Desert");
          int num = Utils.Next<int>(Main.rand, new int[2]
          {
            2157,
            2156
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(276, (string) null), 276, 100);
        }
        else if (localPlayer.ZoneJungle)
        {
          str = LumberJack.LumberChat("Jungle");
          int num1 = Utils.Next<int>(Main.rand, new int[4]
          {
            3194,
            3193,
            3192,
            2121
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num1, (string) null), num1, 5);
          int num2 = Utils.Next<int>(Main.rand, new int[2]
          {
            4292,
            4294
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num2, (string) null), num2, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(620, (string) null), 620, 50);
        }
        else if (localPlayer.ZoneHallow)
        {
          str = LumberJack.LumberChat("Hallow");
          for (int index = 0; index < 5; ++index)
          {
            int num = Utils.Next<int>(Main.rand, new int[4]
            {
              2004,
              4070,
              4069,
              4068
            });
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 1);
          }
          int num3 = Utils.Next<int>(Main.rand, new int[2]
          {
            4297,
            4288
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num3, (string) null), num3, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(621, (string) null), 621, 50);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(4961, (string) null), 4961, 1);
        }
        else if (localPlayer.ZoneGlowshroom && Main.hardMode)
        {
          str = LumberJack.LumberChat("GlowshroomHM");
          int num = Utils.Next<int>(Main.rand, new int[2]
          {
            2007,
            2673
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(183, (string) null), 183, 50);
        }
        else if (localPlayer.ZoneCorrupt || localPlayer.ZoneCrimson)
        {
          str = LumberJack.LumberChat("Evil");
          for (int index = 0; index < 5; ++index)
          {
            int num = Utils.Next<int>(Main.rand, new int[4]
            {
              4289,
              4284,
              4285,
              4296
            });
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 1);
          }
        }
        else if (localPlayer.ZoneSnow)
        {
          str = LumberJack.LumberChat("Snow");
          int num = Utils.Next<int>(Main.rand, new int[2]
          {
            4286,
            4295
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(2503, (string) null), 2503, 50);
        }
        else if (localPlayer.ZoneBeach)
        {
          str = LumberJack.LumberChat("Beach");
          int num = Utils.Next<int>(Main.rand, new int[2]
          {
            4287,
            4283
          });
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(4359, (string) null), 4359, 5);
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(2504, (string) null), 2504, 50);
        }
        else if (localPlayer.ZoneUnderworldHeight)
        {
          str = LumberJack.LumberChat("Underworld");
          for (int index = 0; index < 5; ++index)
          {
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(5215, (string) null), 5215, 50);
            int num4 = Utils.Next<int>(Main.rand, new int[3]
            {
              4845,
              4849,
              4847
            });
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num4, (string) null), num4, 1);
            int num5 = Utils.Next<int>(Main.rand, new int[2]
            {
              5277,
              5278
            });
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num5, (string) null), num5, 1);
          }
        }
        else if (localPlayer.ZoneRockLayerHeight || localPlayer.ZoneDirtLayerHeight)
        {
          if (Utils.NextBool(Main.rand, 2))
          {
            str = LumberJack.LumberChat("DirtRockGem");
            for (int index = 0; index < 5; ++index)
            {
              int num6 = Utils.Next<int>(Main.rand, new int[7]
              {
                182,
                178,
                181,
                179,
                177,
                180,
                999
              });
              localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num6, (string) null), num6, 3);
              int num7 = Utils.Next<int>(Main.rand, new int[14]
              {
                4836,
                4837,
                4831,
                4834,
                4835,
                4833,
                4832,
                4844,
                4838,
                4843,
                4841,
                4842,
                4840,
                4839
              });
              localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num7, (string) null), num7, 1);
            }
          }
          else
          {
            str = LumberJack.LumberChat("DirtRockMouse");
            int num = 2003;
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 5);
          }
        }
        else
        {
          if (Main.dayTime)
          {
            if (Main.WindyEnoughForKiteDrops && Utils.NextBool(Main.rand, 2))
            {
              str = LumberJack.LumberChat("CommonDayTimeWindy");
              int num = 4361;
              localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 5);
            }
            else if (Utils.NextBool(Main.rand, 3))
            {
              str = LumberJack.LumberChat("CommonDayTimeButterfly");
              for (int index = 0; index < 5; ++index)
              {
                int num = Utils.Next<int>(Main.rand, new int[8]
                {
                  2001,
                  1994,
                  1995,
                  1996,
                  1998,
                  1999,
                  1997,
                  2000
                });
                localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 1);
              }
            }
            else if (Utils.NextBool(Main.rand, 20))
            {
              str = LumberJack.LumberChat("CommonDayTimeEucaluptusSap");
              localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(4366, (string) null), 4366, 1);
            }
            else
            {
              str = LumberJack.LumberChat("CommonDayTimeCritter");
              for (int index = 0; index < 5; ++index)
              {
                int num = Utils.Next<int>(Main.rand, new int[6]
                {
                  2740,
                  2018,
                  3563,
                  2015,
                  2016,
                  2017
                });
                localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 1);
              }
            }
          }
          else
          {
            str = LumberJack.LumberChat("CommonNightTime");
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(1992, (string) null), 1992, 1);
          }
          for (int index = 0; index < 5; ++index)
          {
            int num = Utils.Next<int>(Main.rand, new int[5]
            {
              4291,
              4293,
              4282,
              4290,
              4009
            });
            localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(num, (string) null), num, 1);
          }
          localPlayer.QuickSpawnItem(localPlayer.GetSource_OpenItem(9, (string) null), 9, 50);
        }
        Main.npcChatText = str;
        this.dayOver = false;
        this.nightOver = false;
      }
      else
        Main.npcChatText = LumberJack.LumberChat("Rest");
    }

    public virtual void AddShops()
    {
      NPCShop npcShop1 = new NPCShop(this.Type, "Shop");
      Item obj1 = new Item(94, 1, 0);
      obj1.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 5));
      Condition[] conditionArray1 = Array.Empty<Condition>();
      NPCShop npcShop2 = npcShop1.Add(obj1, conditionArray1);
      Item obj2 = new Item(9, 1, 0);
      obj2.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
      Condition[] conditionArray2 = Array.Empty<Condition>();
      NPCShop npcShop3 = npcShop2.Add(obj2, conditionArray2);
      Item obj3 = new Item(2503, 1, 0);
      obj3.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
      Condition[] conditionArray3 = Array.Empty<Condition>();
      NPCShop npcShop4 = npcShop3.Add(obj3, conditionArray3);
      Item obj4 = new Item(620, 1, 0);
      obj4.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 15));
      Condition[] conditionArray4 = Array.Empty<Condition>();
      NPCShop npcShop5 = npcShop4.Add(obj4, conditionArray4);
      Item obj5 = new Item(2504, 1, 0);
      obj5.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 15));
      Condition[] conditionArray5 = Array.Empty<Condition>();
      NPCShop npcShop6 = npcShop5.Add(obj5, conditionArray5);
      Item obj6 = new Item(619, 1, 0);
      obj6.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 15));
      Condition[] conditionArray6 = Array.Empty<Condition>();
      NPCShop npcShop7 = npcShop6.Add(obj6, conditionArray6);
      Item obj7 = new Item(911, 1, 0);
      obj7.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 15));
      Condition[] conditionArray7 = Array.Empty<Condition>();
      NPCShop npcShop8 = npcShop7.Add(obj7, conditionArray7);
      Item obj8 = new Item(5215, 1, 0);
      obj8.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 20));
      Condition[] conditionArray8 = Array.Empty<Condition>();
      NPCShop npcShop9 = npcShop8.Add(obj8, conditionArray8);
      Item obj9 = new Item(621, 1, 0);
      obj9.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 20));
      Condition[] conditionArray9 = new Condition[1]
      {
        Condition.Hardmode
      };
      NPCShop npcShop10 = npcShop9.Add(obj9, conditionArray9);
      Item obj10 = new Item(1729, 1, 0);
      obj10.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50));
      Condition[] conditionArray10 = new Condition[1]
      {
        Condition.DownedPumpking
      };
      NPCShop npcShop11 = npcShop10.Add(obj10, conditionArray10);
      Item obj11 = new Item(276, 1, 0);
      obj11.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
      Condition[] conditionArray11 = Array.Empty<Condition>();
      NPCShop npcShop12 = npcShop11.Add(obj11, conditionArray11);
      Item obj12 = new Item(4564, 1, 0);
      obj12.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10));
      Condition[] conditionArray12 = Array.Empty<Condition>();
      NPCShop npcShop13 = npcShop12.Add(obj12, conditionArray12);
      Item obj13 = new Item(832, 1, 0);
      obj13.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray13 = Array.Empty<Condition>();
      NPCShop npcShop14 = npcShop13.Add(obj13, conditionArray13);
      Item obj14 = new Item(ModContent.ItemType<LumberjackMask>(), 1, 0);
      obj14.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray14 = Array.Empty<Condition>();
      NPCShop npcShop15 = npcShop14.Add(obj14, conditionArray14);
      Item obj15 = new Item(ModContent.ItemType<LumberjackBody>(), 1, 0);
      obj15.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray15 = Array.Empty<Condition>();
      NPCShop npcShop16 = npcShop15.Add(obj15, conditionArray15);
      Item obj16 = new Item(ModContent.ItemType<LumberjackPants>(), 1, 0);
      obj16.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray16 = Array.Empty<Condition>();
      NPCShop npcShop17 = npcShop16.Add(obj16, conditionArray16);
      Item obj17 = new Item(ModContent.ItemType<Fargowiltas.Items.Weapons.LumberJaxe>(), 1, 0);
      obj17.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray17 = Array.Empty<Condition>();
      NPCShop npcShop18 = npcShop17.Add(obj17, conditionArray17);
      Item obj18 = new Item(3198, 1, 0);
      obj18.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000));
      Condition[] conditionArray18 = Array.Empty<Condition>();
      NPCShop npcShop19 = npcShop18.Add(obj18, conditionArray18);
      Item obj19 = new Item(ModContent.ItemType<WoodenToken>(), 1, 0);
      obj19.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 10000));
      Condition[] conditionArray19 = Array.Empty<Condition>();
      ((AbstractNPCShop) npcShop19.Add(obj19, conditionArray19)).Register();
    }

    public virtual void ModifyActiveShop(string shopName, Item[] items)
    {
    }

    public virtual void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
      damage = 20;
      knockback = 4f;
    }

    public virtual void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
      cooldown = 30;
      randExtraCooldown = 30;
    }

    public virtual void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
      projType = ModContent.ProjectileType<Fargowiltas.Projectiles.LumberJaxe>();
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

    public virtual void OnKill() => FargoWorld.DownedBools["lumberjack"] = true;

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<LumberHat>(), 3, 1, 1));
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
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_1, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "LumberGore3").Type, 1f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_2, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "LumberGore2").Type, 1f);
        Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width - 8), (float) Main.rand.Next(((Entity) this.NPC).height / 2)));
        Gore.NewGore(((Entity) this.NPC).GetSource_Death((string) null), vector2_3, ((Entity) this.NPC).velocity, ModContent.Find<ModGore>("Fargowiltas", "LumberGore1").Type, 1f);
      }
      else
      {
        for (int index = 0; (double) index < (double) (((NPC.HitInfo) ref hit).Damage / this.NPC.lifeMax) * 50.0; ++index)
          Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 5, (float) hit.HitDirection, -1f, 0, new Color(), 0.6f);
      }
    }

    private static string LumberChat(string key, params object[] args)
    {
      return Language.GetTextValue("Mods.Fargowiltas.NPCs.LumberJack.Chat." + key, args);
    }
  }
}
