using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FinalFinanceTrack
{
    public partial class Overview : Window
    {
        private const string ProfilePicturePath = "ProfilePicture.png";

        public Overview()
        {
            InitializeComponent();
            LoadProfilePicture();
        }

        public void LoadProfilePicture()
        {
            if (File.Exists(ProfilePicturePath))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(ProfilePicturePath, UriKind.RelativeOrAbsolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                ProfileImage.Source = bitmap;
                ProfileImage.Visibility = Visibility.Visible;
            }
        }

        private void IncomeButton_Click(object sender, RoutedEventArgs e)
        {
            Income incomePage = new Income();
            incomePage.Show();
            this.Close();
        }

        private void ExpensesButton_Click(object sender, RoutedEventArgs e)
        {
            Expenses expensePage = new Expenses();
            expensePage.Show();
            this.Close();
        }

        private void SavingsButton_Click(object sender, RoutedEventArgs e)
        {
            Savings savingsPage = new Savings();
            savingsPage.Show();
            this.Close();
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            History historyPage = new History();
            historyPage.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = Utility.GetCurrentUserId();
            SettingsWindow settingsPage = new SettingsWindow(userId);
            settingsPage.Show();
            this.Close();
        }

        private void ProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            int userId = Utility.GetCurrentUserId();
            AddProfilePic addProfilePicWindow = new AddProfilePic(userId);
            addProfilePicWindow.ProfilePictureSaved += AddProfilePicWindow_ProfilePictureSaved;
            addProfilePicWindow.ShowDialog();
        }


        private void AddProfilePicWindow_ProfilePictureSaved()
        {
            LoadProfilePicture(); // Reload the profile picture after it has been saved
        }
    }
}