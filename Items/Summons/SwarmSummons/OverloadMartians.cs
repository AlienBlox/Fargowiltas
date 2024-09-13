﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.SwarmSummons.OverloadMartians
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.SwarmSummons
{
  public class OverloadMartians : ModItem
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 1;
      this.Item.value = 1000;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
    }

    public virtual bool? UseItem(Player player)
    {
      if (FargoWorld.OverloadMartians)
      {
        Main.invasionSize = 1;
        FargoWorld.OverloadMartians = false;
        if (Main.netMode == 2)
          ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.Fargowiltas.MessageInfo.OverloadMartiansStop", Array.Empty<object>()), new Color(175, 75, (int) byte.MaxValue), -1);
        else
          Main.NewText(Language.GetTextValue("Mods.Fargowiltas.MessageInfo.OverloadMartiansStop"), (byte) 175, (byte) 75, byte.MaxValue);
      }
      else
      {
        if (Main.netMode != 1)
        {
          Main.invasionDelay = 0;
          Main.StartInvasion(4);
          Main.invasionSize = 15000;
          Main.invasionSizeStart = 15000;
        }
        else
          NetMessage.SendData(61, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, -7f, 0.0f, 0.0f, 0, 0, 0);
        FargoWorld.OverloadMartians = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "RunawayProbe", 1).AddIngredient((Mod) null, "Overloader", 10).AddTile(125).Register();
    }
  }
}
