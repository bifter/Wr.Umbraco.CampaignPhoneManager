namespace Wr.UmbracoCampaignPhoneManager.Tests
{
    public static class TestHelpers
    {
        public static bool CheckIfNullOrEmpty(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfNotNullOrEmpty(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                return true;
            }
            return false;
        }
    }
}
