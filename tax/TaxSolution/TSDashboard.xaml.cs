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
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TSDashboard.xaml
    /// </summary>
    public partial class TSDashboard : Window
    {
        public TSDashboard()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void personalReturn_Click(object sender, MouseButtonEventArgs e)
        {
            PRAssesseeInformation PRAssesseeInformationForm = new PRAssesseeInformation();
            PRAssesseeInformationForm.Owner = this;
            PRAssesseeInformationForm.Show();
        }

        private void tinBtn_Click(object sender, MouseButtonEventArgs e)
        {
            AFTIN AFTINForm = new AFTIN();
            //this.Hide();
            AFTINForm.Owner = this;
            AFTINForm.Show();
            //this.Show();
        }

        private void button2_Click(object sender, MouseButtonEventArgs e)
        {
            TSAbout about = new TSAbout(this);
            about.Show();
        }

        private void userListBtn_Click(object sender, MouseButtonEventArgs e)
        {
            TSClientList TSClientListForm = new TSClientList();
            TSClientListForm.Owner = this;
            TSClientListForm.Show();
        }

        private void resourceBtn_Click(object sender, MouseButtonEventArgs e)
        {
            TSResources TSResourcesForm = new TSResources();
            TSResourcesForm.Owner = this;
            TSResourcesForm.Show();
        }

        private void settingsBtn_Click(object sender, MouseButtonEventArgs e)
        {
            if (TaxSolution.Properties.Settings.Default.logintype == "superadmin")
            {
                TSSettings TSSettingsForm = new TSSettings();
                TSSettingsForm.Owner = this;
                TSSettingsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You don't have access to this area", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void openBtn_Click(object sender, MouseButtonEventArgs e)
        {
            string mydoc = "";

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".ts";
            dlg.Filter = "Tax Solution file (.ts)|*.ts";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                mydoc = dlg.FileName.ToString();
                XpsDocument xps = new XpsDocument(@mydoc, System.IO.FileAccess.Read);
                TSPrintPreview preview = new TSPrintPreview();
                preview.Owner = this;
                preview.Document = xps.GetFixedDocumentSequence();
                preview.Show();
            }
        }
    }
}
