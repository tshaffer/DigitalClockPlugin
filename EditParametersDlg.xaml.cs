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

namespace DigitalClockPlugin
{
    /// <summary>
    /// Interaction logic for EditParametersDlg.xaml
    /// </summary>
    public partial class EditParametersDlg : Window
    {
        private string _foregroundTextColor = "FFA0A0A0";

        public EditParametersDlg()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private string GetCBValue(ComboBox cb)
        {
            int selectedIndex = cb.SelectedIndex;
            if (selectedIndex < 0) selectedIndex = 0;
            ComboBoxItem cbi = (ComboBoxItem)cb.Items[selectedIndex];
            return (string)cbi.Tag;
        }

        private void SetCBValue(ComboBox cb, string value)
        {
            int selectedIndex = 0;
            int index = 0;
            foreach (ComboBoxItem cbi in cb.Items)
            {
                if ((string)cbi.Tag == value)
                {
                    selectedIndex = index;
                    break;
                }
                index++;
            }
            cb.SelectedIndex = selectedIndex;
        }

        public string Format
        {
            get { return GetCBValue(cbFormat); }
            set { SetCBValue(cbFormat, value); }
        }

        public string Size
        {
            get 
            {
                if ((bool)rbAutoSize.IsChecked)
                {
                    return "-1";
                }
                else
                {
                    return GetCBValue(cbFontSize);
                }
            }
            set
            {
                if (value == "-1")
                {
                    rbAutoSize.IsChecked = true;
                }
                else
                {
                    rbFixedSize.IsChecked = true;
                    SetCBValue(cbFontSize, value);
                }
            }
        }

        public String Color
        {
            get
            {
                return _foregroundTextColor;
            }
            set
            {
                _foregroundTextColor = value;
                int argb = Int32.Parse(_foregroundTextColor, System.Globalization.NumberStyles.AllowHexSpecifier);

                string red = _foregroundTextColor.Substring(2, 2);
                byte redValue = Byte.Parse(red, System.Globalization.NumberStyles.AllowHexSpecifier);
                string green = _foregroundTextColor.Substring(4, 2);
                byte greenValue = Byte.Parse(green, System.Globalization.NumberStyles.AllowHexSpecifier);
                string blue = _foregroundTextColor.Substring(6, 2);
                byte blueValue = Byte.Parse(blue, System.Globalization.NumberStyles.AllowHexSpecifier);

                SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, redValue, greenValue, blueValue));
                tbTextColor.Foreground = brush;
            }
        }

        private void btnTextColor_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog clrdlg = new System.Windows.Forms.ColorDialog();

            int argbInit = Int32.Parse(_foregroundTextColor, System.Globalization.NumberStyles.AllowHexSpecifier);
            System.Drawing.Color clr = System.Drawing.Color.FromArgb(argbInit);
            clrdlg.Color = clr;

            if (clrdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int argb = clrdlg.Color.ToArgb();

                _foregroundTextColor = argb.ToString("X");

                string red = _foregroundTextColor.Substring(2, 2);
                byte redValue = Byte.Parse(red, System.Globalization.NumberStyles.AllowHexSpecifier);
                string green = _foregroundTextColor.Substring(4, 2);
                byte greenValue = Byte.Parse(green, System.Globalization.NumberStyles.AllowHexSpecifier);
                string blue = _foregroundTextColor.Substring(6, 2);
                byte blueValue = Byte.Parse(blue, System.Globalization.NumberStyles.AllowHexSpecifier);

                SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, redValue, greenValue, blueValue));
                tbTextColor.Foreground = brush;
            }

        }

    }
}
