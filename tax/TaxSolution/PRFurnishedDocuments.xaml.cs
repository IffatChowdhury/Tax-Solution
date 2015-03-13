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
using System.Data;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for PRFurnishedDocuments.xaml
    /// </summary>
    public partial class PRFurnishedDocuments : Window
    {
        private int returnid;
        private int customerid;
        public PRFurnishedDocuments(int returnid)
        {
            InitializeComponent();
            this.returnid = returnid;
            this.customerid = 0;
            populatedata();
        }

        private void populatedata()
        {
            loaddocument();
        }

        private void loaddocument()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prdocumentsTableAdapter taxDBDataSetprdocumentsTableAdapter = new TaxDBDataSetTableAdapters.prdocumentsTableAdapter();
            DataTable dt = taxDBDataSetprdocumentsTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int count = int.Parse(this.Doccount.Text.ToString());
                    count++;
                    this.Doccount.Text = count.ToString();

                    WrapPanel wp = new WrapPanel();
                    wp.Name = "DocWP" + count.ToString();

                    TextBox tb1 = new TextBox();
                    tb1.Name = "Docname" + count.ToString();
                    tb1.Text = dr["document"].ToString();
                    tb1.Width = 350;
                    tb1.Margin = new Thickness(10);

                    CheckBox rmchk = new CheckBox();
                    rmchk.Name = "DocRemove" + count.ToString();
                    rmchk.Content = "Mark to remove";
                    rmchk.IsChecked = false;
                    rmchk.Margin = new Thickness(10);

                    wp.Children.Add(tb1);
                    wp.Children.Add(rmchk);

                    this.Docitems.Children.Add(wp);
                }
            }
        }

        private void NextBtn_Click(object sender, MouseButtonEventArgs e)
        {
            documentupdate();
            PRPrint PRPrintForm = new PRPrint(this.returnid);
            PRPrintForm.Owner = this.Owner;
            PRPrintForm.Show();
            this.Close();
        }

        private void documentupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prdocumentsTableAdapter taxDBDataSetprdocumentsTableAdapter = new TaxDBDataSetTableAdapters.prdocumentsTableAdapter();
            taxDBDataSetprdocumentsTableAdapter.DeleteByReturn(this.returnid);
            int count = int.Parse(this.Doccount.Text);
            for (int i = 1; i <= count; i++)
            {
                TextBox name = FindChildren.FindChild<TextBox>(this, "Docname" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "DocRemove" + i);

                if (name != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        taxDBDataSetprdocumentsTableAdapter.Insert(this.returnid, name.Text);
                    }
                }
            }
            taxDBDataSetprdocumentsTableAdapter.Update(taxDBDataSet);
        }

        private void addRowBtn_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(this.Doccount.Text.ToString());
            count++;
            if (count < 16)
            {
                this.Doccount.Text = count.ToString();

                WrapPanel wp = new WrapPanel();
                wp.Name = "DocWP" + count.ToString();

                TextBox tb1 = new TextBox();
                tb1.Name = "Docname" + count.ToString();
                tb1.Width = 350;
                tb1.Margin = new Thickness(10);

                if (this.documentCombo.SelectedIndex >= 0 && this.documentCombo.SelectedIndex != 14)
                {
                    switch (this.documentCombo.SelectedIndex)
                    {
                        case 0:
                            tb1.Text = "Salary Certificate/Statement";
                            break;
                        case 1:
                            tb1.Text = "Bank Statement/Certificate";
                            break;
                        case 2:
                            tb1.Text = "Dividend Warrant(s)";
                            break;
                        case 3:
                            tb1.Text = "Loan Statement/Certificate";
                            break;
                        case 4:
                            tb1.Text = "Land Revenue receipt";
                            break;
                        case 5:
                            tb1.Text = "Municipal Tax receipt";
                            break;
                        case 6:
                            tb1.Text = "Vehicle Registration Certificate";
                            break;
                        case 7:
                            tb1.Text = "Land/Flat Registration deed";
                            break;
                        case 8:
                            tb1.Text = "Rental agreement";
                            break;
                        case 9:
                            tb1.Text = "Life Insurance premium payment receipt";
                            break;
                        case 10:
                            tb1.Text = "DPS payment receipt";
                            break;
                        case 11:
                            tb1.Text = "Evidence of deduction at source";
                            break;
                        case 12:
                            tb1.Text = "BO account statement";
                            break;
                        case 13:
                            tb1.Text = "Copy of challan/ pay order in support of tax payment";
                            break;
                    }
                    tb1.IsReadOnly = true;
                }

                CheckBox rmchk = new CheckBox();
                rmchk.Name = "DocRemove" + count.ToString();
                rmchk.Content = "Mark to remove";
                rmchk.IsChecked = false;
                rmchk.Margin = new Thickness(10);

                wp.Children.Add(tb1);
                wp.Children.Add(rmchk);

                this.Docitems.Children.Add(wp);
            }
            else
            {
                MessageBox.Show("To add more items, use seperate sheet");
            }
        }

        private void PrevBtn_Click(object sender, MouseButtonEventArgs e)
        {
            PRAssetsAndLiabilities PRAssetsAndLiabilitiesForm = new PRAssetsAndLiabilities(this.returnid);
            PRAssetsAndLiabilitiesForm.Owner = this.Owner;
            PRAssetsAndLiabilitiesForm.Show();
            this.Close();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void navActivate(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Style = (Style)FindResource("TSNavigationItemActive");
        }

        private void navDiactivate(object sender, MouseEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            tb.Style = (Style)FindResource("TSNavigationItem");
        }

        private void navClick(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            string name = tb.Name.ToString();
            switch (name)
            {
                case "assesseeNav":
                    TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
                    TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
                    DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

                    DataRow dr;
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        this.customerid = int.Parse(dr["customerid"].ToString());
                    }
                    PRAssesseeInformation PRAssesseeInformationForm = new PRAssesseeInformation(this.customerid, this.returnid);
                    PRAssesseeInformationForm.Owner = this.Owner;
                    PRAssesseeInformationForm.Show();
                    this.Close();
                    break;
                case "salaryNav":
                    PRSalaryInformation PRSalaryInformationForm = new PRSalaryInformation(this.returnid);
                    PRSalaryInformationForm.Owner = this.Owner;
                    PRSalaryInformationForm.Show();
                    this.Close();
                    break;
                case "houseNav":
                    PRHousePropertyInformation PRHousePropertyInformationForm = new PRHousePropertyInformation(this.returnid);
                    PRHousePropertyInformationForm.Owner = this.Owner;
                    PRHousePropertyInformationForm.Show();
                    this.Close();
                    break;
                case "investNav":
                    PRInvestmentonTaxCredit PRInvestmentonTaxCreditForm = new PRInvestmentonTaxCredit(this.returnid);
                    PRInvestmentonTaxCreditForm.Owner = this.Owner;
                    PRInvestmentonTaxCreditForm.Show();
                    this.Close();
                    break;
                case "incomeNav":
                    PRIncomeOfAssets PRIncomeOfAssetsForm = new PRIncomeOfAssets(this.returnid);
                    PRIncomeOfAssetsForm.Owner = this.Owner;
                    PRIncomeOfAssetsForm.Show();
                    this.Close();
                    break;
                case "it10bNav":
                    PRAssetsAndLiabilities PRAssetsAndLiabilitiesForm = new PRAssetsAndLiabilities(this.returnid);
                    PRAssetsAndLiabilitiesForm.Owner = this.Owner;
                    PRAssetsAndLiabilitiesForm.Show();
                    this.Close();
                    break;
                case "it10bbNav":
                    PRIT10_BB PRIT10_BBForm = new PRIT10_BB(this.returnid);
                    PRIT10_BBForm.Owner = this.Owner;
                    PRIT10_BBForm.Show();
                    this.Close();
                    break;
                case "documentNav":
                    PRFurnishedDocuments PRFurnishedDocumentsForm = new PRFurnishedDocuments(this.returnid);
                    PRFurnishedDocumentsForm.Owner = this.Owner;
                    PRFurnishedDocumentsForm.Show();
                    this.Close();
                    break;
            }
        }
    }
}
