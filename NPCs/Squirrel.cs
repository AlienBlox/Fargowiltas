// Decompiled with JetBrains decompiler
// Type: Fargowiltas.NPCs.Squirrel
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.Items;
using Fargowiltas.Items.CaughtNPCs;
using Fargowiltas.Items.Misc;
using Fargowiltas.Items.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable enable
namespace Fargowiltas.NPCs
{
  [AutoloadHead]
  public class Squirrel : ModNPC
  {
    private static int shopNum;
    private static bool showCycleShop;
    private static 
    #nullable disable
    Profiles.DefaultNPCProfile NPCProfile;
    private const string ShopName = "Shop";

    private Asset<Texture2D> GlowAsset
    {
      get => ModContent.Request<Texture2D>(this.Texture + "_Glow", (AssetRequestMode) 2);
    }

    private Asset<Texture2D> EyesAsset
    {
      get => ModContent.Request<Texture2D>(this.Texture + "_Eyes", (AssetRequestMode) 2);
    }

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.Type] = 6;
      NPCID.Sets.ExtraFramesCount[this.Type] = 9;
      NPCID.Sets.AttackFrameCount[this.Type] = 4;
      NPCID.Sets.DangerDetectRange[this.Type] = 700;
      NPCID.Sets.AttackType[this.Type] = 0;
      NPCID.Sets.AttackTime[this.Type] = 90;
      NPCID.Sets.AttackAverageChance[this.Type] = 30;
      NPCID.Sets.HatOffsetY[this.Type] = 4;
      NPCID.Sets.CannotSitOnFurniture[this.Type] = true;
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
      ((NPCHappiness) ref happiness2).SetBiomeAffection<UndergroundBiome>((AffectionLevel) -100);
      NPCHappiness happiness3 = this.NPC.Happiness;
      ((NPCHappiness) ref happiness3).SetNPCAffection<LumberJack>((AffectionLevel) 50);
      Squirrel.NPCProfile = new Profiles.DefaultNPCProfile(this.Texture, NPCHeadLoader.GetHeadSlot(this.HeadTexture), this.Texture + "_Party");
    }

    public virtual void SetDefaults()
    {
      this.NPC.townNPC = true;
      this.NPC.friendly = true;
      ((Entity) this.NPC).width = 44;
      ((Entity) this.NPC).height = 42;
      this.NPC.damage = 0;
      this.NPC.defense = 0;
      this.NPC.lifeMax = 100;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit1);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.knockBackResist = 0.25f;
      this.AnimationType = 299;
      this.NPC.aiStyle = 7;
    }

    public virtual ITownNPCProfile TownNPCProfile() => (ITownNPCProfile) Squirrel.NPCProfile;

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.Fargowiltas.Bestiary.Squirrel")
      });
    }

    public virtual List<string> SetNPCNameList()
    {
      return new List<string>()
      {
        "Rick",
        "Acorn",
        "Puff",
        "Coco",
        "Truffle",
        "Furgo",
        "Squeaks"
      };
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      FargoWorld.DownedBools["squirrel"] = true;
      base.OnSpawn(source);
    }

    public virtual void AI() => this.NPC.dontTakeDamage = Main.bloodMoon;

    public virtual bool CanTownNPCSpawn(int numTownNPCs)
    {
      ModItem modItem;
      return !FargoGlobalNPC.AnyBossAlive() && FargoServerConfig.Instance.Squirrel && (FargoWorld.DownedBools["squirrel"] || !Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] && NPC.downedSlimeKing || Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"] && ModContent.TryFind<ModItem>("FargowiltasSouls", "TopHatSquirrelCaught", ref modItem) && ((IEnumerable<Player>) Main.player).Any<Player>((Func<Player, bool>) (p => ((Entity) p).active && p.HasItem(modItem.Type))));
    }

    public virtual string GetChat()
    {
      Mod mod;
      Squirrel.showCycleShop = this.GetSellableItems().Count / 40 > 0 && !Terraria.ModLoader.ModLoader.TryGetMod("ShopExpander", ref mod);
      if (Main.bloodMoon)
        return Squirrel.SquirrelChat("BloodMoon");
      string chat;
      switch (Main.rand.Next(3))
      {
        case 0:
          chat = Squirrel.SquirrelChat("Normal1");
          break;
        case 1:
          chat = Squirrel.SquirrelChat("Normal2");
          break;
        default:
          chat = Squirrel.SquirrelChat("Normal3");
          break;
      }
      return chat;
    }

    public virtual void SetChatButtons(ref string button, ref string button2)
    {
      button = Language.GetTextValue("LegacyInterface.28");
      if (!Squirrel.showCycleShop)
        return;
      ref string local = ref button;
      string str1 = button;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
      interpolatedStringHandler.AppendLiteral(" ");
      interpolatedStringHandler.AppendFormatted<int>(Squirrel.shopNum + 1);
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      string str2 = str1 + stringAndClear;
      local = str2;
      button2 = Language.GetTextValue("Mods.Fargowiltas.NPCs.Mutant.CycleShop");
    }

    public virtual void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
      if (firstButton)
        shopName = "Shop";
      else
        ++Squirrel.shopNum;
      if (Squirrel.shopNum <= this.GetSellableItems().Count / 40)
        return;
      Squirrel.shopNum = 0;
    }

    private static int[] ItemsSoldDirectly
    {
      get
      {
        return new int[17]
        {
          3124,
          5358,
          5437,
          5361,
          5360,
          5359,
          1613,
          1326,
          5000,
          5043,
          ModContent.ItemType<Omnistation>(),
          ModContent.ItemType<Omnistation2>(),
          ModContent.ItemType<CrucibleCosmos>(),
          ModContent.ItemType<ElementalAssembler>(),
          ModContent.ItemType<MultitaskCenter>(),
          ModContent.ItemType<PortableSundial>(),
          ModContent.ItemType<BattleCry>()
        };
      }
    }

    public static SquirrelShopGroup SquirrelSells(Item item, out SquirrelSellType sellType)
    {
      if (item.type == 4956)
      {
        sellType = SquirrelSellType.CraftableMaterialsSold;
        return SquirrelShopGroup.Other;
      }
      if (item.makeNPC != 0 || ((IEnumerable<int>) Squirrel.ItemsSoldDirectly).Contains<int>(item.type))
      {
        sellType = SquirrelSellType.SoldBySquirrel;
        return SquirrelShopGroup.Other;
      }
      if ((item.buffType == 0 || item.type == 4024 ? (FargoGlobalItem.NonBuffPotions.Contains(item.type) ? 1 : 0) : 1) != 0 && item.maxStack >= 30)
      {
        sellType = SquirrelSellType.SoldAtThirtyStack;
        return SquirrelShopGroup.Potion;
      }
      if (Squirrel.IsFargoSoulsItem(item))
      {
        if (((ModType) item.ModItem).Name.EndsWith("Enchant"))
        {
          sellType = SquirrelSellType.SoldBySquirrel;
          return SquirrelShopGroup.Enchant;
        }
        if (((ModType) item.ModItem).Name.EndsWith("Essence"))
        {
          sellType = SquirrelSellType.SoldBySquirrel;
          return SquirrelShopGroup.Essence;
        }
        ModItem modItem1;
        ModItem modItem2;
        if (ModContent.TryFind<ModItem>("FargowiltasSouls", "BionomicCluster", ref modItem1) && modItem1.Type == item.type || ModContent.TryFind<ModItem>("FargowiltasSouls", "HeartoftheMasochist", ref modItem2) && modItem2.Type == item.type)
        {
          sellType = SquirrelSellType.SoldBySquirrel;
          return SquirrelShopGroup.Other;
        }
        if (((ModType) item.ModItem).Name.EndsWith("Force"))
        {
          sellType = SquirrelSellType.SomeMaterialsSold;
          return SquirrelShopGroup.Enchant;
        }
        ModItem modItem3;
        ModItem modItem4;
        ModItem modItem5;
        if (ModContent.TryFind<ModItem>("FargowiltasSouls", "MasochistSoul", ref modItem3) && modItem3.Type == item.type || ModContent.TryFind<ModItem>("FargowiltasSouls", "AeolusBoots", ref modItem4) && item.type == modItem4.Type || ModContent.TryFind<ModItem>("FargowiltasSouls", "ZephyrBoots", ref modItem5) && item.type == modItem5.Type)
        {
          sellType = SquirrelSellType.CraftableMaterialsSold;
          return SquirrelShopGroup.Other;
        }
        if (((ModType) item.ModItem).Name.EndsWith("Soul"))
        {
          foreach (Recipe recipe in ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe => recipe.HasResult(item.type))))
          {
            foreach (Item obj in recipe.requiredItem)
            {
              if (obj.type != 0 && obj.ModItem != null)
              {
                if (((ModType) obj.ModItem).Name.EndsWith("Essence"))
                {
                  sellType = SquirrelSellType.SomeMaterialsSold;
                  return SquirrelShopGroup.Essence;
                }
                if (((ModType) obj.ModItem).Name.EndsWith("Force"))
                {
                  sellType = SquirrelSellType.SomeMaterialsSold;
                  return SquirrelShopGroup.Force;
                }
                if (((ModType) obj.ModItem).Name.EndsWith("Soul"))
                {
                  sellType = SquirrelSellType.SomeMaterialsSold;
                  return SquirrelShopGroup.Soul;
                }
              }
            }
          }
          sellType = SquirrelSellType.SoldBySquirrel;
          return SquirrelShopGroup.Soul;
        }
      }
      sellType = SquirrelSellType.End;
      return SquirrelShopGroup.End;
    }

    private static bool IsFargoSoulsItem(Item item)
    {
      if (item.ModItem == null)
        return false;
      string name = ((ModType) item.ModItem).Mod.Name;
      return name.Equals("FargowiltasSouls") || name.Equals("FargowiltasSoulsDLC");
    }

    private void TryAddItem(
      Item item1,
      Dictionary<SquirrelShopGroup, SortedSet<int>> itemCollections)
    {
      SquirrelSellType sellType;
      SquirrelShopGroup key = Squirrel.SquirrelSells(item1, out sellType);
      switch (sellType)
      {
        case SquirrelSellType.SoldBySquirrel:
          itemCollections[key].Add(item1.type);
          ModItem modItem1;
          if (!ModContent.TryFind<ModItem>("FargowiltasSouls", "WorldShaperSoul", ref modItem1) || item1.type != modItem1.Type)
            break;
          itemCollections[SquirrelShopGroup.Other].Add(5358);
          break;
        case SquirrelSellType.SomeMaterialsSold:
          using (IEnumerator<Recipe> enumerator = ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe => recipe.HasResult(item1.type))).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              foreach (Item obj in enumerator.Current.requiredItem)
              {
                if (obj.ModItem != null && ((ModType) obj.ModItem).Name.EndsWith(key.ToString()))
                  itemCollections[key].Add(obj.type);
                ModItem modItem2;
                if ((obj.type != 4956 || !ModContent.TryFind<ModItem>("FargowiltasSouls", "BerserkerSoul", ref modItem2) ? 0 : (item1.type == modItem2.Type ? 1 : 0)) != 0)
                  itemCollections[SquirrelShopGroup.Other].Add(obj.type);
              }
            }
            break;
          }
        case SquirrelSellType.CraftableMaterialsSold:
          HashSet<int> intSet = new HashSet<int>(((IEnumerable<Recipe>) Main.recipe).SelectMany<Recipe, int>((Func<Recipe, IEnumerable<int>>) (recipe => recipe.requiredItem.Select<Item, int>((Func<Item, int>) (item2 => item2.type)))).Where<int>((Func<int, bool>) (type => type != 0)));
          using (IEnumerator<Recipe> enumerator = ((IEnumerable<Recipe>) Main.recipe).Where<Recipe>((Func<Recipe, bool>) (recipe => recipe.HasResult(item1.type))).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              foreach (Item obj in enumerator.Current.requiredItem)
              {
                if (obj.type != 0 && intSet.Contains(obj.type))
                  itemCollections[key].Add(obj.type);
              }
            }
            break;
          }
        case SquirrelSellType.SoldAtThirtyStack:
          if (item1.stack < 30)
            break;
          itemCollections[key].Add(item1.type);
          break;
      }
    }

    private List<int> GetSellableItems()
    {
      Dictionary<SquirrelShopGroup, SortedSet<int>> dictionary = new Dictionary<SquirrelShopGroup, SortedSet<int>>();
      for (int key = 0; key < 7; ++key)
        dictionary[(SquirrelShopGroup) key] = new SortedSet<int>();
      foreach (Player player in ((IEnumerable<Player>) Main.player).Where<Player>((Func<Player, bool>) (p => ((Entity) p).active)))
      {
        foreach (Item obj in player.inventory)
          this.TryAddItem(obj, dictionary);
        foreach (Item obj in player.armor)
          this.TryAddItem(obj, dictionary);
        foreach (Item obj in player.bank.item)
          this.TryAddItem(obj, dictionary);
        if (player.unlockedBiomeTorches)
          dictionary[SquirrelShopGroup.Other].Add(5043);
      }
      foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.townNPC && CaughtNPCItem.CaughtTownies.ContainsKey(n.type))))
        dictionary[SquirrelShopGroup.Other].Add(CaughtNPCItem.CaughtTownies[npc.type]);
      dictionary[SquirrelShopGroup.Acorn].Add(27);
      dictionary[SquirrelShopGroup.Acorn].Add(4857);
      dictionary[SquirrelShopGroup.Acorn].Add(4852);
      dictionary[SquirrelShopGroup.Acorn].Add(4856);
      dictionary[SquirrelShopGroup.Acorn].Add(4854);
      dictionary[SquirrelShopGroup.Acorn].Add(4855);
      dictionary[SquirrelShopGroup.Acorn].Add(4853);
      dictionary[SquirrelShopGroup.Acorn].Add(4851);
      return dictionary.OrderBy<KeyValuePair<SquirrelShopGroup, SortedSet<int>>, SquirrelShopGroup>((Func<KeyValuePair<SquirrelShopGroup, SortedSet<int>>, SquirrelShopGroup>) (kv => kv.Key)).SelectMany<KeyValuePair<SquirrelShopGroup, SortedSet<int>>, int>((Func<KeyValuePair<SquirrelShopGroup, SortedSet<int>>, IEnumerable<int>>) (kv => (IEnumerable<int>) kv.Value)).ToList<int>();
    }

    public virtual void AddShops() => ((AbstractNPCShop) new NPCShop(this.Type, "Shop")).Register();

    public virtual void ModifyActiveShop(string shopName, Item[] items)
    {
      int index = 0;
      int num1 = 0;
      int num2 = Squirrel.shopNum * 40;
      List<int> sellableItems = this.GetSellableItems();
      ModItem modItem;
      if (Squirrel.shopNum == 0 && ModContent.TryFind<ModItem>("FargowiltasSouls", "TopHatSquirrelCaught", ref modItem))
      {
        items[index] = new Item(modItem.Type, 1, 0)
        {
          shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 100000))
        };
        ++index;
      }
      foreach (int num3 in sellableItems)
      {
        if (++num1 >= num2)
        {
          if (index >= 40)
            break;
          Item obj = new Item(num3, 1, 0);
          bool flag = false;
          int num4;
          if (obj.makeNPC != 0)
          {
            num4 = Item.buyPrice(0, 10, 0, 0);
            if (((IEnumerable<int>) new int[15]
            {
              2673,
              4961,
              2889,
              2890,
              2891,
              4340,
              2892,
              4274,
              2893,
              4362,
              2894,
              4482,
              3564,
              4419,
              2895
            }).Contains<int>(obj.type))
              num4 *= 5;
            else if (obj.ModItem is CaughtNPCItem)
              num4 *= 2;
          }
          else if (num3 == 1326)
          {
            num4 = 250;
            flag = true;
          }
          else
            num4 = obj.value * 2;
          if (flag)
            items[index] = new Item(num3, 1, 0)
            {
              shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, num4)),
              shopSpecialCurrency = CustomCurrencyID.DefenderMedals
            };
          else
            items[index] = new Item(num3, 1, 0)
            {
              shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, num4))
            };
          ++index;
        }
      }
    }

    public virtual bool CanGoToStatue(bool toKingStatue) => toKingStatue;

    public virtual bool UsesPartyHat() => false;

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (!Main.bloodMoon)
        return true;
      Rectangle frame = this.NPC.frame;
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      double num = (double) Main.mouseTextColor / 200.0;
      for (int index = 0; index < 12; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), 4f), Vector2.op_Multiply(Vector2.UnitY, 3f));
        Color red = Color.Red;
        ((Color) ref red).A = (byte) 0;
        Color color = red;
        Texture2D texture2D = ModContent.Request<Texture2D>(this.Texture, (AssetRequestMode) 2).Value;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), screenPos), Vector2.op_Multiply(Vector2.UnitY, this.NPC.gfxOffY)), new Rectangle?(this.NPC.frame), color, this.NPC.rotation, new Vector2((float) (texture2D.Width / 2), (float) (texture2D.Height / 2 / Main.npcFrameCount[this.NPC.type])), this.NPC.scale, spriteEffects, 0.0f);
      }
      return true;
    }

    public virtual void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (!Main.bloodMoon)
        return;
      Rectangle frame = this.NPC.frame;
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY + 3f));
      spriteBatch.Draw(this.EyesAsset.Value, vector2, new Rectangle?(frame), Color.op_Multiply(Color.White, this.NPC.Opacity), this.NPC.rotation, Vector2.op_Division(Utils.Size(frame), 2f), this.NPC.scale, spriteEffects, 0.0f);
    }

    private static string SquirrelChat(string key)
    {
      return Language.GetTextValue("Mods.Fargowiltas.NPCs.Squirrel.Chat." + key);
    }
  }
}
