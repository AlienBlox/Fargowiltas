// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Tiles.FargoGlobalPylon
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.NPCs;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Tiles
{
  public class FargoGlobalPylon : GlobalPylon
  {
    public virtual bool? ValidTeleportCheck_PreAnyDanger(TeleportPylonInfo pylonInfo)
    {
      return FargoServerConfig.Instance.PylonsIgnoreEvents && !FargoGlobalNPC.AnyBossAlive() ? new bool?(true) : base.ValidTeleportCheck_PreAnyDanger(pylonInfo);
    }
  }
}
