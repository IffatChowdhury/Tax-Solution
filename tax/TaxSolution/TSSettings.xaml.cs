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
using System.Management;
using System.IO;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TSSettings.xaml
    /// </summary>
    public partial class TSSettings : Window
    {
        public TSSettings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkLicense())
            {
                this.licenseStatus.Text = "Active";
            }
            else
            {
                this.licenseStatus.Text = "Demo";
            }
            this.licenseKey.Text = TaxSolution.Properties.Settings.Default.licensekey;
        }

        public string EncodePassword(string originalPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(originalPassword);
            data = x.ComputeHash(data);
            string ret = "";
            for (int i = 0; i < data.Length; i++)
                ret += data[i].ToString("x2").ToLower();
            return ret;
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private bool checkLicense()
        {
            string licensekey = TaxSolution.Properties.Settings.Default.licensekey;
            if (licensekey.Length < 1)
            {
                HardDisk hd = new HardDisk();
                string serial = hd.getserial();
                TaxSolution.Properties.Settings.Default.hdserial = serial;

                serial = EncodePassword(serial + "CREASH IS BEST");
                serial = ReverseString(serial);
                serial = "L" + serial.Substring(0, 15).ToUpper();

                TaxSolution.Properties.Settings.Default.licensekey = serial;
                licensekey = serial;
            }
            TaxSolution.Properties.Settings.Default.Save();
            return licenseStatusCheck(licensekey);
        }

        bool licenseStatusCheck(string key)
        {
            string licensekey = "";

            HardDisk hd = new HardDisk();
            string serial = hd.getserial();
            TaxSolution.Properties.Settings.Default.hdserial = serial;

            serial = EncodePassword(serial + "CREASH IS BEST");
            serial = ReverseString(serial);
            serial = "L" + serial.Substring(0, 15).ToUpper();

            TaxSolution.Properties.Settings.Default.licensekey = serial;
            licensekey = serial;
            if (licensekey == key)
            {
                string enc = EncodePassword(key + "CREASH IS BEST" + "lite");
                enc = ReverseString(enc);
                enc = EncodePassword(enc + "CREASH IS BEST");
                enc = ReverseString(enc);
                string activation = enc.Substring(0, 15).ToUpper();
                string savedact = TaxSolution.Properties.Settings.Default.activationkey.ToUpper();

                if (activation == savedact)
                {
                    return true;
                }
            }

            return false;
        }

        private void backupBtn_Click(object sender, RoutedEventArgs e)
        {
            string path = @Directory.GetCurrentDirectory().ToString() + @"\TaxDB.accdb";

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Tax Solution Backup";
            dlg.DefaultExt = ".mdb";
            dlg.Filter = "Database (.mdb)|*.mdb";

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

        private void saveBtn_Click(object sender, MouseEventArgs e)
        {
            string licensekey = this.licenseKey.Text;
            string activationkey = this.activationKey.Text;

            TaxSolution.Properties.Settings.Default.activationkey = activationkey;

            if (this.adminPass.Password.Length > 0)
            {
                TaxSolution.Properties.Settings.Default.adminPass = EncodePassword(this.adminPass.Password);
                this.adminPass.Password = "";
            }
            if (this.superAdminPass.Password.Length > 0)
            {
                TaxSolution.Properties.Settings.Default.superadminPass = EncodePassword(this.superAdminPass.Password);
                this.superAdminPass.Password = "";
            }

            TaxSolution.Properties.Settings.Default.Save();

            MessageBox.Show("Settings saved successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
