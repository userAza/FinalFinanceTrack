using System.Windows;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace FinalFinanceTrack
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Equivalent to Form1_Load
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Your click event logic here
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            UserMan f2 = new UserMan();
            f2.Show();
            this.Hide();
        }

        private void label1_Click(object sender, RoutedEventArgs e)
        {
            // Your label click event logic here
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // Your click event logic here
        }

        private void SETTINGS_Checked(object sender, RoutedEventArgs e)
        {
            // Your radio button checked change logic here
        }
    }
}
