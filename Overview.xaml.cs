using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FinalFinanceTrack
{
    public partial class Overview : Window
    {
        private DbManager dbManager;

        public Overview()
        {
            InitializeComponent();
            dbManager = new DbManager();
            LoadProfilePicture();
        }

        public void LoadProfilePicture()
        {
            int userId = Utility.GetCurrentUserId();
            byte[] profileImageBytes = dbManager.GetProfilePicture(userId);

            if (profileImageBytes != null && profileImageBytes.Length > 0)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(profileImageBytes);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                ProfileImage.Source = bitmap;
                ProfileImage.Visibility = Visibility.Visible;
            }
            else
            {
                ProfileImage.Source = null; // Or set to a default image
                ProfileImage.Visibility = Visibility.Collapsed;
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
            AddProfilePic addProfilePicWindow = new AddProfilePic(userId, "Overview");
            addProfilePicWindow.ProfilePictureSaved += AddProfilePicWindow_ProfilePictureSaved;
            addProfilePicWindow.Show();
            this.Close();
        }


        private void AddProfilePicWindow_ProfilePictureSaved()
        {
            LoadProfilePicture(); // Refresh function to reload the profile picture
        }
    }
}
