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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PRInvestmentonTaxCredit : Window
    {
        private int returnid;
        private int customerid;
        public PRInvestmentonTaxCredit(int returnid)
        {
            InitializeComponent();
            this.returnid = returnid;
            this.customerid = 0;
            populatedata();
        }

        private void populatedata()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            // Load data into the table customer. You can modify this code as needed.
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.li.Text = dr["investment1"].ToString();
                this.cda.Text = dr["investment2"].ToString();
                this.cpf.Text = dr["investment3"].ToString();
                decimal scrpfval = 0;
                try
                {
                    scrpfval = decimal.Parse(dr["salary13a"].ToString()) * (decimal)2;
                }
                catch
                {
                }
                this.scrpf.Text = scrpfval.ToString();
                this.csaf.Text = dr["investment5"].ToString();
                this.iads.Text = dr["investment6"].ToString();
                this.cdps.Text = dr["investment7"].ToString();
                this.cbfgip.Text = dr["investment8"].ToString();
                this.czf.Text = dr["investment9"].ToString();
                this.o.Text = dr["investment10"].ToString();

                calculateTotal(null, null);
            }
        }

        private void calculateTotal(object sender, RoutedEventArgs e)
        {
            decimal ctotal = 0;
            try
            {
                ctotal += decimal.Parse(this.li.Text.ToString());
            }
            catch
            {
                this.li.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.cda.Text.ToString());
            }
            catch
            {
                this.cda.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.cpf.Text.ToString());
            }
            catch
            {
                this.cpf.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.scrpf.Text.ToString());
            }
            catch
            {
                this.scrpf.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.csaf.Text.ToString());
            }
            catch
            {
                this.csaf.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.iads.Text.ToString());
            }
            catch
            {
                this.iads.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.cdps.Text.ToString());
            }
            catch
            {
                this.cdps.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.cbfgip.Text.ToString());
            }
            catch
            {
                this.cbfgip.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.czf.Text.ToString());
            }
            catch
            {
                this.czf.Text = "0";
            }
            try
            {
                ctotal += decimal.Parse(this.o.Text.ToString());
            }
            catch
            {
                this.o.Text = "0";
            }
            this.total.Text = ctotal.ToString();
            formattext();
        }

        private void NextBtn_Click(object sender, MouseEventArgs e)
        {
            calculateTotal(sender, null);

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            // Load data into the table customer. You can modify this code as needed.
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            taxDBDataSetpersonalreturnTableAdapter.UpdatePR4(decimal.Parse(this.li.Text),
                decimal.Parse(this.cda.Text),
                decimal.Parse(this.cpf.Text),
                decimal.Parse(this.scrpf.Text),
                decimal.Parse(this.csaf.Text),
                decimal.Parse(this.iads.Text),
                decimal.Parse(this.cdps.Text),
                decimal.Parse(this.cbfgip.Text),
                decimal.Parse(this.czf.Text),
                decimal.Parse(this.o.Text),
                this.returnid);
            taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);

            PRIncomeOfAssets PRIncomeOfAssetsForm = new PRIncomeOfAssets(this.returnid);
            PRIncomeOfAssetsForm.Owner = this.Owner;
            PRIncomeOfAssetsForm.Show();
            this.Close();
        }

        void PRIncomeOfAssetsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }

        private void PrevBtn_Click(object sender, MouseEventArgs e)
        {
            PRHousePropertyInformation housePropertyForm = new PRHousePropertyInformation(this.returnid);
            housePropertyForm.Owner = this.Owner;
            housePropertyForm.Show();
            this.Close();
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            formattext();
        }

        private void formattext()
        {
            foreach (TextBox tb in FindChildren.FindVisualChildren<TextBox>(this))
            {
                try
                {
                    decimal num = decimal.Parse(tb.Text);
                    tb.Text = String.Format("{0:#,0.##}", num);
                    tb.TextAlignment = TextAlignment.Right;
                }
                catch
                {
                }
            }
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
