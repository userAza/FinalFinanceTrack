namespace FinalFinanceTrack
{
    public static class SessionUser
    {
        public static int CurrentUserId { get; private set; } = 0;
        private static string currentUserEmail;

        // Call this method when user logs in
        public static void Login(int userId, string email)
        {
            CurrentUserId = userId;
            currentUserEmail = email;
        }

        // Call this method when user logs out
        public static void Logout()
        {
            CurrentUserId = 0;  // Resets to 0 or another invalid ID
            currentUserEmail = null;
        }

        // Static method to retrieve the current user ID
        public static int GetCurrentUserId()
        {
            return CurrentUserId;  // Retrieves the user ID from the session
        }

        public static string GetCurrentUserEmail()
        {
            return currentUserEmail;  // Retrieves the user email from the session
        }
    }
}
