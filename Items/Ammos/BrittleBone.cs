// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Ammos.BrittleBone
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Ammos
{
  public class BrittleBone : ModItem
  {
    public virtual string Texture => "Terraria/Images/Item_154";

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 99;
    }

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(154);
      this.Item.shoot = 0;
      this.Item.useAnimation = 0;
      this.Item.useTime = 0;
      this.Item.useStyle = 0;
    }
  }
}
