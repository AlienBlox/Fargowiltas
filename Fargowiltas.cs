// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Fargowiltas
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowilta;
using Fargowiltas.Common.Configs;
using Fargowiltas.Items.CaughtNPCs;
using Fargowiltas.Items.Misc;
using Fargowiltas.Items.Tiles;
using Fargowiltas.NPCs;
using Fargowiltas.Projectiles;
using Fargowiltas.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.Events;
using Terraria.GameContent.Items;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas
{
  public class Fargowiltas : Mod
  {
    internal static MutantSummonTracker summonTracker;
    internal static DevianttDialogueTracker dialogueTracker;
    public static ModKeybind HomeKey;
    public static ModKeybind StatKey;
    public static ModKeybind DashKey;
    public static ModKeybind SetBonusKey;
    private UIManager _userInterfaceManager;
    internal static bool SwarmActive;
    internal static int SwarmKills;
    internal static int SwarmTotal;
    internal static int SwarmSpawned;
    internal static Dictionary<string, bool> ModLoaded;
    internal static Dictionary<int, string> ModRareEnemies = new Dictionary<int, string>();
    public List<StatSheetUI.Stat> ModStats;
    public List<StatSheetUI.PermaUpgrade> PermaUpgrades;
    private string[] mods;
    internal static Fargowiltas.Fargowiltas Instance;

    public static UIManager UserInterfaceManager => Fargowiltas.Fargowiltas.Instance._userInterfaceManager;

    public virtual uint ExtraPlayerBuffSlots => FargoServerConfig.Instance.ExtraBuffSlots;

    public virtual void Load()
    {
      Fargowiltas.Fargowiltas.Instance = this;
      this.ModStats = new List<StatSheetUI.Stat>();
      this.PermaUpgrades = new List<StatSheetUI.PermaUpgrade>()
      {
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5337], (Func<bool>) (() => Main.LocalPlayer.usedAegisCrystal)),
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5338], (Func<bool>) (() => Main.LocalPlayer.usedAegisFruit)),
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5339], (Func<bool>) (() => Main.LocalPlayer.usedArcaneCrystal)),
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5342], (Func<bool>) (() => Main.LocalPlayer.usedAmbrosia)),
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5341], (Func<bool>) (() => Main.LocalPlayer.usedGummyWorm)),
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5340], (Func<bool>) (() => Main.LocalPlayer.usedGalaxyPearl)),
        new StatSheetUI.PermaUpgrade(ContentSamples.ItemsByType[5326], (Func<bool>) (() => Main.LocalPlayer.ateArtisanBread))
      };
      Fargowiltas.Fargowiltas.summonTracker = new MutantSummonTracker();
      Fargowiltas.Fargowiltas.dialogueTracker = new DevianttDialogueTracker();
      Fargowiltas.Fargowiltas.dialogueTracker.AddVanillaDialogue();
      Fargowiltas.Fargowiltas.HomeKey = KeybindLoader.RegisterKeybind((Mod) this, "Home", "Home");
      Fargowiltas.Fargowiltas.StatKey = KeybindLoader.RegisterKeybind((Mod) this, "Stat", "RightShift");
      Fargowiltas.Fargowiltas.DashKey = KeybindLoader.RegisterKeybind((Mod) this, "Dash", "C");
      Fargowiltas.Fargowiltas.SetBonusKey = KeybindLoader.RegisterKeybind((Mod) this, "SetBonus", "V");
      this._userInterfaceManager = new UIManager();
      this._userInterfaceManager.LoadUI();
      this.mods = new string[6]
      {
        "FargowiltasSouls",
        "FargowiltasSoulsDLC",
        "ThoriumMod",
        "CalamityMod",
        "MagicStorage",
        "WikiThis"
      };
      Fargowiltas.Fargowiltas.ModLoaded = new Dictionary<string, bool>();
      foreach (string mod in this.mods)
        Fargowiltas.Fargowiltas.ModLoaded.Add(mod, false);
      CaughtNPCItem.RegisterItems();
      ItemID.Sets.BannerStrength = ItemID.Sets.Factory.CreateCustomSet<ItemID.BannerEffect>(new ItemID.BannerEffect(1f), Array.Empty<object>());
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.DoCommonDashHandle += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C0\u003E__OnVanillaDash ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C0\u003E__OnVanillaDash = new On_Player.hook_DoCommonDashHandle((object) null, __methodptr(OnVanillaDash)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.KeyDoubleTap += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C1\u003E__OnVanillaDoubleTapSetBonus ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C1\u003E__OnVanillaDoubleTapSetBonus = new On_Player.hook_KeyDoubleTap((object) null, __methodptr(OnVanillaDoubleTapSetBonus)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.KeyHoldDown += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C2\u003E__OnVanillaHoldSetBonus ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C2\u003E__OnVanillaHoldSetBonus = new On_Player.hook_KeyHoldDown((object) null, __methodptr(OnVanillaHoldSetBonus)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Recipe.FindRecipes += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C3\u003E__FindRecipes_ElementalAssemblerGraveyardHack ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C3\u003E__FindRecipes_ElementalAssemblerGraveyardHack = new On_Recipe.hook_FindRecipes((object) null, __methodptr(FindRecipes_ElementalAssemblerGraveyardHack)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_WorldGen.CountTileTypesInArea += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C4\u003E__CountTileTypesInArea_PurityTotemHack ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C4\u003E__CountTileTypesInArea_PurityTotemHack = new On_WorldGen.hook_CountTileTypesInArea((object) null, __methodptr(CountTileTypesInArea_PurityTotemHack)));
      // ISSUE: method pointer
      On_SceneMetrics.ExportTileCountsToMain += new On_SceneMetrics.hook_ExportTileCountsToMain((object) this, __methodptr(ExportTileCountsToMain_PurityTotemHack));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.HasUnityPotion += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C5\u003E__OnHasUnityPotion ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C5\u003E__OnHasUnityPotion = new On_Player.hook_HasUnityPotion((object) null, __methodptr(OnHasUnityPotion)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.TakeUnityPotion += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C6\u003E__OnTakeUnityPotion ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C6\u003E__OnTakeUnityPotion = new On_Player.hook_TakeUnityPotion((object) null, __methodptr(OnTakeUnityPotion)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.DropTombstone += Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C7\u003E__DisableTombstones ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C7\u003E__DisableTombstones = new On_Player.hook_DropTombstone((object) null, __methodptr(DisableTombstones)));
    }

    private static IEnumerable<Item> GetWormholes(Player self)
    {
      return ((IEnumerable<Item>) self.inventory).Concat<Item>((IEnumerable<Item>) self.bank.item).Concat<Item>((IEnumerable<Item>) self.bank2.item).Where<Item>((Func<Item, bool>) (x => x.type == 2997));
    }

    private static void OnTakeUnityPotion(On_Player.orig_TakeUnityPotion orig, Player self)
    {
      List<Item> list = Fargowiltas.Fargowiltas.GetWormholes(self).ToList<Item>();
      if (FargoServerConfig.Instance.UnlimitedPotionBuffsOn120 && list.Select<Item, int>((Func<Item, int>) (x => x.stack)).Sum() >= 30)
        return;
      Item obj = list.First<Item>();
      --obj.stack;
      if (obj.stack > 0)
        return;
      obj.SetDefaults(0, false, (ItemVariant) null);
    }

    private static void DisableTombstones(
      On_Player.orig_DropTombstone orig,
      Player self,
      long coinsOwned,
      NetworkText deathText,
      int hitDirection)
    {
      if (FargoServerConfig.Instance.DisableTombstones)
        return;
      orig.Invoke(self, coinsOwned, deathText, hitDirection);
    }

    private static bool OnHasUnityPotion(On_Player.orig_HasUnityPotion orig, Player self)
    {
      return Fargowiltas.Fargowiltas.GetWormholes(self).Select<Item, int>((Func<Item, int>) (x => x.stack)).Sum() > 0;
    }

    private static void FindRecipes_ElementalAssemblerGraveyardHack(
      On_Recipe.orig_FindRecipes orig,
      bool canDelayCheck)
    {
      bool zoneGraveyard = Main.LocalPlayer.ZoneGraveyard;
      if (!Main.gameMenu && ((Entity) Main.LocalPlayer).active && (double) Main.LocalPlayer.GetModPlayer<FargoPlayer>().ElementalAssemblerNearby > 0.0)
        Main.LocalPlayer.ZoneGraveyard = true;
      orig.Invoke(canDelayCheck);
      Main.LocalPlayer.ZoneGraveyard = zoneGraveyard;
    }

    private static void CountTileTypesInArea_PurityTotemHack(
      On_WorldGen.orig_CountTileTypesInArea orig,
      int[] tileTypeCounts,
      int startX,
      int endX,
      int startY,
      int endY)
    {
      orig.Invoke(tileTypeCounts, startX, endX, startY, endY);
      if (tileTypeCounts[ModContent.TileType<PurityTotemSheet>()] <= 0)
        return;
      tileTypeCounts[27] += 1800;
    }

    private void ExportTileCountsToMain_PurityTotemHack(
      On_SceneMetrics.orig_ExportTileCountsToMain orig,
      SceneMetrics self)
    {
      orig.Invoke(self);
      if (self.GetTileCount((ushort) ModContent.TileType<PurityTotemSheet>()) <= 0)
        return;
      self.BloodTileCount = Math.Max(self.BloodTileCount - 9000, 0);
      self.EvilTileCount = Math.Max(self.EvilTileCount - 9000, 0);
      self.GraveyardTileCount = Math.Max(self.GraveyardTileCount - 9000, 0);
      if (self.GetTileCount((ushort) 27) > 0)
        self.HasSunflower = true;
    }

    public virtual void Unload()
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.DoCommonDashHandle -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C0\u003E__OnVanillaDash ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C0\u003E__OnVanillaDash = new On_Player.hook_DoCommonDashHandle((object) null, __methodptr(OnVanillaDash)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.KeyDoubleTap -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C1\u003E__OnVanillaDoubleTapSetBonus ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C1\u003E__OnVanillaDoubleTapSetBonus = new On_Player.hook_KeyDoubleTap((object) null, __methodptr(OnVanillaDoubleTapSetBonus)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.KeyHoldDown -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C2\u003E__OnVanillaHoldSetBonus ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C2\u003E__OnVanillaHoldSetBonus = new On_Player.hook_KeyHoldDown((object) null, __methodptr(OnVanillaHoldSetBonus)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Recipe.FindRecipes -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C3\u003E__FindRecipes_ElementalAssemblerGraveyardHack ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C3\u003E__FindRecipes_ElementalAssemblerGraveyardHack = new On_Recipe.hook_FindRecipes((object) null, __methodptr(FindRecipes_ElementalAssemblerGraveyardHack)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_WorldGen.CountTileTypesInArea -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C4\u003E__CountTileTypesInArea_PurityTotemHack ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C4\u003E__CountTileTypesInArea_PurityTotemHack = new On_WorldGen.hook_CountTileTypesInArea((object) null, __methodptr(CountTileTypesInArea_PurityTotemHack)));
      // ISSUE: method pointer
      On_SceneMetrics.ExportTileCountsToMain -= new On_SceneMetrics.hook_ExportTileCountsToMain((object) this, __methodptr(ExportTileCountsToMain_PurityTotemHack));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.HasUnityPotion -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C5\u003E__OnHasUnityPotion ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C5\u003E__OnHasUnityPotion = new On_Player.hook_HasUnityPotion((object) null, __methodptr(OnHasUnityPotion)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.TakeUnityPotion -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C6\u003E__OnTakeUnityPotion ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C6\u003E__OnTakeUnityPotion = new On_Player.hook_TakeUnityPotion((object) null, __methodptr(OnTakeUnityPotion)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      On_Player.DropTombstone -= Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C7\u003E__DisableTombstones ?? (Fargowiltas.Fargowiltas.\u003C\u003EO.\u003C7\u003E__DisableTombstones = new On_Player.hook_DropTombstone((object) null, __methodptr(DisableTombstones)));
      Fargowiltas.Fargowiltas.summonTracker = (MutantSummonTracker) null;
      Fargowiltas.Fargowiltas.dialogueTracker = (DevianttDialogueTracker) null;
      Fargowiltas.Fargowiltas.HomeKey = (ModKeybind) null;
      Fargowiltas.Fargowiltas.StatKey = (ModKeybind) null;
      this.mods = (string[]) null;
      Fargowiltas.Fargowiltas.ModLoaded = (Dictionary<string, bool>) null;
      Fargowiltas.Fargowiltas.Instance = (Fargowiltas.Fargowiltas) null;
    }

    public virtual void PostSetupContent()
    {
      try
      {
        foreach (string mod1 in this.mods)
        {
          Mod mod2;
          Fargowiltas.Fargowiltas.ModLoaded[mod1] = Terraria.ModLoader.ModLoader.TryGetMod(mod1, ref mod2);
        }
      }
      catch (Exception ex)
      {
        this.Logger.Error((object) ("Fargowiltas PostSetupContent Error: " + ex.StackTrace + ex.Message));
      }
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("Wikithis", ref mod) || Main.dedServ)
        return;
      mod.Call(new object[3]
      {
        (object) "AddModURL",
        (object) this,
        (object) "https://fargosmods.wiki.gg/wiki/{}"
      });
    }

    public virtual object Call(params object[] args)
    {
      try
      {
        string s = args[0].ToString();
        // ISSUE: reference to a compiler-generated method
        switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(s))
        {
          case 744977731:
            if (s == "AddPermaUpgrade")
            {
              if (args[1].GetType() != typeof (Item))
                throw new Exception("Call Error (Fargo Mutant Mod AddStat): args[1] must be of type Item");
              if (args[2].GetType() != typeof (Func<bool>))
                throw new Exception("Call Error (Fargo Mutant Mod AddStat): args[2] must be of type Func<bool>");
              this.PermaUpgrades.Add(new StatSheetUI.PermaUpgrade((Item) args[1], (Func<bool>) args[2]));
              break;
            }
            break;
          case 1512222350:
            if (s == "AddEvilAltar")
            {
              if (args[1].GetType() == typeof (int))
              {
                int index = (int) args[1];
                FargoSets.Tiles.EvilAltars[index] = true;
                break;
              }
              break;
            }
            break;
          case 1902122070:
            if (s == "AddStat")
            {
              if (args[1].GetType() != typeof (int))
                throw new Exception("Call Error (Fargo Mutant Mod AddStat): args[1] must be of type int");
              if (args[2].GetType() != typeof (Func<string>))
                throw new Exception("Call Error (Fargo Mutant Mod AddStat): args[2] must be of type Func<string>");
              this.ModStats.Add(new StatSheetUI.Stat((int) args[1], (Func<string>) args[2]));
              break;
            }
            break;
          case 1952603938:
            if (s == "LowRenderProj")
            {
              ((Projectile) args[1]).GetGlobalProjectile<FargoGlobalProjectile>().lowRender = true;
              break;
            }
            break;
          case 2306174137:
            if (s == "AddIndestructibleWallType")
            {
              if (args[1].GetType() == typeof (int))
              {
                int index = (int) args[1];
                FargoSets.Walls.InstaCannotDestroy[index] = true;
                break;
              }
              break;
            }
            break;
          case 2953053857:
            if (s == "SwarmActive")
              return (object) Fargowiltas.Fargowiltas.SwarmActive;
            break;
          case 3487087213:
            if (s == "DoubleTapDashDisabled")
              return (object) FargoClientConfig.Instance.DoubleTapDashDisabled;
            break;
          case 3723948151:
            if (s == "AddSummon")
            {
              if (Fargowiltas.Fargowiltas.summonTracker.SummonsFinalized)
                throw new Exception("Call Error: Summons must be added before AddRecipes");
              int itemId;
              int index;
              if (args[2].GetType() == typeof (string))
              {
                itemId = ModContent.Find<ModItem>(Convert.ToString(args[2]), Convert.ToString(args[3])).Type;
                index = 4;
              }
              else
              {
                itemId = Convert.ToInt32(args[2]);
                index = 3;
              }
              Fargowiltas.Fargowiltas.summonTracker.AddSummon(Convert.ToSingle(args[1]), itemId, args[index] as Func<bool>, Convert.ToInt32(args[index + 1]));
              break;
            }
            break;
          case 3756949764:
            if (s == "AddDevianttHelpDialogue")
            {
              if (args[4].GetType() == typeof (string) && args[4].ToString().Length > 0)
              {
                Fargowiltas.Fargowiltas.dialogueTracker.AddDialogue(args[1] as string, (byte) args[2], args[3] as Predicate<string>, args[4] as string);
                break;
              }
              Fargowiltas.Fargowiltas.dialogueTracker.AddDialogue(args[1] as string, (byte) args[2], args[3] as Predicate<string>);
              break;
            }
            break;
          case 3829145738:
            if (s == "AddIndestructibleRectangle")
            {
              if (args[1].GetType() == typeof (Rectangle))
              {
                Rectangle rectangle = (Rectangle) args[1];
                FargoGlobalProjectile.CannotDestroyRectangle.Add(rectangle);
                break;
              }
              break;
            }
            break;
          case 3955375821:
            if (s == "AddIndestructibleTileType")
            {
              if (args[1].GetType() == typeof (int))
              {
                int index = (int) args[1];
                FargoSets.Tiles.InstaCannotDestroy[index] = true;
                break;
              }
              break;
            }
            break;
        }
      }
      catch (Exception ex)
      {
        this.Logger.Error((object) ("Call Error: " + ex.StackTrace + ex.Message));
      }
      return base.Call(args);
    }

    public virtual void HandlePacket(BinaryReader reader, int whoAmI)
    {
      byte msg = reader.ReadByte();
      switch (msg)
      {
        case 0:
          FargoNet.HandlePacket(reader, msg);
          break;
        case 1:
          if (whoAmI < 0 || whoAmI >= FargoWorld.CurrentSpawnRateTile.Length)
            break;
          FargoWorld.CurrentSpawnRateTile[whoAmI] = reader.ReadBoolean();
          break;
        case 2:
          if (Main.netMode != 2 || !Fargowiltas.Fargowiltas.IsEventOccurring)
            break;
          Fargowiltas.Fargowiltas.TryClearEvents();
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          break;
        case 3:
          if (Main.netMode != 2)
            break;
          Main.AnglerQuestSwap();
          break;
        case 4:
          int index1 = reader.ReadInt32();
          int num1 = reader.ReadInt32();
          if (Main.netMode != 1 || index1 < 0 || index1 >= Main.maxNPCs)
            break;
          Main.npc[index1].lifeMax = num1;
          break;
        case 5:
          if (Main.netMode != 2)
            break;
          for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
          {
            if (Main.npc[index2] != null && ((Entity) Main.npc[index2]).active && Main.npc[index2].type == ModContent.NPCType<Fargowiltas.NPCs.SuperDummy>())
            {
              NPC npc = Main.npc[index2];
              npc.life = 0;
              npc.HitEffect(0, 10.0, new bool?());
              Main.npc[index2].SimpleStrikeNPC(int.MaxValue, 0, false, 0.0f, (DamageClass) null, false, 0.0f, true);
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
          break;
        case 6:
          if (Main.netMode != 2)
            break;
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          break;
        case 7:
          bool isBattle = reader.ReadBoolean();
          int index3 = reader.ReadInt32();
          bool cry = reader.ReadBoolean();
          BattleCry.GenerateText(isBattle, Main.player[index3], cry);
          break;
        case 8:
          int index4 = reader.ReadInt32();
          Main.player[index4].GetModPlayer<FargoPlayer>().BattleCry = reader.ReadBoolean();
          Main.player[index4].GetModPlayer<FargoPlayer>().CalmingCry = reader.ReadBoolean();
          break;
        case 9:
          int index5 = (int) reader.ReadByte();
          int num2 = (int) reader.ReadByte();
          if (index5 < 0 || index5 >= (int) byte.MaxValue || !((Entity) Main.player[index5]).active)
            break;
          Main.player[index5].GetModPlayer<FargoPlayer>().DeathFruitHealth = num2;
          break;
      }
    }

    internal static bool IsEventOccurring
    {
      get
      {
        if (Main.invasionType != 0 || Main.pumpkinMoon || Main.snowMoon || Main.eclipse || Main.bloodMoon || Main.WindyEnoughForKiteDrops || Main.IsItRaining || Main.IsItStorming || Main.slimeRain || BirthdayParty.PartyIsUp || DD2Event.Ongoing || Sandstorm.Happening)
          return true;
        if (!NPC.downedTowers)
          return false;
        return NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0;
      }
    }

    internal static bool TryClearEvents()
    {
      bool flag = FargoWorld.AbomClearCD <= 0;
      if (flag)
      {
        if (Main.invasionType != 0)
        {
          Main.invasionType = 0;
          FargoUtils.PrintLocalization("MessageInfo.CancelEvent", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.pumpkinMoon)
        {
          Main.pumpkinMoon = false;
          FargoUtils.PrintLocalization("MessageInfo.CancelPumpkinMoon", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.snowMoon)
        {
          Main.snowMoon = false;
          FargoUtils.PrintLocalization("MessageInfo.CancelFrostMoon", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.eclipse)
        {
          Main.eclipse = false;
          FargoUtils.PrintLocalization("MessageInfo.CancelEclipse", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.bloodMoon)
        {
          Main.bloodMoon = false;
          FargoUtils.PrintLocalization("MessageInfo.CancelBloodMoon", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.WindyEnoughForKiteDrops)
        {
          Main.windSpeedTarget = 0.0f;
          Main.windSpeedCurrent = 0.0f;
          FargoUtils.PrintLocalization("MessageInfo.CancelWindyDay", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.slimeRain)
        {
          Main.StopSlimeRain(true);
          Main.slimeWarningDelay = 1;
          Main.slimeWarningTime = 1;
        }
        if (BirthdayParty.PartyIsUp)
          BirthdayParty.CheckNight();
        if (DD2Event.Ongoing && Main.netMode != 1)
        {
          DD2Event.StopInvasion(false);
          FargoUtils.PrintLocalization("MessageInfo.CancelOOA", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Sandstorm.Happening)
        {
          Sandstorm.Happening = false;
          Sandstorm.TimeLeft = 0.0;
          Sandstorm.IntendedSeverity = 0.0f;
          FargoUtils.PrintLocalization("MessageInfo.CancelSandstorm", new Color(175, 75, (int) byte.MaxValue));
        }
        if (NPC.downedTowers && (NPC.LunarApocalypseIsUp || NPC.ShieldStrengthTowerNebula > 0 || NPC.ShieldStrengthTowerSolar > 0 || NPC.ShieldStrengthTowerStardust > 0 || NPC.ShieldStrengthTowerVortex > 0))
        {
          NPC.LunarApocalypseIsUp = false;
          NPC.ShieldStrengthTowerNebula = 0;
          NPC.ShieldStrengthTowerSolar = 0;
          NPC.ShieldStrengthTowerStardust = 0;
          NPC.ShieldStrengthTowerVortex = 0;
          for (int index = 0; index < Main.maxNPCs; ++index)
          {
            if (((Entity) Main.npc[index]).active && (Main.npc[index].type == 507 || Main.npc[index].type == 517 || Main.npc[index].type == 493 || Main.npc[index].type == 422))
            {
              Main.npc[index].dontTakeDamage = false;
              Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().NoLoot = true;
              Main.npc[index].StrikeInstantKill();
            }
          }
          FargoUtils.PrintLocalization("MessageInfo.CancelLunarEvent", new Color(175, 75, (int) byte.MaxValue));
        }
        if (Main.IsItRaining || Main.IsItStorming)
        {
          Main.StopRain();
          Main.cloudAlpha = 0.0f;
          if (Main.netMode == 2)
            Main.SyncRain();
          FargoUtils.PrintLocalization("MessageInfo.CancelRain", new Color(175, 75, (int) byte.MaxValue));
        }
        FargoWorld.AbomClearCD = 7200;
      }
      return flag;
    }

    internal static void SpawnBoss(
      Player player,
      int bossType,
      bool spawnMessage = true,
      int overrideDirection = 0,
      int overrideDirectionY = 0,
      string overrideDisplayName = "",
      bool namePlural = false)
    {
      if (overrideDirection == 0)
        overrideDirection = Utils.NextBool(Main.rand, 2) ? -1 : 1;
      if (overrideDirectionY == 0)
        overrideDirectionY = -1;
      Vector2 npcCenter = Vector2.op_Addition(((Entity) player).Center, new Vector2(MathHelper.Lerp(500f, 800f, (float) Main.rand.NextDouble()) * (float) overrideDirection, 800f * (float) overrideDirectionY));
      Fargowiltas.Fargowiltas.SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
    }

    internal static int SpawnBoss(
      Player player,
      int bossType,
      bool spawnMessage = true,
      Vector2 npcCenter = default (Vector2),
      string overrideDisplayName = "",
      bool namePlural = false)
    {
      if (Vector2.op_Equality(npcCenter, new Vector2()))
        npcCenter = ((Entity) player).Center;
      if (Main.netMode != 1)
      {
        int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) npcCenter.X, (int) npcCenter.Y, bossType, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        ((Entity) Main.npc[index]).Center = npcCenter;
        Main.npc[index].netUpdate2 = true;
        if (spawnMessage)
        {
          string str = !string.IsNullOrEmpty(Main.npc[index].GivenName) ? Main.npc[index].GivenName : overrideDisplayName;
          if (namePlural)
          {
            if (Main.netMode == 0)
              Main.NewText(Language.GetTextValue("Mods.Fargowiltas.MessageInfo.HaveAwoken", (object) str), (byte) 175, (byte) 75, byte.MaxValue);
            else if (Main.netMode == 2)
              ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.HaveAwoken", new object[1]
              {
                (object) str
              }), new Color(175, 75, (int) byte.MaxValue), -1);
          }
          else if (Main.netMode == 0)
            Main.NewText(Language.GetTextValue("Announcement.HasAwoken", (object) str), (byte) 175, (byte) 75, byte.MaxValue);
          else if (Main.netMode == 2)
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[1]
            {
              (object) str
            }), new Color(175, 75, (int) byte.MaxValue), -1);
        }
      }
      else
        FargoNet.SendNetMessage(0, (object) (byte) ((Entity) player).whoAmI, (object) (short) bossType, (object) spawnMessage, (object) (int) npcCenter.X, (object) (int) npcCenter.Y, (object) overrideDisplayName, (object) namePlural);
      return 200;
    }

    private static void OnVanillaDash(
      On_Player.orig_DoCommonDashHandle orig,
      Player player,
      out int dir,
      out bool dashing,
      Player.DashStartAction dashStartAction)
    {
      if (FargoClientConfig.Instance.DoubleTapDashDisabled)
        player.dashTime = 0;
      orig.Invoke(player, ref dir, ref dashing, dashStartAction);
      if (((Entity) player).whoAmI != Main.myPlayer || !Fargowiltas.Fargowiltas.DashKey.JustPressed || player.CCed)
        return;
      InputManager modPlayer = player.GetModPlayer<InputManager>();
      if (player.controlRight && player.controlLeft)
        dir = modPlayer.latestXDirPressed;
      else if (player.controlRight)
        dir = 1;
      else if (player.controlLeft)
        dir = -1;
      if (dir == 0)
        return;
      ((Entity) player).direction = dir;
      dashing = true;
      if (player.dashTime > 0)
        --player.dashTime;
      if (player.dashTime < 0)
        ++player.dashTime;
      if (player.dashTime <= 0 && ((Entity) player).direction == -1 || player.dashTime >= 0 && ((Entity) player).direction == 1)
      {
        player.dashTime = 15;
      }
      else
      {
        dashing = true;
        player.dashTime = 0;
        player.timeSinceLastDashStarted = 0;
        switch (dashStartAction)
        {
          case null:
          case null:
            break;
          default:
            dashStartAction.Invoke(dir);
            goto case null;
        }
      }
    }

    private static void OnVanillaDoubleTapSetBonus(
      On_Player.orig_KeyDoubleTap orig,
      Player player,
      int keyDir)
    {
      if (FargoClientConfig.Instance.DoubleTapSetBonusDisabled && !Fargowiltas.Fargowiltas.SetBonusKey.JustPressed)
        return;
      orig.Invoke(player, keyDir);
    }

    private static void OnVanillaHoldSetBonus(
      On_Player.orig_KeyHoldDown orig,
      Player player,
      int keyDir,
      int holdTime)
    {
      if (FargoClientConfig.Instance.DoubleTapSetBonusDisabled && !Fargowiltas.Fargowiltas.SetBonusKey.Current)
        return;
      orig.Invoke(player, keyDir, holdTime);
    }
  }
}
