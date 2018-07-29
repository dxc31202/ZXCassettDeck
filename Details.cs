using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZXCassetteDeck
{
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
        }

        public string TZXDetails
        {
            set { richTextBox1.Text = value; }
        }
        private void Details_Load(object sender, EventArgs e)
        {

        }
    }
}
