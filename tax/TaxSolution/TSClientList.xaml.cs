using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TSClientList.xaml
    /// </summary>
    public partial class TSClientList : Window
    {
        private int selectedid;

        public TSClientList()
        {
            InitializeComponent();
            this.selectedid = 0;
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter();
            taxDBDataSetcustomerTableAdapter.Fill(taxDBDataSet.customer);
            System.Windows.Data.CollectionViewSource customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = this.customer_name.Text;
            string father = this.customer_father_name.Text;
            string tin = this.customer_tin.Text;
            DateTime dob = DateTime.Today;
            bool hasval = false;
            int strategy = 0;

            if (this.customer_name.Text != "Name" && this.customer_name.Text.Length > 0)
            {
                name = this.customer_name.Text;
                hasval = true;
                strategy = 1;   //Only name available
            }
            if (this.customer_father_name.Text != "Father's Name" && this.customer_father_name.Text.Length > 0)
            {
                father = this.customer_father_name.Text;
                hasval = true;
                if (strategy == 1)
                {
                    strategy = 2;   //Both name and father's name available
                }
                else
                {
                    strategy = 3;   //Only father's name available
                }
            }
            if (this.customer_tin.Text != "TIN" && this.customer_tin.Text.Length > 0)
            {
                tin = this.customer_tin.Text;
                hasval = true;
                strategy = 4;   //Only tin available

                if (strategy == 1)
                {
                    strategy = 5;   //name and tin available
                }
                else if (strategy == 2)
                {
                    strategy = 6;   //name, father's name and tin available
                }
                else if (strategy == 3)
                {
                    strategy = 7;   //father's name and tin available
                }
            }
            if (this.customer_dob.SelectedDate != null)
            {
                dob = this.customer_dob.SelectedDate.Value;
                hasval = true;
                strategy = 8;   //only dob

                switch (strategy)
                {
                    case 1:
                        strategy = 9;   //name & dob
                        break;
                    case 2:
                        strategy = 10;  //name, father's name & dob
                        break;
                    case 3:
                        strategy = 11;  //father's name & dob
                        break;
                    case 4:
                        strategy = 12;  //tin & dob
                        break;
                    case 5:
                        strategy = 13;  //name, tin & dob
                        break;
                    case 6:
                        strategy = 14;  //name, father's name, tin & dob
                        break;
                    case 7:
                        strategy = 15;  //father's name, tin & dob
                        break;
                }
            }

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter();

            switch (strategy)
            {
                case 1:
                    taxDBDataSetcustomerTableAdapter.FillSearch1(taxDBDataSet.customer, name);
                    break;
                case 2:
                    taxDBDataSetcustomerTableAdapter.FillSearch2(taxDBDataSet.customer, name, father);
                    break;
                case 3:
                    taxDBDataSetcustomerTableAdapter.FillSearch3(taxDBDataSet.customer, father);
                    break;
                case 4:
                    taxDBDataSetcustomerTableAdapter.FillSearch4(taxDBDataSet.customer, tin);
                    break;
                case 5:
                    taxDBDataSetcustomerTableAdapter.FillSearch5(taxDBDataSet.customer, name, tin);
                    break;
                case 6:
                    taxDBDataSetcustomerTableAdapter.FillSearch6(taxDBDataSet.customer,name, father, tin);
                    break;
                case 7:
                    taxDBDataSetcustomerTableAdapter.FillSearch7(taxDBDataSet.customer, father, tin);
                    break;
                case 8:
                    taxDBDataSetcustomerTableAdapter.FillSearch8(taxDBDataSet.customer, dob);
                    break;
                case 9:
                    taxDBDataSetcustomerTableAdapter.FillSearch9(taxDBDataSet.customer,name, dob);
                    break;
                case 10:
                    taxDBDataSetcustomerTableAdapter.FillSearch10(taxDBDataSet.customer, name, father, dob);
                    break;
                case 11:
                    taxDBDataSetcustomerTableAdapter.FillSearch11(taxDBDataSet.customer, father, dob);
                    break;
                case 12:
                    taxDBDataSetcustomerTableAdapter.FillSearch12(taxDBDataSet.customer, tin, dob);
                    break;
                case 13:
                    taxDBDataSetcustomerTableAdapter.FillSearch13(taxDBDataSet.customer, name, tin, dob);
                    break;
                case 14:
                    taxDBDataSetcustomerTableAdapter.FillSearch14(taxDBDataSet.customer, name, father, dob, tin);
                    break;
                case 15:
                    taxDBDataSetcustomerTableAdapter.FillSearch15(taxDBDataSet.customer, father, tin, dob);
                    break;
                default:
                    taxDBDataSetcustomerTableAdapter.Fill(taxDBDataSet.customer);
                    break;
            }

            System.Windows.Data.CollectionViewSource customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            
        }

        private void customerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.customerGrid.SelectedIndex >= 0)
            {
                var selectedRow = this.customerGrid.GetRow(this.customerGrid.SelectedIndex);
                var columnCell = this.customerGrid.GetCell(selectedRow, 0);
                if (columnCell != null)
                {
                    TextBlock tb = new TextBlock();
                    tb = (TextBlock)columnCell.Content;
                    int id;
                    try
                    {
                        id = int.Parse(tb.Text.ToString());
                        this.selectedid = id;
                        TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
                        TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter singleCustomer = new TaxDBDataSetTableAdapters.customerTableAdapter();
                        DataTable dt = singleCustomer.GetDataById(id);

                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            string name = dr["customer_name"].ToString();
                            string father = dr["father_name"].ToString();
                            string addr1 = dr["address1"].ToString();
                            string addr1_dist = dr["address1_district"].ToString();
                            string addr1_post = dr["address1_postcode"].ToString();

                            WrapPanel wp1 = new WrapPanel();
                            wp1.Margin = new Thickness(10);
                            wp1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                            WrapPanel wp2 = new WrapPanel();
                            wp2.Margin = new Thickness(10);
                            wp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                            WrapPanel wp3 = new WrapPanel();
                            wp3.Margin = new Thickness(10);
                            wp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                            TextBlock ftb = new TextBlock();
                            ftb.Width = 150;
                            ftb.TextWrapping = TextWrapping.Wrap;
                            ftb.FontWeight = FontWeights.Bold;
                            ftb.Text = "Father's Name: ";

                            TextBlock ftbv = new TextBlock();
                            ftbv.Width = 150;
                            ftbv.TextWrapping = TextWrapping.Wrap;
                            ftbv.FontWeight = FontWeights.Normal;
                            ftbv.Text = father;

                            wp1.Children.Add(ftb);
                            wp1.Children.Add(ftbv);

                            TextBlock ftb2 = new TextBlock();
                            ftb2.Width = 150;
                            ftb2.TextWrapping = TextWrapping.Wrap;
                            ftb2.FontWeight = FontWeights.Bold;
                            ftb2.Text = "Address: ";

                            TextBlock ftbv2 = new TextBlock();
                            ftbv2.Width = 150;
                            ftbv2.TextWrapping = TextWrapping.Wrap;
                            ftbv2.FontWeight = FontWeights.Normal;
                            ftbv2.Text = addr1 + ", " + addr1_dist + " " + addr1_post;
                            
                            wp2.Children.Add(ftb2);
                            wp2.Children.Add(ftbv2);

                            this.customerInfoStack.Children.Clear();
                            this.customerInfoStack.Children.Add(wp1);
                            this.customerInfoStack.Children.Add(wp2);

                            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter pret = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
                            DataTable prcdt = pret.GetDataBycid(id);

                            if (prcdt.Rows.Count > 0)
                            {
                                WrapPanel wp4 = new WrapPanel();
                                wp4.Margin = new Thickness(10);
                                wp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                                Border rb = new Border();
                                rb.BorderBrush = Brushes.Gray;
                                rb.BorderThickness = new Thickness(2);
                                rb.Padding = new Thickness(4,2,4,2);

                                rb.Child = wp4;

                                foreach (DataRow rdr in prcdt.Rows)
                                {
                                    Border rbb = new Border();
                                    rbb.BorderBrush = Brushes.Gray;
                                    rbb.Background = Brushes.White;
                                    rbb.CornerRadius = new CornerRadius(0);
                                    rbb.BorderThickness = new Thickness(2);
                                    rbb.Margin = new Thickness(4,2,4,2);
                                    rbb.Width = 60;

                                    TextBlock rtb = new TextBlock();
                                    rtb.Margin = new Thickness(5);
                                    rtb.Text = rdr["assessmentyear"].ToString();
                                    rtb.Name = "newretx" + rdr["id"] + "x" + id;
                                    rtb.TextAlignment = TextAlignment.Center;
                                    rtb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                                    rtb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                                    rtb.MouseUp += new MouseButtonEventHandler(rtb_MouseUp);

                                    rbb.Child = rtb;

                                    wp4.Children.Add(rbb);
                                }
                                this.customerInfoStack.Children.Add(rb);
                            }

                            Button newret = new Button();
                            newret.Content = "New Return";
                            newret.Style = (Style)FindResource("TSButtonStyle");
                            newret.Padding = new Thickness(10);

                            newret.Click += new RoutedEventHandler(newret_Click);

                            wp3.Children.Add(newret);

                            this.customerInfoStack.Children.Add(wp3);

                            this.customerInfo.Header = name;
                            this.customerInfo.Visibility = Visibility.Visible;
                            
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        void rtb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TextBlock tb = sender as TextBlock;
                string[] slices = tb.Name.Split('x');

                PRAssesseeInformation PRAssesseeInformationForm = new PRAssesseeInformation(int.Parse(slices[2]), int.Parse(slices[1]));
                PRAssesseeInformationForm.Owner = this.Owner;
                PRAssesseeInformationForm.Show();
                this.Close();
            }
            catch
            {
            }
        }

        void newret_Click(object sender, RoutedEventArgs e)
        {
            PRAssesseeInformation PRAssesseeInformationForm = new PRAssesseeInformation(this.selectedid);
            PRAssesseeInformationForm.Owner = this.Owner;
            PRAssesseeInformationForm.Show();
            this.Close();
        }

        private void customer_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.customer_name.Text == "Name")
            {
                this.customer_name.Text = "";
            }
        }

        private void customer_father_name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.customer_father_name.Text == "Father's Name")
            {
                this.customer_father_name.Text = "";
            }
        }

        private void customer_tin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.customer_tin.Text == "TIN")
            {
                this.customer_tin.Text = "";
            }
        }
    }
}
