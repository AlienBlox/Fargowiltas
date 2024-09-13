// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Content.Buffs.WoodDrop
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Content.Buffs
{
  public class WoodDrop : ModBuff
  {
    public virtual void SetStaticDefaults() => Main.buffNoSave[this.Type] = true;
  }
}
