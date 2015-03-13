using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Documents;

namespace TaxSolution
{
    class FindChildren
    {
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }

    public class FlowDocPaginator : DocumentPaginator
    {
        private DocumentPaginator _paginator;

        public FlowDocPaginator(FlowDocument flowDoc)
        {
            _paginator = ((IDocumentPaginatorSource)flowDoc).DocumentPaginator;

        }

        public override bool IsPageCountValid
        {
            get { return _paginator.IsPageCountValid; }
        }

        public override int PageCount
        {
            get { return _paginator.PageCount; }
        }

        public override Size PageSize
        {
            get { return _paginator.PageSize; }
            set { _paginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return _paginator.Source; }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            // Get the requested page.
            DocumentPage page = _paginator.GetPage(pageNumber);

            // Wrap the page in a Visual object. You can then apply transformations
            // and add other elements.
            ContainerVisual newVisual = new ContainerVisual();
            newVisual.Children.Add(page.Visual);

            // Create a header.
            DrawingVisual header = new DrawingVisual();

            using (DrawingContext dc = header.RenderOpen())
            {
                Typeface typeface = new Typeface("Arial");
                FormattedText text = new FormattedText("" +
                                                       (pageNumber + 1).ToString(), CultureInfo.CurrentCulture,
                                                       FlowDirection.LeftToRight, typeface, 12, Brushes.Black);

                // Leave a quarter inch of space between the page edge and this text.
                dc.DrawText(text, new Point(96 * 4, 96 * 10.4));
            }

            // Add the title to the visual.
            newVisual.Children.Add(header);

            // Wrap the visual in a new page.
            DocumentPage newPage = new DocumentPage(newVisual);
            return newPage;
        }

    }
}
