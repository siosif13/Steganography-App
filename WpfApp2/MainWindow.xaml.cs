using System;
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
using System.Drawing;
using System.IO;

namespace SteganographyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filename;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog browser = new Microsoft.Win32.OpenFileDialog();
            browser.DefaultExt = ".png";
            browser.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            Nullable<bool> result = browser.ShowDialog();
            if (result == true)
            {
                filename = browser.FileName;
                ImageBox.Source = new BitmapImage(new Uri(filename));
            }

        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TextBox.Text == "Enter text here...")
            {
                TextBox.Text = string.Empty;
            }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {

            if (ImageBox.Source == null)
            {
                TextBox.Text = "Please select an image first.";
            }
            else
            {
                Steganography steg = new Steganography(System.Drawing.Image.FromFile(filename));

                try
                {
                    steg.Encrypt(TextBox.Text.Trim());

                    string directory = System.IO.Path.GetDirectoryName(filename);
                    string name = System.IO.Path.GetFileNameWithoutExtension(filename);
                    string extension = System.IO.Path.GetExtension(filename);
                    int count = 1;
                    filename = filename.Replace(extension, "_Encrypted.png");
                    while (File.Exists(filename))
                    {
                        filename = directory + "\\" + name + "_Encrypted(" + count + ").png";
                        count++;
                    }

                    steg.Image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);

                    TextBox.Text = "The message has been encrypted." + '\n' + "You will find a new image " +
                        "with the \"_Encrypted\" tag in the same folder.";
                }                
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            Steganography steg = new Steganography(new Bitmap(filename));
            TextBox.Text = steg.Decrypt();
        }

    }
}
