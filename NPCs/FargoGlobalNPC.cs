// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.FargoGlobalNPC
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Content.Buffs;
using Fargowiltas.Items.Ammos;
using Fargowiltas.Items.Explosives;
using Fargowiltas.Items.Summons.Deviantt;
using Fargowiltas.Items.Summons.SwarmSummons.Energizers;
using Fargowiltas.Items.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.NPCs
{
  public class FargoGlobalNPC : GlobalNPC
  {
    internal static int[] Bosses = new int[29]
    {
      50,
      4,
      266,
      222,
      35,
      657,
      134,
      (int) sbyte.MaxValue,
      125,
      126,
      262,
      245,
      370,
      636,
      439,
      398,
      395,
      327,
      345,
      551,
      577,
      243,
      541,
      290,
      344,
      325,
      346,
      315,
      491
    };
    public static int LastWoFIndex = -1;
    public static int WoFDirection = 0;
    internal bool PillarSpawn = true;
    internal bool SwarmActive;
    internal bool PandoraActive;
    internal bool NoLoot;
    public static int eaterBoss = -1;
    public static int brainBoss = -1;
    public static int plantBoss = -1;
    public static int beeBoss = -1;
    public static int boss = -1;

    public virtual bool InstancePerEntity => true;

    public virtual bool CanHitNPC(NPC npc, NPC target)
    {
      return (!target.dontTakeDamage || target.type != ModContent.NPCType<Squirrel>()) && (!target.friendly || !FargoServerConfig.Instance.SaferBoundNPCs || target.type != 105 && target.type != 123 && target.type != 106 && target.type != 579 && target.type != 589) && base.CanHitNPC(npc, target);
    }

    public virtual void SetDefaults(NPC npc)
    {
      FargoServerConfig instance = FargoServerConfig.Instance;
      if ((double) instance.EnemyHealth == 1.0 && (double) instance.BossHealth == 1.0 || npc.townNPC || npc.CountsAsACritter || npc.life <= 10)
        return;
      if (((double) instance.BossHealth <= (double) instance.EnemyHealth ? 0 : (npc.boss || npc.type == 13 || npc.type == 14 || npc.type == 15 ? 1 : (!instance.BossApplyToAllWhenAlive ? 0 : (FargoGlobalNPC.AnyBossAlive() ? 1 : 0)))) != 0)
        npc.lifeMax = (int) Math.Round((double) npc.lifeMax * (double) instance.BossHealth);
      else
        npc.lifeMax = (int) Math.Round((double) npc.lifeMax * (double) instance.EnemyHealth);
    }

    public virtual bool PreAI(NPC npc)
    {
      if (npc.boss)
        FargoGlobalNPC.boss = ((Entity) npc).whoAmI;
      Point point;
      if (npc.townNPC && npc.homeTileX == -1 && npc.homeTileY == -1 && WorldGen.TownManager.HasRoom(npc.type, ref point) && point.X > 0 && point.Y > 0)
        WorldGen.moveRoom(point.X, point.Y - 2, ((Entity) npc).whoAmI);
      switch (npc.type)
      {
        case 13:
          FargoGlobalNPC.eaterBoss = ((Entity) npc).whoAmI;
          break;
        case 222:
          FargoGlobalNPC.beeBoss = ((Entity) npc).whoAmI;
          break;
        case 262:
          FargoGlobalNPC.plantBoss = ((Entity) npc).whoAmI;
          break;
        case 266:
          FargoGlobalNPC.brainBoss = ((Entity) npc).whoAmI;
          break;
        case 398:
          if ((double) npc.ai[0] == 2.0)
          {
            int num = 540;
            if ((double) npc.ai[1] < (double) num && (double) npc.ai[1] % 60.0 == 30.0 && NPC.CountNPCS(npc.type) > 1)
            {
              npc.ai[1] = (float) num;
              npc.netUpdate = true;
              break;
            }
            break;
          }
          break;
        case 439:
          if ((double) npc.ai[0] == -1.0 && (double) npc.ai[1] == 1.0 && !((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == 437 && (double) ((Entity) npc).Distance(((Entity) n).Center) < 400.0)))
          {
            npc.ai[1] = 360f;
            npc.netUpdate = true;
            break;
          }
          break;
      }
      return true;
    }

    public virtual void AI(NPC npc)
    {
      if (!FargoWorld.OverloadMartians || npc.type != 395 || !npc.dontTakeDamage)
        return;
      npc.dontTakeDamage = false;
    }

    public virtual void PostAI(NPC npc)
    {
      if (!this.SwarmActive || npc.type != 245)
        return;
      npc.dontTakeDamage = false;
    }

    public virtual void ModifyShop(NPCShop shop)
    {
      Condition condition1 = new Condition("Mods.Fargowiltas.Conditions.Angler5", (Func<bool>) (() => Main.LocalPlayer.anglerQuestsFinished >= 5));
      Condition condition2 = new Condition("Mods.Fargowiltas.Conditions.Angler10", (Func<bool>) (() => Main.LocalPlayer.anglerQuestsFinished >= 10));
      Condition condition3 = new Condition("Mods.Fargowiltas.Conditions.Angler15", (Func<bool>) (() => Main.LocalPlayer.anglerQuestsFinished >= 15));
      Condition condition4 = new Condition("Mods.Fargowiltas.Conditions.Angler20", (Func<bool>) (() => Main.LocalPlayer.anglerQuestsFinished >= 20));
      Condition condition5 = new Condition("Mods.Fargowiltas.Conditions.Angler25", (Func<bool>) (() => Main.LocalPlayer.anglerQuestsFinished >= 25));
      Condition condition6 = new Condition("Mods.Fargowiltas.Conditions.Angler30", (Func<bool>) (() => Main.LocalPlayer.anglerQuestsFinished >= 30));
      Condition condition7 = new Condition("Mods.Fargowiltas.Conditions.InRockOrDirtLayerHeight", (Func<bool>) (() => Condition.InDirtLayerHeight.IsMet() || Condition.InRockLayerHeight.IsMet()));
      if (!FargoServerConfig.Instance.NPCSales)
        return;
      switch (((AbstractNPCShop) shop).NpcType)
      {
        case 17:
          AddItem(2428, condition: condition1);
          AddItem(2374, condition: condition2);
          AddItem(2373, condition: condition2);
          AddItem(2375, condition: condition2);
          AddItem(3183, condition: condition2);
          AddItem(2360, condition: condition2);
          AddItem(2494, conditions: new Condition[2]
          {
            condition2,
            Condition.Hardmode
          });
          AddItem(3032, conditions: new Condition[2]
          {
            condition2,
            Condition.Hardmode
          });
          AddItem(3031, conditions: new Condition[2]
          {
            condition2,
            Condition.Hardmode
          });
          AddItem(2422, conditions: new Condition[2]
          {
            condition5,
            Condition.Hardmode
          });
          AddItem(2294, conditions: new Condition[2]
          {
            condition6,
            Condition.Hardmode
          });
          AddItem(283, 3, new Condition("Mods.Fargowiltas.Conditions.Seeds", (Func<bool>) (() => ((IEnumerable<Item>) Main.LocalPlayer.inventory).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.useAmmo == AmmoID.Dart)))));
          break;
        case 20:
          AddItem(223, Item.buyPrice(0, 20, 0, 0), Condition.Hardmode);
          AddItem(208, Item.buyPrice(0, 10, 0, 0), Condition.Hardmode);
          AddItem(3385, Item.buyPrice(0, 5, 0, 0), Condition.Hardmode);
          AddItem(3386, Item.buyPrice(0, 5, 0, 0), Condition.Hardmode);
          AddItem(3387, Item.buyPrice(0, 5, 0, 0), Condition.Hardmode);
          AddItem(3388, Item.buyPrice(0, 5, 0, 0), Condition.Hardmode);
          break;
        case 38:
          AddItem(ModContent.ItemType<BoomShuriken>(), Item.buyPrice(0, 0, 1, 25));
          AddItem(12, condition: Condition.Hardmode);
          AddItem(699, condition: Condition.Hardmode);
          AddItem(11, condition: Condition.Hardmode);
          AddItem(700, condition: Condition.Hardmode);
          AddItem(14, condition: Condition.Hardmode);
          AddItem(701, condition: Condition.Hardmode);
          AddItem(13, condition: Condition.Hardmode);
          AddItem(702, condition: Condition.Hardmode);
          AddItem(116, condition: Condition.DownedPlantera);
          AddItem(56, condition: Condition.DownedPlantera);
          AddItem(880, condition: Condition.DownedPlantera);
          AddItem(174, condition: Condition.DownedPlantera);
          AddItem(364, condition: Condition.DownedMoonLord);
          AddItem(1104, condition: Condition.DownedMoonLord);
          AddItem(365, condition: Condition.DownedMoonLord);
          AddItem(1105, condition: Condition.DownedMoonLord);
          AddItem(366, condition: Condition.DownedMoonLord);
          AddItem(1106, condition: Condition.DownedMoonLord);
          AddItem(947, condition: Condition.DownedMoonLord);
          break;
        case 54:
          AddItem(848, Item.buyPrice(0, 1, 0, 0));
          AddItem(866, Item.buyPrice(0, 1, 0, 0));
          AddItem(2367, condition: condition2);
          AddItem(2368, condition: condition3);
          AddItem(2369, condition: condition4);
          AddItem(134, Item.buyPrice(0, 0, 1, 0));
          AddItem(ModContent.ItemType<UnsafeBlueBrickWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<UnsafeBlueSlabWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<UnsafeBlueTileWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(137, Item.buyPrice(0, 0, 1, 0));
          AddItem(ModContent.ItemType<UnsafeGreenBrickWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<UnsafeGreenSlabWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<UnsafeGreenTileWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(139, Item.buyPrice(0, 0, 1, 0));
          AddItem(ModContent.ItemType<UnsafePinkBrickWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<UnsafePinkSlabWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<UnsafePinkTileWall>(), Item.buyPrice(0, 0, 0, 25));
          AddItem(ModContent.ItemType<BrittleBone>(), condition: new Condition("Mods.Fargowiltas.Conditions.BrittleBone", (Func<bool>) (() => ((IEnumerable<Item>) Main.LocalPlayer.inventory).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.useAmmo == 154)))));
          break;
        case 108:
          AddItem(2209, condition: Condition.DownedGolem);
          break;
        case 178:
          AddItem(782, condition: Condition.CorruptWorld);
          AddItem(784, condition: Condition.CrimsonWorld);
          break;
        case 207:
          AddItem(1115, condition: new Condition("Mods.Fargowiltas.Conditions.RedHusk", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["RedHusk"])));
          AddItem(1114, condition: new Condition("Mods.Fargowiltas.Conditions.OrangeBloodroot", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["OrangeBloodroot"])));
          AddItem(1110, condition: new Condition("Mods.Fargowiltas.Conditions.YellowMarigold", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["YellowMarigold"])));
          AddItem(1112, condition: new Condition("Mods.Fargowiltas.Conditions.LimeKelp", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["LimeKelp"])));
          AddItem(1108, condition: new Condition("Mods.Fargowiltas.Conditions.GreenMushroom", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["GreenMushroom"])));
          AddItem(1107, condition: new Condition("Mods.Fargowiltas.Conditions.TealMushroom", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["TealMushroom"])));
          AddItem(1116, condition: new Condition("Mods.Fargowiltas.Conditions.CyanHusk", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["CyanHusk"])));
          AddItem(1109, condition: new Condition("Mods.Fargowiltas.Conditions.SkyBlueFlower", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["SkyBlueFlower"])));
          AddItem(1111, condition: new Condition("Mods.Fargowiltas.Conditions.BlueBerries", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["BlueBerries"])));
          AddItem(1118, condition: new Condition("Mods.Fargowiltas.Conditions.PurpleMucos", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["PurpleMucos"])));
          AddItem(1117, condition: new Condition("Mods.Fargowiltas.Conditions.VioletHusk", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["VioletHusk"])));
          AddItem(1113, condition: new Condition("Mods.Fargowiltas.Conditions.PinkPricklyPear", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["PinkPricklyPear"])));
          AddItem(1119, condition: new Condition("Mods.Fargowiltas.Conditions.BlackInk", (Func<bool>) (() => Main.LocalPlayer.GetModPlayer<FargoPlayer>().FirstDyeIngredients["BlackInk"])));
          break;
        case 208:
          if (!BirthdayParty.PartyIsUp)
            break;
          AddItem(3750);
          break;
        case 227:
          AddItem(1372, condition: Condition.InDungeon);
          AddItem(1375, condition: Condition.InDungeon);
          AddItem(1573, condition: Condition.InDungeon);
          AddItem(1420, condition: Condition.InDungeon);
          AddItem(1435, condition: Condition.InDungeon);
          AddItem(1426, condition: Condition.InDungeon);
          AddItem(1421, condition: Condition.InDungeon);
          AddItem(1500, condition: Condition.InDungeon);
          AddItem(1374, condition: Condition.InDungeon);
          AddItem(1425, condition: Condition.InDungeon);
          AddItem(1438, condition: Condition.InDungeon);
          AddItem(1441, condition: Condition.InDungeon);
          AddItem(1373, condition: Condition.InDungeon);
          AddItem(1433, condition: Condition.InDungeon);
          AddItem(1436, condition: Condition.InDungeon);
          AddItem(1434, condition: Condition.InDungeon);
          AddItem(1424, condition: Condition.InDungeon);
          AddItem(1419, condition: Condition.InDungeon);
          AddItem(2995, condition: Condition.InDungeon);
          AddItem(1422, condition: Condition.InDungeon);
          AddItem(1439, condition: Condition.InDungeon);
          AddItem(1502, condition: Condition.InDungeon);
          AddItem(1423, condition: Condition.InDungeon);
          AddItem(1437, condition: Condition.InDungeon);
          AddItem(1495, condition: condition7);
          AddItem(1575, condition: condition7);
          AddItem(1496, condition: condition7);
          AddItem(1442, condition: condition7);
          AddItem(1480, condition: condition7);
          AddItem(1577, condition: condition7);
          AddItem(1440, condition: condition7);
          AddItem(1477, condition: condition7);
          AddItem(1574, condition: condition7);
          AddItem(1443, condition: condition7);
          AddItem(1498, condition: condition7);
          AddItem(1576, condition: condition7);
          AddItem(1427, condition: condition7);
          AddItem(1428, condition: condition7);
          AddItem(1474, condition: condition7);
          AddItem(1476, condition: Condition.InUnderworldHeight);
          AddItem(1475, condition: Condition.InUnderworldHeight);
          AddItem(1479, condition: Condition.InUnderworldHeight);
          AddItem(1542, condition: Condition.InUnderworldHeight);
          AddItem(1497, condition: Condition.InUnderworldHeight);
          AddItem(1538, condition: Condition.InUnderworldHeight);
          AddItem(1501, condition: Condition.InUnderworldHeight);
          AddItem(1541, condition: Condition.InUnderworldHeight);
          AddItem(1539, condition: Condition.InUnderworldHeight);
          AddItem(1540, condition: Condition.InUnderworldHeight);
          AddItem(1499, condition: Condition.InUnderworldHeight);
          AddItem(1478, condition: Condition.InUnderworldHeight);
          break;
        case 228:
          bool flag = false;
          foreach (NPCShop.Entry entry in (IEnumerable<NPCShop.Entry>) shop.Entries)
          {
            if (!entry.Item.IsAir && entry.Item.type == 2999)
            {
              flag = true;
              break;
            }
          }
          if (flag)
            break;
          AddItem(2999, condition: Condition.DownedSkeletron);
          break;
      }

      void AddItem(int itemID, int customPrice = -1, Condition condition = null, Condition[] conditions = null)
      {
        if (Condition.op_Inequality(condition, (Condition) null))
          conditions = new Condition[1]{ condition };
        if (conditions != null)
        {
          if (customPrice != -1)
          {
            NPCShop shop = shop;
            Item obj = new Item(itemID, 1, 0);
            obj.shopCustomPrice = new int?(customPrice);
            Condition[] conditionArray = conditions;
            shop.Add(obj, conditionArray);
          }
          else
            shop.Add(itemID, conditions);
        }
        else if (customPrice != -1)
        {
          NPCShop shop = shop;
          Item obj = new Item(itemID, 1, 0);
          obj.shopCustomPrice = new int?(customPrice);
          Condition[] conditionArray = Array.Empty<Condition>();
          shop.Add(obj, conditionArray);
        }
        else
          shop.Add(itemID, Array.Empty<Condition>());
      }
    }

    public virtual void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
      FargoPlayer fargoPlayer = player.GetFargoPlayer();
      if (fargoPlayer.BattleCry)
      {
        spawnRate = (int) ((double) spawnRate * 0.1);
        maxSpawns = (int) ((double) maxSpawns * 10.0);
      }
      if (fargoPlayer.CalmingCry)
      {
        float num = 1.15f;
        if (Main.hardMode)
          num += 0.15f;
        if (NPC.downedMechBossAny)
          num += 0.15f;
        if (NPC.downedPlantBoss)
          num += 0.15f;
        if (NPC.downedGolemBoss)
          num += 0.15f;
        if (NPC.downedAncientCultist)
          num += 0.15f;
        spawnRate = (int) ((double) spawnRate * (double) num);
        maxSpawns = (int) ((double) maxSpawns * (1.0 / (double) num));
      }
      if ((FargoWorld.OverloadGoblins || FargoWorld.OverloadPirates) && (double) ((Entity) player).position.X > Main.invasionX * 16.0 - 3000.0 && (double) ((Entity) player).position.X < Main.invasionX * 16.0 + 3000.0)
      {
        if (FargoWorld.OverloadGoblins)
        {
          spawnRate = (int) ((double) spawnRate * 0.2);
          maxSpawns = (int) ((double) maxSpawns * 10.0);
        }
        else if (FargoWorld.OverloadPirates)
        {
          spawnRate = (int) ((double) spawnRate * 0.2);
          maxSpawns = (int) ((double) maxSpawns * 30.0);
        }
      }
      if (FargoWorld.OverloadPumpkinMoon || FargoWorld.OverloadFrostMoon)
      {
        spawnRate = (int) ((double) spawnRate * 0.2);
        maxSpawns = (int) ((double) maxSpawns * 10.0);
      }
      else if (FargoWorld.OverloadMartians)
      {
        spawnRate = (int) ((double) spawnRate * 0.2);
        maxSpawns = (int) ((double) maxSpawns * 30.0);
      }
      if (!FargoGlobalNPC.AnyBossAlive() || !FargoServerConfig.Instance.BossZen || (double) ((Entity) player).Distance(((Entity) Main.npc[FargoGlobalNPC.boss]).Center) >= 6000.0)
        return;
      maxSpawns = 0;
    }

    public virtual void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
      Player localPlayer = Main.LocalPlayer;
      if (FargoWorld.OverloadGoblins && (double) ((Entity) localPlayer).position.X > Main.invasionX * 16.0 - 3000.0 && (double) ((Entity) localPlayer).position.X < Main.invasionX * 16.0 + 3000.0)
      {
        pool[471] = 1f;
        pool[111] = 3f;
        pool[26] = 5f;
        pool[29] = 3f;
        pool[28] = 5f;
        pool[27] = 5f;
        pool[73] = 3f;
      }
      else if (FargoWorld.OverloadPirates && (double) ((Entity) localPlayer).position.X > Main.invasionX * 16.0 - 3000.0 && (double) ((Entity) localPlayer).position.X < Main.invasionX * 16.0 + 3000.0)
      {
        if (NPC.CountNPCS(491) < 4)
          pool[491] = 0.5f;
        pool[252] = 2f;
        pool[216] = 1f;
        pool[215] = 3f;
        pool[213] = 5f;
        pool[214] = 4f;
        pool[212] = 5f;
      }
      else if (FargoWorld.OverloadPumpkinMoon)
      {
        pool[327] = 4f;
        pool[325] = 4f;
        pool[315] = 3f;
        pool[305] = 0.5f;
        pool[306] = 0.5f;
        pool[307] = 0.5f;
        pool[308] = 0.5f;
        pool[309] = 0.5f;
        pool[310] = 0.5f;
        pool[311] = 0.5f;
        pool[312] = 0.5f;
        pool[313] = 0.5f;
        pool[314] = 0.5f;
        pool[329] = 3f;
        pool[330] = 3f;
        pool[326] = 3f;
      }
      else if (FargoWorld.OverloadFrostMoon)
      {
        pool[345] = 5f;
        pool[344] = 5f;
        pool[346] = 5f;
        pool[338] = 1f;
        pool[339] = 1f;
        pool[340] = 1f;
        pool[342] = 2f;
        pool[350] = 2f;
        pool[348] = 3f;
        pool[347] = 3f;
        pool[352] = 2f;
        pool[343] = 4f;
        pool[341] = 2f;
        pool[351] = 4f;
      }
      else
      {
        if (!FargoWorld.OverloadMartians)
          return;
        pool[395] = 1f;
        pool[391] = 3f;
        pool[390] = 2f;
        pool[520] = 3f;
        pool[388] = 2f;
        pool[389] = 1f;
        pool[386] = 2f;
        pool[383] = 2f;
        pool[382] = 1f;
        pool[385] = 1f;
        pool[381] = 1f;
      }
    }

    public virtual bool PreKill(NPC npc)
    {
      if (this.NoLoot || Fargowiltas.Fargowiltas.SwarmActive && (npc.type == 1 || npc.type == 14 || npc.type == 15 || npc.type == 267 || npc.type >= 213 && npc.type <= 215))
        return false;
      if (this.SwarmActive && Fargowiltas.Fargowiltas.SwarmActive && Main.netMode != 1)
      {
        switch (npc.type)
        {
          case 4:
            this.Swarm(npc, 4, 5, 3319, 1360, ModContent.ItemType<EnergizerEye>());
            break;
          case 13:
            this.Swarm(npc, 13, 15, 3320, 1361, ModContent.ItemType<EnergizerWorm>());
            break;
          case 35:
            this.Swarm(npc, 35, -1, 3323, 1363, ModContent.ItemType<EnergizerSkele>());
            break;
          case 50:
            this.Swarm(npc, 50, 1, 3318, 2489, ModContent.ItemType<EnergizerSlime>());
            break;
          case 68:
            this.Swarm(npc, 68, -1, -1, 1169, ModContent.ItemType<EnergizerDG>());
            break;
          case 113:
            this.Swarm(npc, 113, 115, 3324, 1365, ModContent.ItemType<EnergizerWall>());
            break;
          case 125:
            this.Swarm(npc, 125, -1, 3326, 1368, ModContent.ItemType<EnergizerTwins>());
            break;
          case 126:
            this.Swarm(npc, 126, -1, -1, 1369, -1);
            break;
          case (int) sbyte.MaxValue:
            this.Swarm(npc, (int) sbyte.MaxValue, -1, 3327, 1367, ModContent.ItemType<EnergizerPrime>());
            break;
          case 134:
            this.Swarm(npc, 134, 139, 3325, 1366, ModContent.ItemType<EnergizerDestroy>());
            break;
          case 222:
            this.Swarm(npc, 222, 211, 3322, 1364, ModContent.ItemType<EnergizerBee>());
            break;
          case 245:
            this.Swarm(npc, 245, 249, 3329, 1371, ModContent.ItemType<EnergizerGolem>());
            break;
          case 262:
            this.Swarm(npc, 262, 263, 3328, 1370, ModContent.ItemType<EnergizerPlant>());
            break;
          case 266:
            this.Swarm(npc, 266, 267, 3321, 1362, ModContent.ItemType<EnergizerBrain>());
            break;
          case 370:
            this.Swarm(npc, 370, 372, 3330, 2589, ModContent.ItemType<EnergizerFish>());
            break;
          case 398:
            this.Swarm(npc, 398, 400, 3332, 3595, ModContent.ItemType<EnergizerMoon>());
            break;
          case 439:
            this.Swarm(npc, 439, -1, 3331, 3357, ModContent.ItemType<EnergizerCultist>());
            break;
          case 551:
            this.Swarm(npc, 551, 560, 3860, 3866, ModContent.ItemType<EnergizerBetsy>());
            break;
          case 564:
            this.Swarm(npc, 564, -1, 3817, 3867, ModContent.ItemType<EnergizerDarkMage>());
            break;
          case 636:
            this.Swarm(npc, 636, -1, 4782, 4783, ModContent.ItemType<EnergizerEmpress>());
            break;
          case 657:
            this.Swarm(npc, 657, 659, 4957, 4958, ModContent.ItemType<EnergizerQueenSlime>());
            break;
          case 668:
            this.Swarm(npc, 668, -1, 5111, 5108, ModContent.ItemType<EnergizerDeer>());
            break;
        }
        return false;
      }
      return !this.PandoraActive;
    }

    public virtual void OnKill(NPC npc)
    {
      switch (npc.type)
      {
        case 227:
          if (!NPC.AnyNPCs(398))
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, ModContent.ItemType<EchPainting>(), 1, false, 0, false, false);
          break;
        case 315:
          if (Main.dayTime || Main.pumpkinMoon || !Utils.NextBool(Main.rand, 10))
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 1857, 1, false, 0, false, false);
          break;
        case 325:
          if (Main.dayTime || Main.pumpkinMoon)
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 1729, 30, false, 0, false, false);
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[6]
          {
            1829,
            1831,
            1835,
            1837,
            1845,
            Main.expertMode ? 4444 : 1729
          }), 1, false, 0, false, false);
          break;
        case 327:
          if (Main.dayTime || Main.pumpkinMoon)
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[8]
          {
            1826,
            1801,
            1811,
            1798,
            1802,
            1782,
            1784,
            4680
          }), 1, false, 0, false, false);
          break;
        case 344:
          if (Main.dayTime || Main.snowMoon)
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[4]
          {
            1928,
            1916,
            1930,
            1871
          }), 1, false, 0, false, false);
          break;
        case 345:
          if (Main.dayTime || Main.snowMoon)
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[5]
          {
            1931,
            1946,
            1947,
            1959,
            1914
          }), 1, false, 0, false, false);
          break;
        case 346:
          if (Main.dayTime || Main.snowMoon)
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[2]
          {
            1910,
            1929
          }), 1, false, 0, false, false);
          break;
        case 564:
        case 565:
          if (DD2Event.Ongoing)
            break;
          if (Utils.NextBool(Main.rand, 14))
            Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 3864, 1, false, 0, false, false);
          if (Utils.NextBool(Main.rand, 10))
            Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.NextBool(Main.rand) ? 3814 : 3815, 1, false, 0, false, false);
          if (!Utils.NextBool(Main.rand, 6))
            break;
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[2]
          {
            3855,
            3857
          }), 1, false, 0, false, false);
          break;
        case 576:
        case 577:
          if (DD2Event.Ongoing)
            break;
          if (Utils.NextBool(Main.rand, 14))
            Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 3865, 1, false, 0, false, false);
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, Utils.Next<int>(Main.rand, new int[10]
          {
            3809,
            3810,
            3811,
            3812,
            3823,
            3835,
            3836,
            3852,
            3854,
            3856
          }), 1, false, 0, false, false);
          Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 73, Main.rand.Next(4, 7), false, 0, false, false);
          break;
      }
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      switch (npc.type)
      {
        case 17:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(410, 8, 1, 1));
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(411, 8, 1, 1));
          break;
        case 18:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(29, 5, 1, 1));
          break;
        case 20:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3093, 3, 1, 1));
          break;
        case 38:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(167, 2, 5, 5));
          break;
        case 54:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(1274, 20, 1, 1));
          break;
        case 108:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(75, 5, 5, 5));
          break;
        case 109:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(1324, 1, 1, 1));
          break;
        case 124:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(530, 5, 40, 40));
          break;
        case 147:
        case 148:
        case 161:
        case 184:
        case 431:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.OneFromOptions(20, new int[3]
          {
            803,
            804,
            805
          }));
          break;
        case 153:
        case 175:
        case 176:
        case 236:
        case 237:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsHardmode(), ModContent.ItemType<JungleChest>(), 50, 1, 1, 1));
          break;
        case 160:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(470, 8, 1, 1));
          break;
        case 205:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.IsHardmode(), ModContent.ItemType<JungleChest>(), 10, 1, 1, 1));
          break;
        case 209:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(1350, 4, 30, 30));
          break;
        case 301:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(1774, 1, 1, 1));
          break;
        case 333:
        case 334:
        case 335:
        case 336:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(1869, 1, 1, 1));
          break;
        case 369:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.OneFromOptions(2, new int[3]
          {
            2337,
            2339,
            2338
          }));
          break;
        case 398:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(5001, 100, 1, 1));
          break;
        case 441:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(73, 8, 10, 10));
          break;
        case 481:
          ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule =>
          {
            if (!(rule is CommonDrop commonDrop2))
              return false;
            return commonDrop2.itemId == 3187 || commonDrop2.itemId == 3188 || commonDrop2.itemId == 3189;
          }), true);
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.OneFromOptions(10, new int[3]
          {
            3187,
            3188,
            3189
          }));
          break;
        case 489:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.OneFromOptions(200, new int[2]
          {
            1827,
            1825
          }));
          break;
        case 550:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(353, 2, 4, 4));
          break;
        case 564:
        case 565:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 5, 5));
          break;
        case 576:
        case 577:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(3817, 1, 20, 20));
          break;
      }
      base.ModifyNPCLoot(npc, npcLoot);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (npc.FindBuffIndex(ModContent.BuffType<WoodDrop>()) != -1)
        Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, 9, Main.rand.Next(10, 30), false, 0, false, false);
      switch (npc.type)
      {
        case 1:
          if (npc.netID == -4)
          {
            FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "pinky");
            break;
          }
          break;
        case 10:
        case 95:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "worm");
          break;
        case 44:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "undeadMiner");
          break;
        case 45:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "tim");
          break;
        case 52:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "doctorBones");
          break;
        case 71:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (NPC.downedBoss3 ? 1 : 0) != 0, "rareEnemy", "dungeonSlime");
          break;
        case 73:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "goblinScout");
          break;
        case 85:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "mimic");
          break;
        case 87:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "wyvern");
          break;
        case 109:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "clown");
          break;
        case 156:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "redDevil");
          break;
        case 172:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "runeWizard");
          break;
        case 196:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "nymph");
          break;
        case 205:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "moth");
          break;
        case 216:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (!Main.hardMode ? 0 : (NPC.downedPirates ? 1 : 0)) != 0, "rareEnemy", "pirateCaptain");
          break;
        case 243:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "iceGolem");
          break;
        case 244:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "rainbowSlime");
          break;
        case 281:
        case 282:
        case 283:
        case 284:
        case 285:
        case 286:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (NPC.downedPlantBoss ? 1 : 0) != 0, "rareEnemy", "skeletonMage");
          break;
        case 287:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (NPC.downedPlantBoss ? 1 : 0) != 0, "rareEnemy", "boneLee");
          break;
        case 290:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (NPC.downedPlantBoss ? 1 : 0) != 0, "rareEnemy", "paladin");
          break;
        case 291:
        case 292:
        case 293:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (NPC.downedPlantBoss ? 1 : 0) != 0, "rareEnemy", "skeletonGun");
          break;
        case 315:
          FargoUtils.TryDowned("Abominationn", Color.Orange, "headlessHorseman");
          break;
        case 325:
          FargoUtils.TryDowned("Abominationn", Color.Orange, (NPC.downedHalloweenTree ? 1 : 0) != 0, "mourningWood");
          break;
        case 327:
          FargoUtils.TryDowned("Abominationn", Color.Orange, (NPC.downedHalloweenKing ? 1 : 0) != 0, "pumpking");
          break;
        case 344:
          FargoUtils.TryDowned("Abominationn", Color.Orange, (NPC.downedChristmasTree ? 1 : 0) != 0, "everscream");
          break;
        case 345:
          FargoUtils.TryDowned("Abominationn", Color.Orange, (NPC.downedChristmasIceQueen ? 1 : 0) != 0, "iceQueen");
          break;
        case 346:
          FargoUtils.TryDowned("Abominationn", Color.Orange, (NPC.downedChristmasSantank ? 1 : 0) != 0, "santank");
          break;
        case 439:
          if (!this.PillarSpawn)
          {
            for (int index = 0; index < Main.maxNPCs; ++index)
            {
              NPC npc1 = Main.npc[index];
              NPC.LunarApocalypseIsUp = false;
              if (npc1.type == 507 || npc1.type == 517 || npc1.type == 493 || npc1.type == 422)
              {
                NPC.TowerActiveSolar = true;
                ((Entity) npc1).active = false;
              }
              NPC.TowerActiveSolar = false;
            }
            break;
          }
          break;
        case 463:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (NPC.downedPlantBoss ? 1 : 0) != 0, "rareEnemy", "nailhead");
          break;
        case 471:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (!Main.hardMode ? 0 : (NPC.downedGoblins ? 1 : 0)) != 0, "rareEnemy", "goblinSummoner");
          break;
        case 473:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "mimicCorrupt");
          break;
        case 474:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "mimicCrimson");
          break;
        case 475:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "mimicHallow");
          break;
        case 476:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "mimicJungle");
          break;
        case 477:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 ? 0 : (NPC.downedMechBoss3 ? 1 : 0)) != 0, "rareEnemy", "mothron");
          break;
        case 480:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "medusa");
          break;
        case 491:
          FargoUtils.TryDowned("Abominationn", Color.Orange, (NPC.downedPirates ? 1 : 0) != 0, "flyingDutchman");
          break;
        case 541:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "sandElemental");
          break;
        case 564:
        case 565:
          FargoUtils.TryDowned("Abominationn", Color.Orange, "darkMage");
          break;
        case 576:
        case 577:
          FargoUtils.TryDowned("Abominationn", Color.Orange, "ogre");
          break;
        case 586:
        case 587:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "zombieMerman", "eyeFish");
          break;
        case 618:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "dreadnautilus");
          break;
        case 620:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "goblinShark");
          break;
        case 621:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, (Main.hardMode ? 1 : 0) != 0, "rareEnemy", "bloodEel");
          break;
        case 624:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "gnome");
          break;
        case 667:
          FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", "goldenSlime");
          break;
      }
      if (Fargowiltas.Fargowiltas.ModRareEnemies.ContainsKey(npc.type))
        FargoUtils.TryDowned("Deviantt", Color.HotPink, "rareEnemy", Fargowiltas.Fargowiltas.ModRareEnemies[npc.type]);
      if (npc.type == 551 && !this.PandoraActive)
      {
        FargoUtils.PrintText(Language.GetTextValue("Announcement.HasBeenDefeated_Single", (object) Lang.GetNPCNameValue(551)), new Color(175, 75, 0));
        FargoWorld.DownedBools["betsy"] = true;
      }
      if (npc.boss)
        FargoWorld.DownedBools["boss"] = true;
      return true;
    }

    public virtual void ModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      if (!FargoServerConfig.Instance.RottenEggs || projectile.type != 318 || !npc.townNPC)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 20f);
    }

    public virtual void OnChatButtonClicked(NPC npc, bool firstButton)
    {
      if (!FargoServerConfig.Instance.AnglerQuestInstantReset || !Main.anglerQuestFinished)
        return;
      if (Main.netMode == 0)
      {
        Main.AnglerQuestSwap();
      }
      else
      {
        if (Main.netMode != 1)
          return;
        ModPacket packet = ((ModType) this).Mod.GetPacket(256);
        ((BinaryWriter) packet).Write((byte) 3);
        packet.Send(-1, -1);
      }
    }

    private void SpawnBoss(NPC npc, int boss)
    {
      if (this.SwarmActive)
      {
        if (npc.type == 113)
        {
          NPC npc1 = Main.npc[FargoGlobalNPC.LastWoFIndex];
          int x = (int) ((Entity) npc1).position.X;
          int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), x + 400 * FargoGlobalNPC.WoFDirection, (int) ((Entity) npc1).position.Y, 113, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (index == Main.maxNPCs)
            return;
          Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().SwarmActive = true;
          FargoGlobalNPC.LastWoFIndex = index;
        }
        else
        {
          int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) ((Entity) npc).position.X + Main.rand.Next(-1000, 1000), (int) ((Entity) npc).position.Y + Main.rand.Next(-400, -100), boss, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (index == Main.maxNPCs)
            return;
          Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().SwarmActive = true;
          NetMessage.SendData(23, -1, -1, (NetworkText) null, boss, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      else
      {
        int num;
        do
        {
          num = Utils.Next<int>(Main.rand, FargoGlobalNPC.Bosses);
        }
        while (NPC.CountNPCS(num) >= 4);
        int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) ((Entity) npc).position.X + Main.rand.Next(-1000, 1000), (int) ((Entity) npc).position.Y + Main.rand.Next(-400, -100), num, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        if (index == Main.maxNPCs)
          return;
        Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().PandoraActive = true;
        NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
    }

    private void Swarm(NPC npc, int boss, int minion, int bossbag, int trophy, int reward)
    {
      if (bossbag >= 0 && bossbag != 3817)
        npc.DropItemInstanced(((Entity) npc).Center, ((Entity) npc).Size, bossbag, 1, true);
      else if (bossbag >= 0 && bossbag == 3817)
        npc.DropItemInstanced(((Entity) npc).Center, ((Entity) npc).Size, bossbag, 5, true);
      int num1 = 0;
      if (this.SwarmActive)
      {
        num1 = NPC.CountNPCS(boss) - 1;
      }
      else
      {
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && Array.IndexOf<int>(FargoGlobalNPC.Bosses, Main.npc[index].type) > -1)
            ++num1;
        }
      }
      int num2 = Fargowiltas.Fargowiltas.SwarmSpawned - num1;
      ++Fargowiltas.Fargowiltas.SwarmKills;
      if (Fargowiltas.Fargowiltas.SwarmKills % 100 == 0 && reward > 0)
        Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, reward, 1, false, 0, false, false);
      if (Fargowiltas.Fargowiltas.SwarmKills % 10 == 0 && trophy != -1)
        Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, trophy, 1, false, 0, false, false);
      if (Main.netMode == 2)
      {
        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.SwarmKilled", new object[1]
        {
          (object) Fargowiltas.Fargowiltas.SwarmKills
        }), new Color(206, 12, 15), -1);
        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.SwarmTotal", new object[1]
        {
          (object) Fargowiltas.Fargowiltas.SwarmTotal
        }), new Color(206, 12, 15), -1);
      }
      else
      {
        Main.NewText((object) Language.GetTextValue("Mods.Fargowiltas.MessageInfo.SwarmKilled", (object) Fargowiltas.Fargowiltas.SwarmKills), new Color?(new Color(206, 12, 15)));
        Main.NewText((object) Language.GetTextValue("Mods.Fargowiltas.MessageInfo.SwarmTotal", (object) Fargowiltas.Fargowiltas.SwarmTotal), new Color?(new Color(206, 12, 15)));
      }
      if (minion != -1 && NPC.CountNPCS(minion) >= Fargowiltas.Fargowiltas.SwarmSpawned)
      {
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && Main.npc[index].type == minion)
            Main.npc[index].SimpleStrikeNPC(Main.npc[index].lifeMax, -((Entity) Main.npc[index]).direction, true, 0.0f, (DamageClass) null, false, 0.0f, true);
        }
      }
      if (Fargowiltas.Fargowiltas.SwarmKills <= Fargowiltas.Fargowiltas.SwarmTotal - Fargowiltas.Fargowiltas.SwarmSpawned)
      {
        int num3 = 0;
        for (int index1 = 0; index1 < Main.maxNPCs; ++index1)
        {
          int num4 = 0;
          for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
          {
            if (((Entity) Main.npc[index2]).active)
              ++num4;
          }
          if (num4 >= Main.maxNPCs)
          {
            if (this.SwarmActive && minion > 0 && index1 < Main.maxNPCs - 1)
            {
              if (Main.npc[index1].type == minion)
                Main.npc[index1].SimpleStrikeNPC(Main.npc[index1].lifeMax, -((Entity) Main.npc[index1]).direction, true, 0.0f, (DamageClass) null, false, 0.0f, true);
            }
            else if (Array.IndexOf<int>(FargoGlobalNPC.Bosses, Main.npc[index1].type) == -1 && !Main.npc[index1].boss)
              Main.npc[index1].SimpleStrikeNPC(Main.npc[index1].lifeMax, -((Entity) Main.npc[index1]).direction, true, 0.0f, (DamageClass) null, false, 0.0f, true);
          }
          this.SpawnBoss(npc, boss);
          ++num3;
          if (num3 > num2)
            break;
        }
      }
      else if (Fargowiltas.Fargowiltas.SwarmKills >= Fargowiltas.Fargowiltas.SwarmTotal)
      {
        if (Main.netMode == 2)
          ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.SwarmDefeated", Array.Empty<object>()), new Color(206, 12, 15), -1);
        else
          Main.NewText((object) Language.GetTextValue("Mods.Fargowiltas.MessageInfo.SwarmDefeated"), new Color?(new Color(206, 12, 15)));
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          NPC npc1 = Main.npc[index];
          if (((Entity) npc1).active && !npc1.friendly && npc1.type != 507 && npc1.type != 517 && npc1.type != 493 && npc1.type != 422)
          {
            Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().NoLoot = true;
            Main.npc[index].SimpleStrikeNPC(Main.npc[index].lifeMax, -((Entity) Main.npc[index]).direction, true, 0.0f, (DamageClass) null, false, 0.0f, true);
          }
        }
        if (Main.netMode == 1)
          return;
        Fargowiltas.Fargowiltas.SwarmActive = false;
        FargoGlobalNPC.LastWoFIndex = -1;
        FargoGlobalNPC.WoFDirection = 0;
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        if (num1 >= Fargowiltas.Fargowiltas.SwarmSpawned || Fargowiltas.Fargowiltas.SwarmTotal <= 20)
          return;
        int num5 = 0;
        for (int index3 = 0; index3 < Main.maxNPCs; ++index3)
        {
          int num6 = 0;
          for (int index4 = 0; index4 < Main.maxNPCs; ++index4)
          {
            if (((Entity) Main.npc[index4]).active)
              ++num6;
          }
          if (num6 >= Main.maxNPCs)
          {
            if (this.SwarmActive && minion > 0 && index3 < Main.maxNPCs - 1)
            {
              if (Main.npc[index3].type == minion)
                Main.npc[index3].SimpleStrikeNPC(Main.npc[index3].lifeMax, -((Entity) Main.npc[index3]).direction, true, 0.0f, (DamageClass) null, false, 0.0f, true);
            }
            else if (Array.IndexOf<int>(FargoGlobalNPC.Bosses, Main.npc[index3].type) == -1 && !Main.npc[index3].boss)
              Main.npc[index3].SimpleStrikeNPC(Main.npc[index3].lifeMax, -((Entity) Main.npc[index3]).direction, true, 0.0f, (DamageClass) null, false, 0.0f, true);
          }
          this.SpawnBoss(npc, boss);
          ++num5;
          if (num5 >= 5)
            break;
        }
      }
    }

    public static void SpawnWalls(Player player)
    {
      int num = FargoGlobalNPC.LastWoFIndex != -1 ? (int) ((Entity) Main.npc[FargoGlobalNPC.LastWoFIndex]).position.X : (int) ((Entity) player).position.X;
      Vector2 position = ((Entity) player).position;
      if (FargoGlobalNPC.WoFDirection == 0)
        FargoGlobalNPC.WoFDirection = (double) ((Entity) player).position.X / 16.0 > (double) (Main.maxTilesX / 2) ? 1 : -1;
      int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), num + 400 * FargoGlobalNPC.WoFDirection, (int) position.Y, 113, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
      Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().SwarmActive = true;
      FargoGlobalNPC.LastWoFIndex = index;
    }

    public static bool SpecificBossIsAlive(ref int bossID, int bossType)
    {
      if (bossID == -1)
        return false;
      if (((Entity) Main.npc[bossID]).active && Main.npc[bossID].type == bossType)
        return true;
      bossID = -1;
      return false;
    }

    public static bool AnyBossAlive()
    {
      if (FargoGlobalNPC.boss == -1)
        return false;
      NPC npc = Main.npc[FargoGlobalNPC.boss];
      if (((Entity) npc).active && npc.type != 395 && (npc.boss || npc.type == 13))
        return true;
      FargoGlobalNPC.boss = -1;
      return false;
    }
  }
}
