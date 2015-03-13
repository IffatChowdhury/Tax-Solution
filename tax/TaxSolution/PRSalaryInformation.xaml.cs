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
    /// Interaction logic for PRSalaryInformation.xaml
    /// </summary>
    public partial class PRSalaryInformation : Window
    {
        private int customerid;
        private int returnid;
        public PRSalaryInformation(int returnid)
        {
            InitializeComponent();
            this.Aq.SelectedIndex = 0;
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

                this.customerid = int.Parse(dr["customerid"].ToString());

                this.BPaoi.Text = dr["salary1a"].ToString();
                this.BPaoei.Text = dr["salary1b"].ToString();
                this.BPnti.Text = getNTI(this.BPaoi.Text, this.BPaoei.Text).ToString();

                this.SPaoi.Text = dr["salary2a"].ToString();
                this.SPaoei.Text = dr["salary2b"].ToString();
                this.SPnti.Text = getNTI(this.SPaoi.Text, this.SPaoei.Text).ToString();

                this.DAaoi.Text = dr["salary3a"].ToString();
                this.DAaoei.Text = dr["salary3b"].ToString();
                this.DAnti.Text = getNTI(this.DAaoi.Text, this.DAaoei.Text).ToString();

                this.CAaoi.Text = dr["salary4a"].ToString();
                this.CAaoei.Text = dr["salary4b"].ToString();
                this.CAnti.Text = getNTI(this.CAaoi.Text, this.CAaoei.Text).ToString();

                this.HRaoi.Text = dr["salary5a"].ToString();
                this.HRaoei.Text = dr["salary5b"].ToString();
                this.HRnti.Text = getNTI(this.HRaoi.Text, this.HRaoei.Text).ToString();

                this.MAaoi.Text = dr["salary6a"].ToString();
                this.MAaoei.Text = dr["salary6b"].ToString();
                this.MAnti.Text = getNTI(this.MAaoi.Text, this.MAaoei.Text).ToString();

                this.SAaoi.Text = dr["salary7a"].ToString();
                this.SAaoei.Text = dr["salary7b"].ToString();
                this.SAnti.Text = getNTI(this.SAaoi.Text, this.SAaoei.Text).ToString();

                this.LAaoi.Text = dr["salary8a"].ToString();
                this.LAaoei.Text = dr["salary8b"].ToString();
                this.LAnti.Text = getNTI(this.LAaoi.Text, this.LAaoei.Text).ToString();

                this.Haoi.Text = dr["salary9a"].ToString();
                this.Haoei.Text = dr["salary9b"].ToString();
                this.Hnti.Text = getNTI(this.Haoi.Text, this.Haoei.Text).ToString();

                this.OAaoi.Text = dr["salary10a"].ToString();
                this.OAaoei.Text = dr["salary10b"].ToString();
                this.OAnti.Text = getNTI(this.OAaoi.Text, this.OAaoei.Text).ToString();

                this.Baoi.Text = dr["salary11a"].ToString();
                this.Baoei.Text = dr["salary11b"].ToString();
                this.Bnti.Text = getNTI(this.Baoi.Text, this.Baoei.Text).ToString();

                this.OtAaoi.Text = dr["salary12a"].ToString();
                this.OtAaoei.Text = dr["salary12b"].ToString();
                this.OtAnti.Text = getNTI(this.OtAaoi.Text, this.OtAaoei.Text).ToString();

                this.PFaoi.Text = dr["salary13a"].ToString();
                this.PFaoei.Text = dr["salary13b"].ToString();
                this.PFnti.Text = getNTI(this.PFaoi.Text, this.PFaoei.Text).ToString();

                this.PFIaoi.Text = dr["salary14a"].ToString();
                this.PFIaoei.Text = dr["salary14b"].ToString();
                this.PFInti.Text = getNTI(this.PFIaoi.Text, this.PFIaoei.Text).ToString();

                this.TFaoi.Text = dr["salary15a"].ToString();
                this.TFaoei.Text = dr["salary15b"].ToString();
                this.TFnti.Text = getNTI(this.TFaoi.Text, this.TFaoei.Text).ToString();

                this.Aaoi.Text = dr["salary16a"].ToString();
                this.Aaoei.Text = dr["salary16b"].ToString();
                this.Anti.Text = getNTI(this.Aaoi.Text, this.Aaoei.Text).ToString();

                this.Oaoi.Text = dr["salary17a"].ToString();
                this.Oaoei.Text = dr["salary17b"].ToString();
                this.Onti.Text = getNTI(this.Oaoi.Text, this.Oaoei.Text).ToString();

                this.Paoi.Text = dr["salary18a"].ToString();
                this.Paoei.Text = dr["salary18b"].ToString();
                this.Pnti.Text = getNTI(this.Paoi.Text, this.Paoei.Text).ToString();

                this.Laoi.Text = dr["salary19a"].ToString();
                this.Laoei.Text = dr["salary19b"].ToString();
                this.Lnti.Text = getNTI(this.Laoi.Text, this.Laoei.Text).ToString();

                calculateTotal();
            }
        }

        private void calculateTotal()
        {
            decimal totala = 0;
            decimal totalb = 0;
            decimal total = 0;

            try
            {
                totala += decimal.Parse(this.BPaoi.Text.ToString());
                totalb += decimal.Parse(this.BPaoei.Text.ToString());
                total += decimal.Parse(this.BPnti.Text.ToString());
            }
            catch
            {
                this.BPaoi.Text = this.BPaoei.Text = this.BPnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.SPaoi.Text.ToString());
                totalb += decimal.Parse(this.SPaoei.Text.ToString());
                total += decimal.Parse(this.SPnti.Text.ToString());
            }
            catch
            {
                this.SPaoi.Text = this.SPaoei.Text = this.SPnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.DAaoi.Text.ToString());
                totalb += decimal.Parse(this.DAaoei.Text.ToString());
                total += decimal.Parse(this.DAnti.Text.ToString());
            }
            catch
            {
                this.DAaoi.Text = this.DAaoei.Text = this.DAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.CAaoi.Text.ToString());
                totalb += decimal.Parse(this.CAaoei.Text.ToString());
                total += decimal.Parse(this.CAnti.Text.ToString());
            }
            catch
            {
                this.CAaoi.Text = this.CAaoei.Text = this.CAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.HRaoi.Text.ToString());
                totalb += decimal.Parse(this.HRaoei.Text.ToString());
                total += decimal.Parse(this.HRnti.Text.ToString());
            }
            catch
            {
                this.HRaoi.Text = this.HRaoei.Text = this.HRnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.MAaoi.Text.ToString());
                totalb += decimal.Parse(this.MAaoei.Text.ToString());
                total += decimal.Parse(this.MAnti.Text.ToString());
            }
            catch
            {
                this.MAaoi.Text = this.MAaoei.Text = this.MAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.SAaoi.Text.ToString());
                totalb += decimal.Parse(this.SAaoei.Text.ToString());
                total += decimal.Parse(this.SAnti.Text.ToString());
            }
            catch
            {
                this.SAaoi.Text = this.SAaoei.Text = this.SAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.LAaoi.Text.ToString());
                totalb += decimal.Parse(this.LAaoei.Text.ToString());
                total += decimal.Parse(this.LAnti.Text.ToString());
            }
            catch
            {
                this.LAaoi.Text = this.LAaoei.Text = this.LAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.Haoi.Text.ToString());
                totalb += decimal.Parse(this.Haoei.Text.ToString());
                total += decimal.Parse(this.Hnti.Text.ToString());
            }
            catch
            {
                this.Haoi.Text = this.Haoei.Text = this.Hnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.OAaoi.Text.ToString());
                totalb += decimal.Parse(this.OAaoei.Text.ToString());
                total += decimal.Parse(this.OAnti.Text.ToString());
            }
            catch
            {
                this.OAaoi.Text = this.OAaoei.Text = this.OAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.Baoi.Text.ToString());
                totalb += decimal.Parse(this.Baoei.Text.ToString());
                total += decimal.Parse(this.Bnti.Text.ToString());
            }
            catch
            {
                this.Baoi.Text = this.Baoei.Text = this.Bnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.OtAaoi.Text.ToString());
                totalb += decimal.Parse(this.OtAaoei.Text.ToString());
                total += decimal.Parse(this.OtAnti.Text.ToString());
            }
            catch
            {
                this.OtAaoi.Text = this.OtAaoei.Text = this.OtAnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.PFaoi.Text.ToString());
                totalb += decimal.Parse(this.PFaoei.Text.ToString());
                total += decimal.Parse(this.PFnti.Text.ToString());
            }
            catch
            {
                this.PFaoi.Text = this.PFaoei.Text = this.PFnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.PFIaoi.Text.ToString());
                totalb += decimal.Parse(this.PFIaoei.Text.ToString());
                total += decimal.Parse(this.PFInti.Text.ToString());
            }
            catch
            {
                this.PFIaoi.Text = this.PFIaoei.Text = this.PFInti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.TFaoi.Text.ToString());
                totalb += decimal.Parse(this.TFaoei.Text.ToString());
                total += decimal.Parse(this.TFnti.Text.ToString());
            }
            catch
            {
                this.TFaoi.Text = this.TFaoei.Text = this.TFnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.Aaoi.Text.ToString());
                totalb += decimal.Parse(this.Aaoei.Text.ToString());
                total += decimal.Parse(this.Anti.Text.ToString());
            }
            catch
            {
                this.Aaoi.Text = this.Aaoei.Text = this.Anti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.Oaoi.Text.ToString());
                totalb += decimal.Parse(this.Oaoei.Text.ToString());
                total += decimal.Parse(this.Onti.Text.ToString());
            }
            catch
            {
                this.Oaoi.Text = this.Oaoei.Text = this.Onti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.Paoi.Text.ToString());
                totalb += decimal.Parse(this.Paoei.Text.ToString());
                total += decimal.Parse(this.Pnti.Text.ToString());
            }
            catch
            {
                this.Paoi.Text = this.Paoei.Text = this.Pnti.Text = "0";
            }
            try
            {
                totala += decimal.Parse(this.Laoi.Text.ToString());
                totalb += decimal.Parse(this.Laoei.Text.ToString());
                total += decimal.Parse(this.Lnti.Text.ToString());
            }
            catch
            {
                this.Laoi.Text = this.Laoei.Text = this.Lnti.Text = "0";
            }
            try
            {
                this.totala.Text = totala.ToString();
                this.totalb.Text =  totalb.ToString();
                this.total.Text = total.ToString();
            }
            catch
            {
            }

            formattext();
        }

        private decimal getNTI(decimal AI, decimal AEI)
        {
            try
            {
                decimal aoi = decimal.Parse(AI.ToString());
                decimal aoei = decimal.Parse(AEI.ToString());
                decimal nti = Math.Max((aoi - aoei), 0);
                return nti;
            }
            catch
            {
                return (decimal)0;
            }
        }

        private decimal getNTI(string AI, decimal AEI)
        {
            try
            {
                decimal aoi = decimal.Parse(AI);
                decimal aoei = decimal.Parse(AEI.ToString());
                decimal nti = Math.Max((aoi - aoei), 0);
                return nti;
            }
            catch
            {
                return (decimal)0;
            }
        }

        private decimal getNTI(string AI, string AEI)
        {
            try
            {
                decimal aoi = decimal.Parse(AI);
                decimal aoei = decimal.Parse(AEI);
                decimal nti = Math.Max((aoi - aoei), 0);
                return nti;
            }
            catch
            {
                return (decimal)0;
            }
        }

        private void BPaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.BPaoi.Text.ToString(), 0);
            this.BPaoei.Text = "0";
            this.BPnti.Text = nti.ToString();

            calculateTotal();
        }

        private void SPaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.SPaoi.Text.ToString(), 0);
            this.SPaoei.Text = "0";
            this.SPnti.Text = nti.ToString();

            calculateTotal();
        }

        private void DAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.DAaoi.Text.ToString(), 0);
            this.DAaoei.Text = "0";
            this.DAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void CAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.CAaoi.Text.ToString(), 24000);
            this.CAaoei.Text = "24000";
            this.CAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void HRaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal hr = 0;
            try
            {
                hr = decimal.Parse(this.BPaoi.Text.ToString()) / 2;
            }
            catch
            {
                hr = 0;
            }

            hr = Math.Min(hr, 180000);
            
            decimal nti = getNTI(this.HRaoi.Text.ToString(), hr);
            this.HRaoei.Text = hr.ToString();
            this.HRnti.Text = nti.ToString();

            calculateTotal();
        }

        private void MAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            if (this.MAaoei.Text.ToString() == "")
            {
                this.MAaoei.Text = "0";
            }
            decimal nti = getNTI(this.MAaoi.Text.ToString(), this.MAaoei.Text.ToString());
            this.MAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void SAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.SAaoi.Text.ToString(), 0);
            this.SAaoei.Text = "0";
            this.SAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void LAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.LAaoi.Text.ToString(), 0);
            this.LAaoei.Text = "0";
            this.LAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void Haoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.Haoi.Text.ToString(), 0);
            this.Haoei.Text = "0";
            this.Hnti.Text = nti.ToString();

            calculateTotal();
        }

        private void OAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.OAaoi.Text.ToString(), 0);
            this.OAaoei.Text = "0";
            this.OAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void Baoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.Baoi.Text.ToString(), 0);
            this.Baoei.Text = "0";
            this.Bnti.Text = nti.ToString();

            calculateTotal();
        }

        private void OtAaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.OtAaoi.Text.ToString(), 0);
            this.OtAaoei.Text = "0";
            this.OtAnti.Text = nti.ToString();

            calculateTotal();
        }

        private void PFaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.PFaoi.Text.ToString(), 0);
            this.PFaoei.Text = "0";
            this.PFnti.Text = nti.ToString();

            calculateTotal();
        }

        private void PFIaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            this.PFIaoei.Text = this.PFIaoi.Text.ToString();
            decimal nti = getNTI(this.PFIaoi.Text.ToString(), this.PFIaoei.Text.ToString());
            this.PFInti.Text = nti.ToString();

            calculateTotal();
        }

        private void TFq_Checked(object sender, RoutedEventArgs e)
        {
            this.TFaoi.IsEnabled = true;
            this.TFaoei.IsEnabled = true;
            this.TFnti.IsEnabled = true;

            decimal tf = 0;
            try
            {
                tf = decimal.Parse(this.BPaoi.Text.ToString());
                tf *= (decimal)0.075;
            }
            catch
            {
                tf = 0;
            }

            decimal nti = getNTI(tf, 0);
            this.TFaoi.Text = tf.ToString();
            this.TFaoei.Text = "0";
            this.TFnti.Text = nti.ToString();

            calculateTotal();
        }

        private void TFq_Unchecked(object sender, RoutedEventArgs e)
        {
            this.TFaoi.IsEnabled = false;
            this.TFaoei.IsEnabled = false;
            this.TFnti.IsEnabled = false;

            this.TFaoi.Text = "";
            this.TFaoei.Text = "";
            this.TFnti.Text = "";

            calculateTotal();
        }

        private void TFaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Aq_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decimal a = 0;
            decimal nti = 0;
            try
            {
                a = decimal.Parse(this.BPaoi.Text.ToString()) / 4;
            }
            catch
            {
                a = 0;
            }
            switch (this.Aq.SelectedIndex)
            {
                case 1:
                    //Full free

                    this.Aaoi.IsEnabled = true;
                    this.Aaoei.IsEnabled = true;
                    this.Aar.IsEnabled = true;
                    this.Anti.IsEnabled = true;

                    this.Aaoi.Text = a.ToString();
                    this.Aar.Text = "0";
                    this.Aaoei.Text = this.Aar.Text;

                    nti = getNTI(this.Aaoi.Text.ToString(), this.Aaoei.Text.ToString());

                    this.Anti.Text = nti.ToString();

                    break;
                case 2:
                    //Partial free

                    this.Aaoi.IsEnabled = true;
                    this.Aaoei.IsEnabled = true;
                    this.Aar.IsEnabled = true;
                    this.Anti.IsEnabled = true;

                    this.Aar.IsReadOnly = false;

                    this.Aaoi.Text = a.ToString();
                    this.Aar.Text = "0";
                    this.Aaoei.Text = this.Aar.Text;

                    nti = getNTI(this.Aaoi.Text.ToString(), this.Aaoei.Text.ToString());

                    this.Anti.Text = nti.ToString();

                    break;
                case 3:
                    //Government

                    decimal hrcut = 0;
                    try
                    {
                        hrcut = decimal.Parse(this.Aaoi.Text.ToString()) / 4;
                        hrcut *= (decimal)0.075;
                    }
                    catch
                    {
                        hrcut = 0;
                    }

                    this.Aaoi.IsEnabled = true;
                    this.Aaoei.IsEnabled = true;
                    this.Aar.IsEnabled = true;
                    this.Anti.IsEnabled = true;

                    this.Aar.IsReadOnly = false;

                    this.Aaoi.Text = a.ToString();
                    this.Aar.Text = "0";
                    this.Aaoei.Text = hrcut.ToString();

                    nti = getNTI(this.Aaoi.Text.ToString(), this.Aaoei.Text.ToString());

                    this.Anti.Text = nti.ToString();

                    break;
                case 4:
                    //Defense

                    this.Aaoi.IsEnabled = true;
                    this.Aaoei.IsEnabled = true;
                    this.Aar.IsEnabled = true;
                    this.Anti.IsEnabled = true;

                    this.Aaoi.Text = "0";
                    this.Aar.Text = "0";
                    this.Aaoei.Text = this.Aar.Text;
                    this.Anti.Text = "0";

                    break;
                case 0:
                default:
                    this.Aaoi.IsEnabled = false;
                    this.Aaoei.IsEnabled = false;
                    this.Aar.IsEnabled = false;
                    this.Anti.IsEnabled = false;

                    this.Aaoi.IsReadOnly = true;
                    this.Aaoei.IsReadOnly = true;
                    this.Aar.IsReadOnly = true;
                    this.Anti.IsReadOnly = true;

                    this.Aaoi.Text = "0";
                    this.Aar.Text = "0";
                    this.Aaoei.Text = this.Aar.Text;
                    this.Anti.Text = "0";

                    break;
            }

            calculateTotal();
        }

        private void Aar_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal a = 0;
            decimal nti = 0;
            try
            {
                a = decimal.Parse(this.BPaoi.Text.ToString()) / 4;
            }
            catch
            {
                a = 0;
            }
            switch (this.Aq.SelectedIndex)
            {
                case 2:
                    //Partial free

                    this.Aaoi.IsEnabled = true;
                    this.Aaoei.IsEnabled = true;
                    this.Aar.IsEnabled = true;
                    this.Anti.IsEnabled = true;

                    this.Aar.IsReadOnly = false;

                    this.Aaoi.Text = a.ToString();
                    this.Aaoei.Text = this.Aar.Text;

                    nti = getNTI(this.Aaoi.Text.ToString(), this.Aaoei.Text.ToString());

                    this.Anti.Text = nti.ToString();

                    break;
                case 3:
                    //Government

                    decimal hrcut = 0;
                    try
                    {
                        hrcut = decimal.Parse(this.Aaoi.Text.ToString()) / 4;
                        hrcut *= (decimal)0.075;
                        hrcut += decimal.Parse(this.Aar.Text.ToString());
                    }
                    catch
                    {
                        hrcut = 0;
                    }

                    this.Aaoi.IsEnabled = true;
                    this.Aaoei.IsEnabled = true;
                    this.Aar.IsEnabled = true;
                    this.Anti.IsEnabled = true;

                    this.Aar.IsReadOnly = false;

                    this.Aaoi.Text = a.ToString();
                    this.Aaoei.Text = hrcut.ToString();

                    nti = getNTI(this.Aaoi.Text.ToString(), this.Aaoei.Text.ToString());

                    this.Anti.Text = nti.ToString();

                    break;
                default:
                    break;
            }

            calculateTotal();
        }

        private void Aaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Oaoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            if (this.Oaoei.Text.ToString() == "")
            {
                this.Oaoei.Text = "0";
            }
            decimal nti = getNTI(this.Oaoi.Text.ToString(), this.Oaoei.Text.ToString());
            this.Onti.Text = nti.ToString();

            calculateTotal();
        }

        private void Paoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            this.Paoei.Text = this.Paoi.Text.ToString();
            decimal nti = getNTI(this.Paoi.Text.ToString(), this.Paoei.Text.ToString());
            this.Pnti.Text = nti.ToString();

            calculateTotal();
        }

        private void Laoi_MouseLeave(object sender, RoutedEventArgs e)
        {
            decimal nti = getNTI(this.Laoi.Text.ToString(), 0);
            this.Laoei.Text = "0";
            this.Lnti.Text = nti.ToString();

            calculateTotal();
        }

        private void NextBtn_Click(object sender, MouseEventArgs e)
        {
            calculateTotal();
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            // Load data into the table customer. You can modify this code as needed.
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();

            taxDBDataSetpersonalreturnTableAdapter.UpdatePR2(decimal.Parse(this.BPaoi.Text), decimal.Parse(this.BPaoei.Text),
                decimal.Parse(this.SPaoi.Text), decimal.Parse(this.SPaoei.Text),
                decimal.Parse(this.DAaoi.Text), decimal.Parse(this.DAaoei.Text),
                decimal.Parse(this.CAaoi.Text), decimal.Parse(this.CAaoei.Text),
                decimal.Parse(this.HRaoi.Text), decimal.Parse(this.HRaoei.Text),
                decimal.Parse(this.MAaoi.Text), decimal.Parse(this.MAaoei.Text),
                decimal.Parse(this.SAaoi.Text), decimal.Parse(this.SAaoei.Text),
                decimal.Parse(this.LAaoi.Text), decimal.Parse(this.LAaoei.Text),
                decimal.Parse(this.Haoi.Text), decimal.Parse(this.Haoei.Text),
                decimal.Parse(this.OAaoi.Text), decimal.Parse(this.OAaoei.Text),
                decimal.Parse(this.Baoi.Text), decimal.Parse(this.Baoei.Text),
                decimal.Parse(this.OtAaoi.Text), decimal.Parse(this.OtAaoei.Text),
                decimal.Parse(this.PFaoi.Text), decimal.Parse(this.PFaoei.Text),
                decimal.Parse(this.PFIaoi.Text), decimal.Parse(this.PFIaoei.Text),
                decimal.Parse(this.TFaoi.Text), decimal.Parse(this.TFaoei.Text),
                decimal.Parse(this.Aaoi.Text), decimal.Parse(this.Aaoei.Text),
                decimal.Parse(this.Oaoi.Text), decimal.Parse(this.Oaoei.Text),
                decimal.Parse(this.Paoi.Text), decimal.Parse(this.Paoei.Text),
                decimal.Parse(this.Laoi.Text), decimal.Parse(this.Laoei.Text),
                this.returnid);

            taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);

            PRHousePropertyInformation PRHousePropertyInformationForm = new PRHousePropertyInformation(this.returnid);
            PRHousePropertyInformationForm.Owner = this.Owner;
            PRHousePropertyInformationForm.Show();
            this.Close();
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void PrevBtn_Click(object sender, MouseEventArgs e)
        {
            PRAssesseeInformation PRAssesseeInformationForm = new PRAssesseeInformation(this.customerid, this.returnid);
            PRAssesseeInformationForm.Owner = this.Owner;
            PRAssesseeInformationForm.Show(); 
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
