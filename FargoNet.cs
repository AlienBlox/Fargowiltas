// Decompiled with JetBrains decompiler
// Type: Fargowiltas.FargoNet
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas
{
  public static class FargoNet
  {
    public const byte SummonNPCFromClient = 0;
    private const bool Debug = true;

    public static void SendData(
      int dataType,
      int dataA,
      int dataB,
      string text,
      int playerID,
      float dataC,
      float dataD,
      float dataE,
      int clientType)
    {
      NetMessage.SendData(dataType, dataA, dataB, NetworkText.FromLiteral(text), playerID, dataC, dataD, dataE, clientType, 0, 0);
    }

    public static ModPacket WriteToPacket(ModPacket packet, byte msg, params object[] param)
    {
      ((BinaryWriter) packet).Write(msg);
      for (int index = 0; index < param.Length; ++index)
      {
        object obj = param[index];
        switch (obj)
        {
          case byte[] _:
            foreach (byte num in (byte[]) obj)
              ((BinaryWriter) packet).Write(num);
            break;
          case bool flag:
            ((BinaryWriter) packet).Write(flag);
            break;
          case byte num1:
            ((BinaryWriter) packet).Write(num1);
            break;
          case short num2:
            ((BinaryWriter) packet).Write(num2);
            break;
          case int num3:
            ((BinaryWriter) packet).Write(num3);
            break;
          case float num4:
            ((BinaryWriter) packet).Write(num4);
            break;
        }
      }
      return packet;
    }

    public static void SyncAI(Entity codable, float[] ai, int aitype)
    {
      int num;
      switch (codable)
      {
        case NPC _:
          num = 0;
          break;
        case Projectile _:
          num = 1;
          break;
        default:
          num = -1;
          break;
      }
      int entType = num;
      if (entType == -1)
        return;
      int id = codable is NPC ? codable.whoAmI : ((Projectile) codable).identity;
      FargoNet.SyncAI(entType, id, ai, aitype);
    }

    public static void SyncAI(int entType, int id, float[] ai, int aitype)
    {
      object[] objArray = new object[ai.Length + 4];
      objArray[0] = (object) (byte) entType;
      objArray[1] = (object) (short) id;
      objArray[2] = (object) (byte) aitype;
      objArray[3] = (object) (byte) ai.Length;
      for (int index = 4; index < objArray.Length; ++index)
        objArray[index] = (object) ai[index - 4];
      FargoNet.SendFargoNetMessage(1, objArray);
    }

    public static object[] WriteVector2Array(Vector2[] array)
    {
      List<object> objectList = new List<object>()
      {
        (object) array.Length
      };
      foreach (Vector2 vector2 in array)
      {
        objectList.Add((object) vector2.X);
        objectList.Add((object) vector2.Y);
      }
      return objectList.ToArray();
    }

    public static void WriteVector2Array(Vector2[] array, BinaryWriter writer)
    {
      writer.Write(array.Length);
      foreach (Vector2 vector2 in array)
      {
        writer.Write(vector2.X);
        writer.Write(vector2.Y);
      }
    }

    public static Vector2[] ReadVector2Array(BinaryReader reader)
    {
      int length = reader.ReadInt32();
      Vector2[] vector2Array = new Vector2[length];
      for (int index = 0; index < length; ++index)
        vector2Array[index] = new Vector2(reader.ReadSingle(), reader.ReadSingle());
      return vector2Array;
    }

    public static void SendFargoNetMessage(int msg, params object[] param)
    {
      if (Main.netMode == 0)
        return;
      FargoNet.WriteToPacket(ModContent.GetInstance<Fargowiltas.Fargowiltas>().GetPacket(256), (byte) msg, param).Send(-1, -1);
    }

    public static void HandlePacket(BinaryReader bb, byte msg)
    {
      ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- ") + "HANDING MESSAGE: " + msg.ToString()));
      try
      {
        if (msg != (byte) 0 || Main.netMode != 2)
          return;
        int index = (int) bb.ReadByte();
        int bossType = (int) bb.ReadInt16();
        bool spawnMessage = bb.ReadBoolean();
        int num1 = bb.ReadInt32();
        int num2 = bb.ReadInt32();
        string overrideDisplayName = bb.ReadString();
        bool namePlural = bb.ReadBoolean();
        Fargowiltas.Fargowiltas.SpawnBoss(Main.player[index], bossType, spawnMessage, new Vector2((float) num1, (float) num2), overrideDisplayName, namePlural);
      }
      catch (Exception ex)
      {
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- ") + "ERROR HANDLING MSG: " + msg.ToString() + ": " + ex.Message));
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ex.StackTrace);
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) "-------");
      }
    }

    public static void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
      ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- ") + "SYNC PLAYER CALLED! NEWPLAYER: " + newPlayer.ToString() + ". TOWHO: " + toWho.ToString() + ". FROMWHO:" + fromWho.ToString()));
      if (Main.netMode != 2 || toWho <= -1 && fromWho <= -1)
        return;
      FargoNet.PlayerConnected();
    }

    public static void PlayerConnected()
    {
      ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Info((object) "--SERVER-- PLAYER JOINED!");
    }

    public static void SendNetMessage(int msg, params object[] param)
    {
      FargoNet.SendNetMessageClient(msg, -1, param);
    }

    public static void SendNetMessageClient(int msg, int client, params object[] param)
    {
      try
      {
        if (Main.netMode == 0)
          return;
        FargoNet.WriteToPacket(ModContent.GetInstance<Fargowiltas.Fargowiltas>().GetPacket(256), (byte) msg, param).Send(client, -1);
      }
      catch (Exception ex)
      {
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ((Main.netMode == 2 ? "--SERVER-- " : "--CLIENT-- ") + "ERROR SENDING MSG: " + msg.ToString() + ": " + ex.Message));
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ex.StackTrace);
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) "-------");
        string empty = string.Empty;
        for (int index = 0; index < param.Length; ++index)
          empty += param[index]?.ToString();
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) ("PARAMS: " + empty));
        ModContent.GetInstance<Fargowiltas.Fargowiltas>().Logger.Error((object) "-------");
      }
    }
  }
}
