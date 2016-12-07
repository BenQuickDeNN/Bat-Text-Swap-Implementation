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

namespace DirectoryTextManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            initialize_Widget();
            initialize_GlobalVar();
        }
        void initialize_Widget()
        {
            button_start.Click += new EventHandler(button_start_Click);
            textBox_dotname.Text = "*.";
            checkBox_include_sub.Checked = true;
            checkBox_include_sub.Enabled = false;
        }
        void initialize_GlobalVar()
        {

        }
        void button_start_Click(object obj, EventArgs ea)
        {
            try {
                progressBar_progress.Value = 0;
                DirectoryInfo thisFolder = new DirectoryInfo(@textBox_url.Text);
                DirectoryInfo[] info = thisFolder.GetDirectories();
                progressBar_progress.Maximum = info.Length;
                foreach (DirectoryInfo di in info)
                {
                    FileInfo[] fi = di.GetFiles(textBox_dotname.Text);
                    foreach (FileInfo fn in fi)
                    {
                        richTextBox_msg.AppendText("替换 " + fn.FullName + "\n");
                        swapContent(fn.FullName, textBox_replaced.Text, textBox_replace.Text);
                    }
                    progressBar_progress.Value += 1;
                    Application.DoEvents();
                }
            }catch(Exception e)
            {
                richTextBox_msg.AppendText(e.ToString() + "\n");
            }
        }
        /// <summary>
        /// 替换内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name=""></param>
        void swapContent(string filePath, string str_replaced, string str_replace)
        {
            StreamReader contentReader = new StreamReader(filePath, Encoding.Default);
            string content = "";
            content = contentReader.ReadToEnd();
            contentReader.Close();
            content = content.Replace(str_replaced, str_replace);// 替换
            StreamWriter contentWriter = new StreamWriter(filePath, false, Encoding.Default);// 覆盖原文件
            contentWriter.Write(content);
            contentWriter.Close();
        }
    }
}
