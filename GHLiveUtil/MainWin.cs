using FolderSelect;
using GameArchives;
using GHLiveUtil.Wem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GHLiveUtil
{
  public partial class MainWin : Form
  {
    public MainWin()
    {
      InitializeComponent();
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog of = new OpenFileDialog();
      of.Filter = "SoundbanksInfo.xml|SoundbanksInfo.xml";
      if(of.ShowDialog() == DialogResult.OK)
      {
        var x = new XmlDocument();
        x.Load(of.FileName);
        XmlNodeList nodes = x.SelectNodes("/SoundBanksInfo/StreamedFiles/File[@Language='SFX']");
        IDirectory cfgDir = Util.LocalDir(Path.GetDirectoryName(of.FileName));
        listView1.Items.Clear();
        foreach (XmlNode node in nodes)
        {
          string id = node.Attributes["Id"].Value;
          string name = node.SelectSingleNode("ShortName").InnerText;
          if(name.Contains("FEM_"))
          {
            IFile tmp = cfgDir.GetFile(id + ".wem");
            IWemFile wem = WemLoader.LoadWem(tmp, Path.Combine(Path.GetDirectoryName(of.FileName), id + ".wem"));
            var newItem = new ListViewItem(new string[] {
              name.Split('\\').Last().Replace(".wav", ""),
              tmp.Size.ToString(),
              wem.Codec.ToString(),
              id
            });
            newItem.Tag = wem;
            listView1.Items.Add(newItem);
          }
        }
      }
    }

    private void saveToToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if(listView1.SelectedItems.Count >= 1)
      {
        FolderSelectDialog sfd = new FolderSelectDialog();
        if(sfd.ShowDialog())
        {
          foreach(ListViewItem item in listView1.SelectedItems)
          {
            IWemFile wem = item.Tag as IWemFile;
            string name = item.Text + (wem.Codec == WemCodec.XMA ? ".xma" : ".ogg");
            wem.ConvertTo(Path.Combine(sfd.FileName, name));
          }
          MessageBox.Show("Done!");
        }
      }
    }
  }
}
