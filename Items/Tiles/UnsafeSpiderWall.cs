﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.UnsafeSpiderWall
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System.Runtime.CompilerServices;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class UnsafeSpiderWall : UnsafeWall
  {
    public virtual string Texture
    {
      get
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
        interpolatedStringHandler.AppendLiteral("Terraria/Images/Item_");
        interpolatedStringHandler.AppendFormatted<short>((short) 4503);
        return interpolatedStringHandler.ToStringAndClear();
      }
    }

    public UnsafeSpiderWall()
      : base("Unsafe Spider Wall", 62, 4503)
    {
    }
  }
}