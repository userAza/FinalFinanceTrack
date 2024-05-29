using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FinalFinanceTrack
{
    public partial class AddProfilePic : Window
    {
        public event Action ProfilePictureSaved = delegate { };
        private int userId;
        private DbManager dbManager;

        public AddProfilePic(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            dbManager = new DbManager();
        }

        private void UploadPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(selectedFileName);

                if (dbManager.UpdateProfilePicture(userId, imageBytes))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(imageBytes);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    ProfileImage.Source = bitmap;

                    ProfilePictureSaved?.Invoke();

                    MessagePopup.IsOpen = true;
                    Task.Delay(2000).ContinueWith(_ => MessagePopup.Dispatcher.Invoke(() => MessagePopup.IsOpen = false));
                }
                else
                {
                    MessageBox.Show("Failed to upload profile picture.");
                }
            }
        }

        private void DeletePicture_Click(object sender, RoutedEventArgs e)
        {
            if (dbManager.DeleteProfilePicture(userId))
            {
                ProfileImage.Source = null;
                ProfilePictureSaved?.Invoke();
                MessageBox.Show("Profile picture deleted successfully.");
            }
            else
            {
                MessageBox.Show("Failed to delete profile picture.");
            }
        }

        private void SavePicture_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
