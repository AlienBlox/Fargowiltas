// Decompiled with JetBrains decompiler
// Type: Fargowiltas.MutantSummonTracker
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Summons.Mutant;
using Fargowiltas.Items.Summons.VanillaCopy;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas
{
  internal class MutantSummonTracker
  {
    public const float KingSlime = 1f;
    public const float EyeOfCthulhu = 2f;
    public const float EaterOfWorlds = 3f;
    public const float QueenBee = 4f;
    public const float Skeletron = 5f;
    public const float DeerClops = 6f;
    public const float WallOfFlesh = 7f;
    public const float QueenSlime = 8f;
    public const float TheTwins = 9f;
    public const float TheDestroyer = 10f;
    public const float SkeletronPrime = 11f;
    public const float Plantera = 12f;
    public const float Golem = 13f;
    public const float DukeFishron = 14f;
    public const float EmpressOfLight = 15f;
    public const float Betsy = 16f;
    public const float LunaticCultist = 17f;
    public const float Moonlord = 18f;
    internal List<MutantSummonInfo> SortedSummons;
    internal List<MutantSummonInfo> EventSummons;
    internal bool SummonsFinalized = false;

    public MutantSummonTracker()
    {
      Fargowiltas.Fargowiltas.summonTracker = this;
      this.InitializeVanillaSummons();
    }

    private void InitializeVanillaSummons()
    {
      this.SortedSummons = new List<MutantSummonInfo>()
      {
        new MutantSummonInfo(1f, ModContent.ItemType<SlimyCrown>(), (Func<bool>) (() => NPC.downedSlimeKing), Item.buyPrice(0, 5, 0, 0)),
        new MutantSummonInfo(2f, ModContent.ItemType<SuspiciousEye>(), (Func<bool>) (() => NPC.downedBoss1), Item.buyPrice(0, 8, 0, 0)),
        new MutantSummonInfo(3f, ModContent.ItemType<WormyFood>(), (Func<bool>) (() => NPC.downedBoss2), Item.buyPrice(0, 10, 0, 0)),
        new MutantSummonInfo(3f, ModContent.ItemType<GoreySpine>(), (Func<bool>) (() => NPC.downedBoss2), Item.buyPrice(0, 10, 0, 0)),
        new MutantSummonInfo(6f, ModContent.ItemType<DeerThing2>(), (Func<bool>) (() => NPC.downedDeerclops), Item.buyPrice(0, 12, 0, 0)),
        new MutantSummonInfo(4f, ModContent.ItemType<Abeemination2>(), (Func<bool>) (() => NPC.downedQueenBee), Item.buyPrice(0, 15, 0, 0)),
        new MutantSummonInfo(5f, ModContent.ItemType<SuspiciousSkull>(), (Func<bool>) (() => NPC.downedBoss3), Item.buyPrice(0, 15, 0, 0)),
        new MutantSummonInfo(7f, ModContent.ItemType<FleshyDoll>(), (Func<bool>) (() => Main.hardMode), Item.buyPrice(0, 20, 0, 0)),
        new MutantSummonInfo(7.0001f, ModContent.ItemType<DeathBringerFairy>(), (Func<bool>) (() => Main.hardMode), Item.buyPrice(0, 50, 0, 0)),
        new MutantSummonInfo(8f, ModContent.ItemType<JellyCrystal>(), (Func<bool>) (() => NPC.downedQueenSlime), Item.buyPrice(0, 25, 0, 0)),
        new MutantSummonInfo(9f, ModContent.ItemType<MechEye>(), (Func<bool>) (() => NPC.downedMechBoss2), Item.buyPrice(0, 40, 0, 0)),
        new MutantSummonInfo(10f, ModContent.ItemType<MechWorm>(), (Func<bool>) (() => NPC.downedMechBoss1), Item.buyPrice(0, 40, 0, 0)),
        new MutantSummonInfo(11f, ModContent.ItemType<MechSkull>(), (Func<bool>) (() => NPC.downedMechBoss3), Item.buyPrice(0, 40, 0, 0)),
        new MutantSummonInfo(11.0001f, ModContent.ItemType<MechanicalAmalgam>(), (Func<bool>) (() => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3), Item.buyPrice(1, 0, 0, 0)),
        new MutantSummonInfo(12f, ModContent.ItemType<PlanterasFruit>(), (Func<bool>) (() => NPC.downedPlantBoss), Item.buyPrice(0, 50, 0, 0)),
        new MutantSummonInfo(13f, ModContent.ItemType<LihzahrdPowerCell2>(), (Func<bool>) (() => NPC.downedGolemBoss), Item.buyPrice(0, 60, 0, 0)),
        new MutantSummonInfo(15f, ModContent.ItemType<PrismaticPrimrose>(), (Func<bool>) (() => NPC.downedEmpressOfLight), Item.buyPrice(0, 60, 0, 0)),
        new MutantSummonInfo(14f, ModContent.ItemType<TruffleWorm2>(), (Func<bool>) (() => NPC.downedFishron), Item.buyPrice(0, 60, 0, 0)),
        new MutantSummonInfo(17f, ModContent.ItemType<CultistSummon>(), (Func<bool>) (() => NPC.downedAncientCultist), Item.buyPrice(0, 75, 0, 0)),
        new MutantSummonInfo(18f, ModContent.ItemType<CelestialSigil2>(), (Func<bool>) (() => NPC.downedMoonlord), Item.buyPrice(1, 0, 0, 0)),
        new MutantSummonInfo(18.0001f, ModContent.ItemType<MutantVoodoo>(), (Func<bool>) (() => NPC.downedMoonlord), Item.buyPrice(2, 0, 0, 0))
      };
      this.EventSummons = new List<MutantSummonInfo>();
    }

    internal void FinalizeSummonData()
    {
      this.SortedSummons.Sort((Comparison<MutantSummonInfo>) ((x, y) => x.progression.CompareTo(y.progression)));
      this.SummonsFinalized = true;
    }

    internal void AddSummon(float progression, int itemId, Func<bool> downed, int price)
    {
      this.SortedSummons.Add(new MutantSummonInfo(progression, itemId, downed, price));
    }

    internal void AddEventSummon(float progression, int itemId, Func<bool> downed, int price)
    {
      this.EventSummons.Add(new MutantSummonInfo(progression, itemId, downed, price));
    }
  }
}
