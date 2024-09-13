// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.CelestialSigil2
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria.Localization;

#nullable disable
namespace Fargowiltas.Items.Summons
{
  public class CelestialSigil2 : BaseSummon
  {
    public virtual string Texture => "Terraria/Images/Item_3601";

    public override string NPCName => Language.GetTextValue("Enemies.MoonLord");

    public override int NPCType => 398;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3601, 1).AddTile(18).Register();
    }
  }
}
