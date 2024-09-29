// Decompiled with JetBrains decompiler
// Type: Fargowiltas.MutantSummonInfo
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using System;

#nullable disable
namespace Fargowiltas
{
  internal class MutantSummonInfo
  {
    internal float progression;
    internal string modSource;
    internal int itemId;
    internal Func<bool> downed;
    internal int price;

    internal MutantSummonInfo(float progression, int itemId, Func<bool> downed, int price)
    {
      this.progression = progression;
      this.itemId = itemId;
      this.downed = downed;
      this.price = price;
    }
  }
}
