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
using System.Windows.Forms;  
using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using System.Data;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TinPrint.xaml
    /// </summary>
    public partial class TinPrint : Window
    {
        private int customerid;
        public TinPrint(int customerid)
        {
            InitializeComponent();
            this.customerid = customerid;
            if (checkLicense())
            {
                populateData();
            }
            else
            {
                System.Windows.MessageBox.Show("Please activate the software first.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private bool checkLicense()
        {
            string licensekey = TaxSolution.Properties.Settings.Default.licensekey;
            if (licensekey.Length < 1)
            {
                HardDisk hd = new HardDisk();
                string serial = hd.getserial();
                TaxSolution.Properties.Settings.Default.hdserial = serial;

                serial = EncodePassword(serial + "CREASH IS BEST");
                serial = ReverseString(serial);
                serial = serial.Substring(0, 15).ToUpper();

                TaxSolution.Properties.Settings.Default.licensekey = serial;
                licensekey = serial;
            }
            return licenseStatusCheck(licensekey);
        }

        bool licenseStatusCheck(string key)
        {
            string enc = EncodePassword(key + "CREASH IS BEST");
            enc = ReverseString(enc);
            enc = EncodePassword(enc + "CREASH IS BEST");
            enc = ReverseString(enc);
            string activation = enc.Substring(0, 15).ToUpper();
            string savedact = TaxSolution.Properties.Settings.Default.activationkey.ToUpper();

            if (activation == savedact)
            {
                return true;
            }

            return false;
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
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

        public TinPrint()
        {
            System.Windows.MessageBox.Show("Error occured. Printing of the application is halted.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            this.Close();
        }

        private void populateData()
        {
            if (this.customerid > 0)
            {
                TaxSolution.TaxDBDataSet taxDBDataSet = new TaxSolution.TaxDBDataSet();
                // Load data into the table customerTinJoin. You can modify this code as needed.
                TaxSolution.TaxDBDataSetTableAdapters.customerTinJoinTableAdapter taxDBDataSetcustomerTinJoinTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTinJoinTableAdapter();
                taxDBDataSetcustomerTinJoinTableAdapter.FillByCId(taxDBDataSet.customerTinJoin, this.customerid);
                DataTable dt = taxDBDataSetcustomerTinJoinTableAdapter.GetDataByCId(this.customerid);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    char[] cname = dr["cname"].ToString().ToUpper().ToCharArray();
                    char[] fname = dr["fname"].ToString().ToUpper().ToCharArray();
                    char[] mname = dr["mname"].ToString().ToUpper().ToCharArray();
                    string dobtemp = DateTime.Parse(dr["date_of_birth"].ToString()).ToString("ddMMyyyy").ToUpper();
                    if (dobtemp == "01011900")
                    {
                        dobtemp = "";
                    }
                    char[] dob = dobtemp.ToCharArray();
                    char[] sname = dr["sname"].ToString().ToUpper().ToCharArray();
                    
                    string nat = dr["nat"].ToString();

                    char[] natan = dr["natan"].ToString().ToUpper().ToCharArray();
                    char[] natbn = dr["natbn"].ToString().ToUpper().ToCharArray();
                    char[] natcn = dr["natcn"].ToString().ToUpper().ToCharArray();
                    char[] natdn = dr["natdn"].ToString().ToUpper().ToCharArray();
                    char[] naten = dr["naten"].ToString().ToUpper().ToCharArray();
                    char[] natfn = dr["natfn"].ToString().ToUpper().ToCharArray();
                    char[] natgn = dr["natgn"].ToString().ToUpper().ToCharArray();

                    char[] natat = dr["natat"].ToString().ToUpper().ToCharArray();
                    char[] natbt = dr["natbt"].ToString().ToUpper().ToCharArray();
                    char[] natct = dr["natct"].ToString().ToUpper().ToCharArray();
                    char[] natdt = dr["natdt"].ToString().ToUpper().ToCharArray();
                    char[] natet = dr["natet"].ToString().ToUpper().ToCharArray();
                    char[] natft = dr["natft"].ToString().ToUpper().ToCharArray();
                    char[] natgt = dr["natgt"].ToString().ToUpper().ToCharArray();

                    char[] ino = dr["ino"].ToString().ToUpper().ToCharArray();
                    string irdatetemp = DateTime.Parse(dr["date_of_birth"].ToString()).ToString("ddMMyyyy").ToUpper();
                    if (irdatetemp == "01011900")
                    {
                        irdatetemp = "";
                    }
                    char[] irdate = irdatetemp.ToCharArray();

                    char[] cad = dr["cad"].ToString().ToUpper().ToCharArray();
                    char[] cadd = dr["cadd"].ToString().ToUpper().ToCharArray();
                    char[] cadp = dr["cadp"].ToString().ToUpper().ToCharArray();

                    char[] tel = dr["phone_office"].ToString().ToUpper().ToCharArray();
                    char[] fax = dr["fax"].ToString().ToUpper().ToCharArray();
                    char[] email = dr["email"].ToString().ToUpper().ToCharArray();

                    char[] pad = dr["pad"].ToString().ToUpper().ToCharArray();
                    char[] padd = dr["padd"].ToString().ToUpper().ToCharArray();
                    char[] padp = dr["padp"].ToString().ToUpper().ToCharArray();

                    char[] oad = dr["oad"].ToString().ToUpper().ToCharArray();
                    char[] oadd = dr["oadd"].ToString().ToUpper().ToCharArray();
                    char[] oadp = dr["oadp"].ToString().ToUpper().ToCharArray();

                    char[] vra = dr["vra"].ToString().ToUpper().ToCharArray();
                    char[] vrb = dr["vrb"].ToString().ToUpper().ToCharArray();
                    char[] vrc = dr["vrc"].ToString().ToUpper().ToCharArray();
                    char[] vrd = dr["vrd"].ToString().ToUpper().ToCharArray();
                    char[] vre = dr["vre"].ToString().ToUpper().ToCharArray();

                    char[] challanno = dr["challanno"].ToString().ToUpper().ToCharArray();
                    string challandatetemp = DateTime.Parse(dr["challandate"].ToString()).ToString("ddMMyyyy").ToUpper();
                    if (challandatetemp == "01011900")
                    {
                        challandatetemp = "";
                    }
                    char[] challandate = challandatetemp.ToCharArray();
                    char[] bankname = dr["bankname"].ToString().ToUpper().ToCharArray();
                    char[] bankbrunch = dr["bankbrunch"].ToString().ToUpper().ToCharArray();

                    char[] natid = dr["natid"].ToString().ToUpper().ToCharArray();

                    // Print Customer Name
                    int j = 0;
                    for (int i = 0; i < 69; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < cname.Length)
                        {
                            c = cname[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameOfAssessee.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.nameOfAssessee.Inlines.Add(lb);
                        }
                    }

                    // Print Father's Name
                    j = 0;
                    for (int i = 0; i < 46; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < fname.Length)
                        {
                            c = fname[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.fatherName.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.fatherName.Inlines.Add(lb);
                        }
                    }

                    // Print Mother's Name
                    j = 0;
                    for (int i = 0; i < 46; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < mname.Length)
                        {
                            c = mname[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.motherName.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.motherName.Inlines.Add(lb);
                        }
                    }

                    // Print DOB
                    j = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < dob.Length)
                        {
                            c = dob[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.dob.Inlines.Add((UIElement)b);

                        if (j == 8)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.dob.Inlines.Add(lb);

                            Border b2 = new Border();
                            b2.Width = 40;
                            b2.Height = 20;
                            TextBlock tb2 = new TextBlock();
                            tb2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            tb2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            tb2.FontWeight = FontWeights.Bold;
                            tb2.Text = "Day";

                            b2.Child = tb2;
                            this.dob.Inlines.Add((UIElement)b2);

                            Border b3 = new Border();
                            b3.Width = 40;
                            b3.Height = 20;
                            TextBlock tb3 = new TextBlock();
                            tb3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            tb3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            tb3.FontWeight = FontWeights.Bold;
                            tb3.Text = "Month";

                            b3.Child = tb3;
                            this.dob.Inlines.Add((UIElement)b3);

                            Border b4 = new Border();
                            b4.Width = 80;
                            b4.Height = 20;
                            TextBlock tb4 = new TextBlock();
                            tb4.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            tb4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            tb4.FontWeight = FontWeights.Bold;
                            tb4.Text = "Year";

                            b4.Child = tb4;
                            this.dob.Inlines.Add((UIElement)b4);
                        }
                    }

                    // Print Spouce Name
                    j = 0;
                    for (int i = 0; i < 46; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < sname.Length)
                        {
                            c = sname[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.husbandName.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.husbandName.Inlines.Add(lb);
                        }
                    }

                    // Print Name and TIN

                    #region NATA

                    //NATAN
                    Border natanb = new Border();
                    natanb.Style = (Style)FindResource("char-box-first");
                    TextBlock natantb = new TextBlock();
                    natantb.Style = (Style)FindResource("char-box-char");
                    natantb.Text = "a";

                    natanb.Child = natantb;
                    this.nameandtin.Inlines.Add((UIElement)natanb);
                    
                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natan.Length)
                        {
                            c = natan[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATAT
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natat.Length)
                        {
                            c = natat[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natalb = new LineBreak();
                    this.nameandtin.Inlines.Add(natalb);

                    #endregion NATA

                    #region NATB

                    //NATBN
                    Border natbnb = new Border();
                    natbnb.Style = (Style)FindResource("char-box-first");
                    TextBlock natbntb = new TextBlock();
                    natbntb.Style = (Style)FindResource("char-box-char");
                    natbntb.Text = "b";

                    natbnb.Child = natbntb;
                    this.nameandtin.Inlines.Add((UIElement)natbnb);

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natbn.Length)
                        {
                            c = natbn[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATBT
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natbt.Length)
                        {
                            c = natbt[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natblb = new LineBreak();
                    this.nameandtin.Inlines.Add(natblb);

                    #endregion NATB

                    #region NATC

                    //NATCN
                    Border natcnb = new Border();
                    natcnb.Style = (Style)FindResource("char-box-first");
                    TextBlock natcntb = new TextBlock();
                    natcntb.Style = (Style)FindResource("char-box-char");
                    natcntb.Text = "c";

                    natcnb.Child = natcntb;
                    this.nameandtin.Inlines.Add((UIElement)natcnb);

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natcn.Length)
                        {
                            c = natcn[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATCT
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natct.Length)
                        {
                            c = natct[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natclb = new LineBreak();
                    this.nameandtin.Inlines.Add(natclb);

                    #endregion NATC

                    #region NATD

                    //NATDN
                    Border natdnb = new Border();
                    natdnb.Style = (Style)FindResource("char-box-first");
                    TextBlock natdntb = new TextBlock();
                    natdntb.Style = (Style)FindResource("char-box-char");
                    natdntb.Text = "d";

                    natdnb.Child = natdntb;
                    this.nameandtin.Inlines.Add((UIElement)natdnb);

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natdn.Length)
                        {
                            c = natdn[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATDT
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natdt.Length)
                        {
                            c = natdt[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natdlb = new LineBreak();
                    this.nameandtin.Inlines.Add(natdlb);

                    #endregion NATD

                    #region NATE

                    //NATEN
                    Border natenb = new Border();
                    natenb.Style = (Style)FindResource("char-box-first");
                    TextBlock natentb = new TextBlock();
                    natentb.Style = (Style)FindResource("char-box-char");
                    natentb.Text = "e";

                    natenb.Child = natentb;
                    this.nameandtin.Inlines.Add((UIElement)natenb);

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < naten.Length)
                        {
                            c = naten[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATET
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natet.Length)
                        {
                            c = natet[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natelb = new LineBreak();
                    this.nameandtin.Inlines.Add(natelb);

                    #endregion NATE

                    #region NATF

                    //NATFN
                    Border natfnb = new Border();
                    natfnb.Style = (Style)FindResource("char-box-first");
                    TextBlock natfntb = new TextBlock();
                    natfntb.Style = (Style)FindResource("char-box-char");
                    natfntb.Text = "f";

                    natfnb.Child = natfntb;
                    this.nameandtin.Inlines.Add((UIElement)natfnb);

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natfn.Length)
                        {
                            c = natfn[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATFT
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natft.Length)
                        {
                            c = natft[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natflb = new LineBreak();
                    this.nameandtin.Inlines.Add(natflb);

                    #endregion NATF

                    #region NATG

                    //NATGN
                    Border natgnb = new Border();
                    natgnb.Style = (Style)FindResource("char-box-first");
                    TextBlock natgntb = new TextBlock();
                    natgntb.Style = (Style)FindResource("char-box-char");
                    natgntb.Text = "g";

                    natgnb.Child = natgntb;
                    this.nameandtin.Inlines.Add((UIElement)natgnb);

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natgn.Length)
                        {
                            c = natgn[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }
                    //NATGT
                    for (int i = 0; i < 12; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < natgt.Length)
                        {
                            c = natgt[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameandtin.Inlines.Add((UIElement)b);
                    }

                    LineBreak natglb = new LineBreak();
                    this.nameandtin.Inlines.Add(natglb);

                    #endregion NATG

                    #region incorporation

                    // Print Incorporation No
                    j = 0;
                    for (int i = 0; i < 46; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < ino.Length)
                        {
                            c = ino[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.incno.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.incno.Inlines.Add(lb);
                        }
                    }

                    // Print Inc Date
                    j = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < irdate.Length)
                        {
                            c = irdate[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.incdate.Inlines.Add((UIElement)b);

                        if (j == 8)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.incdate.Inlines.Add(lb);

                            Border b2 = new Border();
                            b2.Width = 40;
                            b2.Height = 20;
                            TextBlock tb2 = new TextBlock();
                            tb2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            tb2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            tb2.FontWeight = FontWeights.Bold;
                            tb2.Text = "Day";

                            b2.Child = tb2;
                            this.incdate.Inlines.Add((UIElement)b2);

                            Border b3 = new Border();
                            b3.Width = 40;
                            b3.Height = 20;
                            TextBlock tb3 = new TextBlock();
                            tb3.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            tb3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            tb3.FontWeight = FontWeights.Bold;
                            tb3.Text = "Month";

                            b3.Child = tb3;
                            this.incdate.Inlines.Add((UIElement)b3);

                            Border b4 = new Border();
                            b4.Width = 80;
                            b4.Height = 20;
                            TextBlock tb4 = new TextBlock();
                            tb4.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                            tb4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                            tb4.FontWeight = FontWeights.Bold;
                            tb4.Text = "Year";

                            b4.Child = tb4;
                            this.incdate.Inlines.Add((UIElement)b4);
                        }
                    }

                    #endregion incorporation

                    #region currentaddress
                    // Print Current Address
                    j = 0;
                    for (int i = 0; i < 69; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < cad.Length)
                        {
                            c = cad[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.currentAddress.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.currentAddress.Inlines.Add(lb);
                        }
                    }

                    LineBreak cad1lb = new LineBreak();
                    this.currentAddress.Inlines.Add(cad1lb);
                    LineBreak cad2lb = new LineBreak();
                    this.currentAddress.Inlines.Add(cad2lb);

                    //District

                    for (int i = 0; i < 18; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < cadd.Length)
                        {
                            c = cadd[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.currentAddress.Inlines.Add((UIElement)b);
                    }

                    Border cadb1 = new Border();
                    cadb1.Width = 20;
                    cadb1.Height = 20;
                    this.currentAddress.Inlines.Add((UIElement)cadb1);

                    //Post code

                    for (int i = 0; i < 4; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < cadp.Length)
                        {
                            c = cadp[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.currentAddress.Inlines.Add((UIElement)b);
                    }

                    LineBreak cad3lb = new LineBreak();
                    this.currentAddress.Inlines.Add(cad3lb);

                    Border caddb = new Border();
                    caddb.Width = 380;
                    caddb.Height = 20;
                    TextBlock caddt = new TextBlock();
                    caddt.Text = "District";
                    caddb.Child = caddt;

                    Border cadpb = new Border();
                    cadpb.Width = 80;
                    cadpb.Height = 20;
                    TextBlock cadpt = new TextBlock();
                    cadpt.Text = "Post Code";
                    cadpb.Child = cadpt;

                    this.currentAddress.Inlines.Add((UIElement)caddb);
                    this.currentAddress.Inlines.Add((UIElement)cadpb);

                    #endregion currentaddress

                    #region Telephone

                    //Telephone

                    for (int i = 0; i < 11; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < tel.Length)
                        {
                            c = tel[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.telephonenum.Inlines.Add((UIElement)b);
                    }

                    Border telb = new Border();
                    telb.Width = 40;
                    telb.Height = 20;

                    #endregion Telephone

                    #region FAX

                    //FAX

                    for (int i = 0; i < 10; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < fax.Length)
                        {
                            c = fax[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.faxnum.Inlines.Add((UIElement)b);
                    }

                    #endregion FAX

                    #region Email
                    //FAX

                    for (int i = 0; i < 23; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < email.Length)
                        {
                            c = email[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.email.Inlines.Add((UIElement)b);
                    }

                    #endregion Email

                    #region permanentaddress
                    // Print Permanent Address
                    j = 0;
                    for (int i = 0; i < 69; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < pad.Length)
                        {
                            c = pad[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.permanentAddress.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.permanentAddress.Inlines.Add(lb);
                        }
                    }

                    LineBreak pad1lb = new LineBreak();
                    this.permanentAddress.Inlines.Add(pad1lb);
                    LineBreak pad2lb = new LineBreak();
                    this.permanentAddress.Inlines.Add(pad2lb);

                    //District

                    for (int i = 0; i < 18; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < padd.Length)
                        {
                            c = padd[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.permanentAddress.Inlines.Add((UIElement)b);
                    }

                    Border padb1 = new Border();
                    padb1.Width = 20;
                    padb1.Height = 20;
                    this.permanentAddress.Inlines.Add((UIElement)padb1);

                    //Post code

                    for (int i = 0; i < 4; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < padp.Length)
                        {
                            c = padp[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.permanentAddress.Inlines.Add((UIElement)b);
                    }

                    LineBreak pad3lb = new LineBreak();
                    this.permanentAddress.Inlines.Add(pad3lb);

                    Border paddb = new Border();
                    paddb.Width = 380;
                    paddb.Height = 20;
                    TextBlock paddt = new TextBlock();
                    paddt.Text = "District";
                    paddb.Child = paddt;

                    Border padpb = new Border();
                    padpb.Width = 80;
                    padpb.Height = 20;
                    TextBlock padpt = new TextBlock();
                    padpt.Text = "Post Code";
                    padpb.Child = padpt;

                    this.permanentAddress.Inlines.Add((UIElement)paddb);
                    this.permanentAddress.Inlines.Add((UIElement)padpb);

                    #endregion permanentaddress

                    #region otheraddress
                    // Print Other Address
                    j = 0;
                    for (int i = 0; i < 69; i++)
                    {
                        j++;
                        string rn = "char-box";
                        if (j > 0)
                        {
                            rn = "char-box-first";
                        }

                        char c = ' ';
                        if (i < oad.Length)
                        {
                            c = oad[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.otherAddress.Inlines.Add((UIElement)b);

                        if (j == 23)
                        {
                            j = 0;
                            LineBreak lb = new LineBreak();
                            this.otherAddress.Inlines.Add(lb);
                        }
                    }

                    LineBreak oad1lb = new LineBreak();
                    this.otherAddress.Inlines.Add(oad1lb);
                    LineBreak oad2lb = new LineBreak();
                    this.otherAddress.Inlines.Add(oad2lb);

                    //District

                    for (int i = 0; i < 18; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < oadd.Length)
                        {
                            c = oadd[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.otherAddress.Inlines.Add((UIElement)b);
                    }

                    Border oadb1 = new Border();
                    oadb1.Width = 20;
                    oadb1.Height = 20;
                    this.otherAddress.Inlines.Add((UIElement)oadb1);

                    //Post code

                    for (int i = 0; i < 4; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < oadp.Length)
                        {
                            c = oadp[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.otherAddress.Inlines.Add((UIElement)b);
                    }

                    LineBreak oad3lb = new LineBreak();
                    this.otherAddress.Inlines.Add(oad3lb);

                    Border oaddb = new Border();
                    oaddb.Width = 380;
                    oaddb.Height = 20;
                    TextBlock oaddt = new TextBlock();
                    oaddt.Text = "District";
                    oaddb.Child = oaddt;

                    Border oadpb = new Border();
                    oadpb.Width = 80;
                    oadpb.Height = 20;
                    TextBlock oadpt = new TextBlock();
                    oadpt.Text = "Post Code";
                    oadpb.Child = oadpt;

                    this.otherAddress.Inlines.Add((UIElement)oaddb);
                    this.otherAddress.Inlines.Add((UIElement)oadpb);

                    #endregion otheraddress

                    #region VAT reg no

                    //A
                    for (int i = 0; i < 11; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < vra.Length)
                        {
                            c = vra[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.vata.Inlines.Add((UIElement)b);
                    }

                    //B
                    for (int i = 0; i < 11; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < vrb.Length)
                        {
                            c = vrb[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.vatb.Inlines.Add((UIElement)b);
                    }

                    //C
                    for (int i = 0; i < 11; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < vrc.Length)
                        {
                            c = vrc[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.vatc.Inlines.Add((UIElement)b);
                    }

                    //D
                    for (int i = 0; i < 11; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < vrd.Length)
                        {
                            c = vrd[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.vatd.Inlines.Add((UIElement)b);
                    }

                    //E
                    for (int i = 0; i < 11; i++)
                    {
                        string rn = "char-box-first";

                        char c = ' ';
                        if (i < vre.Length)
                        {
                            c = vre[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.vate.Inlines.Add((UIElement)b);
                    }

                    #endregion VAT reg no

                    #region challan

                    for (int i = 0; i < 16; i++)
                    {
                        string rn = "char-box-small";

                        char c = ' ';
                        if (i < challanno.Length)
                        {
                            c = challanno[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.challanno.Inlines.Add((UIElement)b);
                    }

                    Border challanb = new Border();
                    challanb.Width = 18.4;
                    challanb.Height = 20;
                    this.challanno.Inlines.Add((UIElement)challanb);

                    for (int i = 0; i < 8; i++)
                    {
                        string rn = "char-box-small";

                        char c = ' ';
                        if (i < challandate.Length)
                        {
                            c = challandate[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.challandate.Inlines.Add((UIElement)b);
                    }

                    for (int i = 0; i < 25; i++)
                    {
                        string rn = "char-box-small";

                        char c = ' ';
                        if (i < bankname.Length)
                        {
                            c = bankname[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameofbank.Inlines.Add((UIElement)b);
                    }

                    for (int i = 0; i < 25; i++)
                    {
                        string rn = "char-box-small";

                        char c = ' ';
                        if (i < bankbrunch.Length)
                        {
                            c = bankbrunch[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nameofbranch.Inlines.Add((UIElement)b);
                    }

                    #endregion challan

                    #region natid

                    for (int i = 0; i < 13; i++)
                    {
                        string rn = "char-box-small";

                        char c = ' ';
                        if (i < natid.Length)
                        {
                            c = natid[i];
                        }

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        TextBlock tb = new TextBlock();
                        tb.Style = (Style)FindResource("char-box-char");
                        tb.Text = c.ToString();

                        b.Child = tb;
                        this.nationalid.Inlines.Add((UIElement)b);
                    }

                    #endregion natid

                    #region misc

                    for (int i = 0; i < 20; i++)
                    {
                        string rn = "char-box-first";

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        this.zone.Inlines.Add((UIElement)b);
                    }

                    for (int i = 0; i < 20; i++)
                    {
                        string rn = "char-box-first";

                        Border b = new Border();
                        b.Style = (Style)FindResource(rn);
                        this.circle.Inlines.Add((UIElement)b);
                    }

                    #endregion misc

                    #region background
                    //ImageBrush imb = new ImageBrush();
                    //imb.ImageSource = new BitmapImage(
                    //    new Uri(@"component/Images/demo.png", UriKind.Relative)
                    //    );
                    //imb.Stretch = Stretch.None;
                    //imb.TileMode = TileMode.Tile;
                    
                    //this.printabletinapp.Background = imb;
                    #endregion background
                }
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.printabletinapp.PagePadding = new Thickness(96);
                //ApplicationCommands.PrintPreview.Execute("TIN Application", (IInputElement)this.printabletinapp);
                ApplicationCommands.Print.Execute("Application for TIN", (IInputElement)this.printabletinapp);
            }
            catch
            {
            }
        }
    }
}
