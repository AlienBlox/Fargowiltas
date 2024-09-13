// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Mutant.FleshyDoll
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
  public class FleshyDoll : ModItem
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
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
    }

    public virtual bool CanUseItem(Player player)
    {
      return (double) ((Entity) player).position.Y / 16.0 > (double) (Main.maxTilesY - 200) && !NPC.AnyNPCs(113);
    }

    public virtual bool? UseItem(Player player)
    {
      NPC.SpawnWOF(((Entity) player).Center);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return new bool?(true);
    }

    public virtual void PostUpdate()
    {
      if (!((Entity) this.Item).lavaWet || NPC.AnyNPCs(113))
        return;
      NPC.SpawnWOF(((Entity) this.Item).position);
      this.Item.TurnToAir(false);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(267, 1).AddTile(18).Register();
    }
  }
}
