using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace notion
{
    public partial class Form1 : Form
    {
        public struct Notion
        {
            public string notion, notionName;
        }
        public List<Notion> Notions = new List<Notion>();

        private int _notionPageCount = 0;


        public Form1()
        {
            InitializeComponent();
            JsonDataRead();
            richTextBox1.Text = Notions[0].notion;
            textBox1.Text = Notions[0].notionName;
            comboBox1.SelectedIndex = 0;
        }

        private void JsonDataWrite()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if (!Directory.Exists(currentDirectory + "\\Json\\"))
            {
                Directory.CreateDirectory(currentDirectory + "\\Json\\");
                StreamWriter fc = File.CreateText(currentDirectory + "\\Json\\Notions.json");
                fc.Close();
            }
            else
            {
                File.Delete(currentDirectory + "\\Json\\Notions.json");
                StreamWriter fc = File.CreateText(currentDirectory + "\\Json\\Notions.json");
                fc.Close();
            }
            string JsonNotions = JsonConvert.SerializeObject(Notions);
            File.WriteAllText(currentDirectory + "\\Json\\Notions.json", JsonNotions);
        }
        private void JsonDataRead()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if (Directory.Exists(currentDirectory + "\\Json\\"))
            {
                string JsonNotions = File.ReadAllText(currentDirectory + "\\Json\\Notions.json");
                List<Notion> Data = JsonConvert.DeserializeObject<List<Notion>>(JsonNotions);

                for (int i = 0; i < Data.Count; i++)
                {
                    Notions.Add(Data[i]);
                    comboBox1.Items.Add(Data[i].notionName);
                }
            }
            else
            {
                Notion not = new Notion();
                not.notionName = "Yeni bir Not yaz";
                not.notion = "";
                Notions.Add(not);
                comboBox1.Items.Add("Yeni bir Not yaz");
                JsonDataWrite();
            }

        }
        private void JsonReset()
        {
            int limit = Notions.Count;
            for (int i = limit - 1; i > 0; i--)
            {
                Notions.RemoveAt(i);
                comboBox1.Items.RemoveAt(i);
            }
            JsonDataWrite();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Notion not = new Notion();
            not.notion = richTextBox1.Text.ToString();
            not.notionName = textBox1.Text.ToString();

            if (_notionPageCount == 0)
            {
                Notions.Add(not);
                comboBox1.Items.Add(not.notionName);
            }
            else
            {
                Notions[_notionPageCount] = not;
                comboBox1.Items[_notionPageCount] = not.notionName;
            }
            _notionPageCount = 0;
            richTextBox1.Text = Notions[_notionPageCount].notion;
            textBox1.Text = Notions[_notionPageCount].notionName;
            JsonDataWrite();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Enabled = true;
            textBox1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Notions.RemoveAt(_notionPageCount);
            comboBox1.Items.RemoveAt(_notionPageCount);
            _notionPageCount = 0;
            richTextBox1.Text = Notions[_notionPageCount].notion;
            textBox1.Text = Notions[_notionPageCount].notionName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            JsonReset();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _notionPageCount = comboBox1.SelectedIndex;
            if (_notionPageCount == 0)
            {
                richTextBox1.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                richTextBox1.Enabled = false;
                textBox1.Enabled = false;
            }
            richTextBox1.Text = Notions[_notionPageCount].notion.ToString();
            textBox1.Text = Notions[_notionPageCount].notionName.ToString();
        }
    }
}