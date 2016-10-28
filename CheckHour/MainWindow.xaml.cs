using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace CheckHour
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string REQUIRED_MESSAGE = "{0} is required!";
        const string BELONG_MESSAGE = "{0} belong to {1} -> {2}";
        const string NOT_BELONG_MESSAGE = "{0} not belong to {1} -> {2}";
        const string LINE_BREAK = "\n";
        const int MAX_TIME = 23;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check input time. Time must be integer and between 0 to 23
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkInputTime(object sender, TextCompositionEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            tbkResult.Text = "";
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (int.Parse(textbox.Text) > MAX_TIME)
            {
                textbox.TextChanged -= checkTextChanged;
                textbox.Text = MAX_TIME.ToString();
                textbox.TextChanged += checkTextChanged;
            }
        }

        /// <summary>
        /// Click button check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_Click(object sender, RoutedEventArgs e)
        {
            var resultMessage = "";
            if(string.IsNullOrEmpty(tbStartTime.Text))
            {
                resultMessage = string.Format(REQUIRED_MESSAGE, "Start time");
            }
            if(string.IsNullOrEmpty(tbEndTime.Text))
            {
                if(resultMessage != "")
                {
                    resultMessage += LINE_BREAK;
                }
                resultMessage += string.Format(REQUIRED_MESSAGE, "End time");
            }
            if(string.IsNullOrEmpty(tbHour.Text))
            {
                if (resultMessage != "")
                {
                    resultMessage += LINE_BREAK;
                }
                resultMessage += string.Format(REQUIRED_MESSAGE, "Hour");
            }

            if(resultMessage != "")
            {
                tbkResult.Text = resultMessage;
                tbkResult.Foreground = Brushes.Red;
            }
            else
            {
                int startTime = int.Parse(tbStartTime.Text);
                int endTime = int.Parse(tbEndTime.Text);
                int hour = int.Parse(tbHour.Text);
                if (checkTime(startTime, endTime, hour))
                {
                    tbkResult.Text = string.Format(BELONG_MESSAGE, hour, startTime, endTime);
                }
                else
                {
                    tbkResult.Text = string.Format(NOT_BELONG_MESSAGE, hour, startTime, endTime);
                }
                tbkResult.Foreground = Brushes.Blue;
            }
            
        }

        /// <summary>
        /// Check hour
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        private bool checkTime(int startTime, int endTime, int hour)
        {
            if(startTime <= endTime)
            {
                return startTime <= hour && hour <= endTime;
            }
            else
            {
                return hour >= startTime || hour <= endTime;
            }
        }
    }
}
