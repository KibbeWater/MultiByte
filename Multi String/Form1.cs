using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using MultiByte.JSON;

namespace Multi_String
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] SafeFileNames;
        private string[] FileNames;
        private string[] Reverts;

        private void Button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
                
            };
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SafeFileNames = dialog.SafeFileNames;
                FileNames = dialog.FileNames;

                textBox1.Text = "";
                foreach (string String in dialog.SafeFileNames)
                {
                    textBox1.Text += " " + String + ";";
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
                foreach(string FileName in FileNames)
                {
                    var content = File.ReadAllLines(FileName);
                    int amount = 1;
                    foreach (string line in content)
                    {
                        if (line.Contains(textBox2.Text))
                        {
                            richTextBox1.AppendText("Found string \"" + content[amount - 1] + "\", Found on file: " + amount + ", Found on line: " + amount + "\n");
                        }
                        amount++;
                    }
                }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            int files = 0;
            int lines = 0;
            foreach (string FileName in FileNames)
            {
                var content = File.ReadAllLines(FileName);
                int amount = 1;
                foreach (string line in content)
                {
                    if (line.ToLower().Contains(textBox2.Text.ToLower()))
                    if (line.ToLower().Contains(textBox2.Text.ToLower()))
                    if (line.ToLower().Contains(textBox2.Text.ToLower()))
                    if (line.ToLower().Contains(textBox2.Text.ToLower()))
                    if (line.ToLower().Contains(textBox2.Text.ToLower()))
                                {
                        richTextBox1.AppendText("Found string \"" + content[amount - 1].Replace("    ", "") + "\",    Found on file: " + FileName + ",    Found on line: " + amount + "\n");
                    }
                    amount++;
                    lines++;
                }
                files++;
            }
            FileLabel.Text = "Files: " + files;
            LinesLabel.Text = "Lines: " + lines;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            richTextBox1.Size = new Size(this.Size.Width-301,this.Size.Height-63);
            label3.Location = new Point(0, this.Size.Height - 54);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = Application.ProductVersion;
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //if(Reverts[0] != null)
            //Array.Clear(Reverts,0,Reverts.Length);
            var amount = 0;
            foreach(string s in SafeFileNames)
            {
                amount++;
            }
            Reverts = new string[amount];
            var i = 0;
            foreach (string FileName in FileNames)
            {
                var content = File.ReadAllText(FileName);
                Revert revert = new Revert();
                revert.fileName = FileName;
                revert.content = content;
                string jsonified = JsonConvert.SerializeObject(revert);
                var fileText = content.Replace(replace_text.Text,replace_with.Text);
                File.WriteAllText(FileName,fileText);
                Reverts[i] = jsonified;
                i++;
                Console.WriteLine("Replaced a string in " + FileName + ",   Changed to \"" + fileText + "\"");
                Console.WriteLine("Old value: " + replace_text.Text + " New Value: " + replace_with.Text);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            foreach(string JSON in Reverts)
            {
                var content = JsonConvert.DeserializeObject<Revert>(JSON);
                File.WriteAllText(content.fileName,content.content);
            }
        }
    }
}
