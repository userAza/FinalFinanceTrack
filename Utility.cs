namespace FinalFinanceTrack
{
    public static class Utility
    {
        public static int GetCurrentUserId()
        {
            return SessionUser.GetCurrentUserId();
        }

        public static string GetCurrentUserEmail()
        {
            return SessionUser.GetCurrentUserEmail();
        }
    }
}
