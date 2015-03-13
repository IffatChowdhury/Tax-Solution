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
    /// Interaction logic for PRAssetsAndLiabilities.xaml
    /// </summary>
    public partial class PRAssetsAndLiabilities : Window
    {
        private int returnid;
        private int customerid;

        public PRAssetsAndLiabilities(int returnid)
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

                this.BCDSbc.Text = dr["asset1a"].ToString();
                this.Is.Text = dr["asset4a"].ToString();
                this.Isc.Text = dr["asset4b"].ToString();
                this.Ipb.Text = dr["asset4c"].ToString();
                this.Ilg.Text = dr["asset4d"].ToString();
                this.Ioi.Text = dr["asset4e"].ToString();
                this.AFf.Text = dr["asset7"].ToString();
                this.AEe.Text = dr["asset8"].ToString();
                this.ACih.Text = dr["asset9a"].ToString();
                this.ACib.Text = dr["asset9b"].ToString();
                this.ACod.Text = dr["asset9c"].ToString();
                this.LLm.Text = dr["asset11a"].ToString();
                this.LLul.Text = dr["asset11b"].ToString();
                this.LLbl.Text = dr["asset11c"].ToString();
                this.LLo.Text = dr["asset11d"].ToString();
                this.ANWldopiy.Text = dr["asset13"].ToString();

                try
                {
                    this.FEt.Text = String.Format("{0:#,0.##}", totalfamilyexp());
                    this.FEt.IsReadOnly = true;
                }
                catch
                {
                    this.FEt.Text = dr["asset15a"].ToString();
                    this.FEt.IsReadOnly = true;
                }

                this.FEa.Text = dr["asset15badult"].ToString();
                this.FEc.Text = dr["asset15bchild"].ToString();
                this.SOFor.Text = dr["asset17c"].ToString();

                this.SOFsri.Text = getsoi12().ToString();
                this.SOFte.Text = getexempted().ToString();


                loaddirectorshare();
                loadnonagreculture();
                loadagreculture();
                loadmotorvechicle();
                loadjewelleary();
            }
        }

        private decimal totalfamilyexp()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            decimal total = 0;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                try
                {
                    total += decimal.Parse(dr["it10bb1a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb2a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb3a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb4a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb5a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb6a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb7a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb8a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb9a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb10a"].ToString());
                }
                catch
                {
                }
                try
                {
                    total += decimal.Parse(dr["it10bb11a"].ToString());
                }
                catch
                {
                }
            }
            return total;
        }

        private decimal getsoi12()
        {
            decimal total = 0;

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                for (int i = 1; i < 12; i++)
                {
                    if (i != 10)
                    {
                        try
                        {
                            total += decimal.Parse(dr["soi" + i].ToString());
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return total;
        }

        private decimal getexempted()
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

        private void NextBtn_Click(object sender, MouseButtonEventArgs e)
        {
            updatealldata();
            directorshareupdate();
            nogagpropertyupdate();
            agpropertyupdate();
            motorvehicleupdate();
            jewelleryupdate();
            otherupdate();

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();

            decimal asset1a = 0,
                asset4a = 0,
                asset4b = 0,
                asset4c = 0,
                asset4d = 0,
                asset4e = 0,
                asset7 = 0,
                asset8 = 0,
                asset9a = 0,
                asset9b = 0,
                asset9c = 0,
                asset11a = 0,
                asset11b = 0,
                asset11c = 0,
                asset11d = 0,
                asset13 = 0,
                asset15a = 0,
                asset15badult = 0,
                asset15bchild = 0,
                asset17c = 0;

            try
            {
                asset1a = decimal.Parse(this.BCDSbc.Text);
            }
            catch
            {
            }
            try
            {
                asset4a = decimal.Parse(this.Is.Text);
            }
            catch
            {
            }
            try
            {
                asset4b = decimal.Parse(this.Isc.Text);
            }
            catch
            {
            }
            try
            {
                asset4c = decimal.Parse(this.Ipb.Text);
            }
            catch
            {
            }
            try
            {
                asset4d = decimal.Parse(this.Ilg.Text);
            }
            catch
            {
            }
            try
            {
                asset4e = decimal.Parse(this.Ioi.Text);
            }
            catch
            {
            }
            try
            {
                asset7 = decimal.Parse(this.AFf.Text);
            }
            catch
            {
            }
            try
            {
                asset8 = decimal.Parse(this.AEe.Text);
            }
            catch
            {
            }
            try
            {
                asset9a = decimal.Parse(this.ACih.Text);
            }
            catch
            {
            }
            try
            {
                asset9b = decimal.Parse(this.ACib.Text);
            }
            catch
            {
            }
            try
            {
                asset9c = decimal.Parse(this.ACod.Text);
            }
            catch
            {
            }
            try
            {
                asset11a = decimal.Parse(this.LLm.Text);
            }
            catch
            {
            }
            try
            {
                asset11b = decimal.Parse(this.LLul.Text);
            }
            catch
            {
            }
            try
            {
                asset11c = decimal.Parse(this.LLbl.Text);
            }
            catch
            {
            }
            try
            {
                asset11d = decimal.Parse(this.LLo.Text);
            }
            catch
            {
            }
            try
            {
                asset13 = decimal.Parse(this.ANWldopiy.Text);
            }
            catch
            {
            }
            try
            {
                asset15a = decimal.Parse(this.FEt.Text);
            }
            catch
            {
            }
            try
            {
                asset15badult = decimal.Parse(this.FEa.Text);
            }
            catch
            {
            }
            try
            {
                asset15bchild = decimal.Parse(this.FEc.Text);
            }
            catch
            {
            }
            try
            {
                asset17c = decimal.Parse(this.SOFor.Text);
            }
            catch
            {
            }

            taxDBDataSetpersonalreturnTableAdapter.UpdatePR6(asset1a, asset4a, asset4b, asset4c, asset4d, asset4e, asset7, asset8, asset9a, asset9b, asset9c, asset11a, asset11b, asset11c, asset11d, asset13, asset15a, asset15badult, asset15bchild, asset17c, this.returnid);
            taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);

            PRFurnishedDocuments PRFurnishedDocumentsForm = new PRFurnishedDocuments(this.returnid);
            PRFurnishedDocumentsForm.Owner = this.Owner;
            PRFurnishedDocumentsForm.Show();
            this.Close();
        }

        private void loaddirectorshare()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prdirectorsshareTableAdapter taxDBDataSetprdirectorsshareTableAdapter = new TaxDBDataSetTableAdapters.prdirectorsshareTableAdapter();
            DataTable dt = taxDBDataSetprdirectorsshareTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                decimal total = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    int count = int.Parse(this.BCDScount.Text.ToString());
                    count++;
                    this.BCDScount.Text = count.ToString();

                    WrapPanel wp = new WrapPanel();
                    wp.Name = "BCDWP" + count.ToString();

                    TextBox tb1 = new TextBox();
                    tb1.Name = "BCDSname" + count.ToString();
                    tb1.Width = 250;
                    tb1.Margin = new Thickness(10);
                    tb1.Text = dr["companyname"].ToString();

                    TextBox tb2 = new TextBox();
                    tb2.Name = "BCDSshare" + count.ToString();
                    tb2.Width = 120;
                    tb2.Margin = new Thickness(10);
                    tb2.Text = dr["numberofshare"].ToString();

                    TextBox tb3 = new TextBox();
                    tb3.Name = "BCDSamount" + count.ToString();
                    tb3.Width = 50;
                    tb3.Margin = new Thickness(10);
                    tb3.LostFocus += new RoutedEventHandler(BCDSadd_LostFocus);
                    tb3.Text = dr["amount"].ToString();

                    try
                    {
                        total += decimal.Parse(dr["amount"].ToString());
                    }
                    catch
                    {
                    }

                    CheckBox rmchk = new CheckBox();
                    rmchk.Name = "BCDSRemove" + count.ToString();
                    rmchk.Content = "Mark to remove";
                    rmchk.IsChecked = false;
                    rmchk.Margin = new Thickness(10);
                    rmchk.Checked += new RoutedEventHandler(BCDSadd_Checked);
                    rmchk.Unchecked += new RoutedEventHandler(BCDSadd_Unchecked);

                    wp.Children.Add(tb1);
                    wp.Children.Add(tb2);
                    wp.Children.Add(tb3);
                    wp.Children.Add(rmchk);

                    this.BCDSitems.Children.Add(wp);
                }
                this.BCDSdsilc.Text = total.ToString();
            }
        }

        private void loadnonagreculture()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prnonagpropertyTableAdapter taxDBDataSetprnonagpropertyTableAdapter = new TaxDBDataSetTableAdapters.prnonagpropertyTableAdapter();
            DataTable dt = taxDBDataSetprnonagpropertyTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                decimal total = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    int count = int.Parse(this.NAPcount.Text.ToString());
                    count++;
                    this.NAPcount.Text = count.ToString();

                    WrapPanel wp = new WrapPanel();
                    wp.Name = "NAPWP" + count.ToString();

                    TextBox tb1 = new TextBox();
                    tb1.Name = "NAPname" + count.ToString();
                    tb1.Width = 250;
                    tb1.Margin = new Thickness(10);
                    tb1.Text = dr["description"].ToString();

                    TextBox tb2 = new TextBox();
                    tb2.Name = "NAPamount" + count.ToString();
                    tb2.Width = 50;
                    tb2.Margin = new Thickness(10);
                    tb2.LostFocus += new RoutedEventHandler(NAPadd_LostFocus);
                    tb2.Text = dr["amount"].ToString();
                    try
                    {
                        total += decimal.Parse(dr["amount"].ToString());
                    }
                    catch
                    {
                    }

                    CheckBox rmchk = new CheckBox();
                    rmchk.Name = "NAPRemove" + count.ToString();
                    rmchk.Content = "Mark to remove";
                    rmchk.IsChecked = false;
                    rmchk.Margin = new Thickness(10);
                    rmchk.Checked += new RoutedEventHandler(NAPadd_Checked);
                    rmchk.Unchecked += new RoutedEventHandler(NAPadd_Unchecked);

                    wp.Children.Add(tb1);
                    wp.Children.Add(tb2);
                    wp.Children.Add(rmchk);

                    this.NAPitems.Children.Add(wp);
                }
                this.NAPna.Text = total.ToString();
            }
        }

        private void loadagreculture()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.pragpropertyTableAdapter taxDBDataSetpragpropertyTableAdapter = new TaxDBDataSetTableAdapters.pragpropertyTableAdapter();
            DataTable dt = taxDBDataSetpragpropertyTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                decimal total = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    int count = int.Parse(this.APcount.Text.ToString());
                    count++;
                    this.APcount.Text = count.ToString();

                    WrapPanel wp = new WrapPanel();
                    wp.Name = "APWP" + count.ToString();

                    TextBox tb1 = new TextBox();
                    tb1.Name = "APname" + count.ToString();
                    tb1.Width = 250;
                    tb1.Margin = new Thickness(10);
                    tb1.Text = dr["description"].ToString();

                    TextBox tb2 = new TextBox();
                    tb2.Name = "APamount" + count.ToString();
                    tb2.Width = 50;
                    tb2.Margin = new Thickness(10);
                    tb2.LostFocus += new RoutedEventHandler(APadd_LostFocus);
                    tb2.Text = dr["amount"].ToString();
                    try
                    {
                        total += decimal.Parse(dr["amount"].ToString());
                    }
                    catch
                    {
                    }

                    CheckBox rmchk = new CheckBox();
                    rmchk.Name = "APRemove" + count.ToString();
                    rmchk.Content = "Mark to remove";
                    rmchk.IsChecked = false;
                    rmchk.Margin = new Thickness(10);
                    rmchk.Checked += new RoutedEventHandler(APadd_Checked);
                    rmchk.Unchecked += new RoutedEventHandler(APadd_Unchecked);

                    wp.Children.Add(tb1);
                    wp.Children.Add(tb2);
                    wp.Children.Add(rmchk);

                    this.APitems.Children.Add(wp);
                }
                this.APp.Text = total.ToString();
            }
        }

        private void loadmotorvechicle()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prmotorvehicleTableAdapter taxDBDataSetprmotorvehicleTableAdapter = new TaxDBDataSetTableAdapters.prmotorvehicleTableAdapter();
            DataTable dt = taxDBDataSetprmotorvehicleTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                decimal total = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    int count = int.Parse(this.AMVcount.Text.ToString());
                    count++;
                    this.AMVcount.Text = count.ToString();

                    WrapPanel wp = new WrapPanel();
                    wp.Name = "AMVWP" + count.ToString();

                    TextBox tb1 = new TextBox();
                    tb1.Name = "AMVname" + count.ToString();
                    tb1.Width = 250;
                    tb1.Margin = new Thickness(10);
                    tb1.Text = dr["type"].ToString();

                    TextBox tb2 = new TextBox();
                    tb2.Name = "AMVamount" + count.ToString();
                    tb2.Width = 50;
                    tb2.Margin = new Thickness(10);
                    tb2.LostFocus += new RoutedEventHandler(APadd_LostFocus);
                    tb2.Text = dr["amount"].ToString();
                    try
                    {
                        total += decimal.Parse(dr["amount"].ToString());
                    }
                    catch
                    {
                    }

                    CheckBox rmchk = new CheckBox();
                    rmchk.Name = "AMVRemove" + count.ToString();
                    rmchk.Content = "Mark to remove";
                    rmchk.IsChecked = false;
                    rmchk.Margin = new Thickness(10);
                    rmchk.Checked += new RoutedEventHandler(APadd_Checked);
                    rmchk.Unchecked += new RoutedEventHandler(APadd_Unchecked);

                    wp.Children.Add(tb1);
                    wp.Children.Add(tb2);
                    wp.Children.Add(rmchk);

                    this.AMVitems.Children.Add(wp);
                }
                this.AMVmv.Text = total.ToString();
            }
        }

        private void loadjewelleary()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prjewelleryTableAdapter taxDBDataSetprjewelleryTableAdapter = new TaxDBDataSetTableAdapters.prjewelleryTableAdapter();
            DataTable dt = taxDBDataSetprjewelleryTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                decimal total = 0;

                DataRow dr = dt.Rows[0];

                try
                {
                    total += decimal.Parse(dr["amount"].ToString());
                }
                catch
                {
                }

                this.AJj.Text = dr["description"].ToString();                
                this.AJjc.Text = total.ToString();
            }
        }

        private void loadother()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.protherTableAdapter taxDBDataSetprotherTableAdapter = new TaxDBDataSetTableAdapters.protherTableAdapter();
            DataTable dt = taxDBDataSetprotherTableAdapter.GetDataByReturn(this.returnid);

            if (dt.Rows.Count > 0)
            {
                decimal total = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    int count = int.Parse(this.AOAcount.Text.ToString());
                    count++;
                    this.AOAcount.Text = count.ToString();

                    WrapPanel wp = new WrapPanel();
                    wp.Name = "AOAWP" + count.ToString();

                    TextBox tb1 = new TextBox();
                    tb1.Name = "AOAname" + count.ToString();
                    tb1.Width = 250;
                    tb1.Margin = new Thickness(10);
                    tb1.Text = dr["type"].ToString();

                    TextBox tb2 = new TextBox();
                    tb2.Name = "AOAamount" + count.ToString();
                    tb2.Width = 50;
                    tb2.Margin = new Thickness(10);
                    tb2.LostFocus += new RoutedEventHandler(APadd_LostFocus);
                    tb2.Text = dr["amount"].ToString();
                    try
                    {
                        total += decimal.Parse(dr["amount"].ToString());
                    }
                    catch
                    {
                    }

                    CheckBox rmchk = new CheckBox();
                    rmchk.Name = "AOARemove" + count.ToString();
                    rmchk.Content = "Mark to remove";
                    rmchk.IsChecked = false;
                    rmchk.Margin = new Thickness(10);
                    rmchk.Checked += new RoutedEventHandler(APadd_Checked);
                    rmchk.Unchecked += new RoutedEventHandler(APadd_Unchecked);

                    wp.Children.Add(tb1);
                    wp.Children.Add(tb2);
                    wp.Children.Add(rmchk);

                    this.APitems.Children.Add(wp);
                }
                this.AOa.Text = total.ToString();
            }
        }

        private void directorshareupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prdirectorsshareTableAdapter taxDBDataSetprdirectorsshareTableAdapter = new TaxDBDataSetTableAdapters.prdirectorsshareTableAdapter();
            taxDBDataSetprdirectorsshareTableAdapter.DeleteByRid(this.returnid);
            int count = int.Parse(this.BCDScount.Text);
            for (int i = 1; i <= count; i++)
            {
                TextBox amount = FindChildren.FindChild<TextBox>(this, "BCDSamount" + i);
                TextBox name = FindChildren.FindChild<TextBox>(this, "BCDSname" + i);
                TextBox share = FindChildren.FindChild<TextBox>(this, "BCDSshare" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "BCDSRemove" + i);
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            decimal amt = decimal.Parse(amount.Text);
                            taxDBDataSetprdirectorsshareTableAdapter.Insert(this.returnid, name.Text, share.Text, amt);
                            taxDBDataSetprdirectorsshareTableAdapter.Update(taxDBDataSet);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            taxDBDataSetprdirectorsshareTableAdapter.Update(taxDBDataSet);
        }

        private void nogagpropertyupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prnonagpropertyTableAdapter taxDBDataSetprnonagpropertyTableAdapter = new TaxDBDataSetTableAdapters.prnonagpropertyTableAdapter();
            taxDBDataSetprnonagpropertyTableAdapter.DeleteByReturn(this.returnid);
            int count = int.Parse(this.BCDScount.Text);
            for (int i = 1; i <= count; i++)
            {
                TextBox name = FindChildren.FindChild<TextBox>(this, "NAPname" + i);
                TextBox amount = FindChildren.FindChild<TextBox>(this, "NAPamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "NAPRemove" + i);
                
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            decimal amt = decimal.Parse(amount.Text);
                            taxDBDataSetprnonagpropertyTableAdapter.Insert(this.returnid, name.Text, amt);
                            taxDBDataSetprnonagpropertyTableAdapter.Update(taxDBDataSet);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            taxDBDataSetprnonagpropertyTableAdapter.Update(taxDBDataSet);
        }

        private void agpropertyupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.pragpropertyTableAdapter taxDBDataSetpragpropertyTableAdapter = new TaxDBDataSetTableAdapters.pragpropertyTableAdapter();
            taxDBDataSetpragpropertyTableAdapter.DeleteByReturn(this.returnid);
            int count = int.Parse(this.BCDScount.Text);
            for (int i = 1; i <= count; i++)
            {
                TextBox name = FindChildren.FindChild<TextBox>(this, "APname" + i);
                TextBox amount = FindChildren.FindChild<TextBox>(this, "APamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "APRemove" + i);

                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            decimal amt = decimal.Parse(amount.Text);
                            taxDBDataSetpragpropertyTableAdapter.Insert(this.returnid, name.Text, amt);
                            taxDBDataSetpragpropertyTableAdapter.Update(taxDBDataSet);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            taxDBDataSetpragpropertyTableAdapter.Update(taxDBDataSet);
        }

        private void motorvehicleupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prmotorvehicleTableAdapter taxDBDataSetprmotorvehicleTableAdapter = new TaxDBDataSetTableAdapters.prmotorvehicleTableAdapter();
            taxDBDataSetprmotorvehicleTableAdapter.DeleteByReturn(this.returnid);
            int count = int.Parse(this.BCDScount.Text);
            for (int i = 1; i <= count; i++)
            {
                TextBox name = FindChildren.FindChild<TextBox>(this, "AMVname" + i);
                TextBox amount = FindChildren.FindChild<TextBox>(this, "AMVamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "AMVRemove" + i);

                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            decimal amt = decimal.Parse(amount.Text);
                            taxDBDataSetprmotorvehicleTableAdapter.Insert(this.returnid, name.Text, amt);
                            taxDBDataSetprmotorvehicleTableAdapter.Update(taxDBDataSet);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            taxDBDataSetprmotorvehicleTableAdapter.Update(taxDBDataSet);
        }

        private void jewelleryupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.prjewelleryTableAdapter taxDBDataSetprjewelleryTableAdapter = new TaxDBDataSetTableAdapters.prjewelleryTableAdapter();
            taxDBDataSetprjewelleryTableAdapter.DeleteByReturn(this.returnid);
            try
            {
                taxDBDataSetprjewelleryTableAdapter.Insert(this.returnid, this.AJj.Text, decimal.Parse(this.AJjc.Text));
            }
            catch
            {
            }
            taxDBDataSetprjewelleryTableAdapter.Update(taxDBDataSet);
        }

        private void otherupdate()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.protherTableAdapter taxDBDataSetprotherTableAdapter = new TaxDBDataSetTableAdapters.protherTableAdapter();
            taxDBDataSetprotherTableAdapter.DeleteByReturn(this.returnid);
            int count = int.Parse(this.BCDScount.Text);
            for (int i = 1; i <= count; i++)
            {
                TextBox name = FindChildren.FindChild<TextBox>(this, "AOAname" + i);
                TextBox amount = FindChildren.FindChild<TextBox>(this, "AOAamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "AOARemove" + i);

                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            decimal amt = decimal.Parse(amount.Text);
                            taxDBDataSetprotherTableAdapter.Insert(this.returnid, name.Text, amt);
                            taxDBDataSetprotherTableAdapter.Update(taxDBDataSet);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            taxDBDataSetprotherTableAdapter.Update(taxDBDataSet);
        }

        private void PrevBtn_Click(object sender, MouseButtonEventArgs e)
        {
            PRIT10_BB PRIT10_BBForm = new PRIT10_BB(this.returnid);
            PRIT10_BBForm.Owner = this.Owner;
            PRIT10_BBForm.Show();
            this.Close();
        }

        private void closeBtn_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void updatealldata()
        {
            BCDStotal();
            NAPtotal();
            APtotal();
            AMVtotal();
            AOAtotal();
        }

        private void BCDStotal()
        {
            int count = int.Parse(this.BCDScount.Text);
            decimal total = 0;
            for (int i = 1; i <= count; i++)
            {
                TextBox amount = FindChildren.FindChild<TextBox>(this, "BCDSamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "BCDSRemove" + i);
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            total += decimal.Parse(amount.Text);
                        }
                        catch
                        {
                            amount.BorderBrush = Brushes.Red;
                        }
                    }
                }
            }
            this.BCDSdsilc.Text = total.ToString();
        }

        private void NAPtotal()
        {
            int count = int.Parse(this.NAPcount.Text);
            decimal total = 0;
            for (int i = 1; i <= count; i++)
            {
                TextBox amount = FindChildren.FindChild<TextBox>(this, "NAPamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "NAPRemove" + i);
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            total += decimal.Parse(amount.Text);
                        }
                        catch
                        {
                            amount.BorderBrush = Brushes.Red;
                        }
                    }
                }
            }
            this.NAPna.Text = total.ToString();
        }

        private void APtotal()
        {
            int count = int.Parse(this.APcount.Text);
            decimal total = 0;
            for (int i = 1; i <= count; i++)
            {
                TextBox amount = FindChildren.FindChild<TextBox>(this, "APamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "APRemove" + i);
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            total += decimal.Parse(amount.Text);
                        }
                        catch
                        {
                            amount.BorderBrush = Brushes.Red;
                        }
                    }
                }
            }
            this.APp.Text = total.ToString();
        }

        private void AMVtotal()
        {
            int count = int.Parse(this.AMVcount.Text);
            decimal total = 0;
            for (int i = 1; i <= count; i++)
            {
                TextBox amount = FindChildren.FindChild<TextBox>(this, "AMVamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "AMVRemove" + i);
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            total += decimal.Parse(amount.Text);
                        }
                        catch
                        {
                            amount.BorderBrush = Brushes.Red;
                        }
                    }
                }
            }
            this.AMVmv.Text = total.ToString();
        }

        private void AOAtotal()
        {
            int count = int.Parse(this.AOAcount.Text);
            decimal total = 0;
            for (int i = 1; i <= count; i++)
            {
                TextBox amount = FindChildren.FindChild<TextBox>(this, "AOAamount" + i);
                CheckBox remove = FindChildren.FindChild<CheckBox>(this, "AOARemove" + i);
                if (amount != null && remove != null)
                {
                    if (!(bool)remove.IsChecked)
                    {
                        try
                        {
                            total += decimal.Parse(amount.Text);
                        }
                        catch
                        {
                            amount.BorderBrush = Brushes.Red;
                        }
                    }
                }
            }
            this.AOa.Text = total.ToString();
        }

        private void BCDSaddbtn_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(this.BCDScount.Text.ToString());
            count++;
            if (count < 3)
            {
                this.BCDScount.Text = count.ToString();

                WrapPanel wp = new WrapPanel();
                wp.Name = "BCDWP" + count.ToString();

                TextBox tb1 = new TextBox();
                tb1.Name = "BCDSname" + count.ToString();
                tb1.Width = 250;
                tb1.Margin = new Thickness(10);

                TextBox tb2 = new TextBox();
                tb2.Name = "BCDSshare" + count.ToString();
                tb2.Width = 120;
                tb2.Margin = new Thickness(10);

                TextBox tb3 = new TextBox();
                tb3.Name = "BCDSamount" + count.ToString();
                tb3.Width = 50;
                tb3.Margin = new Thickness(10);
                tb3.LostFocus += new RoutedEventHandler(BCDSadd_LostFocus);

                CheckBox rmchk = new CheckBox();
                rmchk.Name = "BCDSRemove" + count.ToString();
                rmchk.Content = "Mark to remove";
                rmchk.IsChecked = false;
                rmchk.Margin = new Thickness(10);
                rmchk.Checked += new RoutedEventHandler(BCDSadd_Checked);
                rmchk.Unchecked += new RoutedEventHandler(BCDSadd_Unchecked);

                wp.Children.Add(tb1);
                wp.Children.Add(tb2);
                wp.Children.Add(tb3);
                wp.Children.Add(rmchk);

                this.BCDSitems.Children.Add(wp);
            }
            else
            {
                MessageBox.Show("To add more items, use seperate sheet");
            }
        }

        void BCDSadd_Unchecked(object sender, RoutedEventArgs e)
        {
            BCDStotal();
        }

        void BCDSadd_Checked(object sender, RoutedEventArgs e)
        {
            BCDStotal();
        }

        void BCDSadd_LostFocus(object sender, RoutedEventArgs e)
        {
            BCDStotal();
        }

        private void NAPaddbtn_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(this.NAPcount.Text.ToString());
            count++;
            if (count < 3)
            {
                this.NAPcount.Text = count.ToString();

                WrapPanel wp = new WrapPanel();
                wp.Name = "NAPWP" + count.ToString();

                TextBox tb1 = new TextBox();
                tb1.Name = "NAPname" + count.ToString();
                tb1.Width = 250;
                tb1.Margin = new Thickness(10);

                TextBox tb2 = new TextBox();
                tb2.Name = "NAPamount" + count.ToString();
                tb2.Width = 50;
                tb2.Margin = new Thickness(10);
                tb2.LostFocus += new RoutedEventHandler(NAPadd_LostFocus);

                CheckBox rmchk = new CheckBox();
                rmchk.Name = "NAPRemove" + count.ToString();
                rmchk.Content = "Mark to remove";
                rmchk.IsChecked = false;
                rmchk.Margin = new Thickness(10);
                rmchk.Checked += new RoutedEventHandler(NAPadd_Checked);
                rmchk.Unchecked += new RoutedEventHandler(NAPadd_Unchecked);

                wp.Children.Add(tb1);
                wp.Children.Add(tb2);
                wp.Children.Add(rmchk);

                this.NAPitems.Children.Add(wp);
            }
            else
            {
                MessageBox.Show("To add more items, use seperate sheet");
            }
        }

        void NAPadd_Unchecked(object sender, RoutedEventArgs e)
        {
            NAPtotal();
        }

        void NAPadd_Checked(object sender, RoutedEventArgs e)
        {
            NAPtotal();
        }

        void NAPadd_LostFocus(object sender, RoutedEventArgs e)
        {
            NAPtotal();
        }

        private void APaddbtn_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(this.APcount.Text.ToString());
            count++;
            if (count < 3)
            {
                this.APcount.Text = count.ToString();

                WrapPanel wp = new WrapPanel();
                wp.Name = "APWP" + count.ToString();

                TextBox tb1 = new TextBox();
                tb1.Name = "APname" + count.ToString();
                tb1.Width = 250;
                tb1.Margin = new Thickness(10);

                TextBox tb2 = new TextBox();
                tb2.Name = "APamount" + count.ToString();
                tb2.Width = 50;
                tb2.Margin = new Thickness(10);
                tb2.LostFocus += new RoutedEventHandler(APadd_LostFocus);

                CheckBox rmchk = new CheckBox();
                rmchk.Name = "APRemove" + count.ToString();
                rmchk.Content = "Mark to remove";
                rmchk.IsChecked = false;
                rmchk.Margin = new Thickness(10);
                rmchk.Checked += new RoutedEventHandler(APadd_Checked);
                rmchk.Unchecked += new RoutedEventHandler(APadd_Unchecked);

                wp.Children.Add(tb1);
                wp.Children.Add(tb2);
                wp.Children.Add(rmchk);

                this.APitems.Children.Add(wp);
            }
            else
            {
                MessageBox.Show("To add more items, use seperate sheet");
            }
        }

        void APadd_Unchecked(object sender, RoutedEventArgs e)
        {
            APtotal();
        }

        void APadd_Checked(object sender, RoutedEventArgs e)
        {
            APtotal();
        }

        void APadd_LostFocus(object sender, RoutedEventArgs e)
        {
            APtotal();
        }

        private void AMVaddbtn_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(this.AMVcount.Text.ToString());
            count++;
            if (count < 3)
            {
                this.AMVcount.Text = count.ToString();

                WrapPanel wp = new WrapPanel();
                wp.Name = "AMVWP" + count.ToString();

                TextBox tb1 = new TextBox();
                tb1.Name = "AMVname" + count.ToString();
                tb1.Width = 250;
                tb1.Margin = new Thickness(10);

                TextBox tb2 = new TextBox();
                tb2.Name = "AMVamount" + count.ToString();
                tb2.Width = 50;
                tb2.Margin = new Thickness(10);
                tb2.LostFocus += new RoutedEventHandler(AMVadd_LostFocus);

                CheckBox rmchk = new CheckBox();
                rmchk.Name = "AMVRemove" + count.ToString();
                rmchk.Content = "Mark to remove";
                rmchk.IsChecked = false;
                rmchk.Margin = new Thickness(10);
                rmchk.Checked += new RoutedEventHandler(AMVadd_Checked);
                rmchk.Unchecked += new RoutedEventHandler(AMVadd_Unchecked);

                wp.Children.Add(tb1);
                wp.Children.Add(tb2);
                wp.Children.Add(rmchk);

                this.AMVitems.Children.Add(wp);
            }
            else
            {
                MessageBox.Show("To add more items, use seperate sheet");
            }
        }

        void AMVadd_Unchecked(object sender, RoutedEventArgs e)
        {
            AMVtotal();
        }

        void AMVadd_Checked(object sender, RoutedEventArgs e)
        {
            AMVtotal();
        }

        void AMVadd_LostFocus(object sender, RoutedEventArgs e)
        {
            AMVtotal();
        }

        private void AOAaddbtn_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(this.AOAcount.Text.ToString());
            count++;
            if (count < 3)
            {
                this.AOAcount.Text = count.ToString();

                WrapPanel wp = new WrapPanel();
                wp.Name = "AOAWP" + count.ToString();

                TextBox tb1 = new TextBox();
                tb1.Name = "AOAname" + count.ToString();
                tb1.Width = 250;
                tb1.Margin = new Thickness(10);

                TextBox tb2 = new TextBox();
                tb2.Name = "AOAamount" + count.ToString();
                tb2.Width = 50;
                tb2.Margin = new Thickness(10);
                tb2.LostFocus += new RoutedEventHandler(AOAadd_LostFocus);

                CheckBox rmchk = new CheckBox();
                rmchk.Name = "AOARemove" + count.ToString();
                rmchk.Content = "Mark to remove";
                rmchk.IsChecked = false;
                rmchk.Margin = new Thickness(10);
                rmchk.Checked += new RoutedEventHandler(AOAadd_Checked);
                rmchk.Unchecked += new RoutedEventHandler(AOAadd_Unchecked);

                wp.Children.Add(tb1);
                wp.Children.Add(tb2);
                wp.Children.Add(rmchk);

                this.AOAitems.Children.Add(wp);
            }
            else
            {
                MessageBox.Show("To add more items, use seperate sheet");
            }
        }

        void AOAadd_Unchecked(object sender, RoutedEventArgs e)
        {
            AOAtotal();
        }

        void AOAadd_Checked(object sender, RoutedEventArgs e)
        {
            AOAtotal();
        }

        void AOAadd_LostFocus(object sender, RoutedEventArgs e)
        {
            AOAtotal();
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
