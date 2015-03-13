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
    /// Interaction logic for PRIncomeOfAssets.xaml
    /// </summary>
    public partial class PRIncomeOfAssets : Window
    {
        private int customerid;
        private int returnid;
        private string sex;
        private int age;
        private int disabled;
        private decimal a13;
        private decimal[] invest = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public PRIncomeOfAssets(int returnid)
        {
            InitializeComponent();
            this.customerid = 0;
            this.returnid = returnid;
            this.sex = "MALE";
            this.age = 0;
            this.a13 = 0;
            this.disabled = 0;
            populateinvest();
            populatedata();
        }

        //public PRIncomeOfAssets()
        //{
        //    InitializeComponent();
        //    this.customerid = 0;
        //    this.returnid = 3;
        //    this.sex = "MALE";
        //    this.age = 0;
        //    this.a13 = 0;
        //    populateinvest();
        //    populatedata();
        //}

        private void populateinvest()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                for (int i = 1; i <= 11; i++)
                {
                    try
                    {
                        this.invest[i - 1] = decimal.Parse(dr["investment" + i].ToString());
                    }
                    catch
                    {
                    }
                }
                try
                {
                    this.a13 = decimal.Parse(dr["salary13a"].ToString());
                }
                catch
                {
                }
            }
        }

        private void populatedata()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter();
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                this.customerid = int.Parse(dr["customerid"].ToString());
            }

            DataTable cdt = taxDBDataSetcustomerTableAdapter.GetDataById(this.customerid);
            if (cdt.Rows.Count > 0)
            {
                DataRow cdr = cdt.Rows[0];
                this.sex = cdr["sex"].ToString();
                try
                {
                    this.disabled = int.Parse(cdr["disabled"].ToString());
                }
                catch
                {
                }
                string dob = cdr["date_of_birth"].ToString();
                if(dob != "" && dob != DateTime.Parse("01-01-1900").ToShortDateString())
                {
                    TimeSpan tf = DateTime.Today - DateTime.Parse(dob);
                    this.age = tf.Days / 365;
                }
            }
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];

                try
                {
                    this.incomeyear.SelectedDate = DateTime.Parse(dr["incomeyear"].ToString());
                }
                catch
                {
                    try
                    {
                        this.incomeyear.SelectedDate = DateTime.Parse("06/30/" + DateTime.Today.Year.ToString());
                    }
                    catch
                    {
                        this.incomeyear.SelectedDate = DateTime.Parse("01/01/" + DateTime.Today.Year.ToString());
                    }
                }

                this.textBox1.Text = calculatesoi1().ToString();
                this.textBox2.Text = dr["soi2"].ToString();
                this.textBox3.Text = calculatesoi3().ToString();
                this.textBox4.Text = dr["soi4"].ToString();
                this.textBox5.Text = dr["soi5"].ToString();
                this.textBox6.Text = dr["soi6"].ToString();
                this.textBox7.Text = dr["soi7"].ToString();
                this.textBox8.Text = dr["soi8"].ToString();
                this.textBox9.Text = dr["soi9"].ToString();

                this.textBox10.Text = calculatesoi10().ToString();

                this.textBox11.Text = dr["soi11"].ToString();

                this.textBox12.Text = calculatesoi12().ToString();

                this.textBox13.Text = calculatesoi13().ToString();
                this.textBox14.Text = calculatesoi14().ToString();

                this.textBox15.Text = calculatesoi15().ToString();

                this.tb16a.Text = dr["soi16a"].ToString();
                this.tb16b.Text = dr["soi16b"].ToString();
                this.tb16c.Text = dr["soi16c"].ToString();
                this.tb16d.Text = dr["soi16d"].ToString();

                this.textBox17.Text = calculatesoi16().ToString();
                this.textBox18.Text = calculatesoi17().ToString();

                this.textBox19.Text = getexempted().ToString();
                this.textBox20.Text = dr["soi19"].ToString();
            }
        }

        public void calculateTotal(object sender, RoutedEventArgs e)
        {
            this.textBox10.Text = String.Format("{0:#,0.##}", calculatesoi10());
            this.textBox12.Text = String.Format("{0:#,0.##}", calculatesoi12());

            this.textBox13.Text = String.Format("{0:#,0.##}", calculatesoi13());
            this.textBox14.Text = String.Format("{0:#,0.##}", calculatesoi14());

            this.textBox15.Text = String.Format("{0:#,0.##}", calculatesoi15());

            this.textBox17.Text = String.Format("{0:#,0.##}", calculatesoi16());
            this.textBox18.Text = String.Format("{0:#,0.##}", calculatesoi17());

            this.textBox19.Text = String.Format("{0:#,0.##}", getexempted());
        }

        public decimal getexempted()
        {
            decimal total = 0;

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                for (int i = 1; i < 20; i++)
                {
                    try
                    {
                        total += decimal.Parse(dr["salary" + i + "b"].ToString());
                    }
                    catch
                    {
                    }
                }
            }

            return total;
        }

        public decimal calculatesoi1()
        {
            decimal total = 0;
            decimal a = 0;
            decimal b = 0;

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                try
                {
                    a =
                        decimal.Parse(dr["salary1a"].ToString()) +
                        decimal.Parse(dr["salary2a"].ToString()) +
                        decimal.Parse(dr["salary3a"].ToString()) +
                        decimal.Parse(dr["salary4a"].ToString()) +
                        decimal.Parse(dr["salary5a"].ToString()) +
                        decimal.Parse(dr["salary6a"].ToString()) +
                        decimal.Parse(dr["salary7a"].ToString()) +
                        decimal.Parse(dr["salary8a"].ToString()) +
                        decimal.Parse(dr["salary9a"].ToString()) +
                        decimal.Parse(dr["salary10a"].ToString()) +
                        decimal.Parse(dr["salary11a"].ToString()) +
                        decimal.Parse(dr["salary12a"].ToString()) +
                        decimal.Parse(dr["salary13a"].ToString()) +
                        decimal.Parse(dr["salary14a"].ToString()) +
                        decimal.Parse(dr["salary15a"].ToString()) +
                        decimal.Parse(dr["salary16a"].ToString()) +
                        decimal.Parse(dr["salary17a"].ToString()) +
                        decimal.Parse(dr["salary18a"].ToString()) +
                        decimal.Parse(dr["salary19a"].ToString());
                }
                catch
                {
                }

                try
                {
                    b =
                        decimal.Parse(dr["salary1b"].ToString()) +
                        decimal.Parse(dr["salary2b"].ToString()) +
                        decimal.Parse(dr["salary3b"].ToString()) +
                        decimal.Parse(dr["salary4b"].ToString()) +
                        decimal.Parse(dr["salary5b"].ToString()) +
                        decimal.Parse(dr["salary6b"].ToString()) +
                        decimal.Parse(dr["salary7b"].ToString()) +
                        decimal.Parse(dr["salary8b"].ToString()) +
                        decimal.Parse(dr["salary9b"].ToString()) +
                        decimal.Parse(dr["salary10b"].ToString()) +
                        decimal.Parse(dr["salary11b"].ToString()) +
                        decimal.Parse(dr["salary12b"].ToString()) +
                        decimal.Parse(dr["salary13b"].ToString()) +
                        decimal.Parse(dr["salary14b"].ToString()) +
                        decimal.Parse(dr["salary15b"].ToString()) +
                        decimal.Parse(dr["salary16b"].ToString()) +
                        decimal.Parse(dr["salary17b"].ToString()) +
                        decimal.Parse(dr["salary18b"].ToString()) +
                        decimal.Parse(dr["salary19b"].ToString());
                }
                catch
                {
                }
            }
            total = a - b;

            return total;
        }

        public decimal calculatesoi3()
        {
            decimal total = 0;
            decimal a = 0;
            decimal b = 0;

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                try
                {
                    a = decimal.Parse(dr["house1"].ToString());
                }
                catch
                {
                }

                try
                {
                    b =
                        decimal.Parse(dr["house2a"].ToString()) +
                        decimal.Parse(dr["house2b"].ToString()) +
                        decimal.Parse(dr["house2c"].ToString()) +
                        decimal.Parse(dr["house2d"].ToString()) +
                        decimal.Parse(dr["house2e"].ToString()) +
                        decimal.Parse(dr["house2f"].ToString()) +
                        decimal.Parse(dr["house2g"].ToString());
                }
                catch
                {
                }
            }
            total = a - b;

            return total;
        }

        public decimal calculatesoi10()
        {
            decimal total = 0;

            try
            {
                total += decimal.Parse(this.textBox1.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox2.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox3.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox4.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox5.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox6.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox7.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox8.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox9.Text);
            }
            catch
            {
            }
            return total;
        }

        public decimal calculatesoi12()
        {
            decimal total = 0;

            try
            {
                total += decimal.Parse(this.textBox10.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.textBox11.Text);
            }
            catch
            {
            }

            return total;
        }

        public decimal calculatesoi13()
        {
            decimal total = 0;
            decimal income = 0;
            string sc = "a";

            if (this.sex == "MALE" && this.age < 65)
            {
                sc = "a";
            }
            else
            {
                sc = "b";
            }

            if (this.disabled == 1)
            {
                sc = "c";
            }

            try
            {
                income = decimal.Parse(this.textBox12.Text);
            }
            catch
            {
                return (decimal)0;
            }

            switch (sc)
            {
                case "a":
                    if (income <= 165000)
                    {
                        return (decimal)0;
                    }
                    else
                    {
                        income -= 165000;
                        if (income > 275000)
                        {
                            total += 27500;
                            income -= 275000;

                            if (income > 325000)
                            {
                                total += 48750;
                                income -= 325000;

                                if (income > 375000)
                                {
                                    total += 75000;
                                    income -= 375000;

                                    if (income > 0)
                                    {
                                        decimal per = income * (decimal).25;
                                        total += per;
                                        return total;
                                    }
                                }
                                else
                                {
                                    decimal per = income * (decimal).2;
                                    total += per;
                                    return total;
                                }
                            }
                            else
                            {
                                decimal per = income * (decimal).15;
                                total += per;
                                return total;
                            }
                        }
                        else
                        {
                            decimal per = income * (decimal).1;
                            total += per;
                            return total;
                        }
                    }
                    break;
                case "b":
                    if (income <= 180000)
                    {
                        return (decimal)0;
                    }
                    else
                    {
                        income -= 180000;
                        if (income >= 275000)
                        {
                            total += 27500;
                            income -= 275000;

                            if (income >= 325000)
                            {
                                total += 48750;
                                income -= 325000;

                                if (income >= 375000)
                                {
                                    total += 75000;
                                    income -= 375000;

                                    if (income > 0)
                                    {
                                        decimal per = income * (decimal).25;
                                        total += per;
                                        return total;
                                    }
                                }
                                else
                                {
                                    decimal per = income * (decimal).2;
                                    total += per;
                                    return total;
                                }
                            }
                            else
                            {
                                decimal per = income * (decimal).15;
                                total += per;
                                return total;
                            }
                        }
                        else
                        {
                            decimal per = income * (decimal).1;
                            total += per;
                            return total;
                        }
                    }
                    break;
                case "c":
                    if (income <= 200000)
                    {
                        return (decimal)0;
                    }
                    else
                    {
                        income -= 200000;
                        if (income >= 275000)
                        {
                            total += 27500;
                            income -= 275000;

                            if (income >= 325000)
                            {
                                total += 48750;
                                income -= 325000;

                                if (income >= 375000)
                                {
                                    total += 75000;
                                    income -= 375000;

                                    if (income > 0)
                                    {
                                        decimal per = income * (decimal).25;
                                        total += per;
                                        return total;
                                    }
                                }
                                else
                                {
                                    decimal per = income * (decimal).2;
                                    total += per;
                                    return total;
                                }
                            }
                            else
                            {
                                decimal per = income * (decimal).15;
                                total += per;
                                return total;
                            }
                        }
                        else
                        {
                            decimal per = income * (decimal).1;
                            total += per;
                            return total;
                        }
                    }
                    break;
            }

            return total;
        }

        public decimal calculatesoi14()
        {
            decimal total = 0;
            decimal x12 = 0;
            decimal totalinvest = 0;
            decimal item1, item2, item3;
            try
            {
                x12 = decimal.Parse(this.textBox12.Text);
            }
            catch
            {
            }

            try
            {
                totalinvest = this.invest.Sum();
            }
            catch
            {
            }

            item1 = (x12 - this.a13) * (decimal).25;
            item2 = totalinvest;
            item3 = 1000000;

            total = Math.Min(Math.Min(item1, item2), item3) * (decimal).1;

            return total;
        }

        public decimal calculatesoi15()
        {
            decimal total = 0;
            try
            {
                total += decimal.Parse(this.textBox13.Text);
            }
            catch
            {
            }
            try
            {
                total -= decimal.Parse(this.textBox14.Text);
            }
            catch
            {
            }
            return Math.Max(total, 0);
        }

        public decimal calculatesoi16()
        {
            decimal total = 0;

            try
            {
                total += decimal.Parse(this.tb16a.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.tb16b.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.tb16c.Text);
            }
            catch
            {
            }
            try
            {
                total += decimal.Parse(this.tb16d.Text);
            }
            catch
            {
            }
            return total;
        }

        public decimal calculatesoi17()
        {
            decimal total = 0;

            try
            {
                total += decimal.Parse(this.textBox15.Text);
            }
            catch
            {
            }
            try
            {
                total -= decimal.Parse(this.textBox17.Text);
            }
            catch
            {
            }
            return total;
        }

        private void NextBtn_Click(object sender, MouseEventArgs e)
        {
            decimal soi1 = 0,
                soi2 = 0,
                soi3 = 0,
                soi4 = 0,
                soi5 = 0,
                soi6 = 0,
                soi7 = 0,
                soi8 = 0,
                soi9 = 0,
                soi11 = 0,
                soi13 = 0,
                soi14 = 0,
                soi16a = 0,
                soi16b = 0,
                soi16c = 0,
                soi16d = 0,
                soi18 = 0,
                soi19 = 0;
            DateTime incomeyear = DateTime.Parse("1/1/1900");

            try
            {
                soi1 = decimal.Parse(this.textBox1.Text);
            }
            catch
            {
            }

            try
            {
                soi2 = decimal.Parse(this.textBox2.Text);
            }
            catch
            {
            }

            try
            {
                soi3 = decimal.Parse(this.textBox3.Text);
            }
            catch
            {
            }

            try
            {
                soi4 = decimal.Parse(this.textBox4.Text);
            }
            catch
            {
            }

            try
            {
                soi5 = decimal.Parse(this.textBox5.Text);
            }
            catch
            {
            }

            try
            {
                soi6 = decimal.Parse(this.textBox6.Text);
            }
            catch
            {
            }

            try
            {
                soi7 = decimal.Parse(this.textBox7.Text);
            }
            catch
            {
            }

            try
            {
                soi8 = decimal.Parse(this.textBox8.Text);
            }
            catch
            {
            }

            try
            {
                soi9 = decimal.Parse(this.textBox9.Text);
            }
            catch
            {
            }

            try
            {
                soi11 = decimal.Parse(this.textBox11.Text);
            }
            catch
            {
            }

            try
            {
                soi13 = decimal.Parse(this.textBox13.Text);
            }
            catch
            {
            }

            try
            {
                soi14 = decimal.Parse(this.textBox14.Text);
            }
            catch
            {
            }

            try
            {
                soi16a = decimal.Parse(this.tb16a.Text);
            }
            catch
            {
            }

            try
            {
                soi16b = decimal.Parse(this.tb16b.Text);
            }
            catch
            {
            }

            try
            {
                soi16c = decimal.Parse(this.tb16c.Text);
            }
            catch
            {
            }

            try
            {
                soi16d = decimal.Parse(this.tb16d.Text);
            }
            catch
            {
            }

            try
            {
                soi18 = decimal.Parse(this.textBox19.Text);
            }
            catch
            {
            }

            try
            {
                soi19 = decimal.Parse(this.textBox20.Text);
            }
            catch
            {
            }
            try
            {
                incomeyear = this.incomeyear.SelectedDate.Value;
            }
            catch
            {
            }

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            // Load data into the table customer. You can modify this code as needed.
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            taxDBDataSetpersonalreturnTableAdapter.UpdatePR5(soi1, soi2, soi3, soi4, soi5, soi6, soi7, soi8, soi9, soi11, soi13, soi14, soi16a, soi16b, soi16c, soi16d, soi18, soi19, incomeyear, this.returnid);
            taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);

            PRIT10_BB PRIT10_BBForm = new PRIT10_BB(this.returnid);
            PRIT10_BBForm.Owner = this.Owner;
            PRIT10_BBForm.Show();
            this.Close();
        }

        private void PrevBtn_Click(object sender, MouseEventArgs e)
        {
            PRInvestmentonTaxCredit PRInvestmentonTaxCreditForm = new PRInvestmentonTaxCredit(this.returnid);
            PRInvestmentonTaxCreditForm.Owner = this.Owner;
            PRInvestmentonTaxCreditForm.Show();
            this.Close();
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void tb16_MouseLeave(object sender, RoutedEventArgs e)
        {
            this.textBox17.Text = calculatesoi16().ToString();
            calculateTotal(sender, e);
            formattext();
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
