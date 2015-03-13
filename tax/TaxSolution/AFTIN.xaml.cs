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

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class AFTIN : Window
    {
        public AFTIN()
        {
            InitializeComponent();
        }

        private bool formvalidate()
        {
            bool ret = true;

            if (this.AFname.Text == "")
            {
                this.expander1.BorderBrush = Brushes.Red;
                this.AFname.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.CAa.Text == "")
            {
                this.expander4.BorderBrush = Brushes.Red;
                this.CAa.BorderBrush = Brushes.Red;
                ret = false;
            }
            if (this.CAd.Text == "")
            {
                this.expander4.BorderBrush = Brushes.Red;
                this.CAd.BorderBrush = Brushes.Red;
                ret = false;
            }
            //if (this.Ic.Text == "")
            //{
            //    this.expander9.BorderBrush = Brushes.Red;
            //    this.Ic.BorderBrush = Brushes.Red;
            //    ret = false;
            //}
            //if (this.AFChallanDate.SelectedDate == null)
            //{
            //    this.expander9.BorderBrush = Brushes.Red;
            //    this.AFChallanDate.BorderBrush = Brushes.Red;
            //    ret = false;
            //}

            return ret;
        }

        private void NextBtn_Click(object sender, MouseEventArgs e)
        {
            if (formvalidate())
            {
                string name = this.AFname.Text.ToString();
                string fname = this.AFfathername.Text.ToString();
                string mname = this.AFmothername.Text.ToString();
                string dob = "";
                if (this.AFdob.SelectedDate != null)
                {
                    dob = this.AFdob.SelectedDate.Value.ToShortDateString();
                }
                else
                {
                    dob = DateTime.Parse("1/1/1900").ToShortDateString();
                }
                string husband = this.AFhusbandname.Text.ToString();
                string nat = "";
                if (this.NATb.IsChecked.Value)
                {
                    nat = "a";
                }
                else if (this.NATp.IsChecked.Value)
                {
                    nat = "b";
                }
                else
                {
                    nat = "c";
                }
                string[,] natc = new string[7, 2];

                natc[0, 0] = this.tinNamea.Text.ToString();
                natc[0, 1] = this.tina.Text.ToString();

                natc[1, 0] = this.tinNameb.Text.ToString();
                natc[1, 1] = this.tinb.Text.ToString();

                natc[2, 0] = this.tinNamec.Text.ToString();
                natc[2, 1] = this.tinc.Text.ToString();

                natc[3, 0] = this.tinNamed.Text.ToString();
                natc[3, 1] = this.tind.Text.ToString();

                natc[4, 0] = this.tinNamee.Text.ToString();
                natc[4, 1] = this.tine.Text.ToString();

                natc[5, 0] = this.tinNamef.Text.ToString();
                natc[5, 1] = this.tinf.Text.ToString();

                natc[6, 0] = this.tinNameg.Text.ToString();
                natc[6, 1] = this.ting.Text.ToString();

                string incno = this.AFIno.Text.ToString();
                string incdate = "";
                if (this.AFIdate.SelectedDate != null)
                {
                    incdate = this.AFIdate.SelectedDate.Value.ToShortDateString();
                }
                else
                {
                    incdate = DateTime.Parse("1/1/1900").ToShortDateString();
                }

                string cad = this.CAa.Text.ToString();
                string cadd = this.CAd.Text.ToString();
                string cadp = this.CApc.Text.ToString();

                string pad = this.PAa.Text.ToString();
                string padd = this.PAd.Text.ToString();
                string padp = this.PApc.Text.ToString();

                string oad = this.OAa.Text.ToString();
                string oadd = this.OAd.Text.ToString();
                string oadp = this.OApc.Text.ToString();

                string tel = this.Ttn.Text.ToString();
                string fax = this.Tfn.Text.ToString();
                string email = this.Tema.Text.ToString();

                string[] vatreg = new string[] { 
                                        this.vatrega.Text.ToString(),
                                        this.vatregb.Text.ToString(),
                                        this.vatregc.Text.ToString(),
                                        this.vatregd.Text.ToString(),
                                        this.vatrege.Text.ToString()
                                    };

                string challan = this.Ic.Text.ToString();
                string challandate = "";
                if(this.AFChallanDate.SelectedDate != null)
                {
                    challandate = this.AFChallanDate.SelectedDate.Value.ToShortDateString();
                }
                else
                {
                    challandate = DateTime.Parse("1/1/1900").ToShortDateString();
                }

                string nameofbank = this.Inb.Text.ToString();
                string nameofbranch = this.Inbr.Text.ToString();

                string natid = this.NId.Text.ToString();

                TaxSolution.TaxDBDataSet taxDBDataSet = ((TaxSolution.TaxDBDataSet)(this.FindResource("taxDBDataSet")));
                // Load data into the table customer. You can modify this code as needed.
                TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter taxDBDataSetcustomerTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.customerTableAdapter();
                TaxSolution.TaxDBDataSetTableAdapters.tinTableAdapter taxDBDataSettinTableAdapter = new TaxSolution.TaxDBDataSetTableAdapters.tinTableAdapter();
                
                taxDBDataSetcustomerTableAdapter.Insert(
                    name, 
                    fname, 
                    mname, 
                    husband, 
                    "", 
                    natid, 
                    "", 
                    "", 
                    DateTime.Parse(dob), 
                    cad, 
                    cadd, 
                    cadp, 
                    pad, 
                    padd, 
                    padp, 
                    oad, 
                    oadd, 
                    oadp, 
                    tel, 
                    "", 
                    vatreg[0], 
                    email, 
                    fax, 
                    0
                );
                
                taxDBDataSetcustomerTableAdapter.Update(taxDBDataSet);
                int cid = int.Parse(taxDBDataSetcustomerTableAdapter.SelectLID().Value.ToString());

                taxDBDataSettinTableAdapter.Insert(cid, 
                                                    nat, 
                                                    natc[0, 0], natc[1, 0], natc[2, 0], natc[3, 0], natc[4, 0], natc[5, 0], natc[6, 0], 
                                                    natc[0, 1], natc[1, 1], natc[2, 1], natc[3, 1], natc[4, 1], natc[5, 1], natc[6, 1], 
                                                    incno, DateTime.Parse(incdate), 
                                                    cad, cadd, cadp, 
                                                    pad, padd, padp, 
                                                    oad, oadd, oadp, 
                                                    vatreg[0], vatreg[1], vatreg[2], vatreg[3], vatreg[4], 
                                                    challan, DateTime.Parse(challandate), nameofbank, nameofbranch);
                taxDBDataSettinTableAdapter.Update(taxDBDataSet);

                TinPrint TinPrintForm = new TinPrint(cid);
                TinPrintForm.Owner = this.Owner;
                TinPrintForm.Show();
                this.Close();
            }
        }

        private void closeBtn_Click(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void samePermanentAddress_Checked(object sender, RoutedEventArgs e)
        {
            if (this.CAa.Text.ToString() != "")
            {
                this.PAa.Text = this.CAa.Text;
                this.PAd.Text = this.CAd.Text;
                this.PApc.Text = this.CApc.Text;
            }
            else
            {
                this.CAa.BorderBrush = Brushes.Red;
                this.samePermanentAddress.IsChecked = false;
            }
        }

        private void samePermanentAddress_Unchecked(object sender, RoutedEventArgs e)
        {
            this.PAa.Text = "";
            this.PAd.Text = "";
            this.PApc.Text = "";
        }

        private void sameOtherAddress_Checked(object sender, RoutedEventArgs e)
        {
            if (this.CAa.Text.ToString() != "")
            {
                this.OAa.Text = this.CAa.Text;
                this.OAd.Text = this.CAd.Text;
                this.OApc.Text = this.CApc.Text;
            }
            else
            {
                this.CAa.BorderBrush = Brushes.Red;
                this.samePermanentAddress.IsChecked = false;
            }
        }

        private void sameOtherAddress_Unchecked(object sender, RoutedEventArgs e)
        {
            this.OAa.Text = "";
            this.OAd.Text = "";
            this.OApc.Text = "";
        }
    }
}
