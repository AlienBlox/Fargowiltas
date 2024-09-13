// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.FargoGlobalItem
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Items.Ammos.Coins;
using Fargowiltas.Items.CaughtNPCs;
using Fargowiltas.Items.Misc;
using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Items;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items
{
  public class FargoGlobalItem : GlobalItem
  {
    private static readonly int[] Hearts = new int[3]
    {
      58,
      1734,
      1867
    };
    private static readonly int[] Stars = new int[3]
    {
      184,
      1735,
      1868
    };
    private bool firstTick = true;
    public static List<int> BuffStations = new List<int>()
    {
      3198,
      2177,
      487,
      2999,
      3814
    };
    private static int[] Informational = new int[29]
    {
      3119,
      15,
      707,
      708,
      16,
      17,
      709,
      18,
      393,
      3084,
      3118,
      3095,
      3102,
      3099,
      486,
      3120,
      3096,
      3037,
      395,
      3122,
      3121,
      3036,
      3123,
      3124,
      5358,
      5437,
      5361,
      5360,
      5359
    };
    private static int[] Construction = new int[8]
    {
      407,
      1923,
      2215,
      2216,
      2214,
      2217,
      3624,
      3061
    };
    public static List<int> NonBuffPotions = new List<int>()
    {
      2350,
      4870,
      2997,
      2351,
      ModContent.ItemType<BigSuckPotion>()
    };

    public virtual bool InstancePerEntity => true;

    private static string ExpandedTooltipLoc(string line)
    {
      return Language.GetTextValue("Mods.Fargowiltas.ExpandedTooltips." + line);
    }

    public virtual GlobalItem Clone(Item item, Item itemClone)
    {
      return ((GlobalType<Item, GlobalItem>) this).Clone(item, itemClone);
    }

    private TooltipLine FountainTooltip(string biome)
    {
      return new TooltipLine(((ModType) this).Mod, "Tooltip0", "[i:909] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("Fountain" + biome) + "]");
    }

    public virtual void PickAmmo(
      Item weapon,
      Item ammo,
      Player player,
      ref int type,
      ref float speed,
      ref StatModifier damage,
      ref float knockback)
    {
      if (weapon.type != 905)
        return;
      if (ammo.type == 71 || ammo.type == ModContent.ItemType<CopperCoinBag>())
        type = 158;
      if (ammo.type == 72 || ammo.type == ModContent.ItemType<SilverCoinBag>())
        type = 159;
      if (ammo.type == 73 || ammo.type == ModContent.ItemType<GoldCoinBag>())
        type = 160;
      if (ammo.type != 74 && ammo.type != ModContent.ItemType<PlatinumCoinBag>())
        return;
      type = 161;
    }

    public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
      FargoServerConfig instance = FargoServerConfig.Instance;
      if (!FargoClientConfig.Instance.ExpandedTooltips)
        return;
      List<FargoGlobalItem.ShopTooltip> source1 = new List<FargoGlobalItem.ShopTooltip>();
      foreach (AbstractNPCShop allShop in NPCShopDatabase.AllShops)
      {
        AbstractNPCShop shop = allShop;
        foreach (AbstractNPCShop.Entry entry in shop.ActiveEntries.Where<AbstractNPCShop.Entry>((Func<AbstractNPCShop.Entry, bool>) (e => !e.Item.IsAir && e.Item.type == item.type)))
        {
          Item obj = (Item) null;
          using (IEnumerator<KeyValuePair<int, Item>> enumerator = ContentSamples.ItemsByType.Where<KeyValuePair<int, Item>>((Func<KeyValuePair<int, Item>, bool>) (i => i.Value.ModItem != null && i.Value.ModItem is CaughtNPCItem modItem && modItem.AssociatedNpcId == shop.NpcType)).GetEnumerator())
          {
            if (enumerator.MoveNext())
              obj = enumerator.Current.Value;
          }
          if (obj == null)
            obj = item;
          string str1 = "";
          int num = 0;
          foreach (Condition condition in entry.Conditions)
          {
            string str2 = num > 0 ? ", " : "";
            str1 = str1 + str2 + condition.Description.Value;
            ++num;
          }
          string conditionLine = num > 0 ? ": " + str1 : "";
          string npcName = ContentSamples.NpcsByNetId[shop.NpcType].FullName;
          if (!source1.Any<FargoGlobalItem.ShopTooltip>((Func<FargoGlobalItem.ShopTooltip, bool>) (t => t.NpcNames.Any<string>((Func<string, bool>) (n => n == npcName)) && t.Condition == conditionLine)))
          {
            bool flag = false;
            foreach (FargoGlobalItem.ShopTooltip shopTooltip in source1)
            {
              if (shopTooltip.Condition == conditionLine && !shopTooltip.NpcNames.Contains(npcName))
              {
                shopTooltip.NpcNames.Add(npcName);
                shopTooltip.NpcItemIDs.Add(obj.type);
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              source1.Add(new FargoGlobalItem.ShopTooltip()
              {
                NpcItemIDs = {
                  obj.type
                },
                NpcNames = {
                  npcName
                },
                Condition = conditionLine
              });
              break;
            }
            break;
          }
        }
      }
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      foreach (FargoGlobalItem.ShopTooltip shopTooltip in source1)
      {
        IEnumerable<int> source2 = shopTooltip.NpcItemIDs.Where<int>((Func<int, bool>) (i => i != item.type));
        List<int> list = source2 != null ? source2.ToList<int>() : (List<int>) null;
        int type = item.type;
        if (list.Any<int>())
        {
          int index = (int) ((double) Main.GlobalTimeWrappedHourly * 60.0) / 60 % list.Count;
          type = list[index];
        }
        string str3 = "";
        int num = 0;
        foreach (string npcName in shopTooltip.NpcNames)
        {
          string str4 = num > 0 ? ", " : "";
          str3 = str3 + str4 + npcName;
          ++num;
        }
        if (num > 5)
          str3 = FargoGlobalItem.ExpandedTooltipLoc("SeveralVendors");
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 4);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(type);
        interpolatedStringHandler.AppendLiteral("] [c/AAAAAA:");
        interpolatedStringHandler.AppendFormatted(FargoGlobalItem.ExpandedTooltipLoc("SoldBy"));
        interpolatedStringHandler.AppendLiteral(" ");
        interpolatedStringHandler.AppendFormatted(str3);
        interpolatedStringHandler.AppendFormatted(shopTooltip.Condition);
        interpolatedStringHandler.AppendLiteral("]");
        TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "TooltipNPCSold", interpolatedStringHandler.ToStringAndClear());
        tooltips.Add(tooltipLine);
      }
      switch (item.type)
      {
        case 909:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Ocean"));
            break;
          }
          break;
        case 910:
        case 4417:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Desert"));
            break;
          }
          break;
        case 940:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Jungle"));
            break;
          }
          break;
        case 941:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Snow"));
            break;
          }
          break;
        case 942:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Corruption"));
            break;
          }
          break;
        case 943:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Crimson"));
            break;
          }
          break;
        case 944:
          if (instance.Fountains)
          {
            tooltips.Add(this.FountainTooltip("Hallow"));
            break;
          }
          break;
        case 1991:
        case 3183:
        case 4821:
          if (instance.CatchNPCs)
          {
            tooltips.Add(new TooltipLine(((ModType) this).Mod, "Tooltip0", "[i:1991] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("CatchNPCs") + "]"));
            break;
          }
          break;
      }
      if (instance.ExtraLures)
      {
        if (item.type == 2354)
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Tooltip1", "[i:2373] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("ExtraLure1") + "]");
          tooltips.Insert(3, tooltipLine);
        }
        if (item.type == 2292 || item.type == 2293 || item.type == 2421 || item.type == 4442 || item.type == 4325)
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Tooltip1", "[i:2373] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("Lures2") + "]");
          tooltips.Insert(3, tooltipLine);
        }
        if (item.type == 2295 || item.type == 2296)
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Tooltip1", "[i:2373] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("Lures3") + "]");
          tooltips.Insert(3, tooltipLine);
        }
        if (item.type == 2294 || item.type == 2422)
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "Tooltip1", "[i:2373] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("Lures5") + "]");
          tooltips.Insert(3, tooltipLine);
        }
      }
      if (instance.TorchGodEX && item.type == 5043)
      {
        TooltipLine tooltipLine1 = new TooltipLine(((ModType) this).Mod, "TooltipTorchGod1", "[i:5043] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("AutoTorch") + "]");
        tooltips.Add(tooltipLine1);
        TooltipLine tooltipLine2 = new TooltipLine(((ModType) this).Mod, "TooltipTorchGod2", "[i:5043] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("TrueTorchLuck") + "]");
        tooltips.Add(tooltipLine2);
      }
      if (instance.UnlimitedPotionBuffsOn120 && item.maxStack > 1)
      {
        if (item.buffType != 0)
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "TooltipUnlim", "[i:87] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("UnlimitedBuff30") + "]");
          tooltips.Add(tooltipLine);
        }
        else if (item.bait > 0)
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "TooltipUnlim", "[i:5139] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("UnlimitedUse30") + "]");
          tooltips.Add(tooltipLine);
        }
        else if (FargoGlobalItem.BuffStations.Contains(item.type))
        {
          TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "TooltipUnlim", "[i:87] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("PermanentEffectNearby") + "]");
          tooltips.Add(tooltipLine);
        }
      }
      if (instance.PiggyBankAcc && (((IEnumerable<int>) FargoGlobalItem.Informational).Contains<int>(item.type) || ((IEnumerable<int>) FargoGlobalItem.Construction).Contains<int>(item.type)))
      {
        TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "TooltipUnlim", "[i:87] [c/AAAAAA:" + FargoGlobalItem.ExpandedTooltipLoc("WorksFromBanks") + "]");
        tooltips.Add(tooltipLine);
      }
      SquirrelSellType sellType;
      if (Squirrel.SquirrelSells(item, out sellType) == SquirrelShopGroup.End)
        return;
      Mod mod = ((ModType) this).Mod;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 2);
      interpolatedStringHandler.AppendLiteral("[i:");
      interpolatedStringHandler.AppendFormatted<int>(CaughtNPCItem.CaughtTownies[ModContent.NPCType<Squirrel>()]);
      interpolatedStringHandler.AppendLiteral("] [c/AAAAAA:");
      interpolatedStringHandler.AppendFormatted(FargoGlobalItem.ExpandedTooltipLoc(sellType.ToString()));
      interpolatedStringHandler.AppendLiteral("]");
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      TooltipLine tooltipLine3 = new TooltipLine(mod, "TooltipSquirrel", stringAndClear);
      tooltips.Add(tooltipLine3);
    }

    public virtual void SetDefaults(Item item)
    {
      if (!FargoServerConfig.Instance.IncreaseMaxStack || item.maxStack <= 10 || item.maxStack == 100 || item.type >= 71 && item.type <= 74)
        return;
      item.maxStack = 9999;
    }

    public virtual void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
      switch (item.type)
      {
        case 2334:
          if (!Main.remixWorld && !Main.zenithWorld)
          {
            ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(40, new int[4]
            {
              280,
              281,
              3069,
              284
            }));
            break;
          }
          ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(40, new int[3]
          {
            280,
            281,
            284
          }));
          break;
        case 2336:
          ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(10, new int[10]
          {
            49,
            50,
            53,
            55,
            975,
            930,
            54,
            906,
            857,
            934
          }));
          ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(3064, 20, 1, 1));
          break;
        case 3318:
          ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(1309, 25, 1, 1));
          break;
      }
    }

    public virtual void PostUpdate(Item item)
    {
      if (FargoServerConfig.Instance.Halloween != SeasonSelections.AlwaysOn || FargoServerConfig.Instance.Christmas != SeasonSelections.AlwaysOn || !this.firstTick)
        return;
      if (Array.IndexOf<int>(FargoGlobalItem.Hearts, item.type) >= 0)
        item.type = FargoGlobalItem.Hearts[Main.rand.Next(FargoGlobalItem.Hearts.Length)];
      if (Array.IndexOf<int>(FargoGlobalItem.Stars, item.type) >= 0)
        item.type = FargoGlobalItem.Stars[Main.rand.Next(FargoGlobalItem.Stars.Length)];
      this.firstTick = false;
    }

    public virtual bool CanUseItem(Item item, Player player)
    {
      if (item.type == 424 || item.type == 1103 || item.type == 3347)
      {
        if (FargoServerConfig.Instance.ExtractSpeed && player.GetModPlayer<FargoPlayer>().extractSpeed)
        {
          item.useTime = 2;
          item.useAnimation = 3;
        }
        else
        {
          item.useTime = 10;
          item.useAnimation = 15;
        }
      }
      return base.CanUseItem(item, player);
    }

    public static void TryUnlimBuff(Item item, Player player)
    {
      if (item.IsAir || !FargoServerConfig.Instance.UnlimitedPotionBuffsOn120 || item.stack < 30 || item.buffType == 0)
        return;
      player.AddBuff(item.buffType, 2, true, false);
      if (item.type == 4478)
      {
        player.GetModPlayer<FargoPlayer>().luckPotionBoost = Math.Max(player.GetModPlayer<FargoPlayer>().luckPotionBoost, 0.1f);
      }
      else
      {
        if (item.type != 4479)
          return;
        player.GetModPlayer<FargoPlayer>().luckPotionBoost = Math.Max(player.GetModPlayer<FargoPlayer>().luckPotionBoost, 0.2f);
      }
    }

    public static void TryPiggyBankAcc(Item item, Player player)
    {
      if (item.IsAir || item.maxStack > 1 || !FargoServerConfig.Instance.PiggyBankAcc)
        return;
      if (((IEnumerable<int>) FargoGlobalItem.Informational).Contains<int>(item.type))
      {
        player.RefreshInfoAccsFromItemType(item);
      }
      else
      {
        if (!((IEnumerable<int>) FargoGlobalItem.Construction).Contains<int>(item.type))
          return;
        player.ApplyEquipFunctional(item, true);
      }
    }

    public virtual void UpdateInventory(Item item, Player player)
    {
      FargoGlobalItem.TryUnlimBuff(item, player);
    }

    public virtual void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
      if (item.type != 576 || Main.curMusic <= 0 || Main.curMusic > 41)
        return;
      int num;
      switch (Main.curMusic)
      {
        case 1:
          num = 562;
          break;
        case 2:
          num = 563;
          break;
        case 3:
          num = 564;
          break;
        case 4:
          num = 566;
          break;
        case 5:
          num = 567;
          break;
        case 6:
          num = 565;
          break;
        case 7:
          num = 568;
          break;
        case 8:
          num = 569;
          break;
        case 9:
          num = 571;
          break;
        case 10:
          num = 570;
          break;
        case 11:
          num = 573;
          break;
        case 12:
          num = 572;
          break;
        case 13:
          num = 574;
          break;
        case 28:
          num = 1963;
          break;
        case 29:
          num = 1610;
          break;
        case 30:
          num = 1963;
          break;
        case 31:
          num = 1964;
          break;
        case 32:
          num = 1965;
          break;
        case 33:
          num = 2742;
          break;
        case 34:
          num = 3370;
          break;
        case 35:
          num = 3236;
          break;
        case 36:
          num = 3237;
          break;
        case 37:
          num = 3235;
          break;
        case 38:
          num = 3044;
          break;
        case 39:
          num = 3371;
          break;
        case 40:
          num = 3796;
          break;
        case 41:
          num = 3869;
          break;
        default:
          num = 1596 + Main.curMusic - 14;
          break;
      }
      for (int index = 0; index < player.armor.Length; ++index)
      {
        Item obj = player.armor[index];
        if (obj.accessory && obj.type == item.type)
        {
          player.armor[index].SetDefaults(num, false, (ItemVariant) null);
          break;
        }
      }
    }

    public virtual bool CanBeConsumedAsAmmo(Item ammo, Item weapon, Player player)
    {
      return !FargoServerConfig.Instance.UnlimitedAmmo || !Main.hardMode || ammo.ammo == 0 || ammo.stack < 3996;
    }

    public virtual bool? CanConsumeBait(Player player, Item bait)
    {
      return FargoServerConfig.Instance.UnlimitedPotionBuffsOn120 && bait.stack >= 30 ? new bool?(false) : base.CanConsumeBait(player, bait);
    }

    public virtual bool ConsumeItem(Item item, Player player)
    {
      return (!FargoServerConfig.Instance.UnlimitedConsumableWeapons || !Main.hardMode || item.damage <= 0 || item.ammo != 0 || item.stack < 3996) && (!FargoServerConfig.Instance.UnlimitedPotionBuffsOn120 || item.buffType <= 0 && !FargoGlobalItem.NonBuffPotions.Contains(item.type) || item.stack < 30 && !((IEnumerable<Item>) player.inventory).Any<Item>((Func<Item, bool>) (i => i.type == item.type && !i.IsAir && i.stack >= 30)));
    }

    public virtual bool OnPickup(Item item, Player player)
    {
      string key = "";
      switch (item.type)
      {
        case 1107:
          key = "TealMushroom";
          break;
        case 1108:
          key = "GreenMushroom";
          break;
        case 1109:
          key = "SkyBlueFlower";
          break;
        case 1110:
          key = "YellowMarigold";
          break;
        case 1111:
          key = "BlueBerries";
          break;
        case 1112:
          key = "LimeKelp";
          break;
        case 1113:
          key = "PinkPricklyPear";
          break;
        case 1114:
          key = "OrangeBloodroot";
          break;
        case 1115:
          key = "RedHusk";
          break;
        case 1116:
          key = "CyanHusk";
          break;
        case 1117:
          key = "VioletHusk";
          break;
        case 1118:
          key = "PurpleMucos";
          break;
        case 1119:
          key = "BlackInk";
          break;
      }
      if (key != "")
        player.GetModPlayer<FargoPlayer>().FirstDyeIngredients[key] = true;
      return base.OnPickup(item, player);
    }

    public virtual bool CanAccessoryBeEquippedWith(
      Item equippedItem,
      Item incomingItem,
      Player player)
    {
      if (equippedItem.wingSlot != 0 && incomingItem.wingSlot != 0)
        player.GetModPlayer<FargoPlayer>().ResetStatSheetWings();
      return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
    }

    public virtual void VerticalWingSpeeds(
      Item item,
      Player player,
      ref float ascentWhenFalling,
      ref float ascentWhenRising,
      ref float maxCanAscendMultiplier,
      ref float maxAscentMultiplier,
      ref float constantAscend)
    {
      player.GetModPlayer<FargoPlayer>().StatSheetMaxAscentMultiplier = maxAscentMultiplier;
      player.GetModPlayer<FargoPlayer>().CanHover = new bool?(ArmorIDs.Wing.Sets.Stats[item.wingSlot].HasDownHoverStats || ArmorIDs.Wing.Sets.Stats[player.wingsLogic].HasDownHoverStats);
    }

    public virtual void HorizontalWingSpeeds(
      Item item,
      Player player,
      ref float speed,
      ref float acceleration)
    {
      player.GetModPlayer<FargoPlayer>().StatSheetWingSpeed = speed;
    }

    public virtual void GrabRange(Item item, Player player, ref int grabRange)
    {
      if (!player.GetFargoPlayer().bigSuck)
        return;
      grabRange += 144000;
    }

    public virtual bool GrabStyle(Item item, Player player)
    {
      if (player.GetFargoPlayer().bigSuck)
      {
        Item obj1 = item;
        ((Entity) obj1).position = Vector2.op_Addition(((Entity) obj1).position, Vector2.op_Division(Vector2.op_Subtraction(player.MountedCenter, ((Entity) item).Center), 15f));
        Item obj2 = item;
        ((Entity) obj2).position = Vector2.op_Addition(((Entity) obj2).position, Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition));
      }
      return base.GrabStyle(item, player);
    }

    internal class ShopTooltip
    {
      public List<int> NpcItemIDs = new List<int>();
      public List<string> NpcNames = new List<string>();
      public string Condition;
    }
  }
}
