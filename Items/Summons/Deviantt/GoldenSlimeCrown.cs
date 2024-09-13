// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Deviantt.GoldenSlimeCrown
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Tiles;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Deviantt
{
  public class GoldenSlimeCrown : BaseSummon
  {
    public override int NPCType => 667;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<PinkSlimeCrown>(), 1).AddIngredient(1348, 999).AddTile(ModContent.TileType<GoldenDippingVatSheet>()).Register();
    }
  }
}
