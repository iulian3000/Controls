using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ion.Controls
{
    public class MyCanvas : Panel
    {

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.RegisterAttached("Left", typeof(double), typeof(MyCanvas), new FrameworkPropertyMetadata(double.NegativeInfinity, FrameworkPropertyMetadataOptions.AffectsParentArrange));


        public static readonly DependencyProperty TopProperty =
            DependencyProperty.RegisterAttached("Top", typeof(double), typeof(MyCanvas), new FrameworkPropertyMetadata(double.NegativeInfinity, FrameworkPropertyMetadataOptions.AffectsParentArrange));

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static double GetLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(LeftProperty);
        }

        [AttachedPropertyBrowsableForChildren]
        [Category("Layout")]
        public static double GetTop(DependencyObject obj)
        {
            return (double)obj.GetValue(TopProperty);
        }

        public static void SetLeft(DependencyObject obj, double value)
        {
            obj.SetValue(LeftProperty, value);
        }
        public static void SetTop(DependencyObject obj, double value)
        {
            obj.SetValue(TopProperty, value);
        }


        #region Temp left right







        public static double GetPreviousLeft(DependencyObject obj)
        {
            return (double)obj.GetValue(PreviousLeftProperty);
        }

        public static void SetPreviousLeft(DependencyObject obj, double value)
        {
            obj.SetValue(PreviousLeftProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviousLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousLeftProperty =
            DependencyProperty.RegisterAttached("PreviousLeft", typeof(double), typeof(MyCanvas), new PropertyMetadata(double.NaN));






        public static double GetPreviousTop(DependencyObject obj)
        {
            return (double)obj.GetValue(PreviousTopProperty);
        }

        public static void SetPreviousTop(DependencyObject obj, double value)
        {
            obj.SetValue(PreviousTopProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviousTop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviousTopProperty =
            DependencyProperty.RegisterAttached("PreviousTop", typeof(double), typeof(MyCanvas), new PropertyMetadata(double.NaN));



        #endregion
      



        protected override Size MeasureOverride(Size availableSize)
        {
            Size avSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement uiElement in this.InternalChildren)
            {
                if (uiElement != null)
                    uiElement.Measure(avSize);
            }
            return new Size();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {

            foreach (UIElement element in this.InternalChildren)
            {
                if (element != null )
                {
                    double x = 0.0;
                    double y = 0.0;
                    double prevLeft = (double)element.GetValue(PreviousLeftProperty);
                    double prevTop = (double)element.GetValue(PreviousTopProperty);

                    double left = MyCanvas.GetLeft(element);
                    if (!double.IsNaN(left))
                    {
                        x = left;
                    }

                    double top = MyCanvas.GetTop(element);
                    if (!double.IsNaN(top))
                    {
                        y = top;
                    }

                    if (prevLeft != left || prevTop != top)
                    {
                        element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
                        element.SetValue(PreviousLeftProperty, x);
                        element.SetValue(PreviousTopProperty, y); 
                    }
                }
            }
            return finalSize;
        }
    }
}
