using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParsingHelper
{
    public partial class Form1 : Form
    {
        enum PktVersion
        {
            NoHeader = 0,
            V2_1 = 0x201,
            V2_2 = 0x202,
            V3_0 = 0x300,
            V3_1 = 0x301
        }

        const string MASS_PARSE_VERSION_PH = "@PHTOKENVERSION@";
        const string MASS_PARSE_NAME_PH = "@PHTOKENNAME@";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseWpp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = txtWppDirectory.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtWppDirectory.Text = folderBrowserDialog1.SelectedPath;
                if (!System.IO.File.Exists(Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe")))
                    MessageBox.Show("WowPacketParser.exe not found in directory!", "Parsing Helper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        Dictionary<uint, List<string>> _sniffsPerBuild = new Dictionary<uint, List<string>>();
        private void AddSniffToList(uint build, string sniff)
        {
            if (build == 0)
            {
                MessageBox.Show("Skipping file with unknown build:\r\n" + sniff);
                return;
            }

            if (_sniffsPerBuild.ContainsKey(build))
            {
                _sniffsPerBuild[build].Add(sniff);
            }
            else
            {
                List<string> fileList = new List<string>();
                fileList.Add(sniff);
                _sniffsPerBuild.Add(build, fileList);
            }
        }
        private void ShowSniffsInListView()
        {
            listView1.Items.Clear();
            listView1.Sorting = SortOrder.None;
            foreach (var itr in _sniffsPerBuild)
            {
                string versionName;
                if (Enum.IsDefined(typeof(ClientVersionBuild), (int)itr.Key))
                    versionName = ((ClientVersionBuild)itr.Key).ToString();
                else
                    versionName = "Build_" + itr.Key.ToString();

                ListViewItem lvi = new ListViewItem();
                lvi.Text = versionName;
                lvi.SubItems.Add(itr.Value.Count.ToString());
                lvi.Tag = itr.Value;
                
                foreach (var fileName in itr.Value)
                {
                    if (!String.IsNullOrEmpty(lvi.ToolTipText))
                        lvi.ToolTipText += "\r\n";
                    lvi.ToolTipText += Path.GetFileName(fileName);
                }

                listView1.Items.Add(lvi);
            }
            listView1.Sorting = SortOrder.Ascending;
            listView1.Sort();
        }
        private void LoadAllSniffsInDirectory()
        {
            _sniffsPerBuild.Clear();
            foreach (string file in Directory.EnumerateFiles(txtSniffsDirectory.Text, "*.pkt", SearchOption.AllDirectories))
            {
                AddSniffToList(ReadSniffHeader(file), file);
            }
            ShowSniffsInListView();
        }
        private void txtSniffsDirectory_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSniffsDirectory.Text))
                LoadAllSniffsInDirectory();
        }
        private void btnBrowseSniffs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.SelectedPath = txtSniffsDirectory.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSniffsDirectory.Text = folderBrowserDialog1.SelectedPath;
                LoadAllSniffsInDirectory();
            }
        }

        private uint ReadSniffHeader(string filePath)
        {
            PktVersion pktVersion = PktVersion.NoHeader;
            uint build = 0;
            uint snifferId = 0;
            int snifferVersion = 0;
            long startTime = 0;
            uint startTickCount = 0;
            string locale = "";

            using (FileStream fs = File.OpenRead(filePath))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                var headerStart = reader.ReadBytes(3);             // PKT
                if (Encoding.ASCII.GetString(headerStart) != "PKT")
                {
                    // file does not have a header
                    reader.BaseStream.Position = 0;
                    return 0;
                }

                pktVersion = (PktVersion)reader.ReadUInt16();      // sniff version

                int additionalLength;

                switch (pktVersion)
                {
                    case PktVersion.V2_1:
                    {
                        build = reader.ReadUInt16(); // client build
                        reader.ReadBytes(40);        // session key
                        break;
                    }
                    case PktVersion.V2_2:
                    {
                        snifferId = reader.ReadByte();            // sniffer id
                        build = reader.ReadUInt16();              // client build
                        reader.ReadBytes(4);                      // client locale
                        reader.ReadBytes(20);                     // packet key
                        reader.ReadBytes(64);                     // realm name
                        break;
                    }
                    case PktVersion.V3_0:
                    {
                        snifferId = reader.ReadByte();            // sniffer id
                        build = reader.ReadUInt32();              // client build
                        reader.ReadBytes(4);                      // client locale
                        reader.ReadBytes(40);                     // session key
                        additionalLength = reader.ReadInt32();
                        var preAdditionalPos = reader.BaseStream.Position;
                        reader.ReadBytes(additionalLength);
                        var postAdditionalPos = reader.BaseStream.Position;
                        if (snifferId == 10)                      // xyla
                        {
                            reader.BaseStream.Position = preAdditionalPos;
                            startTime = reader.ReadUInt32();      // start time
                            startTickCount = reader.ReadUInt32(); // start tick count
                            reader.BaseStream.Position = postAdditionalPos;
                        }
                        break;
                    }
                    case PktVersion.V3_1:
                    {
                        snifferId = reader.ReadByte();                                        // sniffer id
                        build = reader.ReadUInt32();                                          // client build
                        locale = Encoding.ASCII.GetString(reader.ReadBytes(4));               // client locale
                        reader.ReadBytes(40);                                                 // session key
                        startTime = reader.ReadUInt32();                                      // start time
                        startTickCount = reader.ReadUInt32();                                 // start tick count
                        additionalLength = reader.ReadInt32();
                        var optionalData = reader.ReadBytes(additionalLength);
                        if (snifferId == 'S') // WSTC
                        {
                            // versions 1.5 and older store human readable sniffer description string in header
                            // version 1.6 adds 3 bytes before that data, 0xFF separator, one byte for major version and one byte for minor version, expecting 0x0106 for 1.6
                            if (additionalLength >= 3 && optionalData[0] == 0xFF)
                                snifferVersion = BitConverter.ToInt16(optionalData, 1);
                            else
                                snifferVersion = 0x0105;

                            if (snifferVersion >= 0x0107)
                                startTime = BitConverter.ToInt64(optionalData, 3);
                        }
                        break;
                    }
                    default:
                    {
                        // not supported version - let's assume the PKT bytes were a coincidence
                        reader.BaseStream.Position = 0;
                        break;
                    }
                }
            }
            return build;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe")))
            { 
                MessageBox.Show("WowPacketParser.exe not found in directory!", "Parsing Helper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (String.IsNullOrEmpty(txtSniffsDirectory.Text) || !Directory.Exists(txtSniffsDirectory.Text))
            {
                MessageBox.Show("Invalid sniff directory!", "Parsing Helper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbMode.SelectedIndex == 0)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (!item.Checked)
                        continue;

                    this.Text = "Parsing " + item.Text;

                    List<string> fileList = (List<string>)item.Tag;
                    CreateIndividualParseBatchFile(item.Text, fileList);
                    System.Diagnostics.Process.Start("parse.bat").WaitForExit();
                }
            }
            else if (cmbMode.SelectedIndex == 1)
            {
                if (String.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("You must enter your name for mass parses!", "Parsing Helper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (ListViewItem item in listView1.Items)
                {
                    if (!item.Checked)
                        continue;

                    this.Text = "Parsing " + item.Text;

                    List<string> fileList = (List<string>)item.Tag;
                    UpdateMassParseConfig(item.Text.ToLower(), txtName.Text.ToLower());
                    CreateMassParseBatchFile(item.Text, fileList);
                    System.Diagnostics.Process.Start("parse.bat").WaitForExit();
                }
            }
            else
            {
                MessageBox.Show("No mode selected!", "Parsing Helper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Text = "Parsing Helper";
            MessageBox.Show("Done!", "Parsing Helper");
        }

        private void UpdateMassParseConfig(string version, string name)
        {
            string configText = "";
            foreach (string line in System.IO.File.ReadLines("mass.config"))
            {
                if (!String.IsNullOrEmpty(configText))
                    configText += Environment.NewLine;

                string tmp = line;
                if (tmp.Contains(MASS_PARSE_VERSION_PH))
                    tmp = tmp.Replace(MASS_PARSE_VERSION_PH, version);
                if (tmp.Contains(MASS_PARSE_NAME_PH))
                    tmp = tmp.Replace(MASS_PARSE_NAME_PH, name);
                configText += tmp;
            }

            File.WriteAllText(Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe.config"), configText);
        }

        private void CreateIndividualParseBatchFile(string build, List<string> files)
        {
            string batchContents = "";
            batchContents += "echo Starting individual parse of " + build + "." + Environment.NewLine;
            batchContents += "xcopy /y \"individual.config\" \"" + Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe.config") + "\"" + Environment.NewLine;
            batchContents += Path.GetPathRoot(txtSniffsDirectory.Text).Replace("\\", "") + Environment.NewLine;
            batchContents += "cd \"" + txtSniffsDirectory.Text + "\"" + Environment.NewLine;

            string wppPath = "\"" + Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe") + "\"";
            int counter = 0;

            foreach (var file in files)
            {
                string shortFilePath = file.Replace(txtSniffsDirectory.Text + "\\", "");

                if (counter > 2)
                {
                    batchContents += "timeout 30" + Environment.NewLine;
                    counter = 0;
                }
                else
                {
                    batchContents += "start \"" + Path.GetFileName(file) + "\" ";
                    counter++;
                }

                batchContents += wppPath + " \"" + shortFilePath + "\"" + Environment.NewLine;
            }

            batchContents += "exit";
            File.WriteAllText("parse.bat", batchContents);
        }

        private void CreateMassParseBatchFile(string build, List<string> files)
        {
            string batchContents = "";
            batchContents += "echo Starting mass parse of " + build + "." + Environment.NewLine;
            //batchContents += "xcopy /y \"mass.config\" \"" + Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe.config") + "\"" + Environment.NewLine;
            batchContents += Path.GetPathRoot(txtSniffsDirectory.Text).Replace("\\", "") + Environment.NewLine;
            batchContents += "cd \"" + txtSniffsDirectory.Text + "\"" + Environment.NewLine;
            batchContents += "\"" + Path.Combine(txtWppDirectory.Text, "WowPacketParser.exe") + "\"";
            foreach (var file in files)
            {
                string shortFilePath = file.Replace(txtSniffsDirectory.Text + "\\", "");
                batchContents += " \"" + shortFilePath + "\"";
            }
            batchContents += Environment.NewLine + "exit";
            File.WriteAllText("parse.bat", batchContents);
        }

        private void btnReverseSelection_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = !item.Checked;
            }
        }
    }
}
