﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Mutant.PlanterasFruit
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

#nullable disable
namespace Fargowiltas.Items.Summons.Mutant
{
  public class PlanterasFruit : BaseSummon
  {
    public override int NPCType => 262;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1006, 2).AddIngredient(314, 5).AddIngredient(315, 5).AddTile(26).DisableDecraft().Register();
    }
  }
}
