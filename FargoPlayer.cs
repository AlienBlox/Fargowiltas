// Decompiled with JetBrains decompiler
// Type: Fargowiltas.FargoPlayer
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Items;
using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace Fargowiltas
{
  public class FargoPlayer : ModPlayer
  {
    public bool extractSpeed;
    public bool HasDrawnDebuffLayer;
    internal bool BattleCry;
    internal bool CalmingCry;
    internal int originalSelectedItem;
    internal bool autoRevertSelectedItem;
    public float luckPotionBoost;
    public float ElementalAssemblerNearby;
    public float StatSheetMaxAscentMultiplier;
    public float StatSheetWingSpeed;
    public bool? CanHover = new bool?();
    public int DeathFruitHealth;
    public bool bigSuck;
    public int StationSoundCooldown;
    internal Dictionary<string, bool> FirstDyeIngredients = new Dictionary<string, bool>();
    public bool[] ItemHasBeenOwned;
    public bool[] ItemHasBeenOwnedAtThirtyStack;
    private readonly string[] tags = new string[13]
    {
      "RedHusk",
      "OrangeBloodroot",
      "YellowMarigold",
      "LimeKelp",
      "GreenMushroom",
      "TealMushroom",
      "CyanHusk",
      "SkyBlueFlower",
      "BlueBerries",
      "PurpleMucos",
      "VioletHusk",
      "PinkPricklyPear",
      "BlackInk"
    };

    public virtual void Initialize()
    {
      this.ItemHasBeenOwned = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
      this.ItemHasBeenOwnedAtThirtyStack = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
    }

    public virtual void SaveData(TagCompound tag)
    {
      string str = "FargoDyes" + this.Player.name;
      List<string> list = new List<string>();
      foreach (string tag1 in this.tags)
      {
        if (this.FirstDyeIngredients.TryGetValue(tag1, out bool _))
          list.AddWithCondition<string>(tag1, this.FirstDyeIngredients[tag1]);
        else
          list.AddWithCondition<string>(tag1, false);
      }
      tag.Add(str, (object) list);
      tag.Add("DeathFruitHealth", (object) this.DeathFruitHealth);
      if (this.BattleCry)
        tag.Add("FargoBattleCry" + this.Player.name, (object) true);
      if (this.CalmingCry)
        tag.Add("FargoCalmingCry" + this.Player.name, (object) true);
      List<string> stringList1 = new List<string>();
      for (int index = 0; index < this.ItemHasBeenOwned.Length; ++index)
      {
        if (this.ItemHasBeenOwned[index])
        {
          if (index >= (int) ItemID.Count)
          {
            ModItem modItem = ItemLoader.GetItem(index);
            if (modItem != null && modItem != null)
              stringList1.Add(((ModType) modItem).FullName ?? "");
          }
          else
          {
            List<string> stringList2 = stringList1;
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
            interpolatedStringHandler.AppendFormatted<int>(index);
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            stringList2.Add(stringAndClear);
          }
        }
      }
      tag.Add("OwnedItemsList", (object) stringList1);
    }

    public virtual void LoadData(TagCompound tag)
    {
      string str = "FargoDyes" + this.Player.name;
      IList<string> list = tag.GetList<string>(str);
      foreach (string tag1 in this.tags)
        this.FirstDyeIngredients[tag1] = list.Contains(tag1);
      this.DeathFruitHealth = tag.GetInt("DeathFruitHealth");
      this.BattleCry = tag.ContainsKey("FargoBattleCry" + this.Player.name);
      this.CalmingCry = tag.ContainsKey("FargoCalmingCry" + this.Player.name);
      this.ItemHasBeenOwned = ItemID.Sets.Factory.CreateBoolSet(false, Array.Empty<int>());
      foreach (string s in (IEnumerable<string>) tag.GetList<string>("OwnedItemsList"))
      {
        int result;
        if (int.TryParse(s, out result) && result < (int) ItemID.Count)
        {
          this.ItemHasBeenOwned[result] = true;
        }
        else
        {
          ModItem modItem;
          if (ModContent.TryFind<ModItem>(s, ref modItem))
            this.ItemHasBeenOwned[modItem.Type] = true;
        }
      }
    }

    public virtual void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
      ModPacket packet = ((ModType) this).Mod.GetPacket(256);
      ((BinaryWriter) packet).Write((byte) 9);
      ((BinaryWriter) packet).Write((byte) ((Entity) this.Player).whoAmI);
      ((BinaryWriter) packet).Write((byte) this.DeathFruitHealth);
      packet.Send(toWho, fromWho);
    }

    public void ReceivePlayerSync(BinaryReader reader)
    {
      this.DeathFruitHealth = (int) reader.ReadByte();
    }

    public virtual void CopyClientState(ModPlayer targetCopy)
    {
      ((FargoPlayer) targetCopy).DeathFruitHealth = this.DeathFruitHealth;
    }

    public virtual void SendClientChanges(ModPlayer clientPlayer)
    {
      if (this.DeathFruitHealth == ((FargoPlayer) clientPlayer).DeathFruitHealth)
        return;
      base.SyncPlayer(-1, Main.myPlayer, false);
    }

    public virtual void ModifyStartingInventory(
      IReadOnlyDictionary<string, List<Item>> itemsByMod,
      bool mediumCoreDeath)
    {
      foreach (string tag in this.tags)
        this.FirstDyeIngredients[tag] = false;
    }

    public virtual void OnEnterWorld() => Fargowiltas.Items.Misc.BattleCry.SyncCry(this.Player);

    public virtual void ResetEffects()
    {
      this.extractSpeed = false;
      this.HasDrawnDebuffLayer = false;
      this.bigSuck = false;
    }

    public virtual void ProcessTriggers(TriggersSet triggersSet)
    {
      if (Fargowiltas.Fargowiltas.HomeKey.JustPressed)
        this.AutoUseMirror();
      if (!Fargowiltas.Fargowiltas.StatKey.JustPressed)
        return;
      if (!Main.playerInventory)
        Main.playerInventory = true;
      Fargowiltas.Fargowiltas.UserInterfaceManager.ToggleStatSheet();
    }

    public virtual void PostUpdateBuffs()
    {
      if (FargoServerConfig.Instance.UnlimitedPotionBuffsOn120)
      {
        foreach (Item obj in this.Player.bank.item)
          FargoGlobalItem.TryUnlimBuff(obj, this.Player);
        foreach (Item obj in this.Player.bank2.item)
          FargoGlobalItem.TryUnlimBuff(obj, this.Player);
      }
      if (!FargoServerConfig.Instance.PiggyBankAcc)
        return;
      foreach (Item obj in this.Player.bank.item)
        FargoGlobalItem.TryPiggyBankAcc(obj, this.Player);
      foreach (Item obj in this.Player.bank2.item)
        FargoGlobalItem.TryPiggyBankAcc(obj, this.Player);
    }

    public virtual void PostUpdateEquips()
    {
      if (!Fargowiltas.Fargowiltas.SwarmActive)
        return;
      this.Player.buffImmune[37] = true;
    }

    public virtual void UpdateDead() => this.StationSoundCooldown = 0;

    public virtual void PostUpdateMiscEffects()
    {
      if ((double) this.ElementalAssemblerNearby > 0.0)
      {
        --this.ElementalAssemblerNearby;
        this.Player.alchemyTable = true;
      }
      if (this.StationSoundCooldown > 0)
        --this.StationSoundCooldown;
      if (this.Player.equippedWings == null)
        this.ResetStatSheetWings();
      this.ForceBiomes();
    }

    public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
      FargoServerConfig instance = FargoServerConfig.Instance;
      if ((double) instance.EnemyDamage == 1.0 && (double) instance.BossDamage == 1.0)
        return;
      if ((double) instance.BossDamage > (double) instance.EnemyDamage && (npc.boss || npc.type == 13 || npc.type == 14 || npc.type == 15 || instance.BossApplyToAllWhenAlive && FargoGlobalNPC.AnyBossAlive()))
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, instance.BossDamage);
      }
      else
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, instance.EnemyDamage);
      }
    }

    public void ResetStatSheetWings()
    {
      this.StatSheetMaxAscentMultiplier = 0.0f;
      this.StatSheetWingSpeed = 0.0f;
      this.CanHover = new bool?();
    }

    private void ForceBiomes()
    {
      if (FargoGlobalNPC.SpecificBossIsAlive(ref FargoGlobalNPC.eaterBoss, 13) && (double) ((Entity) this.Player).Distance(((Entity) Main.npc[FargoGlobalNPC.eaterBoss]).Center) < 3000.0)
        this.Player.ZoneCorrupt = true;
      if (FargoGlobalNPC.SpecificBossIsAlive(ref FargoGlobalNPC.brainBoss, 266) && (double) ((Entity) this.Player).Distance(((Entity) Main.npc[FargoGlobalNPC.brainBoss]).Center) < 3000.0)
        this.Player.ZoneCrimson = true;
      if (FargoGlobalNPC.SpecificBossIsAlive(ref FargoGlobalNPC.plantBoss, 262) && (double) ((Entity) this.Player).Distance(((Entity) Main.npc[FargoGlobalNPC.plantBoss]).Center) < 3000.0 || FargoGlobalNPC.SpecificBossIsAlive(ref FargoGlobalNPC.beeBoss, 222) && (double) ((Entity) this.Player).Distance(((Entity) Main.npc[FargoGlobalNPC.beeBoss]).Center) < 3000.0)
        this.Player.ZoneJungle = true;
      if (!FargoServerConfig.Instance.Fountains)
        return;
      switch (Main.SceneMetrics.ActiveFountainColor)
      {
        case 0:
          this.Player.ZoneBeach = true;
          break;
        case 2:
          this.Player.ZoneCorrupt = true;
          break;
        case 3:
          this.Player.ZoneJungle = true;
          break;
        case 4:
          if (Main.hardMode)
          {
            this.Player.ZoneHallow = true;
            break;
          }
          break;
        case 5:
          this.Player.ZoneSnow = true;
          break;
        case 6:
        case 12:
          this.Player.ZoneDesert = true;
          if ((double) ((Entity) this.Player).Center.Y > 3200.0)
          {
            this.Player.ZoneUndergroundDesert = true;
            break;
          }
          break;
        case 10:
          this.Player.ZoneCrimson = true;
          break;
      }
    }

    public virtual void PostUpdate()
    {
      if (this.autoRevertSelectedItem && this.Player.itemTime == 0 && this.Player.itemAnimation == 0)
      {
        this.Player.selectedItem = this.originalSelectedItem;
        this.autoRevertSelectedItem = false;
      }
      if (!FargoWorld.OverloadedSlimeRain || !Utils.NextBool(Main.rand, 20))
        return;
      this.SlimeRainSpawns();
    }

    public void SlimeRainSpawns()
    {
      int num1 = -3;
      int[] numArray = new int[12]
      {
        535,
        537,
        147,
        184,
        16,
        204,
        71,
        225,
        141,
        81,
        183,
        138
      };
      int num2 = Main.rand.Next(50);
      if (num2 == 0)
        num1 = -4;
      else if (num2 < 20)
        num1 = numArray[Main.rand.Next(numArray.Length)];
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) ((int) ((Entity) this.Player).position.X + Main.rand.Next(-800, 800)), (float) ((int) ((Entity) this.Player).position.Y + Main.rand.Next(-800, -250)));
    }

    public virtual bool PreModifyLuck(ref float luck)
    {
      if (FargoWorld.Matsuri && !Main.IsItRaining && !Main.IsItStorming)
      {
        LanternNight.GenuineLanterns = true;
        LanternNight.ManualLanterns = false;
      }
      return base.PreModifyLuck(ref luck);
    }

    public virtual void ModifyLuck(ref float luck)
    {
      luck += this.luckPotionBoost;
      this.luckPotionBoost = 0.0f;
    }

    public void AutoUseMirror()
    {
      int index1 = -1;
      int index2 = -1;
      int index3 = -1;
      for (int index4 = 0; index4 < this.Player.inventory.Length; ++index4)
      {
        switch (this.Player.inventory[index4].type)
        {
          case 50:
          case 3124:
          case 3199:
          case 5358:
            index3 = index4;
            break;
          case 2350:
            index2 = index4;
            break;
          case 4870:
            index1 = index4;
            break;
        }
      }
      if (index1 != -1)
        this.QuickUseItemAt(index1);
      else if (index2 != -1)
      {
        this.QuickUseItemAt(index2);
      }
      else
      {
        if (index3 == -1)
          return;
        this.QuickUseItemAt(index3);
      }
    }

    public virtual void ModifyMaxStats(out StatModifier health, out StatModifier mana)
    {
      ref StatModifier local = ref health;
      StatModifier statModifier1 = StatModifier.Default;
      statModifier1.Base = (float) -this.DeathFruitHealth;
      StatModifier statModifier2 = statModifier1;
      local = statModifier2;
      mana = StatModifier.Default;
    }

    public void QuickUseItemAt(int index, bool use = true)
    {
      if (this.autoRevertSelectedItem || this.Player.selectedItem == index || this.Player.inventory[index].type == 0)
        return;
      this.originalSelectedItem = this.Player.selectedItem;
      this.autoRevertSelectedItem = true;
      this.Player.selectedItem = index;
      this.Player.controlUseItem = true;
      if (use && CombinedHooks.CanUseItem(this.Player, this.Player.inventory[this.Player.selectedItem]) && ((Entity) this.Player).whoAmI == Main.myPlayer)
        this.Player.ItemCheck();
    }
  }
}
