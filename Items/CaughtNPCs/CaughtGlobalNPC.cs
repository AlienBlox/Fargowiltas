// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.CaughtNPCs.CaughtGlobalNPC
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.CaughtNPCs
{
  public class CaughtGlobalNPC : GlobalNPC
  {
    private static HashSet<int> npcCatchableWasFalse;

    public virtual void Load() => CaughtGlobalNPC.npcCatchableWasFalse = new HashSet<int>();

    public virtual void Unload()
    {
      if (CaughtGlobalNPC.npcCatchableWasFalse == null)
        return;
      foreach (int index in CaughtGlobalNPC.npcCatchableWasFalse)
        Main.npcCatchable[index] = false;
      CaughtGlobalNPC.npcCatchableWasFalse = (HashSet<int>) null;
    }

    public virtual void SetDefaults(NPC npc)
    {
      int type = npc.type;
      if (!CaughtNPCItem.CaughtTownies.ContainsKey(type) || !FargoServerConfig.Instance.CatchNPCs)
        return;
      npc.catchItem = (int) (short) CaughtNPCItem.CaughtTownies.FirstOrDefault<KeyValuePair<int, int>>((Func<KeyValuePair<int, int>, bool>) (x => x.Key.Equals(type))).Value;
      if (Main.npcCatchable[type])
        return;
      CaughtGlobalNPC.npcCatchableWasFalse.Add(type);
      Main.npcCatchable[type] = true;
    }
  }
}
