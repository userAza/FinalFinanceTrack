using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FinalFinanceTrack
{
    public partial class AddProfilePic : Window
    {
        public event Action ProfilePictureSaved;
        private const string ProfilePicturePath = "ProfilePicture.png";

        public AddProfilePic()
        {
            InitializeComponent();
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
                var fileExtension = Path.GetExtension(openFileDialog.FileName)?.ToLower();
                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    MessageBox.Show("The wrong format is being used. Please use JPEG, JPG, or PNG formats.");
                    return;
                }

                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.Rotation = GetRotation(bitmap.UriSource.LocalPath);
                    bitmap.EndInit();

                    ProfileImage.Source = bitmap;
                    ProfileImage.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the image: {ex.Message}");
                }
            }
        }

        private Rotation GetRotation(string imagePath)
        {
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                var metadata = (BitmapMetadata)decoder.Frames[0].Metadata;

                if (metadata != null && metadata.ContainsQuery("System.Photo.Orientation"))
                {
                    var orientation = (ushort)metadata.GetQuery("System.Photo.Orientation");

                    switch (orientation)
                    {
                        case 6:
                            return Rotation.Rotate90;
                        case 8:
                            return Rotation.Rotate270;
                        case 3:
                            return Rotation.Rotate180;
                        default:
                            return Rotation.Rotate0;
                    }
                }
                return Rotation.Rotate0;
            }
        }

        private async void SavePicture_Click(object sender, RoutedEventArgs e)
        {
            var rtb = new RenderTargetBitmap(200, 200, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(ProfileImage.Parent as Visual);

            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            using (var stream = new FileStream(ProfilePicturePath, FileMode.Create))
            {
                png.Save(stream);
            }

            // Display popup message
            MessagePopup.IsOpen = true;
            await Task.Delay(2000); // Wait for 2 seconds
            MessagePopup.IsOpen = false;

            // Raise the event to notify the Overview window
            ProfilePictureSaved?.Invoke();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
