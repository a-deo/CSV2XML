using System.IO;
using System.Linq;
using System.Windows;
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

        public void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Open CSV file
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);

            // Read CSV file
            string[] lines = File.ReadAllLines(openFileDialog.FileName);

            // Get the attribute names from the first line of the CSV file
            string[] attributeNames = lines[0].Split(',');

            // !! MODIFY THE FOLLOWING SECTION TO CHANGE XML STRUCTURE !!
            // Create the XML document
            XDocument doc = new(
                new XElement("root",
                    // Loop through the CSV file lines
                    lines.Skip(1).Select(line => {
                        // Split the line into fields
                        string[] fields = line.Split(',');

                        // Create the XML element
                        return new XElement("attribute",
                            // Loop through the attribute names and fields
                            attributeNames.Zip<string, string, object>(fields, (name, value) => {
                                // Add custom logic here to verify if value are valid.
                                // If the attribute name is "description", create an XElement
                                if (name == "description")
                                {
                                    // Only return the XElement if the field value is not empty
                                    return value != "" ? new XElement(name, value) : null;
                                }
                                // Otherwise, create an XAttribute
                                else
                                {
                                    // Only return the XAttribute if the field value is not empty
                                    return value != "" ? new XAttribute(name, value) : null;
                                }
                            })
                        );
                    })
                )
            );
            // !! END OF SECTION TO MODIFY. DO NOT GO BELOW !!

            // Print to XML canvas
            txtEditor2.Text = doc.ToString();
        }

        // Logic for 'Copy XML' button
        private void BtnCopyXML_Click(object sender, RoutedEventArgs e)
        {
            string text = txtEditor2.Text;
            System.Windows.Clipboard.SetText( text );
        }

        // Logic for 'Save XML' button
        private void BtnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtEditor2.Text);
        }
    }
}
