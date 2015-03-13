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

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TSLogin.xaml
    /// </summary>
    public partial class TSLogin : Window
    {
        public TSLogin()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = this.loginname.Text;
            string pass = EncodePassword(this.loginpass.Password);

            string pass2 = TaxSolution.Properties.Settings.Default.adminPass.ToString();
            string pass3 = TaxSolution.Properties.Settings.Default.superadminPass.ToString();
            
            if (name == "admin" && pass == pass2)
            {
                TaxSolution.Properties.Settings.Default.loggedIn = true;
                TaxSolution.Properties.Settings.Default.logintype = "admin";
                TSDashboard tsDashboard = new TSDashboard();
                tsDashboard.Show();
                this.Close();
            }
            else if (name == "superadmin" && pass == pass3)
            {
                TaxSolution.Properties.Settings.Default.loggedIn = true;
                TaxSolution.Properties.Settings.Default.logintype = "superadmin";
                TSDashboard tsDashboard = new TSDashboard();
                tsDashboard.Show();
                this.Close();
            }
            else
            {
                TaxSolution.Properties.Settings.Default.loggedIn = false;
                TaxSolution.Properties.Settings.Default.logintype = "";
                MessageBox.Show("Invalid user name/password combination", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
