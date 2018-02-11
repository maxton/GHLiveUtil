using GameArchives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLiveUtil.Wem
{
  public enum WemCodec
  {
    Vorbis,
    XMA,
    Unknown
  }

  public enum Endianness
  {
    Big,
    Little
  }

  public interface IWemFile
  {
    /// <summary>
    /// The codec used by this wem file.
    /// </summary>
    WemCodec Codec { get; }

    /// <summary>
    /// Convert this wem container to a more standard one for the codec it uses.
    /// For example, this should output:
    /// - Ogg containing Vorbis audio if the codec is vorbis.
    /// - RIFF containing XMA if the codec is XMA
    /// - RIFF containing PCM if the codec is PCM
    /// etc.
    /// </summary>
    /// <param name="output"></param>
    void ConvertTo(string output);
  }

  public static class WemLoader
  {
    public static IWemFile LoadWem(IFile wemFile, string path)
    {
      Stream wem = wemFile.GetStream();
      wem.Position = 0;
      string code = wem.ReadASCIINullTerminated(4);
      short codecType;
      Endianness endian;
      if (code == "RIFF")
      {
        wem.Seek(0x14, SeekOrigin.Begin);
        codecType = wem.ReadInt16LE();
        endian = Endianness.Little;
      }
      else if (code == "RIFX")
      {
        wem.Seek(0x14, SeekOrigin.Begin);
        codecType = wem.ReadInt16BE();
        endian = Endianness.Big;
      }
      else if (code == "FSGC")
      {
        throw new NotSupportedException("Encrypted media is not supported.");
      }
      else
      {
        throw new InvalidDataException("WEM is unrecognizable.");
      }
      switch(codecType)
      {
        case -1:
          return new VorbisWem(wemFile, endian, path);
        case 0x166:
          return new XmaWem(wemFile, endian);
      }
      throw new InvalidDataException("WEM is of unknown codec type.");
    }
  }
}
