namespace FinalFinanceTrack
{
    public static class Utility
    {
        public static int GetCurrentUserId()
        {
            // Implement this method to retrieve the current user's ID.
            
            return SessionUser.GetCurrentUserId();
        }
    }
}
