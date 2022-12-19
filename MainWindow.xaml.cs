using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Xml.Linq;

namespace CSV2XML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);

            //Load CSV
            var lines = File.ReadAllLines(openFileDialog.FileName);

            //Construct array with comma delimiter
            string[] headers = lines[0].Split(',').Select(x => x.Trim('\"')).ToArray();

            //Length of records
            int lengthOfRecords = headers.Length - 1;

            //Generate XML
            var xml = new XElement("Root",
               lines.Where((line, index) => index > 0).Select(line => new XElement("attribute",
                  line.Split(',').Select((column, index) => new XElement(headers[index], column)),
                  line.Split(',').Select((column, index) => new XAttribute(headers[index], column)))));

            //txtEditor2.Text = (string)xml;
            xml.Save(@".\xml01.xml");
        }

        private void btnGenerateXML_Click(object sender, RoutedEventArgs e)
        {
            //Load XML file
            XDocument xdoc = XDocument.Load(@".\xml01.xml");

            //Remove description attribute
            xdoc.Descendants().Attributes("description").Remove();

            //Remove all elements except for description
            xdoc.Root.Elements("attribute").Elements().Where(e => e.Name != "description").Remove();

            xdoc.Save(@".\xml02.xml");

            //Open generated second XML document
            const string directory = @".\xml02.xml";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = directory;
            txtEditor2.Text = File.ReadAllText(openFileDialog.InitialDirectory);
        }

        private void btnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtEditor2.Text);
        }
    }
}
