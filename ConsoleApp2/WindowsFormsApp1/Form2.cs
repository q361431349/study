using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form1 form1 { get; set; }
        public Form2(Form1 nForm1)
        {
            form1 = nForm1;
            InitializeComponent();
        }


        public Action<string, string, ProgressBar> Update;

        private void Form2_Load(object sender, EventArgs e)
        {
            Update = form1.DownloadFile;
        }

        public void UpdateUI(string URL, string filename)
        {
            Update?.Invoke(URL, filename, progressBar1);
        }

    }
}
