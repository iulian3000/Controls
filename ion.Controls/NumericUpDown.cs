using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Ion.Controls
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value).ToString(CultureInfo.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;

            double tempValue;
            double.TryParse(
                str,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out tempValue);

            return tempValue;
        }
    }

    public class NumericUpDown : Control
    {
        public static readonly DependencyProperty DecimalsProperty =
            DependencyProperty.Register("Decimals", typeof(int), typeof(NumericUpDown), new PropertyMetadata(1, OnDecimalsChanged));

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(NumericUpDown), new PropertyMetadata(100d, OnMaxValueChanged));

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(NumericUpDown), new PropertyMetadata(-100d, OnMinValueChanged));

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(double), typeof(NumericUpDown), new PropertyMetadata(0.1d));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericUpDown), new PropertyMetadata(0d, OnValueChanged, OnValueCoerce));

        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public NumericUpDown()
        {
            IncreaseValueCommand = new RoutedUICommand("Increase Value", "IncreaseValue", typeof(NumericUpDown));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new CommandBinding(IncreaseValueCommand, OnIncreaseValueCommandExecuted, OnIncreaseValueCommandCanExecute));

            DecreaseValueCommand = new RoutedUICommand("Decrease Value", "DecreaseValue", typeof(NumericUpDown));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new CommandBinding(DecreaseValueCommand, OnDecreaseValueCommandExecuted, OnDecreaseValueCommandCanExecute));
        }

        public static ICommand DecreaseValueCommand { get; set; }

        public static ICommand IncreaseValueCommand { get; set; }

        public static ICommand NegateValueCommand { get; set; }

        public static ICommand ResetValueCommand { get; set; }

        [Category("MySettings")]
        public int Decimals
        {
            get { return (int)GetValue(DecimalsProperty); }
            set { SetValue(DecimalsProperty, value); }
        }

        [Category("MySettings")]
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        [Category("MySettings")]
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        [Category("MySettings")]
        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        [Category("MySettings")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnDecimalsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumericUpDown).CoerceValue(ValueProperty);
        }

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumericUpDown).CoerceValue(ValueProperty);
        }

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NumericUpDown).CoerceValue(ValueProperty);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static object OnValueCoerce(DependencyObject d, object baseValue)
        {
            var value = (double)baseValue;
            var numericBox = d as NumericUpDown;

            if (value < numericBox.MinValue)
                value = numericBox.MinValue;
            else if (value > numericBox.MaxValue)
                value = numericBox.MaxValue;
            else
                value = Math.Round(value, numericBox.Decimals);

            return value;
        }

        private void OnDecreaseValueCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Value > MinValue;
        }

        private void OnDecreaseValueCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Value -= Step;
        }

        private void OnIncreaseValueCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Value < MaxValue;
        }

        private void OnIncreaseValueCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Value += Step;
        }
    }
}