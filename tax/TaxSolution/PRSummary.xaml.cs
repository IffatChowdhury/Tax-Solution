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
    /// Interaction logic for PRSummary.xaml
    /// </summary>
    public partial class PRSummary : Window
    {
        double width = 8.5;
        double height = 13;
        private int customerid;
        private int returnid;
        private string sex;
        private int age;
        private int disabled;
        private decimal a13;
        private decimal[] invest = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private decimal summarytotal;

        public PRSummary(int returnid)
        {
            InitializeComponent();
            this.returnid = returnid;
            this.summarytotal = 0;
        }

        public PRSummary()
        {
            InitializeComponent();
            this.returnid = 2;
            this.summarytotal = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            populatedata();
        }

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
                if (dob != "" && dob != DateTime.Parse("01-01-1900").ToShortDateString())
                {
                    TimeSpan tf = DateTime.Today - DateTime.Parse(dob);
                    this.age = tf.Days / 365;
                }

                this.noa.Text = cdr["customer_name"].ToString();
                this.tin.Text = cdr["tin"].ToString();
            }
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];

                this.ay.Text = dr["assessmentyear"].ToString();

                calculatesalary();
                try
                {
                    this.summary2c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi2"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi2"].ToString());
                }
                catch
                {
                }
                calculatehouse();

                try
                {
                    this.summary4c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi4"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi4"].ToString());
                }
                catch
                {
                }

                try
                {
                    this.summary5c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi5"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi5"].ToString());
                }
                catch
                {
                }

                try
                {
                    this.summary6c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi6"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi6"].ToString());
                }
                catch
                {
                }

                try
                {
                    this.summary7c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi7"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi7"].ToString());
                }
                catch
                {
                } 
                
                try
                {
                    this.summary8c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi8"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi8"].ToString());
                }
                catch
                {
                } 
                
                try
                {
                    this.summary9c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi9"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi9"].ToString());
                }
                catch
                {
                } 
                
                try
                {
                    this.summary10c.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi11"].ToString())));
                    this.summarytotal += decimal.Parse(dr["soi11"].ToString());
                }
                catch
                {
                }

                this.summarytc.Inlines.Add(String.Format("{0:#,0.##}", this.summarytotal));


                decimal payabletax = calculatesoi13();

                Paragraph pa1 = new Paragraph();
                pa1.Inlines.Add("Total");
                pa1.Margin = new Thickness(3);
                
                Paragraph pa2 = new Paragraph();
                pa2.Inlines.Add(String.Format("{0:#,0.##}", this.summarytotal));
                pa2.Margin = new Thickness(3);
                Paragraph pa3 = new Paragraph();
                pa3.Inlines.Add("");
                pa3.Margin = new Thickness(3);
                Paragraph pa4 = new Paragraph();
                pa4.Inlines.Add(String.Format("{0:#,0.##}", payabletax));
                pa4.Margin = new Thickness(3);

                TableCell tca1 = new TableCell(pa1);
                tca1.BorderBrush = Brushes.Black;
                tca1.BorderThickness = new Thickness(0.5);
                TableCell tca2 = new TableCell(pa2);
                tca2.BorderBrush = Brushes.Black;
                tca2.BorderThickness = new Thickness(0.5);
                TableCell tca3 = new TableCell(pa3);
                tca3.BorderBrush = Brushes.Black;
                tca3.BorderThickness = new Thickness(0.5);
                TableCell tca4 = new TableCell(pa4);
                tca4.BorderBrush = Brushes.Black;
                tca4.BorderThickness = new Thickness(0.5);

                TableRow tra1 = new TableRow();
                tra1.Cells.Add(tca1);
                tra1.Cells.Add(tca2);
                tra1.Cells.Add(tca3);
                tra1.Cells.Add(tca4);

                this.taxcalc.RowGroups[0].Rows.Add(tra1);

                this.taxpayablea.Inlines.Add(String.Format("{0:#,0.##}", payabletax));
                decimal trebate = 0;
                try
                {
                    this.taxpayableb.Inlines.Add(String.Format("{0:#,0.##}", decimal.Parse(dr["soi14"].ToString())));
                    trebate = decimal.Parse(dr["soi14"].ToString());
                }
                catch
                {
                }
                decimal tp = payabletax - trebate;
                this.taxpayablec.Inlines.Add(String.Format("{0:#,0.##}", tp));

                decimal tpatsource = 0;
                try
                {
                    tpatsource += decimal.Parse(dr["soi16a"].ToString());
                }
                catch
                {
                }
                try
                {
                    tpatsource += decimal.Parse(dr["soi16b"].ToString());
                }
                catch
                {
                }
                this.taxpayabled.Inlines.Add(String.Format("{0:#,0.##}", tpatsource));
                decimal tptotal = 0;
                tptotal = tp - tpatsource;
                this.taxpayabletotal.Inlines.Add(String.Format("{0:#,0.##}", tptotal));
            }
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

        public decimal calculatesalary()
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

            //this.summary1a.Inlines.Add(a.ToString());
            //this.summary1b.Inlines.Add(b.ToString());

            total = a - b;
            this.summarytotal += total;
            this.summary1c.Inlines.Add(String.Format("{0:#,0.##}",total));

            return total;
        }

        public decimal calculatehouse()
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
            //this.summary3a.Inlines.Add(a.ToString());
            //this.summary3b.Inlines.Add(b.ToString());
            total = a - b;
            this.summarytotal += total;
            this.summary3c.Inlines.Add(String.Format("{0:#,0.##}", total));

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
                income = this.summarytotal;
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
                        Paragraph p2 = new Paragraph();
                        p2.Inlines.Add("165,000");
                        p2.Margin = new Thickness(3);
                        Paragraph p3 = new Paragraph();
                        p3.Inlines.Add("Nil");
                        p3.Margin = new Thickness(3);
                        Paragraph p4 = new Paragraph();
                        p4.Inlines.Add("0");
                        p4.Margin = new Thickness(3);
                        
                        TableCell tc1 = new TableCell();
                        tc1.BorderBrush = Brushes.Black;
                        tc1.BorderThickness = new Thickness(0.5);

                        TableCell tc2 = new TableCell(p2);
                        tc2.BorderBrush = Brushes.Black;
                        tc2.BorderThickness = new Thickness(0.5);

                        TableCell tc3 = new TableCell(p3);
                        tc3.BorderBrush = Brushes.Black;
                        tc3.BorderThickness = new Thickness(0.5);

                        TableCell tc4 = new TableCell(p4);
                        tc4.BorderBrush = Brushes.Black;
                        tc4.BorderThickness = new Thickness(0.5);

                        TableRow tr1 = new TableRow();
                        tr1.Cells.Add(tc1);
                        tr1.Cells.Add(tc2);
                        tr1.Cells.Add(tc3);
                        tr1.Cells.Add(tc4);

                        this.taxcalc.RowGroups[0].Rows.Add(tr1);
                        return (decimal)0;
                    }
                    else
                    {
                        income -= 165000;

                        Paragraph p2 = new Paragraph();
                        p2.Inlines.Add("165,000");
                        p2.Margin = new Thickness(3);
                        Paragraph p3 = new Paragraph();
                        p3.Inlines.Add("Nil");
                        p3.Margin = new Thickness(3);
                        Paragraph p4 = new Paragraph();
                        p4.Inlines.Add("0");
                        p4.Margin = new Thickness(3);

                        TableCell tc1 = new TableCell();
                        tc1.BorderBrush = Brushes.Black;
                        tc1.BorderThickness = new Thickness(0.5);

                        TableCell tc2 = new TableCell(p2);
                        tc2.BorderBrush = Brushes.Black;
                        tc2.BorderThickness = new Thickness(0.5);

                        TableCell tc3 = new TableCell(p3);
                        tc3.BorderBrush = Brushes.Black;
                        tc3.BorderThickness = new Thickness(0.5);

                        TableCell tc4 = new TableCell(p4);
                        tc4.BorderBrush = Brushes.Black;
                        tc4.BorderThickness = new Thickness(0.5);

                        TableRow tr1 = new TableRow();
                        tr1.Cells.Add(tc1);
                        tr1.Cells.Add(tc2);
                        tr1.Cells.Add(tc3);
                        tr1.Cells.Add(tc4);

                        this.taxcalc.RowGroups[0].Rows.Add(tr1);

                        if (income > 275000)
                        {
                            total += 27500;
                            income -= 275000;

                            Paragraph pa1 = new Paragraph();
                            pa1.Inlines.Add("Next");
                            pa1.Margin = new Thickness(3);
                            Paragraph pa2 = new Paragraph();
                            pa2.Inlines.Add("275,000");
                            pa2.Margin = new Thickness(3);
                            Paragraph pa3 = new Paragraph();
                            pa3.Inlines.Add("10%");
                            pa3.Margin = new Thickness(3);
                            Paragraph pa4 = new Paragraph();
                            pa4.Inlines.Add("27,500");
                            pa4.Margin = new Thickness(3);

                            TableCell tca1 = new TableCell(pa1);
                            tca1.BorderBrush = Brushes.Black;
                            tca1.BorderThickness = new Thickness(0.5);

                            TableCell tca2 = new TableCell(pa2);
                            tca2.BorderBrush = Brushes.Black;
                            tca2.BorderThickness = new Thickness(0.5);

                            TableCell tca3 = new TableCell(pa3);
                            tca3.BorderBrush = Brushes.Black;
                            tca3.BorderThickness = new Thickness(0.5);

                            TableCell tca4 = new TableCell(pa4);
                            tca4.BorderBrush = Brushes.Black;
                            tca4.BorderThickness = new Thickness(0.5);

                            TableRow tra1 = new TableRow();
                            tra1.Cells.Add(tca1);
                            tra1.Cells.Add(tca2);
                            tra1.Cells.Add(tca3);
                            tra1.Cells.Add(tca4);

                            this.taxcalc.RowGroups[0].Rows.Add(tra1);

                            if (income > 325000)
                            {
                                total += 48750;
                                income -= 325000;

                                Paragraph p21 = new Paragraph();
                                p21.Inlines.Add("Next");
                                p21.Margin = new Thickness(3);
                                Paragraph p22 = new Paragraph();
                                p22.Inlines.Add("325,000");
                                p22.Margin = new Thickness(3);
                                Paragraph p23 = new Paragraph();
                                p23.Inlines.Add("15%");
                                p23.Margin = new Thickness(3);
                                Paragraph p24 = new Paragraph();
                                p24.Inlines.Add("48,750");
                                p24.Margin = new Thickness(3);

                                TableCell tc21 = new TableCell(p21);
                                tc21.BorderBrush = Brushes.Black;
                                tc21.BorderThickness = new Thickness(0.5);

                                TableCell tc22 = new TableCell(p22);
                                tc22.BorderBrush = Brushes.Black;
                                tc22.BorderThickness = new Thickness(0.5);

                                TableCell tc23 = new TableCell(p23);
                                tc23.BorderBrush = Brushes.Black;
                                tc23.BorderThickness = new Thickness(0.5);

                                TableCell tc24 = new TableCell(p24);
                                tc24.BorderBrush = Brushes.Black;
                                tc24.BorderThickness = new Thickness(0.5);

                                TableRow tr21 = new TableRow();
                                tr21.Cells.Add(tc21);
                                tr21.Cells.Add(tc22);
                                tr21.Cells.Add(tc23);
                                tr21.Cells.Add(tc24);

                                this.taxcalc.RowGroups[0].Rows.Add(tr21);

                                if (income > 375000)
                                {
                                    total += 75000;
                                    income -= 375000;

                                    Paragraph p31 = new Paragraph();
                                    p31.Inlines.Add("Next");
                                    p31.Margin = new Thickness(3);
                                    Paragraph p32 = new Paragraph();
                                    p32.Inlines.Add("375,000");
                                    p32.Margin = new Thickness(3);
                                    Paragraph p33 = new Paragraph();
                                    p33.Inlines.Add("20%");
                                    p33.Margin = new Thickness(3);
                                    Paragraph p34 = new Paragraph();
                                    p34.Inlines.Add("75,000");
                                    p34.Margin = new Thickness(3);

                                    TableCell tc31 = new TableCell(p31);
                                    tc31.BorderBrush = Brushes.Black;
                                    tc31.BorderThickness = new Thickness(0.5);

                                    TableCell tc32 = new TableCell(p32);
                                    tc32.BorderBrush = Brushes.Black;
                                    tc32.BorderThickness = new Thickness(0.5);

                                    TableCell tc33 = new TableCell(p33);
                                    tc33.BorderBrush = Brushes.Black;
                                    tc33.BorderThickness = new Thickness(0.5);

                                    TableCell tc34 = new TableCell(p34);
                                    tc34.BorderBrush = Brushes.Black;
                                    tc34.BorderThickness = new Thickness(0.5);

                                    TableRow tr31 = new TableRow();
                                    tr31.Cells.Add(tc31);
                                    tr31.Cells.Add(tc32);
                                    tr31.Cells.Add(tc33);
                                    tr31.Cells.Add(tc34);

                                    this.taxcalc.RowGroups[0].Rows.Add(tr31);

                                    if (income > 0)
                                    {
                                        decimal per = income * (decimal).25;
                                        total += per;

                                        Paragraph p41 = new Paragraph();
                                        p41.Inlines.Add("Next");
                                        p41.Margin = new Thickness(3);
                                        Paragraph p42 = new Paragraph();
                                        p42.Inlines.Add(income.ToString());
                                        p42.Margin = new Thickness(3);
                                        Paragraph p43 = new Paragraph();
                                        p43.Inlines.Add("20%");
                                        p43.Margin = new Thickness(3);
                                        Paragraph p44 = new Paragraph();
                                        p44.Inlines.Add(per.ToString());
                                        p44.Margin = new Thickness(3);

                                        TableCell tc41 = new TableCell(p41);
                                        tc41.BorderBrush = Brushes.Black;
                                        tc41.BorderThickness = new Thickness(0.5);

                                        TableCell tc42 = new TableCell(p42);
                                        tc42.BorderBrush = Brushes.Black;
                                        tc42.BorderThickness = new Thickness(0.5);

                                        TableCell tc43 = new TableCell(p43);
                                        tc43.BorderBrush = Brushes.Black;
                                        tc43.BorderThickness = new Thickness(0.5);

                                        TableCell tc44 = new TableCell(p44);
                                        tc44.BorderBrush = Brushes.Black;
                                        tc44.BorderThickness = new Thickness(0.5);

                                        TableRow tr41 = new TableRow();
                                        tr41.Cells.Add(tc41);
                                        tr41.Cells.Add(tc42);
                                        tr41.Cells.Add(tc43);
                                        tr41.Cells.Add(tc44);

                                        this.taxcalc.RowGroups[0].Rows.Add(tr41);

                                        return total;
                                    }
                                }
                                else
                                {
                                    decimal per = income * (decimal).2;
                                    total += per;

                                    Paragraph p31 = new Paragraph();
                                    p31.Inlines.Add("Next");
                                    p31.Margin = new Thickness(3);
                                    Paragraph p32 = new Paragraph();
                                    p32.Inlines.Add(income.ToString());
                                    p32.Margin = new Thickness(3);
                                    Paragraph p33 = new Paragraph();
                                    p33.Inlines.Add("20%");
                                    p33.Margin = new Thickness(3);
                                    Paragraph p34 = new Paragraph();
                                    p34.Inlines.Add(per.ToString());
                                    p34.Margin = new Thickness(3);

                                    TableCell tc31 = new TableCell(p31);
                                    tc31.BorderBrush = Brushes.Black;
                                    tc31.BorderThickness = new Thickness(0.5);

                                    TableCell tc32 = new TableCell(p32);
                                    tc32.BorderBrush = Brushes.Black;
                                    tc32.BorderThickness = new Thickness(0.5);

                                    TableCell tc33 = new TableCell(p33);
                                    tc33.BorderBrush = Brushes.Black;
                                    tc33.BorderThickness = new Thickness(0.5);

                                    TableCell tc34 = new TableCell(p34);
                                    tc34.BorderBrush = Brushes.Black;
                                    tc34.BorderThickness = new Thickness(0.5);

                                    TableRow tr31 = new TableRow();
                                    tr31.Cells.Add(tc31);
                                    tr31.Cells.Add(tc32);
                                    tr31.Cells.Add(tc33);
                                    tr31.Cells.Add(tc34);

                                    this.taxcalc.RowGroups[0].Rows.Add(tr31);

                                    return total;
                                }
                            }
                            else
                            {
                                decimal per = income * (decimal).15;
                                total += per;

                                Paragraph p21 = new Paragraph();
                                p21.Inlines.Add("Next");
                                p21.Margin = new Thickness(3);
                                Paragraph p22 = new Paragraph();
                                p22.Inlines.Add(income.ToString());
                                p22.Margin = new Thickness(3);
                                Paragraph p23 = new Paragraph();
                                p23.Inlines.Add("15%");
                                p23.Margin = new Thickness(3);
                                Paragraph p24 = new Paragraph();
                                p24.Inlines.Add(per.ToString());
                                p24.Margin = new Thickness(3);

                                TableCell tc21 = new TableCell(p21);
                                tc21.BorderBrush = Brushes.Black;
                                tc21.BorderThickness = new Thickness(0.5);

                                TableCell tc22 = new TableCell(p22);
                                tc22.BorderBrush = Brushes.Black;
                                tc22.BorderThickness = new Thickness(0.5);

                                TableCell tc23 = new TableCell(p23);
                                tc23.BorderBrush = Brushes.Black;
                                tc23.BorderThickness = new Thickness(0.5);

                                TableCell tc24 = new TableCell(p24);
                                tc24.BorderBrush = Brushes.Black;
                                tc24.BorderThickness = new Thickness(0.5);

                                TableRow tr21 = new TableRow();
                                tr21.Cells.Add(tc21);
                                tr21.Cells.Add(tc22);
                                tr21.Cells.Add(tc23);
                                tr21.Cells.Add(tc24);

                                this.taxcalc.RowGroups[0].Rows.Add(tr21);

                                return total;
                            }
                        }
                        else
                        {
                            decimal per = income * (decimal).1;
                            total += per;

                            Paragraph pb1 = new Paragraph();
                            pb1.Inlines.Add("Next");
                            pb1.Margin = new Thickness(3);
                            Paragraph pb2 = new Paragraph();
                            pb2.Inlines.Add(income.ToString());
                            pb2.Margin = new Thickness(3);
                            Paragraph pb3 = new Paragraph();
                            pb3.Inlines.Add("10%");
                            pb3.Margin = new Thickness(3);
                            Paragraph pb4 = new Paragraph();
                            pb4.Inlines.Add(per.ToString());
                            pb4.Margin = new Thickness(3);

                            TableCell tcb1 = new TableCell(pb1);
                            tcb1.BorderBrush = Brushes.Black;
                            tcb1.BorderThickness = new Thickness(0.5);

                            TableCell tcb2 = new TableCell(pb2);
                            tcb2.BorderBrush = Brushes.Black;
                            tcb2.BorderThickness = new Thickness(0.5);

                            TableCell tcb3 = new TableCell(pb3);
                            tcb3.BorderBrush = Brushes.Black;
                            tcb3.BorderThickness = new Thickness(0.5);

                            TableCell tcb4 = new TableCell(pb4);
                            tcb4.BorderBrush = Brushes.Black;
                            tcb4.BorderThickness = new Thickness(0.5);

                            TableRow trb1 = new TableRow();
                            trb1.Cells.Add(tcb1);
                            trb1.Cells.Add(tcb2);
                            trb1.Cells.Add(tcb3);
                            trb1.Cells.Add(tcb4);

                            this.taxcalc.RowGroups[0].Rows.Add(trb1);

                            return total;
                        }
                    }
                    break;
                case "b":
                    if (income <= 180000)
                    {
                        Paragraph p2 = new Paragraph();
                        p2.Inlines.Add("180,000");
                        p2.Margin = new Thickness(3);
                        Paragraph p3 = new Paragraph();
                        p3.Inlines.Add("Nil");
                        p3.Margin = new Thickness(3);
                        Paragraph p4 = new Paragraph();
                        p4.Inlines.Add("0");
                        p4.Margin = new Thickness(3);

                        TableCell tc1 = new TableCell();
                        tc1.BorderBrush = Brushes.Black;
                        tc1.BorderThickness = new Thickness(0.5);

                        TableCell tc2 = new TableCell(p2);
                        tc2.BorderBrush = Brushes.Black;
                        tc2.BorderThickness = new Thickness(0.5);

                        TableCell tc3 = new TableCell(p3);
                        tc3.BorderBrush = Brushes.Black;
                        tc3.BorderThickness = new Thickness(0.5);

                        TableCell tc4 = new TableCell(p4);
                        tc4.BorderBrush = Brushes.Black;
                        tc4.BorderThickness = new Thickness(0.5);

                        TableRow tr1 = new TableRow();
                        tr1.Cells.Add(tc1);
                        tr1.Cells.Add(tc2);
                        tr1.Cells.Add(tc3);
                        tr1.Cells.Add(tc4);

                        this.taxcalc.RowGroups[0].Rows.Add(tr1);

                        return (decimal)0;
                    }
                    else
                    {
                        income -= 180000;

                        Paragraph p2 = new Paragraph();
                        p2.Inlines.Add("180,000");
                        p2.Margin = new Thickness(3);
                        Paragraph p3 = new Paragraph();
                        p3.Inlines.Add("Nil");
                        p3.Margin = new Thickness(3);
                        Paragraph p4 = new Paragraph();
                        p4.Inlines.Add("0");
                        p4.Margin = new Thickness(3);

                        TableCell tc1 = new TableCell();
                        tc1.BorderBrush = Brushes.Black;
                        tc1.BorderThickness = new Thickness(0.5);

                        TableCell tc2 = new TableCell(p2);
                        tc2.BorderBrush = Brushes.Black;
                        tc2.BorderThickness = new Thickness(0.5);

                        TableCell tc3 = new TableCell(p3);
                        tc3.BorderBrush = Brushes.Black;
                        tc3.BorderThickness = new Thickness(0.5);

                        TableCell tc4 = new TableCell(p4);
                        tc4.BorderBrush = Brushes.Black;
                        tc4.BorderThickness = new Thickness(0.5);

                        TableRow tr1 = new TableRow();
                        tr1.Cells.Add(tc1);
                        tr1.Cells.Add(tc2);
                        tr1.Cells.Add(tc3);
                        tr1.Cells.Add(tc4);

                        this.taxcalc.RowGroups[0].Rows.Add(tr1);

                        if (income >= 275000)
                        {
                            total += 27500;
                            income -= 275000;

                            Paragraph pa1 = new Paragraph();
                            pa1.Inlines.Add("Next");
                            pa1.Margin = new Thickness(3);
                            Paragraph pa2 = new Paragraph();
                            pa2.Inlines.Add("275,000");
                            pa2.Margin = new Thickness(3);
                            Paragraph pa3 = new Paragraph();
                            pa3.Inlines.Add("10%");
                            pa3.Margin = new Thickness(3);
                            Paragraph pa4 = new Paragraph();
                            pa4.Inlines.Add("27,500");
                            pa4.Margin = new Thickness(3);

                            TableCell tca1 = new TableCell(pa1);
                            tca1.BorderBrush = Brushes.Black;
                            tca1.BorderThickness = new Thickness(0.5);

                            TableCell tca2 = new TableCell(pa2);
                            tca2.BorderBrush = Brushes.Black;
                            tca2.BorderThickness = new Thickness(0.5);

                            TableCell tca3 = new TableCell(pa3);
                            tca3.BorderBrush = Brushes.Black;
                            tca3.BorderThickness = new Thickness(0.5);

                            TableCell tca4 = new TableCell(pa4);
                            tca4.BorderBrush = Brushes.Black;
                            tca4.BorderThickness = new Thickness(0.5);

                            TableRow tra1 = new TableRow();
                            tra1.Cells.Add(tca1);
                            tra1.Cells.Add(tca2);
                            tra1.Cells.Add(tca3);
                            tra1.Cells.Add(tca4);

                            this.taxcalc.RowGroups[0].Rows.Add(tra1);

                            if (income >= 325000)
                            {
                                total += 48750;
                                income -= 325000;

                                Paragraph p21 = new Paragraph();
                                p21.Inlines.Add("Next");
                                p21.Margin = new Thickness(3);
                                Paragraph p22 = new Paragraph();
                                p22.Inlines.Add("325,000");
                                p22.Margin = new Thickness(3);
                                Paragraph p23 = new Paragraph();
                                p23.Inlines.Add("15%");
                                p23.Margin = new Thickness(3);
                                Paragraph p24 = new Paragraph();
                                p24.Inlines.Add("48,750");
                                p24.Margin = new Thickness(3);

                                TableCell tc21 = new TableCell(p21);
                                tc21.BorderBrush = Brushes.Black;
                                tc21.BorderThickness = new Thickness(0.5);

                                TableCell tc22 = new TableCell(p22);
                                tc22.BorderBrush = Brushes.Black;
                                tc22.BorderThickness = new Thickness(0.5);

                                TableCell tc23 = new TableCell(p23);
                                tc23.BorderBrush = Brushes.Black;
                                tc23.BorderThickness = new Thickness(0.5);

                                TableCell tc24 = new TableCell(p24);
                                tc24.BorderBrush = Brushes.Black;
                                tc24.BorderThickness = new Thickness(0.5);

                                TableRow tr21 = new TableRow();
                                tr21.Cells.Add(tc21);
                                tr21.Cells.Add(tc22);
                                tr21.Cells.Add(tc23);
                                tr21.Cells.Add(tc24);

                                this.taxcalc.RowGroups[0].Rows.Add(tr21);

                                if (income >= 375000)
                                {
                                    total += 75000;
                                    income -= 375000;

                                    Paragraph p31 = new Paragraph();
                                    p31.Inlines.Add("Next");
                                    p31.Margin = new Thickness(3);
                                    Paragraph p32 = new Paragraph();
                                    p32.Inlines.Add("375,000");
                                    p32.Margin = new Thickness(3);
                                    Paragraph p33 = new Paragraph();
                                    p33.Inlines.Add("20%");
                                    p33.Margin = new Thickness(3);
                                    Paragraph p34 = new Paragraph();
                                    p34.Inlines.Add("75,000");
                                    p34.Margin = new Thickness(3);

                                    TableCell tc31 = new TableCell(p31);
                                    tc31.BorderBrush = Brushes.Black;
                                    tc31.BorderThickness = new Thickness(0.5);

                                    TableCell tc32 = new TableCell(p32);
                                    tc32.BorderBrush = Brushes.Black;
                                    tc32.BorderThickness = new Thickness(0.5);

                                    TableCell tc33 = new TableCell(p33);
                                    tc33.BorderBrush = Brushes.Black;
                                    tc33.BorderThickness = new Thickness(0.5);

                                    TableCell tc34 = new TableCell(p34);
                                    tc34.BorderBrush = Brushes.Black;
                                    tc34.BorderThickness = new Thickness(0.5);

                                    TableRow tr31 = new TableRow();
                                    tr31.Cells.Add(tc31);
                                    tr31.Cells.Add(tc32);
                                    tr31.Cells.Add(tc33);
                                    tr31.Cells.Add(tc34);

                                    this.taxcalc.RowGroups[0].Rows.Add(tr31);

                                    if (income > 0)
                                    {
                                        decimal per = income * (decimal).25;
                                        total += per;

                                        Paragraph p41 = new Paragraph();
                                        p41.Inlines.Add("Next");
                                        p41.Margin = new Thickness(3);
                                        Paragraph p42 = new Paragraph();
                                        p42.Inlines.Add(income.ToString());
                                        p42.Margin = new Thickness(3);
                                        Paragraph p43 = new Paragraph();
                                        p43.Inlines.Add("20%");
                                        p43.Margin = new Thickness(3);
                                        Paragraph p44 = new Paragraph();
                                        p44.Inlines.Add(per.ToString());
                                        p44.Margin = new Thickness(3);

                                        TableCell tc41 = new TableCell(p41);
                                        tc41.BorderBrush = Brushes.Black;
                                        tc41.BorderThickness = new Thickness(0.5);

                                        TableCell tc42 = new TableCell(p42);
                                        tc42.BorderBrush = Brushes.Black;
                                        tc42.BorderThickness = new Thickness(0.5);

                                        TableCell tc43 = new TableCell(p43);
                                        tc43.BorderBrush = Brushes.Black;
                                        tc43.BorderThickness = new Thickness(0.5);

                                        TableCell tc44 = new TableCell(p44);
                                        tc44.BorderBrush = Brushes.Black;
                                        tc44.BorderThickness = new Thickness(0.5);

                                        TableRow tr41 = new TableRow();
                                        tr41.Cells.Add(tc41);
                                        tr41.Cells.Add(tc42);
                                        tr41.Cells.Add(tc43);
                                        tr41.Cells.Add(tc44);

                                        this.taxcalc.RowGroups[0].Rows.Add(tr41);

                                        return total;
                                    }
                                }
                                else
                                {
                                    decimal per = income * (decimal).2;
                                    total += per;

                                    Paragraph p31 = new Paragraph();
                                    p31.Inlines.Add("Next");
                                    p31.Margin = new Thickness(3);
                                    Paragraph p32 = new Paragraph();
                                    p32.Inlines.Add(income.ToString());
                                    p32.Margin = new Thickness(3);
                                    Paragraph p33 = new Paragraph();
                                    p33.Inlines.Add("20%");
                                    p33.Margin = new Thickness(3);
                                    Paragraph p34 = new Paragraph();
                                    p34.Inlines.Add(per.ToString());
                                    p34.Margin = new Thickness(3);

                                    TableCell tc31 = new TableCell(p31);
                                    tc31.BorderBrush = Brushes.Black;
                                    tc31.BorderThickness = new Thickness(0.5);

                                    TableCell tc32 = new TableCell(p32);
                                    tc32.BorderBrush = Brushes.Black;
                                    tc32.BorderThickness = new Thickness(0.5);

                                    TableCell tc33 = new TableCell(p33);
                                    tc33.BorderBrush = Brushes.Black;
                                    tc33.BorderThickness = new Thickness(0.5);

                                    TableCell tc34 = new TableCell(p34);
                                    tc34.BorderBrush = Brushes.Black;
                                    tc34.BorderThickness = new Thickness(0.5);

                                    TableRow tr31 = new TableRow();
                                    tr31.Cells.Add(tc31);
                                    tr31.Cells.Add(tc32);
                                    tr31.Cells.Add(tc33);
                                    tr31.Cells.Add(tc34);

                                    this.taxcalc.RowGroups[0].Rows.Add(tr31);

                                    return total;
                                }
                            }
                            else
                            {
                                decimal per = income * (decimal).15;
                                total += per;

                                Paragraph p21 = new Paragraph();
                                p21.Inlines.Add("Next");
                                p21.Margin = new Thickness(3);
                                Paragraph p22 = new Paragraph();
                                p22.Inlines.Add(income.ToString());
                                p22.Margin = new Thickness(3);
                                Paragraph p23 = new Paragraph();
                                p23.Inlines.Add("15%");
                                p23.Margin = new Thickness(3);
                                Paragraph p24 = new Paragraph();
                                p24.Inlines.Add(per.ToString());
                                p24.Margin = new Thickness(3);

                                TableCell tc21 = new TableCell(p21);
                                tc21.BorderBrush = Brushes.Black;
                                tc21.BorderThickness = new Thickness(0.5);

                                TableCell tc22 = new TableCell(p22);
                                tc22.BorderBrush = Brushes.Black;
                                tc22.BorderThickness = new Thickness(0.5);

                                TableCell tc23 = new TableCell(p23);
                                tc23.BorderBrush = Brushes.Black;
                                tc23.BorderThickness = new Thickness(0.5);

                                TableCell tc24 = new TableCell(p24);
                                tc24.BorderBrush = Brushes.Black;
                                tc24.BorderThickness = new Thickness(0.5);

                                TableRow tr21 = new TableRow();
                                tr21.Cells.Add(tc21);
                                tr21.Cells.Add(tc22);
                                tr21.Cells.Add(tc23);
                                tr21.Cells.Add(tc24);

                                this.taxcalc.RowGroups[0].Rows.Add(tr21);

                                return total;
                            }
                        }
                        else
                        {
                            decimal per = income * (decimal).1;
                            total += per;

                            Paragraph pb1 = new Paragraph();
                            pb1.Inlines.Add("Next");
                            pb1.Margin = new Thickness(3);
                            Paragraph pb2 = new Paragraph();
                            pb2.Inlines.Add(income.ToString());
                            pb2.Margin = new Thickness(3);
                            Paragraph pb3 = new Paragraph();
                            pb3.Inlines.Add("10%");
                            pb3.Margin = new Thickness(3);
                            Paragraph pb4 = new Paragraph();
                            pb4.Inlines.Add(per.ToString());
                            pb4.Margin = new Thickness(3);

                            TableCell tcb1 = new TableCell(pb1);
                            tcb1.BorderBrush = Brushes.Black;
                            tcb1.BorderThickness = new Thickness(0.5);

                            TableCell tcb2 = new TableCell(pb2);
                            tcb2.BorderBrush = Brushes.Black;
                            tcb2.BorderThickness = new Thickness(0.5);

                            TableCell tcb3 = new TableCell(pb3);
                            tcb3.BorderBrush = Brushes.Black;
                            tcb3.BorderThickness = new Thickness(0.5);

                            TableCell tcb4 = new TableCell(pb4);
                            tcb4.BorderBrush = Brushes.Black;
                            tcb4.BorderThickness = new Thickness(0.5);

                            TableRow trb1 = new TableRow();
                            trb1.Cells.Add(tcb1);
                            trb1.Cells.Add(tcb2);
                            trb1.Cells.Add(tcb3);
                            trb1.Cells.Add(tcb4);

                            this.taxcalc.RowGroups[0].Rows.Add(trb1);

                            return total;
                        }
                    }
                    break;
                case "c":
                    if (income <= 200000)
                    {
                        Paragraph p2 = new Paragraph();
                        p2.Inlines.Add("200,000");
                        p2.Margin = new Thickness(3);
                        Paragraph p3 = new Paragraph();
                        p3.Inlines.Add("Nil");
                        p3.Margin = new Thickness(3);
                        Paragraph p4 = new Paragraph();
                        p4.Inlines.Add("0");
                        p4.Margin = new Thickness(3);

                        TableCell tc1 = new TableCell();
                        tc1.BorderBrush = Brushes.Black;
                        tc1.BorderThickness = new Thickness(.5);

                        TableCell tc2 = new TableCell(p2);
                        tc2.BorderBrush = Brushes.Black;
                        tc2.BorderThickness = new Thickness(.5);

                        TableCell tc3 = new TableCell(p3);
                        tc3.BorderBrush = Brushes.Black;
                        tc3.BorderThickness = new Thickness(.5);

                        TableCell tc4 = new TableCell(p4);
                        tc4.BorderBrush = Brushes.Black;
                        tc4.BorderThickness = new Thickness(.5);

                        TableRow tr1 = new TableRow();
                        tr1.Cells.Add(tc1);
                        tr1.Cells.Add(tc2);
                        tr1.Cells.Add(tc3);
                        tr1.Cells.Add(tc4);

                        this.taxcalc.RowGroups[0].Rows.Add(tr1);

                        return (decimal)0;
                    }
                    else
                    {
                        income -= 200000;

                        Paragraph p2 = new Paragraph();
                        p2.Inlines.Add("200,000");
                        p2.Margin = new Thickness(3);
                        Paragraph p3 = new Paragraph();
                        p3.Inlines.Add("Nil");
                        p3.Margin = new Thickness(3);
                        Paragraph p4 = new Paragraph();
                        p4.Inlines.Add("0");
                        p4.Margin = new Thickness(3);

                        TableCell tc1 = new TableCell();
                        tc1.BorderBrush = Brushes.Black;
                        tc1.BorderThickness = new Thickness(.5);

                        TableCell tc2 = new TableCell(p2);
                        tc2.BorderBrush = Brushes.Black;
                        tc2.BorderThickness = new Thickness(.5);

                        TableCell tc3 = new TableCell(p3);
                        tc3.BorderBrush = Brushes.Black;
                        tc3.BorderThickness = new Thickness(.5);

                        TableCell tc4 = new TableCell(p4);
                        tc4.BorderBrush = Brushes.Black;
                        tc4.BorderThickness = new Thickness(.5);

                        TableRow tr1 = new TableRow();
                        tr1.Cells.Add(tc1);
                        tr1.Cells.Add(tc2);
                        tr1.Cells.Add(tc3);
                        tr1.Cells.Add(tc4);

                        this.taxcalc.RowGroups[0].Rows.Add(tr1);

                        if (income >= 275000)
                        {
                            total += 27500;
                            income -= 275000;

                            Paragraph pa1 = new Paragraph();
                            pa1.Inlines.Add("Next");
                            pa1.Margin = new Thickness(3);
                            Paragraph pa2 = new Paragraph();
                            pa2.Inlines.Add("275,000");
                            pa2.Margin = new Thickness(3);
                            Paragraph pa3 = new Paragraph();
                            pa3.Inlines.Add("10%");
                            pa3.Margin = new Thickness(3);
                            Paragraph pa4 = new Paragraph();
                            pa4.Inlines.Add("27,500");
                            pa4.Margin = new Thickness(3);

                            TableCell tca1 = new TableCell(pa1);
                            tca1.BorderBrush = Brushes.Black;
                            tca1.BorderThickness = new Thickness(.5);

                            TableCell tca2 = new TableCell(pa2);
                            tca2.BorderBrush = Brushes.Black;
                            tca2.BorderThickness = new Thickness(.5);

                            TableCell tca3 = new TableCell(pa3);
                            tca3.BorderBrush = Brushes.Black;
                            tca3.BorderThickness = new Thickness(.5);

                            TableCell tca4 = new TableCell(pa4);
                            tca4.BorderBrush = Brushes.Black;
                            tca4.BorderThickness = new Thickness(.5);

                            TableRow tra1 = new TableRow();
                            tra1.Cells.Add(tca1);
                            tra1.Cells.Add(tca2);
                            tra1.Cells.Add(tca3);
                            tra1.Cells.Add(tca4);

                            this.taxcalc.RowGroups[0].Rows.Add(tra1);

                            if (income >= 325000)
                            {
                                total += 48750;
                                income -= 325000;

                                Paragraph p21 = new Paragraph();
                                p21.Inlines.Add("Next");
                                p21.Margin = new Thickness(3);
                                Paragraph p22 = new Paragraph();
                                p22.Inlines.Add("325,000");
                                p22.Margin = new Thickness(3);
                                Paragraph p23 = new Paragraph();
                                p23.Inlines.Add("15%");
                                p23.Margin = new Thickness(3);
                                Paragraph p24 = new Paragraph();
                                p24.Inlines.Add("48,750");
                                p24.Margin = new Thickness(3);

                                TableCell tc21 = new TableCell(p21);
                                tc21.BorderBrush = Brushes.Black;
                                tc21.BorderThickness = new Thickness(0.5);

                                TableCell tc22 = new TableCell(p22);
                                tc22.BorderBrush = Brushes.Black;
                                tc22.BorderThickness = new Thickness(0.5);

                                TableCell tc23 = new TableCell(p23);
                                tc23.BorderBrush = Brushes.Black;
                                tc23.BorderThickness = new Thickness(0.5);

                                TableCell tc24 = new TableCell(p24);
                                tc24.BorderBrush = Brushes.Black;
                                tc24.BorderThickness = new Thickness(0.5);

                                TableRow tr21 = new TableRow();
                                tr21.Cells.Add(tc21);
                                tr21.Cells.Add(tc22);
                                tr21.Cells.Add(tc23);
                                tr21.Cells.Add(tc24);

                                this.taxcalc.RowGroups[0].Rows.Add(tr21);

                                if (income >= 375000)
                                {
                                    total += 75000;
                                    income -= 375000;

                                    Paragraph p31 = new Paragraph();
                                    p31.Inlines.Add("Next");
                                    p31.Margin = new Thickness(3);
                                    Paragraph p32 = new Paragraph();
                                    p32.Inlines.Add("375,000");
                                    p32.Margin = new Thickness(3);
                                    Paragraph p33 = new Paragraph();
                                    p33.Inlines.Add("20%");
                                    p33.Margin = new Thickness(3);
                                    Paragraph p34 = new Paragraph();
                                    p34.Inlines.Add("75,000");
                                    p34.Margin = new Thickness(3);

                                    TableCell tc31 = new TableCell(p31);
                                    tc31.BorderBrush = Brushes.Black;
                                    tc31.BorderThickness = new Thickness(0.5);

                                    TableCell tc32 = new TableCell(p32);
                                    tc32.BorderBrush = Brushes.Black;
                                    tc32.BorderThickness = new Thickness(0.5);

                                    TableCell tc33 = new TableCell(p33);
                                    tc33.BorderBrush = Brushes.Black;
                                    tc33.BorderThickness = new Thickness(0.5);

                                    TableCell tc34 = new TableCell(p34);
                                    tc34.BorderBrush = Brushes.Black;
                                    tc34.BorderThickness = new Thickness(0.5);

                                    TableRow tr31 = new TableRow();
                                    tr31.Cells.Add(tc31);
                                    tr31.Cells.Add(tc32);
                                    tr31.Cells.Add(tc33);
                                    tr31.Cells.Add(tc34);

                                    this.taxcalc.RowGroups[0].Rows.Add(tr31);

                                    if (income > 0)
                                    {
                                        decimal per = income * (decimal).25;
                                        total += per;

                                        Paragraph p41 = new Paragraph();
                                        p41.Inlines.Add("Next");
                                        p41.Margin = new Thickness(3);
                                        Paragraph p42 = new Paragraph();
                                        p42.Inlines.Add(income.ToString());
                                        p42.Margin = new Thickness(3);
                                        Paragraph p43 = new Paragraph();
                                        p43.Inlines.Add("20%");
                                        p43.Margin = new Thickness(3);
                                        Paragraph p44 = new Paragraph();
                                        p44.Inlines.Add(per.ToString());
                                        p44.Margin = new Thickness(3);

                                        TableCell tc41 = new TableCell(p41);
                                        tc41.BorderBrush = Brushes.Black;
                                        tc41.BorderThickness = new Thickness(0.5);

                                        TableCell tc42 = new TableCell(p42);
                                        tc42.BorderBrush = Brushes.Black;
                                        tc42.BorderThickness = new Thickness(0.5);

                                        TableCell tc43 = new TableCell(p43);
                                        tc43.BorderBrush = Brushes.Black;
                                        tc43.BorderThickness = new Thickness(0.5);

                                        TableCell tc44 = new TableCell(p44);
                                        tc44.BorderBrush = Brushes.Black;
                                        tc44.BorderThickness = new Thickness(0.5);

                                        TableRow tr41 = new TableRow();
                                        tr41.Cells.Add(tc41);
                                        tr41.Cells.Add(tc42);
                                        tr41.Cells.Add(tc43);
                                        tr41.Cells.Add(tc44);

                                        this.taxcalc.RowGroups[0].Rows.Add(tr41);

                                        return total;
                                    }
                                }
                                else
                                {
                                    decimal per = income * (decimal).2;
                                    total += per;

                                    Paragraph pb21 = new Paragraph();
                                    pb21.Inlines.Add("Next");
                                    pb21.Margin = new Thickness(3);
                                    Paragraph pb22 = new Paragraph();
                                    pb22.Inlines.Add(income.ToString());
                                    pb22.Margin = new Thickness(3);
                                    Paragraph pb23 = new Paragraph();
                                    pb23.Inlines.Add("15%");
                                    pb23.Margin = new Thickness(3);
                                    Paragraph pb24 = new Paragraph();
                                    pb24.Inlines.Add(per.ToString());
                                    pb24.Margin = new Thickness(3);

                                    TableCell tcb21 = new TableCell(pb21);
                                    tcb21.BorderBrush = Brushes.Black;
                                    tcb21.BorderThickness = new Thickness(0.5);

                                    TableCell tcb22 = new TableCell(pb22);
                                    tcb21.BorderBrush = Brushes.Black;
                                    tcb21.BorderThickness = new Thickness(0.5);

                                    TableCell tcb23 = new TableCell(pb23);
                                    tcb21.BorderBrush = Brushes.Black;
                                    tcb21.BorderThickness = new Thickness(0.5);

                                    TableCell tcb24 = new TableCell(pb24);
                                    tcb21.BorderBrush = Brushes.Black;
                                    tcb21.BorderThickness = new Thickness(0.5);

                                    TableRow trb21 = new TableRow();
                                    trb21.Cells.Add(tcb21);
                                    trb21.Cells.Add(tcb22);
                                    trb21.Cells.Add(tcb23);
                                    trb21.Cells.Add(tcb24);

                                    this.taxcalc.RowGroups[0].Rows.Add(trb21);

                                    return total;
                                }
                            }
                            else
                            {
                                decimal per = income * (decimal).15;
                                total += per;

                                Paragraph p31 = new Paragraph();
                                p31.Inlines.Add("Next");
                                p31.Margin = new Thickness(3);
                                Paragraph p32 = new Paragraph();
                                p32.Inlines.Add(income.ToString());
                                p32.Margin = new Thickness(3);
                                Paragraph p33 = new Paragraph();
                                p33.Inlines.Add("20%");
                                p33.Margin = new Thickness(3);
                                Paragraph p34 = new Paragraph();
                                p34.Inlines.Add(per.ToString());
                                p34.Margin = new Thickness(3);

                                TableCell tc31 = new TableCell(p31);
                                tc31.BorderBrush = Brushes.Black;
                                tc31.BorderThickness = new Thickness(0.5);

                                TableCell tc32 = new TableCell(p32);
                                tc32.BorderBrush = Brushes.Black;
                                tc32.BorderThickness = new Thickness(0.5);

                                TableCell tc33 = new TableCell(p33);
                                tc33.BorderBrush = Brushes.Black;
                                tc33.BorderThickness = new Thickness(0.5);

                                TableCell tc34 = new TableCell(p34);
                                tc34.BorderBrush = Brushes.Black;
                                tc34.BorderThickness = new Thickness(0.5);

                                TableRow tr31 = new TableRow();
                                tr31.Cells.Add(tc31);
                                tr31.Cells.Add(tc32);
                                tr31.Cells.Add(tc33);
                                tr31.Cells.Add(tc34);

                                this.taxcalc.RowGroups[0].Rows.Add(tr31);

                                return total;
                            }
                        }
                        else
                        {
                            decimal per = income * (decimal).1;
                            total += per;

                            Paragraph pb1 = new Paragraph();
                            pb1.Inlines.Add("Next");
                            pb1.Margin = new Thickness(3);
                            Paragraph pb2 = new Paragraph();
                            pb2.Inlines.Add(income.ToString());
                            pb2.Margin = new Thickness(3);
                            Paragraph pb3 = new Paragraph();
                            pb3.Inlines.Add("10%");
                            pb3.Margin = new Thickness(3);
                            Paragraph pb4 = new Paragraph();
                            pb4.Inlines.Add(per.ToString());
                            pb4.Margin = new Thickness(3);

                            TableCell tcb1 = new TableCell(pb1);
                            tcb1.BorderBrush = Brushes.Black;
                            tcb1.BorderThickness = new Thickness(0.5);

                            TableCell tcb2 = new TableCell(pb2);
                            tcb2.BorderBrush = Brushes.Black;
                            tcb2.BorderThickness = new Thickness(0.5);

                            TableCell tcb3 = new TableCell(pb3);
                            tcb3.BorderBrush = Brushes.Black;
                            tcb3.BorderThickness = new Thickness(0.5);

                            TableCell tcb4 = new TableCell(pb4);
                            tcb4.BorderBrush = Brushes.Black;
                            tcb4.BorderThickness = new Thickness(0.5);

                            TableRow trb1 = new TableRow();
                            trb1.Cells.Add(tcb1);
                            trb1.Cells.Add(tcb2);
                            trb1.Cells.Add(tcb3);
                            trb1.Cells.Add(tcb4);

                            this.taxcalc.RowGroups[0].Rows.Add(trb1);

                            return total;
                        }
                    }
                    break;
            }

            return total;
        }

        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            ApplicationCommands.Print.Execute("Print summary", this.summaryprint);
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
