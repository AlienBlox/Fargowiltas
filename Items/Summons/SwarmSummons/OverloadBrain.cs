// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.SwarmSummons.OverloadBrain
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Items.Summons.SwarmSummons
{
  public class OverloadBrain : SwarmSummonBase
  {
    public OverloadBrain()
      : base(266, nameof (OverloadBrain), 25, "GoreySpine")
    {
    }

    public virtual void SetStaticDefaults()
    {
    }

    public virtual bool CanUseItem(Player player) => !Fargowiltas.Fargowiltas.SwarmActive;
  }
}
