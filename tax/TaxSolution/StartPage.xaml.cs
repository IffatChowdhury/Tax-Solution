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
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Window
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void startAppBtn_Click(object sender, RoutedEventArgs e)
        {
            TSDashboard TSDashboardForm = new TSDashboard();
            this.Hide();
            TSDashboardForm.ShowDialog();
            this.Close();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
