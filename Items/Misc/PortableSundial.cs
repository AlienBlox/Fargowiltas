// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.PortableSundial
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class PortableSundial : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.rare = 4;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.mana = 15;
      this.Item.UseSound = new SoundStyle?(SoundID.Item4);
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.boss)))
      {
        this.Item.useAnimation = 120;
        this.Item.useTime = 120;
      }
      else
      {
        this.Item.useAnimation = 30;
        this.Item.useTime = 30;
      }
      return !Main.IsFastForwardingTime();
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        Main.sundialCooldown = 0;
        SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
        if (Main.netMode == 1)
        {
          NetMessage.SendData(51, -1, -1, (NetworkText) null, Main.myPlayer, 3f, 0.0f, 0.0f, 0, 0, 0);
          return new bool?(true);
        }
        if (Main.dayTime)
          Main.fastForwardTimeToDusk = true;
        else
          Main.fastForwardTimeToDawn = true;
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else
      {
        int num1 = 27000;
        int num2 = 16200;
        if (Main.dayTime && Main.time < (double) num1)
          Main.time = (double) num1;
        else if (Main.time < (double) num2)
        {
          Main.time = (double) num2;
        }
        else
        {
          Main.dayTime = !Main.dayTime;
          Main.time = 0.0;
          if (Main.dayTime)
          {
            BirthdayParty.CheckMorning();
            Chest.SetupTravelShop();
          }
          else
          {
            BirthdayParty.CheckNight();
            if (!Main.dayTime && ++Main.moonPhase > 7)
              Main.moonPhase = 0;
          }
        }
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3064, 1).AddIngredient(5381, 1).AddTile(305).Register();
    }
  }
}
