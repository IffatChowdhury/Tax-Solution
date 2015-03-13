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
using System.Printing;
using System.Windows.Xps;

namespace TaxSolution
{
    /// <summary>
    /// Interaction logic for TSPrintPreview.xaml
    /// </summary>
    public partial class TSPrintPreview : Window
    {
        public TSPrintPreview()
        {
            InitializeComponent();
        }

        public IDocumentPaginatorSource Document
        {
            get { return _viewer.Document; }
            set { _viewer.Document = value; }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;
            printDialog.UserPageRangeEnabled = true;
            
            printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;

            if ((bool)printDialog.ShowDialog() == true)
            {
                try
                {
                    // Code assumes this.Document will either by a FixedDocument or a FixedDocumentSequence
                    FixedDocument fixedDocument = this._viewer.Document as FixedDocument;
                    FixedDocumentSequence fixedDocumentSequence = this._viewer.Document as FixedDocumentSequence;

                    if (fixedDocument != null)
                        fixedDocument.PrintTicket = printDialog.PrintTicket;

                    if (fixedDocumentSequence != null)
                        fixedDocumentSequence.PrintTicket = printDialog.PrintTicket;

                    XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);

                    if (fixedDocument != null)
                        writer.WriteAsync(fixedDocument, printDialog.PrintTicket);

                    if (fixedDocumentSequence != null)
                        writer.WriteAsync(fixedDocumentSequence, printDialog.PrintTicket);
                }
                catch
                {
                    MessageBox.Show("Error occured while sending print request!");
                }
            }
        }
    }
}
