namespace UK.NHS.CookieBanner.Services
{
    public class CookiePolicy
    {
        private string details = string.Empty;
        private string amendDate = string.Empty;

        public string Details { get => details; set => details = value; }
        public string AmendDate { get => amendDate; set => amendDate = value; }
    }
}
