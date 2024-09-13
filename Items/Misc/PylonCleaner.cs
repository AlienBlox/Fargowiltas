// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.PylonCleaner
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class PylonCleaner : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.maxStack = 99;
      this.Item.consumable = true;
      this.Item.useStyle = 1;
      this.Item.rare = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.itemAnimation > 0 && player.itemTime == 0 && ((Entity) player).whoAmI == Main.myPlayer)
      {
        foreach (TeleportPylonInfo pylon in (IEnumerable<TeleportPylonInfo>) Main.PylonSystem.Pylons)
        {
          Vector2 worldCoordinates = Utils.ToWorldCoordinates(pylon.PositionInTiles, 8f, 8f);
          int num1 = ModContent.ProjectileType<PurityNukeProj>();
          if (pylon.TypeOfPylon == 2)
            num1 = ModContent.ProjectileType<HallowNukeProj>();
          int index1 = Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), worldCoordinates, Vector2.Zero, num1, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index1 != Main.maxProjectiles)
            Main.projectile[index1].timeLeft = 2;
          if (pylon.TypeOfPylon == 7)
          {
            int num2 = ModContent.ProjectileType<MushroomNukeProj>();
            int index2 = Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), worldCoordinates, Vector2.Zero, num2, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            if (index2 != Main.maxProjectiles)
              Main.projectile[index2].timeLeft = 6;
          }
        }
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(66, 1).AddTile(134).Register();
    }
  }
}
