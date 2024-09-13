﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Mutant.MechanicalAmalgam
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Mutant
{
  public class MechanicalAmalgam : ModItem
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
      this.Item.rare = 4;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
    }

    public virtual bool CanUseItem(Player player) => !Main.dayTime;

    public virtual bool? UseItem(Player player)
    {
      NPC.SpawnOnPlayer(((Entity) player).whoAmI, 134);
      NPC.SpawnOnPlayer(((Entity) player).whoAmI, (int) sbyte.MaxValue);
      NPC.SpawnOnPlayer(((Entity) player).whoAmI, 125);
      NPC.SpawnOnPlayer(((Entity) player).whoAmI, 126);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return new bool?(true);
    }
  }
}
