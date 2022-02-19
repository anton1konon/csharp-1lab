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

namespace BirthdayAsker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _is_date_changed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_calc_age_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if (!_is_date_changed)
            {
                Main_textblock.Text = "Enter date";
                return;
            }
            DateTime sel_date = (DateTime)Date_picker.SelectedDate;
            if (sel_date > now) 
            {
                MessageBoxResult mbresult = MessageBox.Show("Date must be in the past!", "Incorrect date", MessageBoxButton.OK);
            }

            int Years = new DateTime(DateTime.Now.Subtract(sel_date).Ticks).Year - 1;

            if (Years >= 135)
            {
                MessageBoxResult mbresult = MessageBox.Show("You can't live more than 135 years!", "Incorrect date", MessageBoxButton.OK);
            }
            DateTime PastYearDate = sel_date.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = now.Subtract(PastYearDate).Hours;
            int Minutes = now.Subtract(PastYearDate).Minutes;
            int Seconds = now.Subtract(PastYearDate).Seconds;
            String age = $"Age: {Years} Year(s) {Months} Month(s) {Days} Day(s) {Hours} Hour(s) {Seconds} Second(s)";


            String west_zodiac = GetHoroName(sel_date);
            String chineese_zodiac = ChineseZodiac(sel_date);



            Main_textblock.Text = $"{age}\nHoro name: {west_zodiac}\nChineese zodiac: {chineese_zodiac}";
            if (now.Month == sel_date.Month && now.Day == sel_date.Day)
            {
                MessageBoxResult mbresult = MessageBox.Show("Conngratulations!!! \nHAPPY BIRTHDAY", "WOW!", MessageBoxButton.OK);
            }

        }

        private string ChineseZodiac(DateTime date)
        {
            System.Globalization.EastAsianLunisolarCalendar cc =
                  new System.Globalization.ChineseLunisolarCalendar();
            int sexagenaryYear = cc.GetSexagenaryYear(date);
            int terrestrialBranch = cc.GetTerrestrialBranch(sexagenaryYear);

            string[] years = new string[] { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

            return years[terrestrialBranch - 1];
        } 

        private string GetHoroName(DateTime dt)
        {
            int month = dt.Month;
            int day = dt.Day;
            switch (month)
            {
                case 1:
                    if (day <= 19)
                        return "Capricorn";
                    else
                        return "Aquarius";

                case 2:
                    if (day <= 18)
                        return "Aquarius";
                    else
                        return "Pisces";
                case 3:
                    if (day <= 20)
                        return "Pisces";
                    else
                        return "Aries";
                case 4:
                    if (day <= 19)
                        return "Aries";
                    else
                        return "Taurus";
                case 5:
                    if (day <= 20)
                        return "Taurus";
                    else
                        return "Gemini";
                case 6:
                    if (day <= 20)
                        return "Gemini";
                    else
                        return "Cancer";
                case 7:
                    if (day <= 22)
                        return "Cancer";
                    else
                        return "Leo";
                case 8:
                    if (day <= 22)
                        return "Leo";
                    else
                        return "Virgo";
                case 9:
                    if (day <= 22)
                        return "Virgo";
                    else
                        return "Libra";
                case 10:
                    if (day <= 22)
                        return "Libra";
                    else
                        return "Scorpio";
                case 11:
                    if (day <= 21)
                        return "Scorpio";
                    else
                        return "Sagittarius";
                case 12:
                    if (day <= 21)
                        return "Sagittarius";
                    else
                        return "Capricorn";
            }
            return "";
        }


        private void Date_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _is_date_changed = true;
        }
    }
}
