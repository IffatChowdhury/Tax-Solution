using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Net;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TSResources.xaml
    /// </summary>
    public partial class TSResources : Window
    {
        public TSResources()
        {
            InitializeComponent();
        }

        private void tinApp_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\TINForm.pdf";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "TIN Application"; 
            dlg.DefaultExt = ".pdf"; 
            dlg.Filter = "PDF Document (.pdf)|*.pdf"; 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void indret_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\Ind-Return English.doc";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Individual Return (English)";
            dlg.DefaultExt = ".doc";
            dlg.Filter = "MSWord Document (.doc)|*.doc";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void indretbn_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\Ind_Return_Form_Bangla.pdf";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Individual return (Bangla)";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Document (.pdf)|*.pdf";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void comret_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\Company Return.doc";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Company Return";
            dlg.DefaultExt = ".doc";
            dlg.Filter = "MSWord Document (.doc)|*.doc";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void l82c_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\List of 82C income.pdf";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "List of 82C income";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Document (.pdf)|*.pdf";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void paripatra_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\Paripatra.pdf";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Paripatra";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Document (.pdf)|*.pdf";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void guide_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\Return_Preparation_Guideline.pdf";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Return Preparation Guideline";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Document (.pdf)|*.pdf";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void taxrate_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\Files\TaxRate.pdf";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Tax Rate";
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Document (.pdf)|*.pdf";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                
                try
                {
                    File.Copy(path, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }
    }
}
