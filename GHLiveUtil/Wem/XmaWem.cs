using GameArchives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHLiveUtil.Wem
{
  class XmaWem : IWemFile
  {
    private IFile wem;
    public XmaWem(IFile input, Endianness end)
    {
      if(end == Endianness.Little)
      {
        throw new NotImplementedException("Support for little-endian XMA files is not currently implemented.");
      }
      wem = input;
    }

    public WemCodec Codec { get; } = WemCodec.XMA;

    public void ConvertTo(string output)
    {
      throw new NotImplementedException();
    }
  }
}
