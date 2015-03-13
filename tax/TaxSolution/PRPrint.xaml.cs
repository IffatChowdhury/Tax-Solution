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
using System.IO;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Printing;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Globalization;
using System.Diagnostics;


namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for PRPrint.xaml
    /// </summary>
    public partial class PRPrint : Window
    {
        private int returnid;
        private int customerid;
        double width = 8.5;
        double height = 13;
        string mydoc;

        public PRPrint(int returnid)
        {
            InitializeComponent();
            this.returnid = returnid;
            this.customerid = 0;
            mydoc = System.Environment.SpecialFolder.MyDocuments.ToString() + "temp.xps";
            //populatedata();
        }

        public PRPrint()
        {
            InitializeComponent();
            this.returnid = 2;
            this.customerid = 0;
            mydoc = System.Environment.SpecialFolder.MyDocuments.ToString() + "temp.xps";
            //populatedata();
        }

        private void populatedata()
        {
            this.printablepr.ColumnWidth = width * 96;
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxDBDataSetTableAdapters.customerTableAdapter();
            DataTable dt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                
                this.customerid = int.Parse(dr["customerid"].ToString());

                if (this.customerid > 0)
                {
                    DataTable cdt = taxDBDataSetcustomerTableAdapter.GetDataById(this.customerid);
                    if (cdt.Rows.Count > 0)
                    {
                        DataRow cdr = cdt.Rows[0];

                        TextBlock ticktext = new TextBlock();
                        ticktext.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        ticktext.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        ticktext.Text = "✔";

                        #region page 1

                        #region return type
                        string returntype = dr["returntype"].ToString();
                        if (returntype == "SELF")
                        {
                            this.prtypeself.Text += "  ✔";
                        }
                        else if (returntype == "UNIVERSAL SELF")
                        {
                            this.prtypeuniversal.Text += "  ✔";
                        }
                        else
                        {
                            this.prtypenormal.Text = "  ✔";
                        }
                        #endregion

                        #region assessee name

                        this.PR1.Inlines.Add(cdr["customer_name"].ToString());
                        this.PR1.BorderBrush = Brushes.Black;
                        this.PR1.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region national id

                        this.PR2.Inlines.Add(cdr["national_id"].ToString());
                        this.PR2.BorderBrush = Brushes.Black;
                        this.PR2.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region utin

                        char[] utin = cdr["utin"].ToString().ToCharArray();
                        if (utin.Length == 12)
                        {
                            foreach (char c in utin)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-pr-1");
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.PR3.Inlines.Add((UIElement)b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-pr-1");
                                if (i == 3 || i == 7)
                                {
                                    TextBlock tb = new TextBlock();
                                    tb.Style = (Style)FindResource("char-box-char");
                                    tb.Text = "-";
                                    b.Child = tb;
                                }
                                this.PR3.Inlines.Add((UIElement)b);
                            }
                        }

                        #endregion

                        #region tin

                        char[] tin = cdr["tin"].ToString().ToCharArray();
                        if (tin.Length == 12)
                        {
                            foreach (char c in tin)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-pr-1");
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.PR4.Inlines.Add((UIElement)b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-pr-1");
                                if (i == 3 || i == 7)
                                {
                                    TextBlock tb = new TextBlock();
                                    tb.Style = (Style)FindResource("char-box-char");
                                    tb.Text = "-";
                                    b.Child = tb;
                                }
                                this.PR4.Inlines.Add((UIElement)b);
                            }
                        }

                        #endregion

                        #region circle

                        this.PR5a.Inlines.Add(dr["circle"].ToString());
                        this.PR5a.BorderBrush = Brushes.Black;
                        this.PR5a.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region zone

                        this.PR5b.Inlines.Add(dr["zone"].ToString());
                        this.PR5b.BorderBrush = Brushes.Black;
                        this.PR5b.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region assessment year

                        this.PR6.Inlines.Add(dr["assessmentyear"].ToString());
                        this.PR6.BorderBrush = Brushes.Black;
                        this.PR6.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region residential status

                        string residentialstatus = dr["residentialstatus"].ToString();

                        if (residentialstatus == "RESIDENT")
                        {
                            TextBlock tb = ticktext;
                            this.PR7a.Child = tb;
                        }
                        else
                        {
                            TextBlock tb = ticktext;
                            this.PR7b.Child = tb;
                        }

                        #endregion

                        #region assessee status

                        string assesseestatus = dr["assesseestatus"].ToString();

                        if (assesseestatus == "I")
                        {
                            TextBlock tbs = new TextBlock();
                            tbs.HorizontalAlignment = ticktext.HorizontalAlignment;
                            tbs.VerticalAlignment = ticktext.VerticalAlignment;
                            tbs.Text = ticktext.Text;
                            this.PR8a.Child = tbs;
                        }
                        else if (assesseestatus == "F")
                        {
                            TextBlock tbs = new TextBlock();
                            tbs.HorizontalAlignment = ticktext.HorizontalAlignment;
                            tbs.VerticalAlignment = ticktext.VerticalAlignment;
                            tbs.Text = ticktext.Text;
                            this.PR8b.Child = tbs;
                        }
                        else if (assesseestatus == "A")
                        {
                            TextBlock tbs = new TextBlock();
                            tbs.HorizontalAlignment = ticktext.HorizontalAlignment;
                            tbs.VerticalAlignment = ticktext.VerticalAlignment;
                            tbs.Text = ticktext.Text;
                            this.PR8c.Child = tbs;
                        }
                        else
                        {
                            TextBlock tbs = new TextBlock();
                            tbs.HorizontalAlignment = ticktext.HorizontalAlignment;
                            tbs.VerticalAlignment = ticktext.VerticalAlignment;
                            tbs.Text = ticktext.Text;
                            this.PR8d.Child = tbs;
                        }

                        #endregion

                        #region employer name

                        this.PR9.Inlines.Add(dr["employer"].ToString());
                        this.PR9.BorderBrush = Brushes.Black;
                        this.PR9.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region spouce name

                        this.PR10.Inlines.Add(cdr["spouce_name"].ToString());
                        this.PR10.BorderBrush = Brushes.Black;
                        this.PR10.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region father name

                        this.PR11.Inlines.Add(cdr["father_name"].ToString());
                        this.PR11.BorderBrush = Brushes.Black;
                        this.PR11.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region mother name

                        this.PR12.Inlines.Add(cdr["mother_name"].ToString());
                        this.PR12.BorderBrush = Brushes.Black;
                        this.PR12.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region dob

                        string dob = DateTime.Parse(cdr["date_of_birth"].ToString()).ToString("ddMMyyyy");
                        
                        char[] doba = dob.ToCharArray();
                        if (dob.Length == 8 && dob != "01011900")
                        {
                            int i = 0;
                            foreach (char c in dob)
                            {
                                i++;
                                Border b = new Border();
                                if (i > 2 && i < 5)
                                {
                                    b.Style = (Style)FindResource("char-box-pr-2");
                                }
                                else
                                {
                                    b.Style = (Style)FindResource("char-box-pr-1");
                                }
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.PR13.Inlines.Add((UIElement)b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-pr-1");
                                if (i == 2 || i == 3)
                                {
                                    b.Style = (Style)FindResource("char-box-pr-2");
                                }
                                this.PR13.Inlines.Add((UIElement)b);
                            }
                        }

                        TextBlock dtb = new TextBlock();
                        dtb.Width = 60;
                        dtb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        dtb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        dtb.TextAlignment = TextAlignment.Center;
                        dtb.Text = "Day";

                        TextBlock mtb = new TextBlock();
                        mtb.Width = 60;
                        mtb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        mtb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        mtb.TextAlignment = TextAlignment.Center;
                        mtb.Text = "Month";

                        TextBlock ytb = new TextBlock();
                        ytb.Width = 120;
                        ytb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        ytb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        ytb.TextAlignment = TextAlignment.Center;
                        ytb.Text = "Year";

                        this.PR13b.Inlines.Add((UIElement)dtb);
                        this.PR13b.Inlines.Add((UIElement)mtb);
                        this.PR13b.Inlines.Add((UIElement)ytb);

                        #endregion

                        #region present address

                        TextBlock atb = new TextBlock();
                        atb.TextWrapping = TextWrapping.Wrap;
                        atb.Text = cdr["address1"].ToString();
                        atb.Height = 40;
                        atb.TextWrapping = TextWrapping.Wrap;
                        atb.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        this.PR14a.Inlines.Add(atb);
                        this.PR14a.BorderBrush = Brushes.Black;
                        this.PR14a.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region permanent address

                        TextBlock atb2 = new TextBlock();
                        atb2.TextWrapping = TextWrapping.Wrap;
                        atb2.Text = cdr["address2"].ToString();
                        atb2.Height = 40;
                        atb2.TextWrapping = TextWrapping.Wrap;
                        atb2.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        this.PR14b.Inlines.Add(atb2);
                        this.PR14b.BorderBrush = Brushes.Black;
                        this.PR14b.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region telephone office

                        this.PR15a.Inlines.Add(cdr["phone_office"].ToString());
                        this.PR15a.BorderBrush = Brushes.Black;
                        this.PR15a.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region telephone residence

                        this.PR15b.Inlines.Add(cdr["phone_residence"].ToString());
                        this.PR15b.BorderBrush = Brushes.Black;
                        this.PR15b.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #region vat reg no

                        this.PR16.Inlines.Add(cdr["vat_reg_no"].ToString());
                        this.PR16.BorderBrush = Brushes.Black;
                        this.PR16.BorderThickness = new Thickness(0, 0, 0, 1);

                        #endregion

                        #endregion

                        #region page 2

                        #region date
                        try
                        {
                            this.page2date.Inlines.Add(DateTime.Parse(dr["incomeyear"].ToString()).ToString("dd-MMM-yyyy"));
                            this.page2date.MinWidth = 80;
                            this.page2date.TextAlignment = TextAlignment.Center;
                        }
                        catch
                        {
                        }
                        #endregion

                        PRIncomeOfAssets prioa = new PRIncomeOfAssets(this.returnid);

                        #region salaries
                        this.P21.Inlines.Add(String.Format("{0:#,0.##}", prioa.calculatesoi1()));
                        this.P21.TextAlignment = TextAlignment.Right;
                        this.P21.MinWidth = 100;

                        #endregion

                        #region securities

                        this.P22.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi2"].ToString())));
                        this.P22.TextAlignment = TextAlignment.Right;
                        this.P22.MinWidth = 100;

                        #endregion

                        #region house

                        this.P23.Inlines.Add(String.Format("{0:#,0.##}", prioa.calculatesoi3()));
                        this.P23.TextAlignment = TextAlignment.Right;
                        this.P23.MinWidth = 100;

                        #endregion

                        #region agri

                        this.P24.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi4"].ToString())));
                        this.P24.TextAlignment = TextAlignment.Right;
                        this.P24.MinWidth = 100;

                        #endregion

                        #region business

                        this.P25.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi5"].ToString())));
                        this.P25.TextAlignment = TextAlignment.Right;
                        this.P25.MinWidth = 100;

                        #endregion

                        #region share

                        this.P26.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi6"].ToString())));
                        this.P26.TextAlignment = TextAlignment.Right;
                        this.P26.MinWidth = 100;

                        #endregion

                        #region spouce income

                        this.P27.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi7"].ToString())));
                        this.P27.TextAlignment = TextAlignment.Right;
                        this.P27.MinWidth = 100;

                        #endregion

                        #region capital

                        this.P28.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi8"].ToString())));
                        this.P28.TextAlignment = TextAlignment.Right;
                        this.P28.MinWidth = 100;

                        #endregion

                        #region other

                        this.P29.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi9"].ToString())));
                        this.P29.TextAlignment = TextAlignment.Right;
                        this.P29.MinWidth = 100;

                        #endregion

                        #region total

                        this.P210.Inlines.Add(String.Format("{0:#,0.##}", prioa.calculatesoi10()));
                        this.P210.TextAlignment = TextAlignment.Right;
                        this.P210.MinWidth = 100;

                        #endregion

                        #region foreign

                        this.P211.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi11"].ToString())));
                        this.P211.TextAlignment = TextAlignment.Right;
                        this.P211.MinWidth = 100;

                        #endregion

                        #region total income

                        this.P212.Inlines.Add(String.Format("{0:#,0.##}", prioa.calculatesoi12()));
                        this.P212.TextAlignment = TextAlignment.Right;
                        this.P212.MinWidth = 100;

                        #endregion

                        #region leviable tax

                        this.P213.Inlines.Add(String.Format("{0:#,0.##}", prioa.calculatesoi13()));
                        this.P213.TextAlignment = TextAlignment.Right;
                        this.P213.MinWidth = 100;

                        #endregion

                        #region rebate

                        this.P214.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi14"].ToString())));
                        this.P214.TextAlignment = TextAlignment.Right;
                        this.P214.MinWidth = 100;

                        #endregion

                        #region payable tax
                        decimal soi15 = prioa.calculatesoi15();

                        this.P215.Inlines.Add(String.Format("{0:#,0.##}", soi15));
                        this.P215.TextAlignment = TextAlignment.Right;
                        this.P215.MinWidth = 100;
                        
                        #endregion

                        #region soi16

                        decimal soi16 = 0;

                        #region 16a

                        this.p216a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi16a"].ToString())));
                        this.p216a.TextAlignment = TextAlignment.Right;
                        try
                        {
                            soi16 += decimal.Parse(dr["soi16a"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region 16b

                        this.p216b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi16b"].ToString())));
                        this.p216b.TextAlignment = TextAlignment.Right;
                        try
                        {
                            soi16 += decimal.Parse(dr["soi16b"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region 16c

                        this.p216c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi16c"].ToString())));
                        this.p216c.TextAlignment = TextAlignment.Right;
                        try
                        {
                            soi16 += decimal.Parse(dr["soi16c"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region 16d

                        this.p216d.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi16d"].ToString())));
                        this.p216d.TextAlignment = TextAlignment.Right;
                        try
                        {
                            soi16 += decimal.Parse(dr["soi16d"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        this.P216.Inlines.Add(String.Format("{0:#,0.##}", soi16));
                        this.P216.TextAlignment = TextAlignment.Right;
                        this.P216.MinWidth = 100;

                        #endregion

                        #region soi17

                        this.P217.Inlines.Add(String.Format("{0:#,0.##}", (soi15 - soi16)));
                        this.P217.TextAlignment = TextAlignment.Right;
                        this.P217.MinWidth = 100;

                        #endregion

                        #region soi18

                        this.P218.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi18"].ToString())));
                        this.P218.TextAlignment = TextAlignment.Right;
                        this.P218.MinWidth = 100;

                        #endregion

                        #region soi19

                        this.P219.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi19"].ToString())));
                        this.P219.TextAlignment = TextAlignment.Right;
                        this.P219.MinWidth = 100;

                        #endregion

                        #region verification

                        this.P2name.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.P2namesign.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        string fh = "";
                        fh = cdr["father_name"].ToString();
                        if (fh.Length < 1)
                        {
                            fh = cdr["spouce_name"].ToString();
                        }
                        string p2t = cdr["tin"].ToString();
                        if (p2t.Length != 12)
                        {
                            p2t = cdr["utin"].ToString();
                        }
                        this.P2father.Inlines.Add(" " + fh + " ");
                        this.P2TIN.Inlines.Add(" " + p2t + " ");

                        #endregion

                        #endregion

                        #region page 3

                        #region top name and tin

                        this.P3name.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.P3name.TextAlignment = TextAlignment.Center;
                        this.P3name.MinWidth = 80;

                        if (tin.Length == 12)
                        {
                            foreach (char c in tin)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.p3tin.Children.Add(b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                if (i == 3 || i == 7)
                                {
                                    TextBlock tb = new TextBlock();
                                    tb.Style = (Style)FindResource("char-box-char");
                                    tb.Text = "-";
                                    b.Child = tb;
                                }
                                this.p3tin.Children.Add(b);
                            }
                        }
                        this.p3tin.Margin = new Thickness(5, 0, 0, 0);

                        #endregion

                        decimal salarya = 0;
                        decimal salaryb = 0;
                        decimal salaryc = 0;

                        #region salary

                        #region s1

                        this.p31a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary1a"].ToString())));
                        this.p31a.TextAlignment = TextAlignment.Right;
                        this.p31a.MinWidth = 80;
                        this.p31a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary1a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p31b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary1b"].ToString())));
                        this.p31b.TextAlignment = TextAlignment.Right;
                        this.p31b.MinWidth = 80;
                        this.p31b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary1b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary1c = 0;
                        try
                        {
                            salary1c = Math.Max(0, decimal.Parse(dr["salary1a"].ToString()) - decimal.Parse(dr["salary1b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p31c.Inlines.Add(String.Format("{0:#,0.##}", salary1c));
                        this.p31c.TextAlignment = TextAlignment.Right;
                        this.p31c.MinWidth = 80;
                        this.p31c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary1c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s2

                        this.p32a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary2a"].ToString())));
                        this.p32a.TextAlignment = TextAlignment.Right;
                        this.p32a.MinWidth = 80;
                        this.p32a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary2a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p32b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary2b"].ToString())));
                        this.p32b.TextAlignment = TextAlignment.Right;
                        this.p32b.MinWidth = 80;
                        this.p32b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary2b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary2c = 0;
                        try
                        {
                            salary2c = Math.Max(0, decimal.Parse(dr["salary2a"].ToString()) - decimal.Parse(dr["salary2b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p32c.Inlines.Add(String.Format("{0:#,0.##}", salary2c));
                        this.p32c.TextAlignment = TextAlignment.Right;
                        this.p32c.MinWidth = 80;
                        this.p32c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary2c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s3

                        this.p33a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary3a"].ToString())));
                        this.p33a.TextAlignment = TextAlignment.Right;
                        this.p33a.MinWidth = 80;
                        this.p33a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary3a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p33b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary3b"].ToString())));
                        this.p33b.TextAlignment = TextAlignment.Right;
                        this.p33b.MinWidth = 80;
                        this.p33b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary3b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary3c = 0;
                        try
                        {
                            salary3c = Math.Max(0, decimal.Parse(dr["salary3a"].ToString()) - decimal.Parse(dr["salary3b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p33c.Inlines.Add(String.Format("{0:#,0.##}", salary3c));
                        this.p33c.TextAlignment = TextAlignment.Right;
                        this.p33c.MinWidth = 80;
                        this.p33c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary3c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s4

                        this.p34a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary4a"].ToString())));
                        this.p34a.TextAlignment = TextAlignment.Right;
                        this.p34a.MinWidth = 80;
                        this.p34a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary4a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p34b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary4b"].ToString())));
                        this.p34b.TextAlignment = TextAlignment.Right;
                        this.p34b.MinWidth = 80;
                        this.p34b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary4b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary4c = 0;
                        try
                        {
                            salary4c = Math.Max(0, decimal.Parse(dr["salary4a"].ToString()) - decimal.Parse(dr["salary4b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p34c.Inlines.Add(String.Format("{0:#,0.##}", salary4c));
                        this.p34c.TextAlignment = TextAlignment.Right;
                        this.p34c.MinWidth = 80;
                        this.p34c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary4c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s5

                        this.p35a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary5a"].ToString())));
                        this.p35a.TextAlignment = TextAlignment.Right;
                        this.p35a.MinWidth = 80;
                        this.p35a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary5a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p35b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary5b"].ToString())));
                        this.p35b.TextAlignment = TextAlignment.Right;
                        this.p35b.MinWidth = 80;
                        this.p35b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary5b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary5c = 0;
                        try
                        {
                            salary5c = Math.Max(0, decimal.Parse(dr["salary5a"].ToString()) - decimal.Parse(dr["salary5b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p35c.Inlines.Add(String.Format("{0:#,0.##}", salary5c));
                        this.p35c.TextAlignment = TextAlignment.Right;
                        this.p35c.MinWidth = 80;
                        this.p35c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary5c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s6

                        this.p36a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary6a"].ToString())));
                        this.p36a.TextAlignment = TextAlignment.Right;
                        this.p36a.MinWidth = 80;
                        this.p36a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary6a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p36b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary6b"].ToString())));
                        this.p36b.TextAlignment = TextAlignment.Right;
                        this.p36b.MinWidth = 80;
                        this.p36b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary6b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary6c = 0;
                        try
                        {
                            salary6c = Math.Max(0, decimal.Parse(dr["salary6a"].ToString()) - decimal.Parse(dr["salary6b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p36c.Inlines.Add(String.Format("{0:#,0.##}", salary6c));
                        this.p36c.TextAlignment = TextAlignment.Right;
                        this.p36c.MinWidth = 80;
                        this.p36c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary6c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s7

                        this.p37a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary7a"].ToString())));
                        this.p37a.TextAlignment = TextAlignment.Right;
                        this.p37a.MinWidth = 80;
                        this.p37a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary7a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p37b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary7b"].ToString())));
                        this.p37b.TextAlignment = TextAlignment.Right;
                        this.p37b.MinWidth = 80;
                        this.p37b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary7b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary7c = 0;
                        try
                        {
                            salary7c = Math.Max(0, decimal.Parse(dr["salary7a"].ToString()) - decimal.Parse(dr["salary7b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p37c.Inlines.Add(String.Format("{0:#,0.##}", salary7c));
                        this.p37c.TextAlignment = TextAlignment.Right;
                        this.p37c.MinWidth = 80;
                        this.p37c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary7c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s8

                        this.p38a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary8a"].ToString())));
                        this.p38a.TextAlignment = TextAlignment.Right;
                        this.p38a.MinWidth = 80;
                        this.p38a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary8a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p38b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary8b"].ToString())));
                        this.p38b.TextAlignment = TextAlignment.Right;
                        this.p38b.MinWidth = 80;
                        this.p38b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary8b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary8c = 0;
                        try
                        {
                            salary8c = Math.Max(0, decimal.Parse(dr["salary8a"].ToString()) - decimal.Parse(dr["salary8b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p38c.Inlines.Add(String.Format("{0:#,0.##}", salary8c));
                        this.p38c.TextAlignment = TextAlignment.Right;
                        this.p38c.MinWidth = 80;
                        this.p38c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary8c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s9

                        this.p39a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary9a"].ToString())));
                        this.p39a.TextAlignment = TextAlignment.Right;
                        this.p39a.MinWidth = 80;
                        this.p39a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary9a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p39b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary9b"].ToString())));
                        this.p39b.TextAlignment = TextAlignment.Right;
                        this.p39b.MinWidth = 80;
                        this.p39b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary9b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary9c = 0;
                        try
                        {
                            salary9c = Math.Max(0, decimal.Parse(dr["salary9a"].ToString()) - decimal.Parse(dr["salary9b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p39c.Inlines.Add(String.Format("{0:#,0.##}", salary9c));
                        this.p39c.TextAlignment = TextAlignment.Right;
                        this.p39c.MinWidth = 80;
                        this.p39c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary9c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s10

                        this.p310a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary10a"].ToString())));
                        this.p310a.TextAlignment = TextAlignment.Right;
                        this.p310a.MinWidth = 80;
                        this.p310a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary10a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p310b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary10b"].ToString())));
                        this.p310b.TextAlignment = TextAlignment.Right;
                        this.p310b.MinWidth = 80;
                        this.p310b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary10b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary10c = 0;
                        try
                        {
                            salary10c = Math.Max(0, decimal.Parse(dr["salary10a"].ToString()) - decimal.Parse(dr["salary10b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p310c.Inlines.Add(String.Format("{0:#,0.##}", salary10c));
                        this.p310c.TextAlignment = TextAlignment.Right;
                        this.p310c.MinWidth = 80;
                        this.p310c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary10c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s11

                        this.p311a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary11a"].ToString())));
                        this.p311a.TextAlignment = TextAlignment.Right;
                        this.p311a.MinWidth = 80;
                        this.p311a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary11a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p311b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary11b"].ToString())));
                        this.p311b.TextAlignment = TextAlignment.Right;
                        this.p311b.MinWidth = 80;
                        this.p311b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary11b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary11c = 0;
                        try
                        {
                            salary11c = Math.Max(0, decimal.Parse(dr["salary11a"].ToString()) - decimal.Parse(dr["salary11b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p311c.Inlines.Add(String.Format("{0:#,0.##}", salary11c));
                        this.p311c.TextAlignment = TextAlignment.Right;
                        this.p311c.MinWidth = 80;
                        this.p311c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary11c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s12

                        this.p312a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary12a"].ToString())));
                        this.p312a.TextAlignment = TextAlignment.Right;
                        this.p312a.MinWidth = 80;
                        this.p312a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary12a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p312b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary12b"].ToString())));
                        this.p312b.TextAlignment = TextAlignment.Right;
                        this.p312b.MinWidth = 80;
                        this.p312b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary12b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary12c = 0;
                        try
                        {
                            salary12c = Math.Max(0, decimal.Parse(dr["salary12a"].ToString()) - decimal.Parse(dr["salary12b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p312c.Inlines.Add(String.Format("{0:#,0.##}", salary12c));
                        this.p312c.TextAlignment = TextAlignment.Right;
                        this.p312c.MinWidth = 80;
                        this.p312c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary12c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s13

                        this.p313a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary13a"].ToString())));
                        this.p313a.TextAlignment = TextAlignment.Right;
                        this.p313a.MinWidth = 80;
                        this.p313a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary13a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p313b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary13b"].ToString())));
                        this.p313b.TextAlignment = TextAlignment.Right;
                        this.p313b.MinWidth = 80;
                        this.p313b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary13b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary13c = 0;
                        try
                        {
                            salary13c = Math.Max(0, decimal.Parse(dr["salary13a"].ToString()) - decimal.Parse(dr["salary13b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p313c.Inlines.Add(String.Format("{0:#,0.##}", salary13c));
                        this.p313c.TextAlignment = TextAlignment.Right;
                        this.p313c.MinWidth = 80;
                        this.p313c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary13c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s14

                        this.p314a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary14a"].ToString())));
                        this.p314a.TextAlignment = TextAlignment.Right;
                        this.p314a.MinWidth = 80;
                        this.p314a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary14a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p314b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary14b"].ToString())));
                        this.p314b.TextAlignment = TextAlignment.Right;
                        this.p314b.MinWidth = 80;
                        this.p314b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary14b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary14c = 0;
                        try
                        {
                            salary14c = Math.Max(0, decimal.Parse(dr["salary14a"].ToString()) - decimal.Parse(dr["salary14b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p314c.Inlines.Add(String.Format("{0:#,0.##}", salary14c));
                        this.p314c.TextAlignment = TextAlignment.Right;
                        this.p314c.MinWidth = 80;
                        this.p314c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary14c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s15

                        this.p315a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary15a"].ToString())));
                        this.p315a.TextAlignment = TextAlignment.Right;
                        this.p315a.MinWidth = 80;
                        this.p315a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary15a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p315b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary15b"].ToString())));
                        this.p315b.TextAlignment = TextAlignment.Right;
                        this.p315b.MinWidth = 80;
                        this.p315b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary15b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary15c = 0;
                        try
                        {
                            salary15c = Math.Max(0, decimal.Parse(dr["salary15a"].ToString()) - decimal.Parse(dr["salary15b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p315c.Inlines.Add(String.Format("{0:#,0.##}", salary15c));
                        this.p315c.TextAlignment = TextAlignment.Right;
                        this.p315c.MinWidth = 80;
                        this.p315c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary15c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s16

                        this.p316a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary16a"].ToString())));
                        this.p316a.TextAlignment = TextAlignment.Right;
                        this.p316a.MinWidth = 80;
                        this.p316a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary16a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p316b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary16b"].ToString())));
                        this.p316b.TextAlignment = TextAlignment.Right;
                        this.p316b.MinWidth = 80;
                        this.p316b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary16b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary16c = 0;
                        try
                        {
                            salary16c = Math.Max(0, decimal.Parse(dr["salary16a"].ToString()) - decimal.Parse(dr["salary16b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p316c.Inlines.Add(String.Format("{0:#,0.##}", salary16c));
                        this.p316c.TextAlignment = TextAlignment.Right;
                        this.p316c.MinWidth = 80;
                        this.p316c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary16c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s17

                        this.p317a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary17a"].ToString())));
                        this.p317a.TextAlignment = TextAlignment.Right;
                        this.p317a.MinWidth = 80;
                        this.p317a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary17a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p317b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary17b"].ToString())));
                        this.p317b.TextAlignment = TextAlignment.Right;
                        this.p317b.MinWidth = 80;
                        this.p317b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary17b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary17c = 0;
                        try
                        {
                            salary17c = Math.Max(0, decimal.Parse(dr["salary17a"].ToString()) - decimal.Parse(dr["salary17b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p317c.Inlines.Add(String.Format("{0:#,0.##}", salary17c));
                        this.p317c.TextAlignment = TextAlignment.Right;
                        this.p317c.MinWidth = 80;
                        this.p317c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary17c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s18

                        this.p318a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary18a"].ToString())));
                        this.p318a.TextAlignment = TextAlignment.Right;
                        this.p318a.MinWidth = 80;
                        this.p318a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary18a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p318b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary18b"].ToString())));
                        this.p318b.TextAlignment = TextAlignment.Right;
                        this.p318b.MinWidth = 80;
                        this.p318b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary18b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary18c = 0;
                        try
                        {
                            salary18c = Math.Max(0, decimal.Parse(dr["salary18a"].ToString()) - decimal.Parse(dr["salary18b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p318c.Inlines.Add(String.Format("{0:#,0.##}", salary18c));
                        this.p318c.TextAlignment = TextAlignment.Right;
                        this.p318c.MinWidth = 80;
                        this.p318c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary18c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s19

                        this.p319a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary19a"].ToString())));
                        this.p319a.TextAlignment = TextAlignment.Right;
                        this.p319a.MinWidth = 80;
                        this.p319a.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salarya += decimal.Parse(dr["salary19a"].ToString());
                        }
                        catch
                        {
                        }

                        this.p319b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["salary19b"].ToString())));
                        this.p319b.TextAlignment = TextAlignment.Right;
                        this.p319b.MinWidth = 80;
                        this.p319b.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryb += decimal.Parse(dr["salary19b"].ToString());
                        }
                        catch
                        {
                        }

                        decimal salary19c = 0;
                        try
                        {
                            salary19c = Math.Max(0, decimal.Parse(dr["salary19a"].ToString()) - decimal.Parse(dr["salary19b"].ToString()));
                        }
                        catch
                        {
                        }
                        this.p319c.Inlines.Add(String.Format("{0:#,0.##}", salary19c));
                        this.p319c.TextAlignment = TextAlignment.Right;
                        this.p319c.MinWidth = 80;
                        this.p319c.TextWrapping = TextWrapping.Wrap;
                        try
                        {
                            salaryc += salary19c;
                        }
                        catch
                        {
                        }

                        #endregion

                        #region s20

                        this.p320a.Inlines.Add(String.Format("{0:#,0.##}", salarya));
                        this.p320a.TextAlignment = TextAlignment.Right;
                        this.p320a.MinWidth = 80;
                        this.p320a.TextWrapping = TextWrapping.Wrap;

                        this.p320b.Inlines.Add(String.Format("{0:#,0.##}", salaryb));
                        this.p320b.TextAlignment = TextAlignment.Right;
                        this.p320b.MinWidth = 80;
                        this.p320b.TextWrapping = TextWrapping.Wrap;

                        this.p320c.Inlines.Add(String.Format("{0:#,0.##}", salaryc));
                        this.p320c.TextAlignment = TextAlignment.Right;
                        this.p320c.MinWidth = 80;
                        this.p320c.TextWrapping = TextWrapping.Wrap;

                        #endregion

                        #endregion

                        #region house property

                        #region location

                        this.P3S2Location.Inlines.Add(dr["houselocation"].ToString());
                        this.P3S2Location.TextWrapping = TextWrapping.Wrap;
                        this.P3S2Location.TextAlignment = TextAlignment.Left;

                        #endregion

                        #region house1
                        decimal house1total = 0;

                        this.P3S21.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house1"].ToString())));
                        this.P3S21.TextAlignment = TextAlignment.Right;
                        this.P3S21.MinWidth = 50;

                        try
                        {
                            house1total += decimal.Parse(dr["house1"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region house2

                        decimal house2total = 0;

                        #region house2a
                        this.P3S22a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2a"].ToString())));
                        this.P3S22a.TextAlignment = TextAlignment.Right;
                        this.P3S22a.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2a"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2b
                        this.P3S22b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2b"].ToString())));
                        this.P3S22b.TextAlignment = TextAlignment.Right;
                        this.P3S22b.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2b"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2c
                        this.P3S22c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2c"].ToString())));
                        this.P3S22c.TextAlignment = TextAlignment.Right;
                        this.P3S22c.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2c"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2d
                        this.P3S22d.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2d"].ToString())));
                        this.P3S22d.TextAlignment = TextAlignment.Right;
                        this.P3S22d.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2d"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2e
                        this.P3S22e.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2e"].ToString())));
                        this.P3S22e.TextAlignment = TextAlignment.Right;
                        this.P3S22e.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2e"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2f
                        this.P3S22f.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2f"].ToString())));
                        this.P3S22f.TextAlignment = TextAlignment.Right;
                        this.P3S22f.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2f"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2g
                        this.P3S22g.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["house2g"].ToString())));
                        this.P3S22g.TextAlignment = TextAlignment.Right;
                        this.P3S22g.MinWidth = 50;

                        try
                        {
                            house2total += decimal.Parse(dr["house2g"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion

                        #region house2total
                        this.P2S22t.Inlines.Add(house2total.ToString());
                        this.P2S22t.TextAlignment = TextAlignment.Right;
                        this.P2S22t.MinWidth = 50;
                        #endregion

                        #endregion

                        #region house3

                        decimal house3total = 0;
                        try
                        {
                            house3total = Math.Max(0, (house1total - house2total));
                        }
                        catch
                        {
                        }
                        this.P2S23.Inlines.Add(String.Format("{0:#,0.##}", house3total));
                        this.P2S23.TextAlignment = TextAlignment.Right;
                        this.P2S23.MinWidth = 50;

                        #endregion

                        #endregion

                        #endregion

                        #region page 4

                        #region s3

                        decimal totalinvestment = 0;

                        #region i1
                        Border itbb1 = new Border();
                        itbb1.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb1.BorderBrush = Brushes.Black;
                        itbb1.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb1 = new TextBlock();
                        itb1.MinWidth = 80;
                        itb1.TextAlignment = TextAlignment.Right;
                        itb1.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment1"].ToString())));

                        itbb1.Child = itb1;

                        this.PR41.Inlines.Add((UIElement)itbb1);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment1"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i2
                        Border itbb2 = new Border();
                        itbb2.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb2.BorderBrush = Brushes.Black;
                        itbb2.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb2 = new TextBlock();
                        itb2.MinWidth = 80;
                        itb2.TextAlignment = TextAlignment.Right;
                        itb2.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment2"].ToString())));

                        itbb2.Child = itb2;

                        this.PR42.Inlines.Add((UIElement)itbb2);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment2"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i3
                        Border itbb3 = new Border();
                        itbb3.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb3.BorderBrush = Brushes.Black;
                        itbb3.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb3 = new TextBlock();
                        itb3.MinWidth = 80;
                        itb3.TextAlignment = TextAlignment.Right;
                        itb3.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment3"].ToString())));

                        itbb3.Child = itb3;

                        this.PR43.Inlines.Add((UIElement)itbb3);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment3"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i4
                        Border itbb4 = new Border();
                        itbb4.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb4.BorderBrush = Brushes.Black;
                        itbb4.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb4 = new TextBlock();
                        itb4.MinWidth = 80;
                        itb4.TextAlignment = TextAlignment.Right;
                        itb4.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment4"].ToString())));

                        itbb4.Child = itb4;

                        this.PR44.Inlines.Add((UIElement)itbb4);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment4"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i5
                        Border itbb5 = new Border();
                        itbb5.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb5.BorderBrush = Brushes.Black;
                        itbb5.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb5 = new TextBlock();
                        itb5.MinWidth = 80;
                        itb5.TextAlignment = TextAlignment.Right;
                        itb5.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment5"].ToString())));

                        itbb5.Child = itb5;

                        this.PR45.Inlines.Add((UIElement)itbb5);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment5"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i6
                        Border itbb6 = new Border();
                        itbb6.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb6.BorderBrush = Brushes.Black;
                        itbb6.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb6 = new TextBlock();
                        itb6.MinWidth = 80;
                        itb6.TextAlignment = TextAlignment.Right;
                        itb6.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment6"].ToString())));

                        itbb6.Child = itb6;

                        this.PR46.Inlines.Add((UIElement)itbb6);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment6"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i7
                        Border itbb7 = new Border();
                        itbb7.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb7.BorderBrush = Brushes.Black;
                        itbb7.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb7 = new TextBlock();
                        itb7.MinWidth = 80;
                        itb7.TextAlignment = TextAlignment.Right;
                        itb7.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment7"].ToString())));

                        itbb7.Child = itb7;

                        this.PR47.Inlines.Add((UIElement)itbb7);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment7"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i8
                        Border itbb8 = new Border();
                        itbb8.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb8.BorderBrush = Brushes.Black;
                        itbb8.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb8 = new TextBlock();
                        itb8.MinWidth = 80;
                        itb8.TextAlignment = TextAlignment.Right;
                        itb8.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment8"].ToString())));

                        itbb8.Child = itb8;

                        this.PR48.Inlines.Add((UIElement)itbb8);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment8"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i9
                        Border itbb9 = new Border();
                        itbb9.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb9.BorderBrush = Brushes.Black;
                        itbb9.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb9 = new TextBlock();
                        itb9.MinWidth = 80;
                        itb9.TextAlignment = TextAlignment.Right;
                        itb9.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment9"].ToString())));
                        
                        itbb9.Child = itb9;
                        
                        this.PR49.Inlines.Add((UIElement)itbb9);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment9"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region i10
                        Border itbb10 = new Border();
                        itbb10.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbb10.BorderBrush = Brushes.Black;
                        itbb10.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itb10 = new TextBlock();
                        itb10.MinWidth = 80;
                        itb10.TextAlignment = TextAlignment.Right;
                        itb10.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["investment10"].ToString())));

                        itbb10.Child = itb10;

                        this.PR410.Inlines.Add((UIElement)itbb10);
                        try
                        {
                            totalinvestment += decimal.Parse(dr["investment10"].ToString());
                        }
                        catch
                        {
                        }
                        #endregion
                        #region it
                        Border itbbt = new Border();
                        itbbt.BorderThickness = new Thickness(0, 0, 0, 1);
                        itbbt.BorderBrush = Brushes.Black;
                        itbbt.Margin = new Thickness(5, 0, 0, 0);

                        TextBlock itbt = new TextBlock();
                        itbt.MinWidth = 80;
                        itbt.TextAlignment = TextAlignment.Right;
                        itbt.Inlines.Add(String.Format("{0:#,0.##}", totalinvestment));

                        itbbt.Child = itbt;

                        this.PR4t.Inlines.Add((UIElement)itbbt);
                        #endregion

                        #endregion

                        #region document
                        
                        TaxSolution.TaxDBDataSetTableAdapters.prdocumentsTableAdapter taxDBDataSetprdocumentsTableAdapter = new TaxDBDataSetTableAdapters.prdocumentsTableAdapter();
                        DataTable ddt = taxDBDataSetprdocumentsTableAdapter.GetDataByReturn(this.returnid);

                        if (ddt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow ddr in ddt.Rows)
                            {
                                if (count < 16)
                                {
                                    Border dnb = new Border();
                                    dnb.Padding = new Thickness(3);

                                    TextBlock dntb = new TextBlock();
                                    dntb.Text = "#" + count.ToString() + ".";
                                    dnb.Child = dntb;

                                    TextBlock dnametb = new TextBlock();
                                    dnametb.Margin = new Thickness(3);
                                    dnametb.Text = ddr["document"].ToString();

                                    WrapPanel dnwp = new WrapPanel();
                                    dnwp.Name = "DocWP" + count.ToString();

                                    dnwp.Children.Add(dnb);
                                    dnwp.Children.Add(dnametb);

                                    this.PR4listofdocu.Children.Add(dnwp);
                                }
                                count++;
                            }
                        }
                        #endregion

                        #endregion

                        #region page 5

                        #region top name, date and tin

                        this.P5name.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.P5name.TextAlignment = TextAlignment.Center;
                        this.P5name.MinWidth = 80;

                        if (tin.Length == 12)
                        {
                            foreach (char c in tin)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.p5tin.Children.Add(b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                if (i == 3 || i == 7)
                                {
                                    TextBlock tb = new TextBlock();
                                    tb.Style = (Style)FindResource("char-box-char");
                                    tb.Text = "-";
                                    b.Child = tb;
                                }
                                this.p5tin.Children.Add(b);
                            }
                        }

                        try{
                            this.PR5date.Inlines.Add(DateTime.Parse(dr["incomeyear"].ToString()).ToString("dd-MMM-yyyy"));
                            this.PR5date.MinWidth = 80;
                            this.PR5date.TextAlignment = TextAlignment.Center;
                        }
                        catch
                        {
                        }

                        #endregion

                        decimal totalasset = 0;

                        #region 1-a

                        this.PR51a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset1a"].ToString())));
                        this.PR51a.MinWidth = 80;
                        this.PR51a.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalasset += decimal.Parse(dr["asset1a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region 1-b

                        decimal totalshare = 0;

                        #region share

                        TaxSolution.TaxDBDataSetTableAdapters.prdirectorsshareTableAdapter taxDBDataSetprdirectorsshareTableAdapter = new TaxDBDataSetTableAdapters.prdirectorsshareTableAdapter();
                        DataTable dsdt = taxDBDataSetprdirectorsshareTableAdapter.GetDataByReturn(this.returnid);

                        if (dsdt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow dsdr in dsdt.Rows)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                dsp1.FontSize = 11;
                                dsp1.Inlines.Add(dsdr["companyname"].ToString());
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp2 = new Paragraph();
                                dsp2.FontSize = 11;
                                dsp2.Inlines.Add(dsdr["numberofshare"].ToString());
                                TableCell dsc2 = new TableCell(dsp2);

                                Paragraph dsp3 = new Paragraph();
                                dsp3.FontSize = 11;
                                
                                dsp3.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dsdr["amount"].ToString())));
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc2);
                                dstr.Cells.Add(dsc3);
                                if (count < 3)
                                {
                                    this.P51b.RowGroups[0].Rows.Add(dstr);
                                }
                                try
                                {
                                    totalshare += decimal.Parse(dsdr["amount"].ToString());
                                }
                                catch
                                {
                                }

                                count++;
                            }
                            if (count < 2)
                            {
                                for (int ci = count; ci <= 2; ci++)
                                {
                                    TableRow dstr = new TableRow();

                                    Paragraph dsp1 = new Paragraph();
                                    
                                    TableCell dsc1 = new TableCell(dsp1);

                                    Paragraph dsp2 = new Paragraph();
                                    
                                    TableCell dsc2 = new TableCell(dsp2);

                                    Paragraph dsp3 = new Paragraph();
                                    
                                    TableCell dsc3 = new TableCell(dsp3);

                                    dstr.Cells.Add(dsc1);
                                    dstr.Cells.Add(dsc2);
                                    dstr.Cells.Add(dsc3);

                                    this.P51b.RowGroups[0].Rows.Add(dstr);
                                }
                            }
                        }
                        else
                        {
                            for (int ci = 1; ci <= 2; ci++)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp2 = new Paragraph();
                                
                                TableCell dsc2 = new TableCell(dsp2);

                                Paragraph dsp3 = new Paragraph();
                                
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc2);
                                dstr.Cells.Add(dsc3);

                                this.P51b.RowGroups[0].Rows.Add(dstr);
                            }
                        }

                        #endregion

                        this.PR51b.Inlines.Add(String.Format("{0:#,0.##}", totalshare));
                        this.PR51b.MinWidth = 80;
                        this.PR51b.TextAlignment = TextAlignment.Right;

                        totalasset += totalshare;

                        #endregion

                        #region 2

                        decimal totalnonag = 0;

                        #region list

                        TaxSolution.TaxDBDataSetTableAdapters.prnonagpropertyTableAdapter taxDBDataSetprnonagpropertyTableAdapter = new TaxDBDataSetTableAdapters.prnonagpropertyTableAdapter();
                        DataTable napdt = taxDBDataSetprnonagpropertyTableAdapter.GetDataByReturn(this.returnid);

                        if (napdt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow dsdr in napdt.Rows)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                dsp1.Inlines.Add(dsdr["description"].ToString());
                                dsp1.FontSize = 11;
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                dsp3.FontSize = 11;
                                dsp3.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dsdr["amount"].ToString())));
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);
                                if (count < 3)
                                {
                                    this.P52.RowGroups[0].Rows.Add(dstr);
                                }
                                try
                                {
                                    totalnonag += decimal.Parse(dsdr["amount"].ToString());
                                }
                                catch
                                {
                                }

                                count++;
                            }
                            if (count < 2)
                            {
                                for (int ci = count; ci <= 2; ci++)
                                {
                                    TableRow dstr = new TableRow();

                                    Paragraph dsp1 = new Paragraph();
                                    
                                    TableCell dsc1 = new TableCell(dsp1);

                                    Paragraph dsp3 = new Paragraph();
                                    
                                    TableCell dsc3 = new TableCell(dsp3);

                                    dstr.Cells.Add(dsc1);
                                    dstr.Cells.Add(dsc3);

                                    this.P52.RowGroups[0].Rows.Add(dstr);
                                }
                            }
                        }
                        else
                        {
                            for (int ci = 1; ci <= 2; ci++)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);

                                this.P52.RowGroups[0].Rows.Add(dstr);
                            }
                        }

                        #endregion

                        this.PR52.Inlines.Add(String.Format("{0:#,0.##}", totalnonag));
                        this.PR52.MinWidth = 80;
                        this.PR52.TextAlignment = TextAlignment.Right;

                        totalasset += totalnonag;

                        #endregion

                        #region 3

                        decimal totalag = 0;

                        #region list

                        TaxSolution.TaxDBDataSetTableAdapters.pragpropertyTableAdapter taxDBDataSetpragpropertyTableAdapter = new TaxDBDataSetTableAdapters.pragpropertyTableAdapter();
                        DataTable apdt = taxDBDataSetpragpropertyTableAdapter.GetDataByReturn(this.returnid);

                        if (apdt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow dsdr in apdt.Rows)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                dsp1.FontSize = 11;
                                dsp1.Inlines.Add(dsdr["description"].ToString());
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                dsp3.FontSize = 11;
                                
                                dsp3.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dsdr["amount"].ToString())));
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);
                                if (count < 3)
                                {
                                    this.P53.RowGroups[0].Rows.Add(dstr);
                                }

                                try
                                {
                                    totalag += decimal.Parse(dsdr["amount"].ToString());
                                }
                                catch
                                {
                                }

                                count++;
                            }
                            if (count < 2)
                            {
                                for (int ci = count; ci <= 2; ci++)
                                {
                                    TableRow dstr = new TableRow();

                                    Paragraph dsp1 = new Paragraph();
                                    
                                    TableCell dsc1 = new TableCell(dsp1);

                                    Paragraph dsp3 = new Paragraph();
                                    
                                    TableCell dsc3 = new TableCell(dsp3);

                                    dstr.Cells.Add(dsc1);
                                    dstr.Cells.Add(dsc3);

                                    this.P53.RowGroups[0].Rows.Add(dstr);
                                }
                            }
                        }
                        else
                        {
                            for (int ci = 1; ci <= 2; ci++)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);

                                this.P53.RowGroups[0].Rows.Add(dstr);
                            }
                        }

                        #endregion

                        this.PR53.Inlines.Add(String.Format("{0:#,0.##}", totalnonag));
                        this.PR53.MinWidth = 80;
                        this.PR53.TextAlignment = TextAlignment.Right;

                        totalasset += totalag;

                        #endregion

                        #region 4
                        decimal totalinv = 0;

                        this.PR54a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset4a"].ToString())));
                        this.PR54a.MinWidth = 80;
                        this.PR54a.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalinv += decimal.Parse(dr["asset4a"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR54b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset4b"].ToString())));
                        this.PR54b.MinWidth = 80;
                        this.PR54b.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalinv += decimal.Parse(dr["asset4b"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR54c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset4c"].ToString())));
                        this.PR54c.MinWidth = 80;
                        this.PR54c.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalinv += decimal.Parse(dr["asset4c"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR54d.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset4d"].ToString())));
                        this.PR54d.MinWidth = 80;
                        this.PR54d.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalinv += decimal.Parse(dr["asset4d"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR54e.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset4e"].ToString())));
                        this.PR54e.MinWidth = 80;
                        this.PR54e.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalinv += decimal.Parse(dr["asset4e"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR54total.Inlines.Add(String.Format("{0:#,0.##}", totalinv));
                        this.PR54total.MinWidth = 80;
                        this.PR54total.TextAlignment = TextAlignment.Right;

                        totalasset += totalinv;

                        #endregion

                        #region 5

                        decimal totalmotor = 0;

                        #region list

                        TaxSolution.TaxDBDataSetTableAdapters.prmotorvehicleTableAdapter taxDBDataSetprmotorvehicleTableAdapter = new TaxDBDataSetTableAdapters.prmotorvehicleTableAdapter();
                        DataTable mvdt = taxDBDataSetprmotorvehicleTableAdapter.GetDataByReturn(this.returnid);

                        if (mvdt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow dsdr in mvdt.Rows)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                dsp1.Inlines.Add(dsdr["type"].ToString());
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                dsp3.Inlines.Add(String.Format("{0:#,0.##}", dsdr["amount"].ToString()));
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);
                                if (count < 3)
                                {
                                    this.P55.RowGroups[0].Rows.Add(dstr);
                                }
                                try
                                {
                                    totalmotor += decimal.Parse(dsdr["amount"].ToString());
                                }
                                catch
                                {
                                }

                                count++;
                            }
                            if (count < 2)
                            {
                                for (int ci = count; ci <= 2; ci++)
                                {
                                    TableRow dstr = new TableRow();

                                    Paragraph dsp1 = new Paragraph();
                                    
                                    TableCell dsc1 = new TableCell(dsp1);

                                    Paragraph dsp3 = new Paragraph();
                                    
                                    TableCell dsc3 = new TableCell(dsp3);

                                    dstr.Cells.Add(dsc1);
                                    dstr.Cells.Add(dsc3);

                                    this.P55.RowGroups[0].Rows.Add(dstr);
                                }
                            }
                        }
                        else
                        {
                            for (int ci = 1; ci <= 2; ci++)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);

                                this.P55.RowGroups[0].Rows.Add(dstr);
                            }
                        }

                        #endregion

                        this.PR55.Inlines.Add(String.Format("{0:#,0.##}", totalmotor));
                        this.PR55.MinWidth = 80;
                        this.PR55.TextAlignment = TextAlignment.Right;

                        totalasset += totalmotor;

                        #endregion

                        #region 6
                        decimal totaljw = 0;

                        TaxSolution.TaxDBDataSetTableAdapters.prjewelleryTableAdapter taxDBDataSetprjewelleryTableAdapter = new TaxDBDataSetTableAdapters.prjewelleryTableAdapter();
                        DataTable jdt = taxDBDataSetprjewelleryTableAdapter.GetDataByReturn(this.returnid);

                        if (jdt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow dsdr in jdt.Rows)
                            {
                                this.P56description.Inlines.Add(dsdr["description"].ToString() + " ");

                                try
                                {
                                    totaljw += decimal.Parse(dsdr["amount"].ToString());
                                }
                                catch
                                {
                                }

                                count++;
                            }
                        }

                        this.PR56.Inlines.Add(String.Format("{0:#,0.##}", totaljw));
                        this.PR56.MinWidth = 80;
                        this.PR56.TextAlignment = TextAlignment.Right;

                        totalasset += totaljw;

                        #endregion

                        #region 7

                        this.PR57.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset7"].ToString())));
                        this.PR57.MinWidth = 80;
                        this.PR57.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalasset += decimal.Parse(dr["asset7"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region 8

                        this.PR58.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset8"].ToString())));
                        this.PR58.MinWidth = 80;
                        this.PR58.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalasset += decimal.Parse(dr["asset8"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region 9
                        decimal totalca = 0;

                        this.PR59a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset9a"].ToString())));
                        this.PR59a.MinWidth = 80;
                        this.PR59a.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalca += decimal.Parse(dr["asset9a"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR59b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset9b"].ToString())));
                        this.PR59b.MinWidth = 80;
                        this.PR59b.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalca += decimal.Parse(dr["asset9b"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR59c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset9c"].ToString())));
                        this.PR59c.MinWidth = 80;
                        this.PR59c.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalca += decimal.Parse(dr["asset9c"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR59total.Inlines.Add(String.Format("{0:#,0.##}", totalca));
                        this.PR59total.MinWidth = 80;
                        this.PR59total.TextAlignment = TextAlignment.Right;

                        totalasset += totalca;

                        #endregion

                        #endregion

                        #region page 6

                        #region bf

                        this.PR6bf.Inlines.Add(String.Format("{0:#,0.##}", totalasset));
                        this.PR6bf.MinWidth = 80;
                        this.PR6bf.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 10

                        decimal totalother = 0;

                        #region list

                        TaxSolution.TaxDBDataSetTableAdapters.protherTableAdapter taxDBDataSetprotherTableAdapter = new TaxDBDataSetTableAdapters.protherTableAdapter();
                        DataTable odt = taxDBDataSetprotherTableAdapter.GetDataByReturn(this.returnid);
                        
                        if (odt.Rows.Count > 0)
                        {
                            int count = 1;
                            foreach (DataRow dsdr in odt.Rows)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                dsp1.Inlines.Add(dsdr["details"].ToString());
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                dsp3.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dsdr["amount"].ToString())));
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);

                                if (count < 3)
                                {
                                    this.P610.RowGroups[0].Rows.Add(dstr);
                                }
                                try
                                {
                                    totalother += decimal.Parse(dsdr["amount"].ToString());
                                }
                                catch
                                {
                                }

                                count++;
                            }
                            if (count < 3)
                            {
                                for (int ci = count; ci <= 3; ci++)
                                {
                                    TableRow dstr = new TableRow();

                                    Paragraph dsp1 = new Paragraph();
                                    
                                    TableCell dsc1 = new TableCell(dsp1);

                                    Paragraph dsp3 = new Paragraph();
                                    
                                    TableCell dsc3 = new TableCell(dsp3);

                                    dstr.Cells.Add(dsc1);
                                    dstr.Cells.Add(dsc3);

                                    this.P610.RowGroups[0].Rows.Add(dstr);
                                }
                            }
                        }
                        else
                        {
                            for (int ci = 1; ci <= 3; ci++)
                            {
                                TableRow dstr = new TableRow();

                                Paragraph dsp1 = new Paragraph();
                                
                                TableCell dsc1 = new TableCell(dsp1);

                                Paragraph dsp3 = new Paragraph();
                                
                                TableCell dsc3 = new TableCell(dsp3);

                                dstr.Cells.Add(dsc1);
                                dstr.Cells.Add(dsc3);

                                this.P610.RowGroups[0].Rows.Add(dstr);
                            }
                        }

                        #endregion

                        this.PR610.Inlines.Add(String.Format("{0:#,0.##}", totalother));
                        this.PR610.MinWidth = 80;
                        this.PR610.TextAlignment = TextAlignment.Right;

                        totalasset += totalother;

                        #endregion

                        #region total asset

                        this.PR6totalassets.Inlines.Add(String.Format("{0:#,0.##}", totalasset));
                        this.PR6totalassets.MinWidth = 80;
                        this.PR6totalassets.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 11
                        decimal totalliability = 0;

                        this.PR611a.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset11a"].ToString())));
                        this.PR611a.MinWidth = 80;
                        this.PR611a.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalliability += decimal.Parse(dr["asset11a"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR611b.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset11b"].ToString())));
                        this.PR611b.MinWidth = 80;
                        this.PR611b.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalliability += decimal.Parse(dr["asset11b"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR611c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset11c"].ToString())));
                        this.PR611c.MinWidth = 80;
                        this.PR611c.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalliability += decimal.Parse(dr["asset11c"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR611d.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset11d"].ToString())));
                        this.PR611d.MinWidth = 80;
                        this.PR611d.TextAlignment = TextAlignment.Right;

                        try
                        {
                            totalliability += decimal.Parse(dr["asset11d"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR611totalliabilites.Inlines.Add(String.Format("{0:#,0.##}", totalca));
                        this.PR611totalliabilites.MinWidth = 80;
                        this.PR611totalliabilites.TextAlignment = TextAlignment.Right;

                        //totalasset += totalliability;

                        #endregion

                        #region 12

                        decimal netwelth = totalasset - totalliability;

                        this.PR612.Inlines.Add(String.Format("{0:#,0.##}", netwelth));
                        this.PR612.MinWidth = 80;
                        this.PR612.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 13

                        decimal netwelthlastyear = 0;
                        try
                        {
                            netwelthlastyear = decimal.Parse(dr["asset13"].ToString());
                        }
                        catch
                        {
                        }
                        this.PR613.Inlines.Add(String.Format("{0:#,0.##}", netwelthlastyear));
                        this.PR613.MinWidth = 80;
                        this.PR613.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 14

                        decimal accretion = netwelth - netwelthlastyear;

                        this.PR614.Inlines.Add(String.Format("{0:#,0.##}", accretion));
                        this.PR614.MinWidth = 80;
                        this.PR614.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 15a

                        decimal familyexp = 0;
                        try
                        {
                            familyexp = decimal.Parse(dr["asset15a"].ToString());
                        }
                        catch
                        {
                        }

                        this.PR615a.Inlines.Add(String.Format("{0:#,0.##}", familyexp));
                        this.PR615a.MinWidth = 80;
                        this.PR615a.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 15b

                        this.P615badult.Inlines.Add(String.Format("{0:0,0}", dr["asset15badult"].ToString()));
                        this.P615badult.MinWidth = 50;
                        this.P615badult.TextAlignment = TextAlignment.Center;

                        this.P615bchild.Inlines.Add(String.Format("{0:0,0}", dr["asset15bchild"].ToString()));
                        this.P615bchild.MinWidth = 50;
                        this.P615bchild.TextAlignment = TextAlignment.Center;

                        #endregion

                        #region 16

                        decimal totalaccretion = accretion + familyexp;

                        this.PR616.Inlines.Add(String.Format("{0:#,0.##}", totalaccretion));
                        this.PR616.MinWidth = 80;
                        this.PR616.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 17

                        decimal sof = 0;

                        decimal sofa = prioa.calculatesoi12();
                        decimal sofb = prioa.calculatesoi14();
                        try
                        {
                            sofb = decimal.Parse(dr["soi14"].ToString());
                        }
                        catch
                        {
                        }
                        decimal sofc = 0;
                        try
                        {
                            sofc = decimal.Parse(dr["asset17c"].ToString());
                        }
                        catch
                        {
                        }

                        sof = sofa + sofb + sofc;

                        this.PR617a.Inlines.Add(String.Format("{0:#,0.##}", sofa));
                        this.PR617a.MinWidth = 80;
                        this.PR617a.TextAlignment = TextAlignment.Right;

                        this.PR617b.Inlines.Add(String.Format("{0:#,0.##}", sofb));
                        this.PR617b.MinWidth = 80;
                        this.PR617b.TextAlignment = TextAlignment.Right;

                        this.PR617c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["asset17c"].ToString())));
                        this.PR617c.MinWidth = 80;
                        this.PR617c.TextAlignment = TextAlignment.Right;

                        this.PR617total.Inlines.Add(String.Format("{0:#,0.##}", sof));
                        this.PR617total.MinWidth = 80;
                        this.PR617total.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region 18

                        decimal diff = totalaccretion - sof;

                        this.PR618.Inlines.Add(String.Format("{0:#,0.##}", diff));
                        this.PR618.MinWidth = 80;
                        this.PR618.TextAlignment = TextAlignment.Right;

                        #endregion

                        #region botton name and date

                        this.PR6name.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.PR6name.TextAlignment = TextAlignment.Left;
                        this.PR6name.MinWidth = 80;

                        try
                        {
                            this.PR6date.Inlines.Add(DateTime.Parse(dr["incomeyear"].ToString()).ToString("dd-MMM-yyyy"));
                        }
                        catch
                        {
                        }

                        #endregion

                        #endregion

                        #region page 7

                        #region top name and tin

                        this.P7name.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.P7name.TextAlignment = TextAlignment.Center;
                        this.P7name.MinWidth = 80;

                        if (tin.Length == 12)
                        {
                            foreach (char c in tin)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.p7tin.Children.Add(b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                if (i == 3 || i == 7)
                                {
                                    TextBlock tb = new TextBlock();
                                    tb.Style = (Style)FindResource("char-box-char");
                                    tb.Text = "-";
                                    b.Child = tb;
                                }
                                this.p7tin.Children.Add(b);
                            }
                        }

                        #endregion

                        decimal p7total = 0;

                        #region p7-1

                        TextBlock p71tb = new TextBlock();
                        p71tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb1a"].ToString()));
                        p71tb.TextAlignment = TextAlignment.Right;
                        p71tb.MinWidth = 60;
                        this.PR71.Inlines.Add((UIElement)p71tb);

                        this.PR71c.Inlines.Add(dr["it10bb1b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb1a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-2

                        TextBlock p72tb = new TextBlock();
                        p72tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb2a"].ToString()));
                        p72tb.TextAlignment = TextAlignment.Right;
                        p72tb.MinWidth = 60;
                        this.PR72.Inlines.Add((UIElement)p72tb);

                        this.PR72c.Inlines.Add(dr["it10bb2b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb2a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-3

                        TextBlock p73tb = new TextBlock();
                        p73tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb3a"].ToString()));
                        p73tb.TextAlignment = TextAlignment.Right;
                        p73tb.MinWidth = 60;
                        this.PR73.Inlines.Add((UIElement)p73tb);

                        this.PR73c.Inlines.Add(dr["it10bb3b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb3a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-4

                        TextBlock p74tb = new TextBlock();
                        p74tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb4a"].ToString()));
                        p74tb.TextAlignment = TextAlignment.Right;
                        p74tb.MinWidth = 60;
                        this.PR74.Inlines.Add((UIElement)p74tb);

                        this.PR74c.Inlines.Add(dr["it10bb4b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb4a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-5

                        TextBlock p75tb = new TextBlock();
                        p75tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb5a"].ToString()));
                        p75tb.TextAlignment = TextAlignment.Right;
                        p75tb.MinWidth = 60;
                        this.PR75.Inlines.Add((UIElement)p75tb);

                        this.PR75c.Inlines.Add(dr["it10bb5b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb5a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-6

                        TextBlock p76tb = new TextBlock();
                        p76tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb6a"].ToString()));
                        p76tb.TextAlignment = TextAlignment.Right;
                        p76tb.MinWidth = 60;
                        this.PR76.Inlines.Add((UIElement)p76tb);

                        this.PR76c.Inlines.Add(dr["it10bb6b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb6a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-7

                        TextBlock p77tb = new TextBlock();
                        p77tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb7a"].ToString()));
                        p77tb.TextAlignment = TextAlignment.Right;
                        p77tb.MinWidth = 60;
                        this.PR77.Inlines.Add((UIElement)p77tb);

                        this.PR77c.Inlines.Add(dr["it10bb7b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb7a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-8

                        TextBlock p78tb = new TextBlock();
                        p78tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb8a"].ToString()));
                        p78tb.TextAlignment = TextAlignment.Right;
                        p78tb.MinWidth = 60;
                        this.PR78.Inlines.Add((UIElement)p78tb);

                        this.PR78c.Inlines.Add(dr["it10bb8b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb8a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-9

                        TextBlock p79tb = new TextBlock();
                        p79tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb9a"].ToString()));
                        p79tb.TextAlignment = TextAlignment.Right;
                        p79tb.MinWidth = 60;
                        this.PR79.Inlines.Add((UIElement)p79tb);

                        this.PR79c.Inlines.Add(dr["it10bb9b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb9a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-10

                        TextBlock p710tb = new TextBlock();
                        p710tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb10a"].ToString()));
                        p710tb.TextAlignment = TextAlignment.Right;
                        p710tb.MinWidth = 60;
                        this.PR710.Inlines.Add((UIElement)p710tb);

                        this.PR710c.Inlines.Add(dr["it10bb10b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb10a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-11

                        TextBlock p711tb = new TextBlock();
                        p711tb.Text = String.Format("{0:#,0.##}", decimal.Parse(dr["it10bb11a"].ToString()));
                        p711tb.TextAlignment = TextAlignment.Right;
                        p711tb.MinWidth = 60;
                        this.PR711.Inlines.Add((UIElement)p711tb);

                        this.PR711c.Inlines.Add(dr["it10bb11b"].ToString());

                        try
                        {
                            p7total += decimal.Parse(dr["it10bb11a"].ToString());
                        }
                        catch
                        {
                        }

                        #endregion

                        #region p7-total

                        TextBlock p712tb = new TextBlock();
                        p712tb.Text = String.Format("{0:#,0.##}", p7total);
                        p712tb.TextAlignment = TextAlignment.Right;
                        p712tb.MinWidth = 60;
                        this.PR7total.Inlines.Add((UIElement)p712tb);

                        #endregion

                        #region bottom name and date

                        this.P7name2.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.P7name2.TextAlignment = TextAlignment.Left;
                        this.P7name2.MinWidth = 80;

                        try
                        {
                            this.PR7date.Inlines.Add(DateTime.Parse(dr["incomeyear"].ToString()).ToString("dd-MMM-yyyy"));
                        }
                        catch
                        {
                        }

                        this.PR7ackname.Inlines.Add(" " + cdr["customer_name"].ToString() + " ");
                        this.PR7ackname.TextAlignment = TextAlignment.Center;
                        this.PR7ackname.MinWidth = 150;

                        this.PR7ackyear.Inlines.Add(dr["assessmentyear"].ToString());
                        this.PR7ackyear.TextAlignment = TextAlignment.Center;
                        this.PR7ackyear.MinWidth = 80;

                        if (tin.Length == 12)
                        {
                            foreach (char c in tin)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-high");
                                TextBlock tb = new TextBlock();
                                tb.Style = (Style)FindResource("char-box-char");
                                tb.Text = c.ToString();

                                b.Child = tb;
                                this.PR7TIN.Children.Add(b);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                Border b = new Border();
                                b.Style = (Style)FindResource("char-box-first");
                                if (i == 3 || i == 7)
                                {
                                    TextBlock tb = new TextBlock();
                                    tb.Style = (Style)FindResource("char-box-char");
                                    tb.Text = "-";
                                    b.Child = tb;
                                }
                                this.PR7TIN.Children.Add(b);
                            }
                        }

                        Border p7cb = new Border();
                        p7cb.BorderBrush = Brushes.Black;
                        p7cb.BorderThickness = new Thickness(0, 0, 0, 1);
                        p7cb.Width = 90;

                        TextBlock p7ctb = new TextBlock();
                        p7ctb.TextWrapping = TextWrapping.Wrap;
                        p7ctb.Text = dr["circle"].ToString();
                        p7ctb.TextAlignment = TextAlignment.Left;
                        p7cb.Child = p7ctb;

                        this.PR7CIRCLE.Inlines.Add((UIElement)p7cb);

                        Border p7zb = new Border();
                        p7zb.BorderBrush = Brushes.Black;
                        p7zb.BorderThickness = new Thickness(0, 0, 0, 1);
                        p7zb.Width = 90;

                        TextBlock p7ztb = new TextBlock();
                        p7ztb.TextWrapping = TextWrapping.Wrap;
                        p7ztb.Text = dr["zone"].ToString();
                        p7ztb.TextAlignment = TextAlignment.Left;
                        p7zb.Child = p7ztb;

                        this.PR7ZONE.Inlines.Add((UIElement)p7zb);

                        #endregion

                        #endregion

                        #region page 8

                        this.PR8totalincome.Inlines.Add(String.Format("{0:#,0.##}", prioa.calculatesoi12()));
                        this.PR8totalincome.MinWidth = 80;
                        this.PR8totalincome.TextAlignment = TextAlignment.Right;

                        this.PR8taxpaid.Inlines.Add(String.Format("{0:#,0.##}", soi16));
                        this.PR8taxpaid.MinWidth = 80;
                        this.PR8taxpaid.TextAlignment = TextAlignment.Right;

                        this.PR8netwealth.Inlines.Add(String.Format("{0:#,0.##}", netwelth));
                        this.PR8netwealth.MinWidth = 80;
                        this.PR8netwealth.TextAlignment = TextAlignment.Right;

                        this.PR8dateofreceipt.Inlines.Add(DateTime.Parse(dr["incomeyear"].ToString()).ToString("dd-MMM-yyyy"));
                        this.PR8dateofreceipt.MinWidth = 80;
                        this.PR8dateofreceipt.TextAlignment = TextAlignment.Center;

                        #region return type
                        
                        if (returntype == "SELF")
                        {
                            this.p8returntypeself.Text += "  ✔";
                        }
                        else if (returntype == "UNIVERSAL SELF")
                        {
                            this.p8returntypeuniself.Text += "  ✔";
                        }
                        else
                        {
                            this.p8returntypenormal.Text = "  ✔";
                        }
                        #endregion

                        #endregion
                    }
                }
            }
        }

        private void printBtn_Click(object sender, MouseEventArgs e)
        {
            try
            {
                //SaveAsXps(@mydoc, this.printablepr);

                XpsDocument xps = new XpsDocument(@mydoc, System.IO.FileAccess.Read);
                TSPrintPreview preview = new TSPrintPreview();
                preview.Owner = this.Owner;
                preview.Document = xps.GetFixedDocumentSequence();
                preview.Show();
                xps.Close();
                

                /********************
                 * Code to email
                 * *****************/

                //Process.Start("mailto://rony@creash.com.bd?subject=Return preparation&body=Attached is the prepared return document&attachment="+@mydoc);

                //Outlook.Application myOutlook = new Outlook.Application();
                //Outlook.MailItem newMail = (Outlook.MailItem)myOutlook.CreateItem(Outlook.OlItemType.olMailItem);
                //newMail.Recipients.Add("rony@creash.com.bd");
                //newMail.Subject = "Hello";
                //newMail.Body = "TEST";
                //newMail.Attachments.Add(@mydoc, Outlook.OlAttachmentType.olByValue, 1, null);
                //newMail.Send();

                //IDocumentPaginatorSource Document = xps.GetFixedDocumentSequence();
                //FixedDocument doc = Document as FixedDocument;

                //FileStream stream = new FileStream(@"D:\test.doc", FileMode.Create, FileAccess.ReadWrite);
                //using (BinaryWriter writer = new BinaryWriter(stream))
                //{
                //    IXpsFixedDocumentSequenceReader fixedDocSeqReader = xps.FixedDocumentSequenceReader;
                //    IXpsFixedDocumentReader docReader = fixedDocSeqReader.FixedDocuments[0];
                //    IXpsFixedPageReader fixedPageReader = docReader.FixedPages[0];
                //    while (fixedPageReader.XmlReader.Read())
                //    {
                //        string page = fixedPageReader.XmlReader.ReadOuterXml();
                //        writer.Write(Encoding.Default.GetBytes(page));
                //    }

                //    writer.Close();
                //}
                
                //Word.Document doc1 = new Word.Document();
                ////doc1.SaveAs();
                //this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void SaveAsXps(string path, FlowDocument document)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
            }
            try
            {
                using (Package package = Package.Open(path, FileMode.Create))
                {
                    using (var xpsDoc = new XpsDocument(
                        package, System.IO.Packaging.CompressionOption.Maximum))
                    {
                        var xpsSm = new XpsSerializationManager(
                            new XpsPackagingPolicy(xpsDoc), false);
                        FlowDocPaginator fdp = new FlowDocPaginator(document);
                        DocumentPaginator dp = (DocumentPaginator)fdp;
                        //((IDocumentPaginatorSource)document).DocumentPaginator;
                        xpsSm.SaveAsXaml(dp);
                    }
                    package.Close();
                }
            }
            catch
            {
            }
        }

        public static void AddDocument(FlowDocument from, FlowDocument to)
        {
            TextRange range = new TextRange(from.ContentStart, from.ContentEnd);

            MemoryStream stream = new MemoryStream();

            System.Windows.Markup.XamlWriter.Save(range, stream);

            range.Save(stream, DataFormats.XamlPackage);

            TextRange range2 = new TextRange(to.ContentEnd, to.ContentEnd);

            range2.Load(stream, DataFormats.XamlPackage);
        }

        //private void AddPageIntoDocument(Panel grid, FixedDocument fixeddoc, Size size)
        //{
        //    FixedPage fixedpage = new FixedPage();
        //    fixedpage.Width = size.Width;
        //    fixedpage.Height = size.Height;
        //    fixedpage.Children.Add(grid);
        //    PageContent pc = new PageContent();
        //    ((IAddChild)pc).AddChild(fixedpage);
        //    fixeddoc.Pages.Add(pc);
        //}

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void printPreviewBtn_Click(object sender, RoutedEventArgs e)
        {
            //DocumentPaginator dp = ((IDocumentPaginatorSource)this.printablepr).DocumentPaginator;
            //FixedDocument fd = new FixedDocument();
            
            ////fd.DocumentPaginator. = (IDocumentPaginatorSource)this.printablepr;
            //TSPrintPreview preview = new TSPrintPreview();
            //preview.Owner = this;
            //preview.Document = (IDocumentPaginatorSource)this.printablepr;
            //preview.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (checkLicense())
            {
                populatedata();
                try
                {
                    File.Delete(@mydoc);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                SaveAsXps(@mydoc, this.printablepr);
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
                serial = "P" + serial.Substring(0, 15).ToUpper();

                TaxSolution.Properties.Settings.Default.licensekey = serial;
                licensekey = serial;
            }
            TaxSolution.Properties.Settings.Default.Save();
            return licenseStatusCheck(licensekey);
        }

        bool licenseStatusCheck(string key)
        {
            string licensekey = "";

            HardDisk hd = new HardDisk();
            string serial = hd.getserial();
            TaxSolution.Properties.Settings.Default.hdserial = serial;

            serial = EncodePassword(serial + "CREASH IS BEST");
            serial = ReverseString(serial);
            serial = "P" + serial.Substring(0, 15).ToUpper();

            TaxSolution.Properties.Settings.Default.licensekey = serial;
            licensekey = serial;
            if (licensekey == key)
            {
                string enc = EncodePassword(key + "CREASH IS BEST" + "premium");
                enc = ReverseString(enc);
                enc = EncodePassword(enc + "CREASH IS BEST");
                enc = ReverseString(enc);
                string activation = enc.Substring(0, 15).ToUpper();
                string savedact = TaxSolution.Properties.Settings.Default.activationkey.ToUpper();

                if (activation == savedact)
                {
                    return true;
                }
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

        private void PrevBtn_Click(object sender, MouseEventArgs e)
        {
            PRFurnishedDocuments PRFurnishedDocumentsForm = new PRFurnishedDocuments(this.returnid);
            PRFurnishedDocumentsForm.Owner = this.Owner;
            PRFurnishedDocumentsForm.Show();
            this.Close();
        }

        private void CompBtn_Click(object sender, MouseEventArgs e)
        {
            PRSummary PRSummaryForm = new PRSummary(this.returnid);
            PRSummaryForm.Owner = this.Owner;
            PRSummaryForm.Show();
        }

        private void saveDeskBtn_Click(object sender, MouseEventArgs e)
        {
            //SaveAsXps(@mydoc, this.printablepr);

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Personal Return - " + this.returnid.ToString();
            dlg.DefaultExt = ".ts";
            dlg.Filter = "Tax Solution file (.ts)|*.ts";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                try
                {
                    File.Copy(@mydoc, filename);
                }
                catch
                {
                    MessageBox.Show("Error occured! Could not read file.");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                File.Delete(@mydoc);
            }
            catch
            {
            }
        }
    }
}
