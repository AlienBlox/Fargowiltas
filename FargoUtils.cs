// Decompiled with JetBrains decompiler
// Type: Fargowiltas.FargoUtils
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

#nullable disable
namespace Fargowiltas
{
  internal static class FargoUtils
  {
    public static readonly BindingFlags UniversalBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    public static bool EternityMode
    {
      get
      {
        if (!Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
          return false;
        return (bool) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) nameof (EternityMode)
        });
      }
    }

    public static bool HasAnyItem(this Player player, params int[] itemIDs)
    {
      return ((IEnumerable<int>) itemIDs).Any<int>((Func<int, bool>) (itemID => player.HasItem(itemID)));
    }

    public static FargoPlayer GetFargoPlayer(this Player player)
    {
      return player.GetModPlayer<FargoPlayer>();
    }

    public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
    {
      if (!condition)
        return;
      list.Add(type);
    }

    public static void AddDebuffImmunities(this NPC npc, List<int> debuffs)
    {
      foreach (int debuff in debuffs)
        NPCID.Sets.SpecificDebuffImmunity[npc.type][debuff] = new bool?(true);
    }

    public static void TryDowned(string seller, Color color, params string[] names)
    {
      FargoUtils.TryDowned(seller, color, true, names);
    }

    public static void TryDowned(
      string seller,
      Color color,
      bool conditions,
      params string[] names)
    {
      bool flag = false;
      foreach (string name in names)
      {
        if (!FargoWorld.DownedBools[name])
        {
          FargoWorld.DownedBools[name] = true;
          flag = true;
        }
      }
      if (!flag)
        return;
      seller = Language.GetTextValue("Mods.Fargowiltas.NPCs." + seller + ".DisplayName");
      string textValue = Language.GetTextValue("Mods.Fargowiltas.MessageInfo.NewItemUnlocked", (object) seller);
      switch (Main.netMode)
      {
        case 0:
          if (conditions)
          {
            Main.NewText((object) textValue, new Color?(color));
            break;
          }
          break;
        case 2:
          if (conditions)
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(textValue), color, -1);
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          break;
      }
    }

    public static void PrintText(string text) => FargoUtils.PrintText(text, Color.White);

    public static void PrintText(string text, Color color)
    {
      if (Main.netMode == 0)
      {
        Main.NewText((object) text, new Color?(color));
      }
      else
      {
        if (Main.netMode != 2)
          return;
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), color, -1);
      }
    }

    public static void PrintText(string text, int r, int g, int b)
    {
      FargoUtils.PrintText(text, new Color(r, g, b));
    }

    public static void PrintLocalization(string fargoKey, params object[] args)
    {
      FargoUtils.PrintText(Language.GetTextValue("Mods.Fargowiltas." + fargoKey, args));
    }

    public static void PrintLocalization(string fargoKey, Color color, params object[] args)
    {
      FargoUtils.PrintText(Language.GetTextValue("Mods.Fargowiltas." + fargoKey, args), color);
    }

    public static void SpawnBossNetcoded(Player player, int bossType)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      if (Main.netMode != 1)
        NPC.SpawnOnPlayer(((Entity) player).whoAmI, bossType);
      else
        NetMessage.SendData(61, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, (float) bossType, 0.0f, 0.0f, 0, 0, 0);
    }
  }
}
