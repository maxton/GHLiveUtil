using GameArchives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GHLiveUtil.Wem
{
  class VorbisWem : IWemFile
  {
    public WemCodec Codec => WemCodec.Vorbis;

    private IFile wem;
    private string path;

    public VorbisWem(IFile file, Endianness end, string path)
    {
      wem = file;
      this.path = path;
    }

    public void ConvertTo(string output)
    {
      ProcessStartInfo info = new ProcessStartInfo();
      info.FileName = Path.Combine("util","ww2ogg.exe");
      info.WorkingDirectory = "util";
      info.Arguments = $"\"{path}\" --pcb packed_codebooks_aoTuV_603.bin -o \"{output}\"";
      info.RedirectStandardOutput = true;
      info.RedirectStandardError = true;
      info.WindowStyle = ProcessWindowStyle.Hidden;
      info.UseShellExecute = false;
      info.CreateNoWindow = true;
      using (Process p = Process.Start(info))
        p.WaitForExit();

      info.FileName = Path.Combine("util", "revorb.exe");
      info.Arguments = "\"" + output + "\"";
      using (Process p = Process.Start(info))
        p.WaitForExit();
    }
  }
}
