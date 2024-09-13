// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Abom.PartyInvite
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Abom
{
  public class PartyInvite : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
    }

    public virtual bool CanUseItem(Player player)
    {
      return Main.dayTime && !BirthdayParty.PartyIsUp && ((IEnumerable<NPC>) Main.npc).Count<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.townNPC && n.aiStyle != 0 && n.type != 37 && n.type != 453 && n.type != 441 && !NPCID.Sets.IsTownPet[n.type])) >= 5;
    }

    public virtual bool? UseItem(Player player)
    {
      if (!NPC.AnyNPCs(208))
        NPC.SpawnOnPlayer(((Entity) player).whoAmI, 208);
      BirthdayParty.PartyDaysOnCooldown = 0;
      if (Main.netMode != 1)
      {
        for (int index = 0; index < 100 && !BirthdayParty.PartyIsUp; ++index)
          BirthdayParty.CheckMorning();
      }
      if (Main.netMode == 2)
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      return new bool?(true);
    }
  }
}
