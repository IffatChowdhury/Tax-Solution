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
    /// Interaction logic for PRHousePropertyInformation.xaml
    /// </summary>
    public partial class PRHousePropertyInformation : Window
    {
        private int returnid;
        private int customerid;
        public PRHousePropertyInformation(int returnid)
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
                string housers = dr["housers"].ToString();

                if (housers == "COMMERCIAL")
                {
                    this.commercial.IsChecked = true;
                }
                else
                {
                    this.residential.IsChecked = true;
                }

                this.AHL.Text = dr["houselocation"].ToString();
                this.ARIi.Text = dr["house1"].ToString();
                this.CEr.Text = dr["house2a"].ToString();
                this.CEmlt.Text = dr["house2b"].ToString();
                this.CElr.Text = dr["house2c"].ToString();
                this.CEiol.Text = dr["house2d"].ToString();
                this.CEip.Text = dr["house2e"].ToString();
                this.CEva.Text = dr["house2f"].ToString();
                this.CEo.Text = dr["house2g"].ToString();

                calculateTotal();
            }
        }

        private void NextBtn_Click(object sender, MouseEventArgs e)
        {
            calculateTotal();
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            // Load data into the table customer. You can modify this code as needed.
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            string housers = "RESIDENTIAL";
            if ((bool)this.commercial.IsChecked)
            {
                housers = "COMMERCIAL";
            }
            taxDBDataSetpersonalreturnTableAdapter.UpdatePR3(
                housers,
                decimal.Parse(this.ARIi.Text),
                decimal.Parse(this.CEr.Text), decimal.Parse(this.CEmlt.Text),
                decimal.Parse(this.CElr.Text), decimal.Parse(this.CEiol.Text),
                decimal.Parse(this.CEip.Text), decimal.Parse(this.CEva.Text),
                decimal.Parse(this.CEo.Text),
                this.AHL.Text,
                this.returnid);
            taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);

            PRInvestmentonTaxCredit PRInvestmentonTaxCreditForm = new PRInvestmentonTaxCredit(this.returnid);
            PRInvestmentonTaxCreditForm.Owner = this.Owner;
            PRInvestmentonTaxCreditForm.Show();
            this.Close();
        }

        private void calculateTotal()
        {
            decimal ari = 0;
            decimal cetotal = 0;
            decimal total = 0;
            try
            {
                ari = decimal.Parse(this.ARIi.Text.ToString());
            }
            catch
            {
                ari = 0;
                this.ARIi.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CEr.Text.ToString());
            }
            catch
            {
                this.CEr.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CEmlt.Text.ToString());
            }
            catch
            {
                this.CEmlt.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CElr.Text.ToString());
            }
            catch
            {
                this.CElr.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CEiol.Text.ToString());
            }
            catch
            {
                this.CEiol.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CEip.Text.ToString());
            }
            catch
            {
                this.CEip.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CEva.Text.ToString());
            }
            catch
            {
                this.CEva.Text = "0";
            }

            try
            {
                cetotal += decimal.Parse(this.CEo.Text.ToString());
            }
            catch
            {
                this.CEo.Text = "0";
            }

            this.CEtotal.Text = cetotal.ToString();
            total = Math.Max((ari - cetotal), 0);
            this.total.Text = total.ToString();
            formattext();
        }

        private void ARIi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal exp = 0;
            try
            {
                if ((bool)this.residential.IsChecked)
                {
                    exp = decimal.Parse(this.ARIi.Text) * (decimal)0.25;
                    this.CEr.Text = exp.ToString();
                }
                else
                {
                    exp = decimal.Parse(this.ARIi.Text) * (decimal)0.3;
                    this.CEr.Text = exp.ToString();
                }
            }
            catch
            {
            }
            calculateTotal();
        }

        private void CEr_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void CEmlt_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void CElr_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void CEiol_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void CEip_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void CEva_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void CEo_MouseLeave(object sender, RoutedEventArgs e)
        {
            calculateTotal();
        }

        private void PrevBtn_Click(object sender, MouseEventArgs e)
        {
            PRSalaryInformation PRSalaryInformationForm = new PRSalaryInformation(this.returnid);
            PRSalaryInformationForm.Owner = this.Owner;
            PRSalaryInformationForm.Show();
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
