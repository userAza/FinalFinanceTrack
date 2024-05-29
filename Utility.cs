namespace FinalFinanceTrack
{
    public static class Utility
    {
        public static int GetCurrentUserId()
        {
            // Implement this method to retrieve the current user's ID.
            
            return SessionUser.GetCurrentUserId();
        }

        public static string GetCurrentUserEmail()
        {
            // Retrieve the current user's email from the session or other authentication mechanism.
            return SessionUser.GetCurrentUserEmail();
        }

    }
}
