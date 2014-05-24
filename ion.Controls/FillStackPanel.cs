//using System;
//using System.ComponentModel;
//using System.Windows;
//using System.Windows.Controls;

//namespace ion.Controls
//{
//    public class FillStackPanel : Panel
//    {
//        public static readonly DependencyProperty HorizontalFillProperty =
//            DependencyProperty.RegisterAttached("HorizontalFill", typeof(bool), typeof(FillStackPanel),
//            new FrameworkPropertyMetadata(false,
//                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

//        public static readonly DependencyProperty InitialSizeProperty =
//            DependencyProperty.RegisterAttached("InitialSize", typeof(Size), typeof(FillStackPanel),
//            new PropertyMetadata(new Size(double.NaN, double.NaN)));

//        public static readonly DependencyProperty OrientationProperty =
//            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FillStackPanel),
//            new FrameworkPropertyMetadata(Orientation.Horizontal,
//                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

//        public static readonly DependencyProperty VerticalFillProperty =
//            DependencyProperty.RegisterAttached("VerticalFill", typeof(bool), typeof(FillStackPanel),
//            new FrameworkPropertyMetadata(false,
//                FrameworkPropertyMetadataOptions.AffectsParentMeasure));



//        [Category("Layout")]
//        public Orientation Orientation
//        {
//            get { return (Orientation)GetValue(OrientationProperty); }
//            set { SetValue(OrientationProperty, value); }
//        }

//        [AttachedPropertyBrowsableForChildren]
//        [Category("Layout")]
//        public static bool GetHorizontalFill(DependencyObject obj)
//        {
//            return (bool)obj.GetValue(HorizontalFillProperty);
//        }

//        [AttachedPropertyBrowsableForChildren]
//        [Category("Layout")]
//        public static Size GetInitialSize(DependencyObject obj)
//        {
//            return (Size)obj.GetValue(InitialSizeProperty);
//        }

//        [AttachedPropertyBrowsableForChildren]
//        [Category("Layout")]
//        public static bool GetVerticalFill(DependencyObject obj)
//        {
//            return (bool)obj.GetValue(VerticalFillProperty);
//        }

//        public static void SetHorizontalFill(DependencyObject obj, bool value)
//        {
//            obj.SetValue(HorizontalFillProperty, value);
//        }

//        public static void SetInitialSize(DependencyObject obj, Size value)
//        {
//            obj.SetValue(InitialSizeProperty, value);
//        }

//        public static void SetVerticalFill(DependencyObject obj, bool value)
//        {
//            obj.SetValue(VerticalFillProperty, value);
//        }

//        public FillStackPanel()
//        {
//            this.Loaded += FillStackPanel_Loaded;
//        }

//        void FillStackPanel_Loaded(object sender, RoutedEventArgs e)
//        {
//            InvalidateMeasure();
//            InvalidateArrange();
//        }


//        // arrange items within contrains
//        protected override Size ArrangeOverride(Size arrangeSize)
//        {
//            var rect = new Rect();
//            double x = 0, y = 0;

//            foreach (FrameworkElement item in InternalChildren)
//            {
//                switch (Orientation)
//                {
//                    case Orientation.Horizontal:
//                        rect.X = rect.X;
//                        rect.Y = rect.Y;
//                        rect.Width = item.DesiredSize.Width;
//                        rect.Height = item.DesiredSize.Height;

//                        item.Arrange(rect);

//                        rect.X += rect.Width;
//                        rect.Y = 0;

//                        break;

//                    case Orientation.Vertical:
//                        rect.X = rect.X;
//                        rect.Y = rect.Y;
//                        rect.Width = item.DesiredSize.Width;
//                        rect.Height = item.DesiredSize.Height;

//                        item.Arrange(rect);

//                        rect.X = 0;
//                        rect.Y += rect.Height;

//                        break;
//                }
//            }

//            return arrangeSize;
//        }



//        // calculate itemsSize
//        // return size of the panel
//        protected override Size MeasureOverride(Size constraint)
//        {
//            var finalSize = new Size();
//            double size = GetSizeItemWithFill(constraint); // <= measures items returns size
//            bool hFill, vFill;
//            Size initialSize;

//            switch (Orientation)
//            {
//                case Orientation.Horizontal:
//                    foreach (FrameworkElement item in InternalChildren)
//                    {
//                        hFill = (bool)item.GetValue(HorizontalFillProperty);
//                        vFill = (bool)item.GetValue(VerticalFillProperty);
//                        initialSize = (Size)item.GetValue(InitialSizeProperty);

//                        item.Width = hFill ? size : initialSize.Width;
//                        item.Height = vFill ? constraint.Height == double.PositiveInfinity ? initialSize.Height : constraint.Height : initialSize.Height;

//                        finalSize.Width += item.DesiredSize.Width;
//                        finalSize.Height = Math.Max(item.DesiredSize.Height, finalSize.Height);
//                    }
//                    break;

//                case Orientation.Vertical:
//                    foreach (FrameworkElement item in InternalChildren)
//                    {
//                        hFill = (bool)item.GetValue(HorizontalFillProperty);
//                        vFill = (bool)item.GetValue(VerticalFillProperty);
//                        initialSize = (Size)item.GetValue(InitialSizeProperty);

//                        item.Width = hFill ? constraint.Width == double.PositiveInfinity ? initialSize.Width : constraint.Width : initialSize.Width;
//                        item.Height = vFill ? size : initialSize.Height;

//                        finalSize.Width = Math.Max(item.DesiredSize.Width, finalSize.Width);
//                        finalSize.Height += item.DesiredSize.Height;
//                    }
//                    break;
//            }

//            return finalSize;
//        }


//        private double GetSizeItemWithFill(Size constraint)
//        {
//            int fillCount = 0;
//            bool fill = false;
//            double size = double.NaN;
//            double avaibleSize = 0;

//            avaibleSize = Orientation == System.Windows.Controls.Orientation.Horizontal ?
//                constraint.Width : constraint.Height;

//            foreach (FrameworkElement item in InternalChildren)
//            {
//                item.Measure(constraint);

//                switch (Orientation)
//                {
//                    case Orientation.Horizontal:
//                        fill = (bool)item.GetValue(HorizontalFillProperty);
//                        if (fill)
//                            fillCount++;
//                        else
//                            avaibleSize -= item.DesiredSize.Width;
//                        break;

//                    case Orientation.Vertical:
//                        fill = (bool)item.GetValue(HorizontalFillProperty);
//                        if (fill)
//                            fillCount++;
//                        else
//                            avaibleSize -= item.DesiredSize.Height;
//                        break;
//                }
//            }

//            var tmpSize = avaibleSize / fillCount;

//            //tmpSize = tmpSize < 0 ? 0 : tmpSize;

//            size = fillCount > 0 ? tmpSize : size;

//            size = double.IsInfinity(size) ? double.NaN : size;

//            return size;
//        }
//    }
//}