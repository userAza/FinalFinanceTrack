using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinalFinanceTrack
{

/*class to navigate through windows still under construction x */
    public static class NavigationManager
    {
        private static Window currentWindow;

        public static void OpenWindow(Window newWindow)
        {
            CloseCurrentWindow();
            currentWindow = newWindow;
            currentWindow.Show();
        }

        public static void CloseCurrentWindow()
        {
            currentWindow?.Close();
        }
    }

}
