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
    /// Interaction logic for PRIT10_BB.xaml
    /// </summary>
    public partial class PRIT10_BB : Window
    {
        private int returnid;
        private int customerid;
        public PRIT10_BB(int returnid)
        {
            InitializeComponent();
            this.returnid = returnid;
            this.customerid = 0;
            populatedata();
        }

        private void populatedata()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                this.PFaot.Text = dr["it10bb1a"].ToString();
                this.PFc.Text = dr["it10bb1b"].ToString();
                if (dr["it10bb2a"].ToString().Length > 0)
                {
                    this.TPaot.Text = dr["it10bb2a"].ToString();
                }
                else
                {
                    decimal tpaot = 0;
                    try
                    {
                        tpaot += decimal.Parse(dr["soi16a"].ToString());
                    }
                    catch
                    {
                    }
                    try
                    {
                        tpaot += decimal.Parse(dr["soi16b"].ToString());
                    }
                    catch
                    {
                    }
                    this.TPaot.Text = String.Format("{0:#,0.##}", tpaot);
                }
                this.TPc.Text = dr["it10bb2b"].ToString();
                this.AEaot.Text = dr["it10bb3a"].ToString();
                this.AEc.Text = dr["it10bb3b"].ToString();
                this.TEaot.Text = dr["it10bb4a"].ToString();
                this.TEc.Text = dr["it10bb4b"].ToString();
                this.EBaot.Text = dr["it10bb5a"].ToString();
                this.EBc.Text = dr["it10bb5b"].ToString();
                this.WBaot.Text = dr["it10bb6a"].ToString();
                this.WBc.Text = dr["it10bb6b"].ToString();
                this.GBaot.Text = dr["it10bb7a"].ToString();
                this.GBc.Text = dr["it10bb7b"].ToString();
                this.TBaot.Text = dr["it10bb8a"].ToString();
                this.TBc.Text = dr["it10bb8b"].ToString();
                this.EEaot.Text = dr["it10bb9a"].ToString();
                this.EEc.Text = dr["it10bb9b"].ToString();
                this.PEFTaot.Text = dr["it10bb10a"].ToString();
                this.PEFTc.Text = dr["it10bb10b"].ToString();
                this.FEaot.Text = dr["it10bb11a"].ToString();
                this.FEc.Text = dr["it10bb11b"].ToString();
                calculatetotal();
            }

        }

        private void NextBtn_Click(object sender, MouseEventArgs e)
        {
            decimal it10bb1a = 0,
                    it10bb2a = 0,
                    it10bb3a = 0,
                    it10bb4a = 0,
                    it10bb5a = 0,
                    it10bb6a = 0,
                    it10bb7a = 0,
                    it10bb8a = 0,
                    it10bb9a = 0,
                    it10bb10a = 0,
                    it10bb11a = 0;
            try
            {
                it10bb1a = decimal.Parse(this.PFaot.Text);
            }
            catch
            {
            }
            String it10bb1b = this.PFc.Text.ToString();
            try
            {
                it10bb2a = decimal.Parse(this.TPaot.Text);
            }
            catch
            {
            }
            String it10bb2b = this.TPc.Text.ToString();
            try
            {
                it10bb3a = decimal.Parse(this.AEaot.Text);
            }
            catch
            {
            }
            String it10bb3b = this.AEc.Text.ToString();
            try
            {
                it10bb4a = decimal.Parse(this.TEaot.Text);
            }
            catch
            {
            }
            String it10bb4b = this.TEc.Text.ToString();
            try
            {
                it10bb5a = decimal.Parse(this.EBaot.Text);
            }
            catch
            {
            }
            String it10bb5b = this.EBc.Text.ToString();
            try
            {
                it10bb6a = decimal.Parse(this.WBaot.Text);
            }
            catch
            {
            }
            String it10bb6b = this.WBc.Text.ToString();
            try
            {
                it10bb7a = decimal.Parse(this.GBaot.Text);
            }
            catch
            {
            }
            String it10bb7b = this.GBc.Text.ToString();
            try
            {
                it10bb8a = decimal.Parse(this.TBaot.Text);
            }
            catch
            {
            }
            String it10bb8b = this.TBc.Text.ToString();
            try
            {
                it10bb9a = decimal.Parse(this.EEaot.Text);
            }
            catch
            {
            }
            String it10bb9b = this.EEc.Text.ToString();
            try
            {
                it10bb10a = decimal.Parse(this.PEFTaot.Text);
            }
            catch
            {
            }
            String it10bb10b = this.PEFTc.Text.ToString();
            try
            {
                it10bb11a = decimal.Parse(this.FEaot.Text);
            }
            catch
            {
            }
            String it10bb11b = this.FEc.Text.ToString();
            

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            // Load data into the table customer. You can modify this code as needed.
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            taxDBDataSetpersonalreturnTableAdapter.UpdatePR7(it10bb1a, it10bb1b, it10bb2a, it10bb2b, it10bb3a, it10bb3b, it10bb4a, it10bb4b, it10bb5a, it10bb5b, it10bb6a, it10bb1b, it10bb7a, it10bb7b, it10bb8a, it10bb8b, it10bb9a, it10bb9b, it10bb10a, it10bb10b, it10bb11a, it10bb11b, this.returnid);
            taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);


            PRAssetsAndLiabilities PRAssetsAndLiabilitiesForm = new PRAssetsAndLiabilities(this.returnid);
            PRAssetsAndLiabilitiesForm.Owner = this.Owner;
            PRAssetsAndLiabilitiesForm.Show();
            this.Close();

        }

        void PRFurnishedDocumentsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }

        private void PrevBtn_Click(object sender, MouseEventArgs e)
        {
            PRIncomeOfAssets PRIncomeOfAssetsForm = new PRIncomeOfAssets(this.returnid);
            PRIncomeOfAssetsForm.Owner = this.Owner;
            PRIncomeOfAssetsForm.Show();
            this.Close();
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void calculatetotal()
        {
            decimal total = 0;
            try{
                total += decimal.Parse(this.PFaot.Text);
            }catch{

            }
            try
            {
                total += decimal.Parse(this.TPaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.AEaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.TEaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.EBaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.WBaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.GBaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.TBaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.EEaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.PEFTaot.Text);
            }
            catch
            {

            }
            try
            {
                total += decimal.Parse(this.FEaot.Text);
            }
            catch
            {

            }
            this.TERaot.Text = total.ToString();
            formattext();
        }

        private void TERaot_LostFocus(object sender, RoutedEventArgs e)
        {
            calculatetotal();
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
