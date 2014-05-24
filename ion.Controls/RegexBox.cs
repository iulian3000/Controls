using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace ion.Controls
{
    public class RegexBox : TextBox
    {
        public static readonly DependencyProperty AllowSpaceProperty =
            DependencyProperty.Register("AllowSpace", typeof(bool), typeof(RegexBox), new PropertyMetadata(false));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty RegexStringProperty =
            DependencyProperty.Register("RegexString", typeof(string), typeof(RegexBox), new PropertyMetadata("[^/]"));

        [Category("MySettings")]
        /// <summary>
        /// Enalbles or Disables Space key, default false
        /// </summary>
        public bool AllowSpace
        {
            get { return (bool)GetValue(AllowSpaceProperty); }
            set { SetValue(AllowSpaceProperty, value); }
        }

        [Category("MySettings")]
        /// <summary>
        /// [^/] =&gt; all characters ^(\-)?[0-9]{0,}?$ =&gt; integer ^(\-?[0-9]{0,})(\.[0-9]{0,})?$
        /// =&gt; double
        /// </summary>
        public string RegexString
        {
            get { return (string)GetValue(RegexStringProperty); }
            set { SetValue(RegexStringProperty, value); }
        }

        protected override void OnPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (!AllowSpace && e.Key == System.Windows.Input.Key.Space)
            {
                e.Handled = true;
                return;
            }

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                var be = GetBindingExpression(TextProperty);
                be.UpdateSource();
            }
        }

        protected override void OnPreviewTextInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            var txt = this.Text;

            txt = txt.Remove(this.SelectionStart, this.SelectionLength);
            txt = txt.Insert(this.SelectionStart, e.Text);

            if (Regex.Match(txt, RegexString).Success == false)
            {
                e.Handled = true;
                return;
            }
        }
    }
}