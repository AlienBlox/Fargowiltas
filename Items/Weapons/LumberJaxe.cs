// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Weapons.LumberJaxe
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Content.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Weapons
{
  public class LumberJaxe : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 15;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 40;
      ((Entity) this.Item).height = 40;
      this.Item.useTime = 30;
      this.Item.useAnimation = 30;
      this.Item.axe = 30;
      this.Item.useStyle = 1;
      this.Item.knockBack = 6f;
      this.Item.value = 5000;
      this.Item.rare = 3;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
    }

    public virtual void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<WoodDrop>(), 600, false);
    }
  }
}
