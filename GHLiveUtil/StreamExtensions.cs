﻿/*
 * I hereby release this file, StreamExtensions.cs, to the public domain.
 * Use it if you wish.
 */
using System;
using System.Text;
using System.IO;
namespace GHLiveUtil
{
  internal static class StreamExtensions
  {
    /// <summary>
    /// Read a signed 8-bit integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static sbyte ReadInt8(this Stream s) => unchecked((sbyte)s.ReadUInt8());

    /// <summary>
    /// Read an unsigned 8-bit integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static byte ReadUInt8(this Stream s)
    {
      byte ret;
      byte[] tmp = new byte[1];
      s.Read(tmp, 0, 1);
      ret = tmp[0];
      return ret;
    }

    /// <summary>
    /// Read an unsigned 16-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ushort ReadUInt16LE(this Stream s) => unchecked((ushort)s.ReadInt16LE());

    /// <summary>
    /// Read a signed 16-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static short ReadInt16LE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[2];
      s.Read(tmp, 0, 2);
      ret = tmp[0] & 0x00FF;
      ret |= (tmp[1] << 8) & 0xFF00;
      return (short)ret;
    }

    /// <summary>
    /// Read an unsigned 16-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ushort ReadUInt16BE(this Stream s) => unchecked((ushort)s.ReadInt16BE());

    /// <summary>
    /// Read a signed 16-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static short ReadInt16BE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[2];
      s.Read(tmp, 0, 2);
      ret = (tmp[0] << 8) & 0xFF00;
      ret |= tmp[1] & 0x00FF;
      return (short)ret;
    }

    /// <summary>
    /// Read an unsigned 24-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadUInt24LE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[0] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[2] << 16) & 0xFF0000;
      return ret;
    }

    /// <summary>
    /// Read a signed 24-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt24LE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[0] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[2] << 16) & 0xFF0000;
      if ((tmp[2] & 0x80) == 0x80)
      {
        ret |= 0xFF << 24;
      }
      return ret;
    }

    /// <summary>
    /// Read an unsigned 24-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static uint ReadUInt24BE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[2] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[0] << 16) & 0xFF0000;
      return (uint)ret;
    }

    /// <summary>
    /// Read a signed 24-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt24BE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[3];
      s.Read(tmp, 0, 3);
      ret = tmp[2] & 0x0000FF;
      ret |= (tmp[1] << 8) & 0x00FF00;
      ret |= (tmp[0] << 16) & 0xFF0000;
      if ((tmp[0] & 0x80) == 0x80)
      {
        ret |= 0xFF << 24; // sign-extend
      }
      return ret;
    }

    /// <summary>
    /// Read an unsigned 32-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static uint ReadUInt32LE(this Stream s) => unchecked((uint)s.ReadInt32LE());

    /// <summary>
    /// Read a signed 32-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt32LE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[4];
      s.Read(tmp, 0, 4);
      ret = tmp[0] & 0x000000FF;
      ret |= (tmp[1] << 8) & 0x0000FF00;
      ret |= (tmp[2] << 16) & 0x00FF0000;
      ret |= (tmp[3] << 24);
      return ret;
    }

    /// <summary>
    /// Read an unsigned 32-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static uint ReadUInt32BE(this Stream s) => unchecked((uint)s.ReadInt32BE());

    /// <summary>
    /// Read a signed 32-bit Big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadInt32BE(this Stream s)
    {
      int ret;
      byte[] tmp = new byte[4];
      s.Read(tmp, 0, 4);
      ret = (tmp[0] << 24);
      ret |= (tmp[1] << 16) & 0x00FF0000;
      ret |= (tmp[2] << 8) & 0x0000FF00;
      ret |= tmp[3] & 0x000000FF;
      return ret;
    }

    /// <summary>
    /// Read an unsigned 64-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ulong ReadUInt64LE(this Stream s) => unchecked((ulong)s.ReadInt64LE());

    /// <summary>
    /// Read a signed 64-bit little-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ReadInt64LE(this Stream s)
    {
      long ret;
      byte[] tmp = new byte[8];
      s.Read(tmp, 0, 8);
      ret = tmp[4] & 0x000000FFL;
      ret |= (tmp[5] << 8) & 0x0000FF00L;
      ret |= (tmp[6] << 16) & 0x00FF0000L;
      ret |= (tmp[7] << 24) & 0xFF000000L;
      ret <<= 32;
      ret |= tmp[0] & 0x000000FFL;
      ret |= (tmp[1] << 8) & 0x0000FF00L;
      ret |= (tmp[2] << 16) & 0x00FF0000L;
      ret |= (tmp[3] << 24) & 0xFF000000L;
      return ret;
    }

    /// <summary>
    /// Read an unsigned 64-bit big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static ulong ReadUInt64BE(this Stream s) => unchecked((ulong)s.ReadInt64BE());

    /// <summary>
    /// Read a signed 64-bit big-endian integer from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ReadInt64BE(this Stream s)
    {
      long ret;
      byte[] tmp = new byte[8];
      s.Read(tmp, 0, 8);
      ret = tmp[3] & 0x000000FFL;
      ret |= (tmp[2] << 8) & 0x0000FF00L;
      ret |= (tmp[1] << 16) & 0x00FF0000L;
      ret |= (tmp[0] << 24) & 0xFF000000L;
      ret <<= 32;
      ret |= tmp[7] & 0x000000FFL;
      ret |= (tmp[6] << 8) & 0x0000FF00L;
      ret |= (tmp[5] << 16) & 0x00FF0000L;
      ret |= (tmp[4] << 24) & 0xFF000000L;
      return ret;
    }

    /// <summary>
    /// Read a single-precision (4-byte) floating-point value from the stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static float ReadFloat(this Stream s)
    {
      byte[] tmp = new byte[4];
      s.Read(tmp, 0, 4);
      return BitConverter.ToSingle(tmp, 0);
    }

    /// <summary>
    /// Read a null-terminated ASCII string from the given stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadASCIINullTerminated(this Stream s, int limit = -1)
    {
      StringBuilder sb = new StringBuilder(255);
      char cur;
      while ((limit == -1 || sb.Length < limit) && (cur = (char)s.ReadByte()) != 0)
      {
        sb.Append(cur);
      }
      return sb.ToString();
    }

    /// <summary>
    /// Read a length-prefixed string of the specified encoding type from the file.
    /// The length is a 32-bit little endian integer.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e">The encoding to use to decode the string.</param>
    /// <returns></returns>
    public static string ReadLengthPrefixedString(this Stream s, Encoding e)
    {
      int length = s.ReadInt32LE();
      byte[] chars = new byte[length];
      s.Read(chars, 0, length);
      return e.GetString(chars);
    }

    /// <summary>
    /// Read a length-prefixed UTF-8 string from the given stream.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReadLengthUTF8(this Stream s)
    {
      return s.ReadLengthPrefixedString(Encoding.UTF8);
    }

    /// <summary>
    /// Read a given number of bytes from a stream into a new byte array.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="count">Number of bytes to read (maximum)</param>
    /// <returns>New byte array of size &lt;=count.</returns>
    public static byte[] ReadBytes(this Stream s, int count)
    {
      // Size of returned array at most count, at least difference between position and length.
      int realCount = (int)((s.Position + count > s.Length) ? (s.Length - s.Position) : count);
      byte[] ret = new byte[realCount];
      s.Read(ret, 0, realCount);
      return ret;
    }

    /// <summary>
    /// Read a variable-length integral value as found in MIDI messages.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ReadMidiMultiByte(this Stream s)
    {
      int ret = 0;
      byte b = (byte)(s.ReadByte());
      ret += b & 0x7f;
      if (0x80 == (b & 0x80))
      {
        ret <<= 7;
        b = (byte)(s.ReadByte());
        ret += b & 0x7f;
        if (0x80 == (b & 0x80))
        {
          ret <<= 7;
          b = (byte)(s.ReadByte());
          ret += b & 0x7f;
          if (0x80 == (b & 0x80))
          {
            ret <<= 7;
            b = (byte)(s.ReadByte());
            ret += b & 0x7f;
            if (0x80 == (b & 0x80))
              throw new InvalidDataException("Variable-length MIDI number > 4 bytes");
          }
        }
      }
      return ret;
    }
  }
}