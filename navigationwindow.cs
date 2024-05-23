using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FinalFinanceTrack
{

    public class NavigationManager
    {
        private Button currentActiveButton;

        public void SetActiveButton(Button button)
        {
            if (currentActiveButton != null)
            {
                currentActiveButton.Background = new SolidColorBrush(Colors.White); // Default color
            }

            currentActiveButton = button;

            currentActiveButton.Background = new SolidColorBrush(Colors.LightBlue); // Highlight color
        }
    }


}

/*class to navigate through windows still under construction x *//*
    public static class NavigationManager
    {
        private static Window currentWindow;

    *//*    public static void OpenWindow(Window newWindow)
        {
            CloseCurrentWindow();
            currentWindow = newWindow;
            currentWindow.Show();
        }

        public static void CloseCurrentWindow()
        {
            currentWindow?.Close();
        }*//*





    }

}*/
