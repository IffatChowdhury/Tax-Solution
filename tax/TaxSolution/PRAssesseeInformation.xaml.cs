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
    /// Interaction logic for PRAssesseeInformation.xaml
    /// </summary>
    public partial class PRAssesseeInformation : Window
    {
        private int customerid;
        private int returnid;

        private string name;
        private string sex;
        private int disabledperson;
        private string natid;
        private string utin;
        private string tin;
        private string circle;
        private string zone;
        private string assessmentyear;
        private string oldassessmentyear;
        private string residentialstatus;
        private string status;
        private string employer;
        private string spouce;
        private string father;
        private string mother;
        private string dateofbirth;
        private string presentadd;
        private string permanentadd;
        private string phoneoff;
        private string phonehome;
        private string vatreg;
        private string returntypename;

        public PRAssesseeInformation()
        {
            InitializeComponent();
            this.customerid = 0;
            this.returnid = 0;

            populatedata();
        }

        public PRAssesseeInformation(int customerid, int returnid)
        {
            InitializeComponent();
            this.customerid = customerid;
            this.returnid = returnid;

            populatedata();
        }

        public PRAssesseeInformation(int customerid)
        {
            InitializeComponent();
            this.customerid = customerid;
            this.returnid = 0;

            populatedata();
        }

        private void populatedata()
        {
            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter();
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();

            this.name = "";
            this.sex = "";
            this.disabledperson = 0;
            this.natid = "";
            this.utin = "";
            this.tin = "";
            this.spouce = "";
            this.father = "";
            this.mother = "";
            this.dateofbirth = "";
            this.presentadd = "";
            this.permanentadd = "";
            this.phoneoff = "";
            this.phonehome = "";
            this.vatreg = "";
            this.circle = "";
            this.zone = "";
            this.assessmentyear = "";
            this.oldassessmentyear = "";
            this.residentialstatus = "";
            this.employer = "";
            this.returntypename = "SELF";

            if (this.returnid > 0)
            {
                DataTable prdt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);
                if (prdt.Rows.Count > 0)
                {
                    DataRow prdr = prdt.Rows[0];
                    this.circle = prdr["circle"].ToString();
                    this.zone = prdr["zone"].ToString();
                    this.assessmentyear = prdr["assessmentyear"].ToString();
                    this.oldassessmentyear = prdr["assessmentyear"].ToString();
                    this.status = prdr["assesseestatus"].ToString();
                    this.residentialstatus = prdr["residentialstatus"].ToString();
                    this.employer = prdr["employer"].ToString().ToUpper();
                    this.returntypename = prdr["returntype"].ToString();
                    try
                    {
                        this.customerid = int.Parse(prdr["customerid"].ToString());
                    }
                    catch
                    {
                    }
                }
            }

            if (this.customerid > 0)
            {
                DataTable cdt = taxDBDataSetcustomerTableAdapter.GetDataById(this.customerid);
                if (cdt.Rows.Count > 0)
                {
                    DataRow cdr = cdt.Rows[0];
                    this.name = cdr["customer_name"].ToString().ToUpper();
                    this.sex = cdr["sex"].ToString().ToUpper();
                    try
                    {
                        this.disabledperson = int.Parse(cdr["disabled"].ToString());
                    }
                    catch
                    {
                        this.disabledperson = 0;
                    }
                    this.natid = cdr["national_id"].ToString().ToUpper();
                    this.utin = cdr["utin"].ToString().ToUpper();
                    this.tin = cdr["tin"].ToString().ToUpper();
                    this.spouce = cdr["spouce_name"].ToString().ToUpper();
                    this.father = cdr["father_name"].ToString().ToUpper();
                    this.mother = cdr["mother_name"].ToString().ToUpper();
                    this.dateofbirth = DateTime.Parse(cdr["date_of_birth"].ToString()).ToString();
                    this.presentadd = cdr["address1"].ToString() + cdr["address1_district"].ToString() + cdr["address1_postcode"].ToString();
                    this.permanentadd = cdr["address2"].ToString() + cdr["address2_district"].ToString() + cdr["address2_postcode"].ToString();
                    this.phoneoff = cdr["phone_office"].ToString();
                    this.phonehome = cdr["phone_residence"].ToString();
                    this.vatreg = cdr["vat_reg_no"].ToString().ToUpper();
                }
            }

            this.textBox1.Text = this.name;
            //if (this.sex == "FEMALE")
            //{
            //    this.comboBox3.SelectedIndex = 1;
            //}
            //else if(this.sex == "MALE")
            //{
            //    this.comboBox3.SelectedIndex = 0;
            //}

            if (this.sex == "FEMALE")
            {
                this.sexRadioFemale.IsChecked = true;
            }
            else
            {
                this.sexRadioMale.IsChecked = true;
            }

            if (this.disabledperson == 1)
            {
                this.disabled.IsChecked = true;
            }

            this.textBox2.Text = this.natid;
            string[] utina = this.utin.Split('-');
            if (utina.Length == 3)
            {
                this.textBox3.Text = utina[0];
                this.textBox7.Text = utina[1];
                this.textBox8.Text = utina[2];
            }
            string[] tina = this.tin.Split('-');
            if (tina.Length == 3)
            {
                this.textBox4.Text = tina[0];
                this.textBox9.Text = tina[1];
                this.textBox14.Text = tina[2];
            }
            this.textBox5.Text = this.circle;
            this.textBox6.Text = this.zone;
            this.textBox20.Text = this.assessmentyear;
            if (this.residentialstatus == "NON RESIDENT")
            {
                this.comboBox1.SelectedIndex = 1;
            }
            else if (this.residentialstatus == "RESIDENT")
            {
                this.comboBox1.SelectedIndex = 0;
            }

            if (this.status == "I")
            {
                this.comboBox2.SelectedIndex = 0;
            }
            else if(this.status == "F")
            {
                this.comboBox2.SelectedIndex = 1;
            }
            else if (this.status == "A")
            {
                this.comboBox2.SelectedIndex = 2;
            }
            else if (this.status == "H")
            {
                this.comboBox2.SelectedIndex = 3;
            }

            this.textBox10.Text = this.employer;

            this.textBox11.Text = this.spouce;
            this.textBox12.Text = this.father;
            this.textBox13.Text = this.mother;

            if (this.dateofbirth != "" && this.dateofbirth != DateTime.Parse("01-01-1990").ToShortDateString())
            {
                this.datePicker2.SelectedDate = DateTime.Parse(this.dateofbirth);
            }

            this.textBox15.Text = this.presentadd;
            this.textBox16.Text = this.permanentadd;
            this.textBox17.Text = this.phoneoff;
            this.textBox18.Text = this.phonehome;
            this.textBox19.Text = this.vatreg;

            if (this.returntypename == "SELF")
            {
                this.returntype.SelectedIndex = 0;
            }
            else if (this.returntypename == "UNIVERSAL SELF")
            {
                this.returntype.SelectedIndex = 1;
            }
            else
            {
                this.returntype.SelectedIndex = 2;
            }
        }

        private void NextBtn_Click(object sender, MouseButtonEventArgs e)
        {
            if (formvalidate())
            {
                TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
                // Load data into the table customer. You can modify this code as needed.
                TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter();
                TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();

                this.name = this.textBox1.Text.ToUpper();
                this.sex = "MALE";
                //if (this.comboBox3.SelectedIndex == 1)
                //{
                //    this.sex = "FEMALE";
                //}
                if ((bool)this.sexRadioFemale.IsChecked)
                {
                    this.sex = "FEMALE";
                }

                this.disabledperson = 0;
                if ((bool)this.disabled.IsChecked)
                {
                    this.disabledperson = 1;
                }
                
                this.natid = this.textBox2.Text.ToUpper();
                this.utin = this.textBox3.Text.ToUpper() + "-" + this.textBox7.Text.ToUpper() + "-" + this.textBox8.Text.ToUpper();
                this.tin = this.textBox4.Text.ToUpper() + "-" + this.textBox9.Text.ToUpper() + "-" + this.textBox14.Text.ToUpper();
                this.spouce = this.textBox11.Text.ToUpper();
                this.father = this.textBox12.Text.ToUpper();
                this.mother = this.textBox13.Text.ToUpper();
                this.dateofbirth = "01-01-1900";
                if (this.datePicker2.SelectedDate != null)
                {
                    this.dateofbirth = this.datePicker2.SelectedDate.Value.ToShortDateString();
                }
                this.presentadd = this.textBox15.Text.ToUpper();
                this.permanentadd = this.textBox16.Text.ToUpper();
                this.phoneoff = this.textBox17.Text.ToUpper();
                this.phonehome = this.textBox18.Text.ToUpper();
                this.vatreg = this.textBox19.Text.ToUpper();
                this.circle = this.textBox5.Text.ToUpper();
                this.zone = this.textBox6.Text.ToUpper();
                this.assessmentyear = this.textBox20.Text.ToUpper();
                this.residentialstatus = "RESIDENT";
                if (this.comboBox1.SelectedIndex == 1)
                {
                    this.residentialstatus = "NON RESIDENT";
                }
                this.status = "";
                if (this.comboBox2.SelectedIndex == 0)
                {
                    this.status = "I";
                }
                else if (this.comboBox2.SelectedIndex == 1)
                {
                    this.status = "F";
                }
                else if (this.comboBox2.SelectedIndex == 2)
                {
                    this.status = "A";
                }
                else if (this.comboBox2.SelectedIndex == 3)
                {
                    this.status = "H";
                }
                
                this.employer = this.textBox10.Text.ToUpper();

                this.returntypename = "SELF";

                if (this.returntype.SelectedIndex == 0)
                {
                    this.returntypename = "SELF";
                }
                else if (this.returntype.SelectedIndex == 1)
                {
                    this.returntypename = "UNIVERSAL SELF";
                }
                else
                {
                    this.returntypename = "NORMAL";
                }

                if (this.customerid > 0)
                {
                    taxDBDataSetcustomerTableAdapter.UpdateCustomerById(this.name, this.father, this.mother, this.spouce, this.disabledperson, this.sex, this.natid, this.tin, this.utin, DateTime.Parse(this.dateofbirth), this.presentadd, this.permanentadd, this.phoneoff, this.phonehome, this.vatreg, this.customerid);
                }
                else
                {
                    taxDBDataSetcustomerTableAdapter.Insert(this.name, this.father, this.mother, this.spouce, this.sex, this.natid, this.tin, this.utin, DateTime.Parse(this.dateofbirth), this.presentadd, "", "", this.permanentadd, "", "", "", "", "", this.phoneoff, this.phonehome, this.vatreg, "", "", this.disabledperson);
                    this.customerid = int.Parse(taxDBDataSetcustomerTableAdapter.SelectLID().Value.ToString());
                }

                if (this.returnid > 0)
                {
                    if (this.assessmentyear != this.oldassessmentyear)
                    {
                        copyreturn(this.returnid);
                    }
                    else
                    {
                        taxDBDataSetpersonalreturnTableAdapter.UpdatePR1(this.circle, this.zone, this.assessmentyear, this.residentialstatus, this.status, this.employer, DateTime.Now, this.returntypename, this.returnid);
                    }
                }
                else
                {
                    taxDBDataSetpersonalreturnTableAdapter.InsertPR1(this.circle, this.zone, this.assessmentyear, this.residentialstatus, this.status, this.employer, 0, DateTime.Now, DateTime.Now, this.customerid, this.returntypename);
                    this.returnid = int.Parse(taxDBDataSetpersonalreturnTableAdapter.SelectLID().Value.ToString());
                }

                taxDBDataSetcustomerTableAdapter.Update(taxDBDataSet);
                taxDBDataSetpersonalreturnTableAdapter.Update(taxDBDataSet);

                PRSalaryInformation PRSalaryInformationForm = new PRSalaryInformation(this.returnid);
                PRSalaryInformationForm.Owner = this.Owner;
                PRSalaryInformationForm.Show();
                this.Close();
            }
        }

        private void copyreturn(int returnid)
        {
            #region get data

            TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
            TaxSolution.TaxDBDataSetTableAdapters.personalreturnTableAdapter taxDBDataSetpersonalreturnTableAdapter = new TaxDBDataSetTableAdapters.personalreturnTableAdapter();
            DataTable rdt = taxDBDataSetpersonalreturnTableAdapter.GetDataById(this.returnid);
            if (rdt.Rows.Count > 0)
            {
                DataRow dr = rdt.Rows[0];
                string circle = this.circle;
                string zone = this.zone;
                string assessmentyear = this.assessmentyear;
                string residentialstatus = this.residentialstatus;
                string assesseestatus = this.status;
                string employer = this.employer;
                decimal soi1 = 0;
                try
                {
                    soi1 = decimal.Parse(dr["soi1"].ToString());
                }
                catch
                {
                }
                decimal soi2 = 0;
                try
                {
                    soi2 = decimal.Parse(dr["soi2"].ToString());
                }
                catch
                {
                }
                decimal soi3 = 0;
                try
                {
                    soi3 = decimal.Parse(dr["soi3"].ToString());
                }
                catch
                {
                }
                decimal soi4 = 0;
                try
                {
                    soi4 = decimal.Parse(dr["soi4"].ToString());
                }
                catch
                {
                }
                decimal soi5 = 0;
                try
                {
                    soi5 = decimal.Parse(dr["soi5"].ToString());
                }
                catch
                {
                }
                decimal soi6 = 0;
                try
                {
                    soi6 = decimal.Parse(dr["soi6"].ToString());
                }
                catch
                {
                }
                decimal soi7 = 0;
                try
                {
                    soi7 = decimal.Parse(dr["soi7"].ToString());
                }
                catch
                {
                }
                decimal soi8 = 0;
                try
                {
                    soi8 = decimal.Parse(dr["soi8"].ToString());
                }
                catch
                {
                }
                decimal soi9 = 0;
                try
                {
                    soi9 = decimal.Parse(dr["soi9"].ToString());
                }
                catch
                {
                }
                decimal soi11 = 0;
                try
                {
                    soi11 = decimal.Parse(dr["soi11"].ToString());
                }
                catch
                {
                }
                decimal soi13 = 0;
                try
                {
                    soi13 = decimal.Parse(dr["soi13"].ToString());
                }
                catch
                {
                }
                decimal soi14 = 0;
                try
                {
                    soi14 = decimal.Parse(dr["soi14"].ToString());
                }
                catch
                {
                }
                decimal soi16a = 0;
                try
                {
                    soi16a = decimal.Parse(dr["soi16a"].ToString());
                }
                catch
                {
                }
                decimal soi16b = 0;
                try
                {
                    soi16b = decimal.Parse(dr["soi16b"].ToString());
                }
                catch
                {
                }
                decimal soi16c = 0;
                try
                {
                    soi16c = decimal.Parse(dr["soi16c"].ToString());
                }
                catch
                {
                }
                decimal soi16d = 0;
                try
                {
                    soi16d = decimal.Parse(dr["soi16d"].ToString());
                }
                catch
                {
                }
                decimal soi18 = 0;
                try
                {
                    soi18 = decimal.Parse(dr["soi18"].ToString());
                }
                catch
                {
                }
                decimal soi19 = 0;
                try
                {
                    soi19 = decimal.Parse(dr["soi19"].ToString());
                }
                catch
                {
                }
                decimal salary1a = 0;
                try
                {
                    salary1a = decimal.Parse(dr["salary1a"].ToString());
                }
                catch
                {
                }
                decimal salary1b = 0;
                try
                {
                    salary1b = decimal.Parse(dr["salary1b"].ToString());
                }
                catch
                {
                }
                decimal salary2a = 0;
                try
                {
                    salary2a = decimal.Parse(dr["salary2a"].ToString());
                }
                catch
                {
                }
                decimal salary2b = 0;
                try
                {
                    salary2b = decimal.Parse(dr["salary2b"].ToString());
                }
                catch
                {
                }
                decimal salary3a = 0;
                try
                {
                    salary3a = decimal.Parse(dr["salary3a"].ToString());
                }
                catch
                {
                }
                decimal salary3b = 0;
                try
                {
                    salary3b = decimal.Parse(dr["salary3b"].ToString());
                }
                catch
                {
                }
                decimal salary4a = 0;
                try
                {
                    salary4a = decimal.Parse(dr["salary4a"].ToString());
                }
                catch
                {
                }
                decimal salary4b = 0;
                try
                {
                    salary4b = decimal.Parse(dr["salary4b"].ToString());
                }
                catch
                {
                }
                decimal salary5a = 0;
                try
                {
                    salary5a = decimal.Parse(dr["salary5a"].ToString());
                }
                catch
                {
                }
                decimal salary5b = 0;
                try
                {
                    salary5b = decimal.Parse(dr["salary5b"].ToString());
                }
                catch
                {
                }
                decimal salary6a = 0;
                try
                {
                    salary6a = decimal.Parse(dr["salary6a"].ToString());
                }
                catch
                {
                }
                decimal salary6b = 0;
                try
                {
                    salary6b = decimal.Parse(dr["salary6b"].ToString());
                }
                catch
                {
                }
                decimal salary7a = 0;
                try
                {
                    salary7a = decimal.Parse(dr["salary7a"].ToString());
                }
                catch
                {
                }
                decimal salary7b = 0;
                try
                {
                    salary7b = decimal.Parse(dr["salary7b"].ToString());
                }
                catch
                {
                }
                decimal salary8a = 0;
                try
                {
                    salary8a = decimal.Parse(dr["salary8a"].ToString());
                }
                catch
                {
                }
                decimal salary8b = 0;
                try
                {
                    salary8b = decimal.Parse(dr["salary8b"].ToString());
                }
                catch
                {
                }
                decimal salary9a = 0;
                try
                {
                    salary9a = decimal.Parse(dr["salary9a"].ToString());
                }
                catch
                {
                }
                decimal salary9b = 0;
                try
                {
                    salary9b = decimal.Parse(dr["salary9b"].ToString());
                }
                catch
                {
                }
                decimal salary10a = 0;
                try
                {
                    salary10a = decimal.Parse(dr["salary10a"].ToString());
                }
                catch
                {
                }
                decimal salary10b = 0;
                try
                {
                    salary10b = decimal.Parse(dr["salary10b"].ToString());
                }
                catch
                {
                }
                decimal salary11a = 0;
                try
                {
                    salary11a = decimal.Parse(dr["salary11a"].ToString());
                }
                catch
                {
                }
                decimal salary11b = 0;
                try
                {
                    salary11b = decimal.Parse(dr["salary11b"].ToString());
                }
                catch
                {
                }
                decimal salary12a = 0;
                try
                {
                    salary12a = decimal.Parse(dr["salary12a"].ToString());
                }
                catch
                {
                }
                decimal salary12b = 0;
                try
                {
                    salary12b = decimal.Parse(dr["salary12b"].ToString());
                }
                catch
                {
                }
                decimal salary13a = 0;
                try
                {
                    salary13a = decimal.Parse(dr["salary13a"].ToString());
                }
                catch
                {
                }
                decimal salary13b = 0;
                try
                {
                    salary13b = decimal.Parse(dr["salary13b"].ToString());
                }
                catch
                {
                }
                decimal salary14a = 0;
                try
                {
                    salary14a = decimal.Parse(dr["salary14a"].ToString());
                }
                catch
                {
                }
                decimal salary14b = 0;
                try
                {
                    salary14b = decimal.Parse(dr["salary14b"].ToString());
                }
                catch
                {
                }
                decimal salary15a = 0;
                try
                {
                    salary15a = decimal.Parse(dr["salary15a"].ToString());
                }
                catch
                {
                }
                decimal salary15b = 0;
                try
                {
                    salary15b = decimal.Parse(dr["salary15b"].ToString());
                }
                catch
                {
                }
                decimal salary16a = 0;
                try
                {
                    salary16a = decimal.Parse(dr["salary16a"].ToString());
                }
                catch
                {
                }
                decimal salary16b = 0;
                try
                {
                    salary16b = decimal.Parse(dr["salary16b"].ToString());
                }
                catch
                {
                }
                decimal salary17a = 0;
                try
                {
                    salary17a = decimal.Parse(dr["salary17a"].ToString());
                }
                catch
                {
                }
                decimal salary17b = 0;
                try
                {
                    salary17b = decimal.Parse(dr["salary17b"].ToString());
                }
                catch
                {
                }
                decimal salary18a = 0;
                try
                {
                    salary18a = decimal.Parse(dr["salary18a"].ToString());
                }
                catch
                {
                }
                decimal salary18b = 0;
                try
                {
                    salary18b = decimal.Parse(dr["salary18b"].ToString());
                }
                catch
                {
                }
                decimal salary19a = 0;
                try
                {
                    salary19a = decimal.Parse(dr["salary19a"].ToString());
                }
                catch
                {
                }
                decimal salary19b = 0;
                try
                {
                    salary19b = decimal.Parse(dr["salary19b"].ToString());
                }
                catch
                {
                }
                string houselocation = dr["houselocation"].ToString();
                decimal house1 = 0;
                try
                {
                    house1 = decimal.Parse(dr["house1"].ToString());
                }
                catch
                {
                }
                decimal house2a = 0;
                try
                {
                    house2a = decimal.Parse(dr["house2a"].ToString());
                }
                catch
                {
                }
                decimal house2b = 0;
                try
                {
                    house2b = decimal.Parse(dr["house2b"].ToString());
                }
                catch
                {
                }
                decimal house2c = 0;
                try
                {
                    house2c = decimal.Parse(dr["house2c"].ToString());
                }
                catch
                {
                }
                decimal house2d = 0;
                try
                {
                    house2d = decimal.Parse(dr["house2d"].ToString());
                }
                catch
                {
                }
                decimal house2e = 0;
                try
                {
                    house2e = decimal.Parse(dr["house2e"].ToString());
                }
                catch
                {
                }
                decimal house2f = 0;
                try
                {
                    house2f = decimal.Parse(dr["house2f"].ToString());
                }
                catch
                {
                }
                decimal house2g = 0;
                try
                {
                    house2g = decimal.Parse(dr["house2g"].ToString());
                }
                catch
                {
                }
                string housers = dr["housers"].ToString();
                decimal investment1 = 0;
                try
                {
                    investment1 = decimal.Parse(dr["investment1"].ToString());
                }
                catch
                {
                }
                decimal investment2 = 0;
                try
                {
                    investment2 = decimal.Parse(dr["investment2"].ToString());
                }
                catch
                {
                }
                decimal investment3 = 0;
                try
                {
                    investment3 = decimal.Parse(dr["investment3"].ToString());
                }
                catch
                {
                }
                decimal investment4 = 0;
                try
                {
                    investment4 = decimal.Parse(dr["investment4"].ToString());
                }
                catch
                {
                }
                decimal investment5 = 0;
                try
                {
                    investment5 = decimal.Parse(dr["investment5"].ToString());
                }
                catch
                {
                }
                decimal investment6 = 0;
                try
                {
                    investment6 = decimal.Parse(dr["investment6"].ToString());
                }
                catch
                {
                }
                decimal investment7 = 0;
                try
                {
                    investment7 = decimal.Parse(dr["investment7"].ToString());
                }
                catch
                {
                }
                decimal investment8 = 0;
                try
                {
                    investment8 = decimal.Parse(dr["investment8"].ToString());
                }
                catch
                {
                }
                decimal investment9 = 0;
                try
                {
                    investment9 = decimal.Parse(dr["investment9"].ToString());
                }
                catch
                {
                }
                decimal investment10 = 0;
                try
                {
                    investment10 = decimal.Parse(dr["investment10"].ToString());
                }
                catch
                {
                }
                decimal asset1a = 0;
                try
                {
                    asset1a = decimal.Parse(dr["asset1a"].ToString());
                }
                catch
                {
                }
                decimal asset4a = 0;
                try
                {
                    asset4a = decimal.Parse(dr["asset4a"].ToString());
                }
                catch
                {
                }
                decimal asset4b = 0;
                try
                {
                    asset4b = decimal.Parse(dr["asset4b"].ToString());
                }
                catch
                {
                }
                decimal asset4c = 0;
                try
                {
                    asset4c = decimal.Parse(dr["asset4c"].ToString());
                }
                catch
                {
                }
                decimal asset4d = 0;
                try
                {
                    asset4d = decimal.Parse(dr["asset4d"].ToString());
                }
                catch
                {
                }
                decimal asset4e = 0;
                try
                {
                    asset4e = decimal.Parse(dr["asset4e"].ToString());
                }
                catch
                {
                }
                decimal asset7 = 0;
                try
                {
                    asset7 = decimal.Parse(dr["asset7"].ToString());
                }
                catch
                {
                }
                decimal asset8 = 0;
                try
                {
                    asset8 = decimal.Parse(dr["asset8"].ToString());
                }
                catch
                {
                }
                decimal asset9a = 0;
                try
                {
                    asset9a = decimal.Parse(dr["asset9a"].ToString());
                }
                catch
                {
                }
                decimal asset9b = 0;
                try
                {
                    asset9b = decimal.Parse(dr["asset9b"].ToString());
                }
                catch
                {
                }
                decimal asset9c = 0;
                try
                {
                    asset9c = decimal.Parse(dr["asset9c"].ToString());
                }
                catch
                {
                }
                decimal asset11a = 0;
                try
                {
                    asset11a = decimal.Parse(dr["asset11a"].ToString());
                }
                catch
                {
                }
                decimal asset11b = 0;
                try
                {
                    asset11b = decimal.Parse(dr["asset11b"].ToString());
                }
                catch
                {
                }
                decimal asset11c = 0;
                try
                {
                    asset11c = decimal.Parse(dr["asset11c"].ToString());
                }
                catch
                {
                }
                decimal asset11d = 0;
                try
                {
                    asset11d = decimal.Parse(dr["asset11d"].ToString());
                }
                catch
                {
                }
                decimal asset13 = 0;
                try
                {
                    asset13 = decimal.Parse(dr["asset13"].ToString());
                }
                catch
                {
                }
                decimal asset15a = 0;
                try
                {
                    asset15a = decimal.Parse(dr["asset15a"].ToString());
                }
                catch
                {
                }
                decimal asset15badult = 0;
                try
                {
                    asset15badult = decimal.Parse(dr["asset15badult"].ToString());
                }
                catch
                {
                }
                decimal asset15bchild = 0;
                try
                {
                    asset15bchild = decimal.Parse(dr["asset15bchild"].ToString());
                }
                catch
                {
                }
                decimal asset17c = 0;
                try
                {
                    asset17c = decimal.Parse(dr["asset17c"].ToString());
                }
                catch
                {
                }
                decimal it10bb1a = 0;
                try
                {
                    it10bb1a = decimal.Parse(dr["it10bb1a"].ToString());
                }
                catch
                {
                }
                string it10bb1b = "";
                try
                {
                    it10bb1b = dr["it10bb1b"].ToString();
                }
                catch
                {
                }
                decimal it10bb2a = 0;
                try
                {
                    it10bb2a = decimal.Parse(dr["it10bb2a"].ToString());
                }
                catch
                {
                }
                string it10bb2b = "";
                try
                {
                    it10bb2b = dr["it10bb2b"].ToString();
                }
                catch
                {
                }
                decimal it10bb3a = 0;
                try
                {
                    it10bb3a = decimal.Parse(dr["it10bb3a"].ToString());
                }
                catch
                {
                }
                string it10bb3b = "";
                try
                {
                    it10bb3b = dr["it10bb3b"].ToString();
                }
                catch
                {
                }
                decimal it10bb4a = 0;
                try
                {
                    it10bb4a = decimal.Parse(dr["it10bb4a"].ToString());
                }
                catch
                {
                }
                string it10bb4b = "";
                try
                {
                    it10bb4b = dr["it10bb4b"].ToString();
                }
                catch
                {
                }
                decimal it10bb5a = 0;
                try
                {
                    it10bb5a = decimal.Parse(dr["it10bb5a"].ToString());
                }
                catch
                {
                }
                string it10bb5b = "";
                try
                {
                    it10bb5b = dr["it10bb5b"].ToString();
                }
                catch
                {
                }
                decimal it10bb6a = 0;
                try
                {
                    it10bb6a = decimal.Parse(dr["it10bb6a"].ToString());
                }
                catch
                {
                }
                string it10bb6b = "";
                try
                {
                    it10bb6b = dr["it10bb6b"].ToString();
                }
                catch
                {
                }
                decimal it10bb7a = 0;
                try
                {
                    it10bb7a = decimal.Parse(dr["it10bb7a"].ToString());
                }
                catch
                {
                }
                string it10bb7b = "";
                try
                {
                    it10bb7b = dr["it10bb7b"].ToString();
                }
                catch
                {
                }
                decimal it10bb8a = 0;
                try
                {
                    it10bb8a = decimal.Parse(dr["it10bb8a"].ToString());
                }
                catch
                {
                }
                string it10bb8b = "";
                try
                {
                    it10bb8b = dr["it10bb8b"].ToString();
                }
                catch
                {
                }
                decimal it10bb9a = 0;
                try
                {
                    it10bb9a = decimal.Parse(dr["it10bb9a"].ToString());
                }
                catch
                {
                }
                string it10bb9b = "";
                try
                {
                    it10bb9b = dr["it10bb9b"].ToString();
                }
                catch
                {
                }
                decimal it10bb10a = 0;
                try
                {
                    it10bb10a = decimal.Parse(dr["it10bb10a"].ToString());
                }
                catch
                {
                }
                string it10bb10b = "";
                try
                {
                    it10bb10b = dr["it10bb10b"].ToString();
                }
                catch
                {
                }
                decimal it10bb11a = 0;
                try
                {
                    it10bb11a = decimal.Parse(dr["it10bb11a"].ToString());
                }
                catch
                {
                }
                string it10bb11b = "";
                try
                {
                    it10bb11b = dr["it10bb11b"].ToString();
                }
                catch
                {
                }
                string returnnature = this.returntypename;
                DateTime incomeyear = DateTime.Parse("06/30/" + DateTime.Today.Year);
            #endregion
                taxDBDataSetpersonalreturnTableAdapter.Insert(circle, zone, assessmentyear, residentialstatus, assesseestatus, employer, soi1, soi2, soi3, soi4, soi5, soi6, soi7, soi8, soi9, soi11, soi13, soi14, soi16a, soi16b,
                            soi16c, soi16d, soi18, soi19, salary1a, salary1b, salary2a, salary2b, salary3a, salary3b, salary4a, salary4b, salary5a, salary5b, salary6a, salary6b, salary7a,
                            salary7b, salary8a, salary8b, salary9a, salary9b, salary10a, salary10b, salary11a, salary11b, salary12a, salary12b, salary13a, salary13b, salary14a, salary14b,
                            salary15a, salary15b, salary16a, salary16b, salary17a, salary17b, salary18a, salary18b, salary19a, salary19b, houselocation, house1, house2a, house2b, house2c,
                            house2d, house2e, house2f, house2g, investment1, investment2, investment3, investment4, investment5, investment6, investment7, investment8, investment9,
                            investment10, asset1a, asset4a, asset4b, asset4c, asset4d, asset4e, asset7, asset8, asset9a, asset9b, asset9c, asset11a, asset11b, asset11c, asset11d,
                            asset15a, asset15badult, asset15bchild, asset17c, it10bb1a, it10bb1b, it10bb2a, it10bb2b, it10bb3a, it10bb3b, it10bb4a, it10bb4b, it10bb5a, it10bb5b, it10bb6a,
                            it10bb6b, it10bb7a, it10bb7b, it10bb8a, it10bb8b, it10bb9a, it10bb9b, it10bb10a, it10bb10b, it10bb11a, it10bb11b, returnnature, 0, DateTime.Now, DateTime.Now,
                            this.customerid, this.returntypename, incomeyear, asset13, housers);
                this.returnid = int.Parse(taxDBDataSetpersonalreturnTableAdapter.SelectLID().Value.ToString());
                    
            }
        }

        private bool formvalidate()
        {
            bool ret = true;
            if (this.textBox1.Text == "")
            {
                this.textBox1.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox4.Text == "")
            {
                this.textBox4.BorderBrush = Brushes.Red;
                this.textBox9.BorderBrush = Brushes.Red;
                this.textBox14.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox4.Text != "")
            {
                string tin = this.textBox4.Text.ToString() + "-" + this.textBox9.Text.ToString() + "-" + this.textBox14.Text.ToString();
                if (tin.Length != 12)
                {
                    this.textBox4.BorderBrush = Brushes.Red;
                    this.textBox9.BorderBrush = Brushes.Red;
                    this.textBox14.BorderBrush = Brushes.Red;
                    ret = false;
                }
            }
            string utin = this.textBox3.Text.ToString() + "-" + this.textBox7.Text.ToString() + "-" + this.textBox8.Text.ToString();
            if (utin.Length != 12 && utin.Length != 2)
            {
                this.textBox3.BorderBrush = Brushes.Red;
                this.textBox7.BorderBrush = Brushes.Red;
                this.textBox8.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox5.Text == "")
            {
                this.textBox5.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox6.Text == "")
            {
                this.textBox6.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox20.Text == "")
            {
                this.textBox20.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.comboBox1.SelectedIndex < 0)
            {
                this.comboBox1.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.comboBox2.SelectedIndex < 0)
            {
                this.comboBox2.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox12.Text == "")
            {
                this.textBox12.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox13.Text == "")
            {
                this.textBox13.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.datePicker2.SelectedDate == null)
            {
                this.datePicker2.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.textBox15.Text == "")
            {
                this.textBox15.BorderBrush = Brushes.Red;
                ret = false;
            }
            return ret;
        }

        private void closeBtn_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void PrevBtn_Click(object sender, MouseButtonEventArgs e)
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
            if (this.customerid > 0 && this.returnid > 0)
            {
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
            else
            {
                MessageBox.Show("You have to complete this form first to navigate to any other form.");
            }
        }
    }
}
