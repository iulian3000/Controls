using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ion.Controls
{
    [TemplatePart(Name = "PART_border", Type = typeof(Border))]
    [TemplatePart(Name = "PART_image", Type = typeof(Image))]
    public class ZoomPanBox : Control
    {
        private Point contentPosition;

        private bool mouseDown;

        private Border PART_border;

        private Image PART_image;

        private Point startPoint;

        static ZoomPanBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomPanBox), new FrameworkPropertyMetadata(typeof(ZoomPanBox)));
            //WidthProperty.OverrideMetadata(typeof(ZoomPanBox), new FrameworkPropertyMetadata(150d));
            //HeightProperty.OverrideMetadata(typeof(ZoomPanBox), new FrameworkPropertyMetadata(150d));
        }

        public ZoomPanBox()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Template != null)
            {
                PART_border = GetTemplateChild("PART_border") as Border;
                PART_image = GetTemplateChild("PART_image") as Image;

                PART_border.PreviewMouseLeftButtonDown += PART_border_PreviewMouseLeftButtonDown;
                PART_border.PreviewMouseMove += PART_border_PreviewMouseMove;
                PART_border.PreviewMouseLeftButtonUp += PART_border_PreviewMouseLeftButtonUp;
                PART_border.PreviewMouseRightButtonDown += PART_border_PreviewMouseRightButtonDown;
                PART_border.PreviewMouseWheel += PART_border_PreviewMouseWheel;
            }
        }

        public void ResetZoomPan()
        {
            Matrix m = new Matrix();
            m.SetIdentity();
            PART_image.RenderTransform = new MatrixTransform(m);
        }

        private void PART_border_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseDown = true;
            startPoint = e.GetPosition(PART_border);
            contentPosition.X = PART_image.RenderTransform.Value.OffsetX;
            contentPosition.Y = PART_image.RenderTransform.Value.OffsetY;
            e.Handled = true;
            PART_border.CaptureMouse();
        }

        private void PART_border_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mouseDown = false;
            PART_border.ReleaseMouseCapture();
        }

        private void PART_border_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mouseDown)
            {
                var pct = e.GetPosition(PART_border);
                var matrix = PART_image.RenderTransform.Value;

                matrix.OffsetX = contentPosition.X + pct.X - startPoint.X;
                matrix.OffsetY = contentPosition.Y + pct.Y - startPoint.Y;

                PART_image.RenderTransform = new MatrixTransform(matrix);
            }
        }

        private void PART_border_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ResetZoomPan();
        }

        private void PART_border_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var pct = e.GetPosition(PART_image);

            Matrix matrix = PART_image.RenderTransform.Value;

            double scale = e.Delta > 0 ? 1.2 : 1 / 1.2;

            matrix.ScaleAtPrepend(scale, scale, pct.X, pct.Y);

            PART_image.RenderTransform = new MatrixTransform(matrix);
        }

        private void SaveImage()
        {
            //TODO save image
        }

        #region DP

        public static readonly DependencyProperty ImagineProperty =
            DependencyProperty.Register("Imagine",
            typeof(ImageSource), typeof(ZoomPanBox),
            new PropertyMetadata(null));

        public ImageSource Imagine
        {
            get { return (ImageSource)GetValue(ImagineProperty); }
            set { SetValue(ImagineProperty, value); }
        }

        #endregion DP
    }
}