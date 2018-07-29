using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;
using System.IO;
using ZXCassetteDeck;
namespace ZXCassetteDeck
{
    delegate void CassetteDeckNextBlockEventHandler(ITZXBlock block, int blockIndex);

    public enum CassetteDeckStates
    {
        Show,
        Hide,
        Toggle
    }

    
    public partial class CassetteDeck : Form
    {
        bool started = false;
        public CassetteDeck()
        {
            InitializeComponent();
            started = false;
            listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;
            TogglePlaying(CassetteController.IsPlaying);

            CassetteController.NextBlock += CassetteDeck_NextBlock;
            CassetteController.FoundBlock += CassetteDeck_FoundBlock;
            CassetteController.BeginLoadFile += CassetteDeck_BeginLoadFile;
            CassetteController.EndFileLoad += CassetteDeck_EndFileLoad;
            CassetteController.FileClosed += CassetteDeck_FileClosed;
            CassetteController.PlayingChanged += CassetteDeck_PlayingChanged;
            CassetteController.Progress += CassetteDeck_Progress;
            CassetteController.DeckToggle += CassetteDeck_DeckToggle;
            FormClosing += Deck_FormClosing;
            Text = "No File";
            SetButtons(false);
        }

        //protected override void OnVisibleChanged(EventArgs e)
        //{
        //    if(!started)
        //        Location = new Point(SystemInformation.VirtualScreen.Width-Width,0);

        //    started = true;
        //    base.OnVisibleChanged(e);
        //}
        Form ownerForm;
        public Form OwnerForm
        {
            get { return ownerForm; }
            set
            {
                ownerForm = value;
                ownerForm.LocationChanged += OwnerForm_LocationChanged;
                ownerForm.Resize += OwnerForm_Resize;
                Height = OwnerForm.Height;
                Location = new Point(ownerForm.Left + ownerForm.Width, ownerForm.Top);
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            //Height = OwnerForm.Height;
            //Location = new Point(ownerForm.Left + ownerForm.Width, ownerForm.Top);
            base.OnVisibleChanged(e);
        }
        private void OwnerForm_Resize(object sender, EventArgs e)
        {
            Height = OwnerForm.Height;
            Location = new Point(OwnerForm.Left + OwnerForm.Width, OwnerForm.Top);
        }

        private void OwnerForm_LocationChanged(object sender, EventArgs e)
        {
            Height = OwnerForm.Height;
            Location = new Point(OwnerForm.Left + OwnerForm.Width, OwnerForm.Top);
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            listView1.SelectedItems[0].EnsureVisible();
        }

        private void CassetteDeck_DeckToggle(CassetteDeckStates cassetteDeckState)
        {
            if (cassetteDeckState == CassetteDeckStates.Toggle)
                Visible = !Visible;
            else
            {
                if (cassetteDeckState == CassetteDeckStates.Show)
                    Visible = true;
                else
                    Visible = false;
            }
        }

        private void Deck_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void CassetteDeck_FileClosed()
        {
            CassetteController.IsPlaying = false;
            TogglePlaying(CassetteController.IsPlaying);
            tapeView.Clear();
            blockView.Clear();
            listView1.Items.Clear();
            Text = "No File";
            SetButtons(false);
        }

        void SetButtons(bool value)
        {
            playButton.Enabled = value;
            pauseButton.Enabled = value;
            previousBlockButton.Enabled = value;
            nextBlockButton.Enabled = value;
            rewindButton.Enabled = value;
            fastForwardButton.Enabled = value;
            ejectButton.Enabled = value;

        }
        public string ProgressText = "";
        void SetCharIndexStart(TZXDataBlock block)
        {
            int b = 0;
            string s = block.Details;
            int charindex = s.IndexOf("TZXTAP Block:") + 16; // "TZXTAP Block: /r/n".Length
            charindexstart = charindex += 5; // "  {\r\n".Length

        }
        private void CassetteDeck_Progress(TZXFile tzxFile, int blocklength, int tapeCounter, int blockCounter, float overallProgress, float blockProgress, string block, ITZXDataBlock tZXDataBlock)
        {
            class11.SelectionStart = class11.SelectionEnd = 0;
            class11.SetSelectionForeColor(true, Color.Lime);
            class11.SetSelectionBackColor(true, SystemColors.WindowFrame);
            if (tzxFile != null) ProgressText = "Block:" + blockCounter.ToString("0000000") + "/" + (blocklength - 1).ToString() + " Total: " + tapeCounter.ToString("0000000") + "/" + tzxFile.TZXLength.ToString();
            counter1.Value = tapeCounter+1;
            if (panel1.Visible)
            {
                toolStripStatusLabel1.Text = blockCounter == 0 ? "Paused" : "Playing";
                blockView.CursorPen = null;
                tapeView.CursorPen = null;
                blockView.Next(blockProgress);
                tapeView.CursorPen = blockView.CursorPen;
                tapeView.Next(overallProgress);
            }
            if (tZXDataBlock != null)
            {
                if (class11.Visible&& tZXDataBlock is ITZXDataBlock)
                {
                    int b = tZXDataBlock.Progress;
                    string s = tZXDataBlock.Details;
                    int charindex = s.IndexOf("TZXTAP Block:") + 16; // "TZXTAP Block: /r/n".Length
                    charindex += 5; // "  {\r\n".Length
                    int row = b / 16;
                    int col = (b % 16);
                    charindex = charindex + (row * 57);
                    charindex += 11;
                    charindex += (col * 3) - 3;
                    class11.GotoPosition(charindex);
                    class11.SelectionStart = charindex;
                    class11.SelectionEnd = charindex + 2;
                    class11.SetSelectionBackColor(true, Color.Lime);
                    class11.SetSelectionForeColor(true, SystemColors.WindowFrame);
                }
            }
        }

        private void CassetteDeck_PlayingChanged(bool isplaying)
        {
            TogglePlaying(isplaying);
        }

        void TogglePlaying(bool isPlaying)
        {
            if (isPlaying)
            {
                playButton.Visible = false;
                pauseButton.Visible = true;
            }
            else
            {
                playButton.Visible = true;
                pauseButton.Visible = false;
            }

        }

        private void CassetteDeck_BeginLoadFile(string filename)
        {
            listView1.Items.Clear();
            listViewItems = new List<ListViewItem>();
        }
        private void CassetteDeck_EndFileLoad(string filename, TZXFile tzxFile)
        {
            listView1.BeginUpdate();
            listView1.Items.AddRange(listViewItems.ToArray());
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].UseItemStyleForSubItems = false;
                listView1.Items[i].SubItems[1].BackColor = SystemColors.ControlDark;
            }
            listView1.EndUpdate();
            CassetteController.TZXBlockIndex = 0;
            Text = Path.GetFileName(filename);
            tapeView.TZXFile = CassetteController.TZXFile;
            UpdateTapeViewBlock();
            SetButtons(true);
        }


        void UpdateTapeViewBlock()
        {

            TZXFile tzxblockfile = new TZXFile();
            tzxblockfile.Blocks.Clear();
            tzxblockfile.Blocks.Add(CassetteController.TZXFile.Blocks[CassetteController.TZXBlockIndex]);
            blockView.TZXFile = tzxblockfile;
        }

        int row;
        int col;
        int charindex;
        int charindexstart;
        private void CassetteDeck_NextBlock(ITZXBlock block, int blockIndex)
        {
            if (InvokeRequired) 
            {
                Invoke(new CassetteDeckNextBlockEventHandler(CassetteDeck_NextBlock), new object[] { block, blockIndex });
                return;
            }
            if (listView1.Items.Count == 0) return;
                foreach (ListViewItem item in listView1.Items)
                item.Selected = false;

            class11.Text = block.Details;
            if (class11.Visible)
            {
                if (block is ITZXDataBlock)
                {
                    int b = 0;
                    string s = block.Details;
                    int charindex = s.IndexOf("TZXTAP Block:") + 16; // "TZXTAP Block: /r/n".Length
                    charindexstart = charindex += 5; // "  {\r\n".Length

                    int row = b / 16;
                    int col = (b % 16);
                    //line += row + 2;
                    charindex = charindex + (row * 57);
                    charindex += 11;
                    charindex += (col * 3) - 3;
                    class11.GotoPosition(charindex);
                    class11.SelectionStart = charindex;
                    class11.SelectionEnd = charindex + 2;
                    class11.SetSelectionBackColor(true, Color.Lime);
                    class11.SetSelectionForeColor(true, SystemColors.WindowFrame);
                }
                else
                {
                    class11.SelectionStart = class11.SelectionEnd = 0;
                    class11.SetSelectionForeColor(true, Color.Lime);
                    class11.SetSelectionBackColor(true, SystemColors.WindowFrame);
                    class11.SelectionEnd = class11.SelectionStart = 0;
                    class11.Invalidate();
                    Console.WriteLine(block.ID.ToString());
                }
            }

            listView1.Items[blockIndex].Selected = true;
            UpdateTapeViewBlock();

        }

        private void Deck_Load(object sender, EventArgs e)
        {
            this.Resize += CassetteDeck_Resize;
            listView1.Clear();
            listView1.Columns.Add("", 0);
            listView1.Columns.Add("", 35, HorizontalAlignment.Right);
            listView1.Columns.Add("Catalogue", -2 ,HorizontalAlignment.Left);
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            listView1.OwnerDraw = true;
            listView1.DrawColumnHeader += ListView1_DrawColumnHeader;
            listView1.DrawItem += ListView1_DrawItem;
            listView1.DrawSubItem += ListView1_DrawSubItem;
        }

        private void CassetteDeck_Resize(object sender, EventArgs e)
        {
            if (listView1.Columns.Count >= 2) listView1.Columns[2].Width = -2;
        }

        private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ListView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat stringFormat = new StringFormat())
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.Bounds);
                switch (e.ColumnIndex)
                {
                    case 1: stringFormat.Alignment = StringAlignment.Far;
                        e.Graphics.FillRectangle(SystemBrushes.ControlDark, e.Bounds);

                        break;
                    case 2: stringFormat.Alignment = StringAlignment.Near; break;
                }
                stringFormat.LineAlignment = StringAlignment.Center;



                using (Font FontBold = new Font("Consolas", 11, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.Lime))
                    e.Graphics.DrawString(listView1.Columns[e.ColumnIndex].Text, FontBold, brush, e.Bounds, stringFormat);
            }
            //e.DrawText();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        public void OpenFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ZXCassetteDeck.Properties.Settings.Default.DefaultDirectory.Length == 0)
                {
                    ZXCassetteDeck.Properties.Settings.Default.DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    ZXCassetteDeck.Properties.Settings.Default.Save();
                }
                ofd.InitialDirectory = ZXCassetteDeck.Properties.Settings.Default.DefaultDirectory;
                ofd.Filter = "TZX Files (*.tzx)|*.tzx| tape files (*.tap)|*.tap|All Tape Files (*.tap;*.tzx)|*.tap;*.tzx";
                ofd.FilterIndex = 3;
                ofd.RestoreDirectory = true;
                ofd.FileOk += Ofd_FileOk;
                ofd.ShowDialog(this);
            }
        }
        private void Ofd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (OpenFileDialog ofd = sender as OpenFileDialog)
            {
                ZXCassetteDeck.Properties.Settings.Default.DefaultDirectory = Path.GetDirectoryName(ofd.FileName);
                ZXCassetteDeck.Properties.Settings.Default.Save();

                LoadFile(ofd.FileName);
            }
        }

        List<ListViewItem> listViewItems;
        void LoadFile(string filename)
        {
            listViewItems = new List<ListViewItem>();
            listView1.Items.Clear();
            CassetteController.LoadFile(filename);
            if (listView1.Items.Count > 0)
            {
                listView1.Items[0].Selected = true;
            }
            
            
        }

        private void CassetteDeck_FoundBlock(ITZXBlock block)
        {

            ListViewItem lvi = new ListViewItem("");

            lvi.SubItems.Add(block.Index.ToString());
            lvi.SubItems.Add(block.ToString());
            listViewItems.Add(lvi);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (!CassetteController.FileLoaded) return;
            CassetteController.Play();
            playButton.Visible = false;
            pauseButton.Visible = true;
            toolStripStatusLabel1.Text = "Play Pressed";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!CassetteController.FileLoaded) return;
            CassetteController.Pause();
            playButton.Visible = true;
            pauseButton.Visible = false;
            toolStripStatusLabel1.Text = "Pause Pressed";
        }

        int CurrentTZXBlockIndex
        {
            get
            {
                int c = 0;
                for (int i = 0; i < CassetteController.TZXBlockIndex; i++) c += CassetteController.TZXFile.Blocks[i].Details.Length;
                return c;
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!CassetteController.FileLoaded) return;
            bool p = CassetteController.IsPlaying;
            CassetteController.IsPlaying = false;
            if (CassetteController.TZXBlockIndex >= 0)
                CassetteController.TZXBlockIndex--;

            CassetteController.TZXTapeCounter = CurrentTZXBlockIndex;
            toolStripStatusLabel1.Text = "Previous Block Pressed";
            //tapeView.Next(CassetteController.Percent(CassetteController.TZXFile.TZXLength, CassetteController.TZXTapeCounter));

            CassetteController.IsPlaying = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!CassetteController.FileLoaded) return;
            bool p = CassetteController.IsPlaying;
            CassetteController.IsPlaying = false;
            if (CassetteController.TZXBlockIndex < CassetteController.TZXFile.Blocks.Count-1)
                CassetteController.TZXBlockIndex++;

            CassetteController.TZXTapeCounter = CurrentTZXBlockIndex;
            tapeView.Next(CassetteController.Percent(CassetteController.TZXFile.TZXLength, CassetteController.TZXTapeCounter));
            toolStripStatusLabel1.Text = "Next Block Pressed";
            CassetteController.IsPlaying = p;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!CassetteController.FileLoaded) return;
            bool p = CassetteController.IsPlaying;
            CassetteController.IsPlaying = false;
            CassetteController.TZXBlockIndex = -1;
            CassetteController.IsPlaying = false ;
            CassetteController.ReLoadFile();

            toolStripStatusLabel1.Text = "First Block Pressed";
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (!CassetteController.FileLoaded) return;
            bool p = CassetteController.IsPlaying;
            CassetteController.IsPlaying = false;
            CassetteController.TZXBlockIndex = CassetteController.TZXFile.Blocks.Count - 1;
            CassetteController.IsPlaying = p;
            toolStripStatusLabel1.Text = "Last Block Pressed";
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            CassetteController.CloseFile();
            toolStripStatusLabel1.Text = "File Ejected";
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            OpenFile();
            toolStripStatusLabel1.Text = "Open File";
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
            tapeView.Visible = panel1.Visible;
            blockView.Visible = panel1.Visible;
            toolStripStatusLabel1.Text = "Tape View " + (panel1.Visible ? "Enabled" : "Disabled");
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            CassetteController.TZXBlockIndex = listView1.SelectedItems[0].Index;
            //CassetteController.Pause();
            playButton.Visible = true;
            pauseButton.Visible = false;
            counter1.Value = 0;
            toolStripStatusLabel1.Text = "Paused";

            Details form1 = new Details();
            form1.TZXDetails = CassetteController.TZXFile.Blocks[CassetteController.TZXBlockIndex].Details;
            form1.ShowDialog(this);
            //    int[] data=null;
            //    switch (CassetteController.TZXFile.Blocks[CassetteController.TZXBlockIndex].GetType().Name)
            //    {
            //        case "StandardSpeedDataBlock":
            //            data = Data(((StandardSpeedDataBlock)CassetteController.TZXFile.Blocks[CassetteController.TZXBlockIndex]).TAPBlock.Data);
            //            break;
            //        case "PureTone":
            //            data = Data(((PureTone)CassetteController.TZXFile.Blocks[CassetteController.TZXBlockIndex]).TAPBlock.Data);
            //            break;
            //    }
            //    if (data != null)
            //    {
            //        form1.LoadData(data);
            //        form1.ShowDialog(this);
            //    }
            //}

            //int[] Data(byte[] data)
            //{
            //    int[] Data = new int[data.Length];
            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        Data[i] = data[i];
            //    }
            //    return Data;
            //}

        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            toolStripButton4.Visible = false;
            toolStripButton5.Visible = true;
            class11.Visible = true;
            Width = 877;
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            toolStripButton5.Visible = false;
            toolStripButton4.Visible = true;
            class11.Visible = false;
            Width = 500;
        }
    }
}
