using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_UI
{
    /// <summary>
    /// Interaction logic for NumericUpdownControl.xaml
    /// </summary>
    public partial class NumericUpDownControl : UserControl
    {
        //private float? num = null;
        //public float? Value
        //{
        //    get { return num; }
        //    set
        //    {
        //        if (value > MaxValue)
        //            num = MaxValue;
        //        else if (value < MinValue)
        //            num = MinValue;
        //        else
        //            num = value;

        //        textNumber.Text = num == null ? "" : num.ToString();
        //    }
        //}



        public float? Value
        {
            get { return (float?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(float?), typeof(NumericUpDownControl),
                new PropertyMetadata(null, PropertyChangedCallback, ValueCoerceValueCallback));



        public static object ValueCoerceValueCallback(DependencyObject d, object baseValue)
        {
            float? value = baseValue as float?;
            NumericUpDownControl THIS = d as NumericUpDownControl;
            if (value > THIS.MaxValue)
                return (float?) THIS.MaxValue;
            else if (value < THIS.MinValue)
                return (float?) THIS.MinValue;
            else return value;
        }

        public static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDownControl THIS = d as NumericUpDownControl;
            THIS.textNumber.Text = THIS.Value == null ? "" : THIS.Value.ToString(); }


        public int MinValue { get; set; }
        //  public int MaxValue { get; set; }



        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUpDownControl), new PropertyMetadata(100));





        public NumericUpDownControl()
        {
            InitializeComponent();
            MaxValue = 100;
        }



        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textNumber == null || textNumber.Text == "" || textNumber.Text == "-")
            {
                Value = null;
                return;
            }

            float val;
            if (!float.TryParse(textNumber.Text, out val))
                textNumber.Text = Value.ToString();
            else
                Value = val;
        }
    }
}
