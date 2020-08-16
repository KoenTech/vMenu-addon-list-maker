using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FiveMAddonGen
{
    public partial class Form1 : Form
    {
        int count;
        StringBuilder stringBuilder = new StringBuilder();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open vehicles.meta File";
            theDialog.Filter = "META files|*.meta";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                GetFromXml(theDialog.FileName.ToString());
            }
        }

        void GetFromXml(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "InitDatas")
                {
                    foreach (XmlNode node1 in node.ChildNodes)
                    {

                        if (node1.Name == "Item")
                        {
                            stringBuilder.AppendLine("    \"" + node1.ChildNodes[0].InnerText + "\",");
                            count++;
                        }
                    }
                }
            }
            if (stringBuilder.Length < 2) stringBuilder.Append("This File is not a vehicles.meta file!");
            richTextBox1.Text = stringBuilder.ToString();
            label1.Text = String.Format("Detected {0} vehicles!", count);
            stringBuilder.Clear();
            count = 0;
        }
    }
}
