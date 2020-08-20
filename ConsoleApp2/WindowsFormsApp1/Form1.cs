using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form2 form2 { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            //DownloadFile(textBox1.Text, textBox2.Text);
            form2.Show();
            form2.UpdateUI(textBox1.Text, textBox2.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
          
            form2 = new Form2(this);
            form2.Show();

        }

        public void DownloadFile(string URL, string filename, ProgressBar prog)
        {
            try
            {
                HttpWebRequest Myrq = (HttpWebRequest)WebRequest.Create(URL);
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }

                Stream st = myrp.GetResponseStream();
                Stream so = new FileStream(filename, FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    Application.DoEvents();
                    so.Write(by, 0, osize);

                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, by.Length);
                }
                so.Close();
                st.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
