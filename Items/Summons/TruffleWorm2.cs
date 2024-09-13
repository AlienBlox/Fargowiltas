// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.TruffleWorm2
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

#nullable disable
namespace Fargowiltas.Items.Summons
{
  public class TruffleWorm2 : BaseSummon
  {
    public virtual string Texture => "Terraria/Images/Item_2673";

    public override int NPCType => 370;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2673, 1).AddTile(18).Register();
    }
  }
}
