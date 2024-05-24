using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFinanceTrack
{
    public static class SessionUser
    {
        public static int CurrentUserId { get; set; } = 1;  // Defaulting to 1 for simplicity

        // Call this method when user logs in
        public static void Login(int userId)
        {
            CurrentUserId = userId;
        }

        // Call this method when user logs out
        public static void Logout()
        {
            CurrentUserId = 0;  // Resets to 0 or another invalid ID
        }

        // Static method to retrieve the current user ID
        public static int GetCurrentUserId()
        {
            return CurrentUserId;  // Retrieves the user ID from the session
        }
    }
}
