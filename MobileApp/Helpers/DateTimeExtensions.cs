namespace Northboundei.Mobile.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToTimeAgo(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return timeSpan.Seconds == 1 ? "1 second ago" : $"{timeSpan.Seconds} seconds ago";

            if (timeSpan.TotalMinutes < 60)
                return timeSpan.Minutes == 1 ? "1 minute ago" : $"{timeSpan.Minutes} minutes ago";

            if (timeSpan.TotalHours < 24)
                return timeSpan.Hours == 1 ? "1 hour ago" : $"{timeSpan.Hours} hours ago";

            if (timeSpan.TotalDays < 7)
                return timeSpan.Days == 1 ? "1 day ago" : $"{timeSpan.Days} days ago";

            if (timeSpan.TotalDays < 30)
            {
                var weeks = (int)(timeSpan.TotalDays / 7);
                return weeks == 1 ? "1 week ago" : $"{weeks} weeks ago";
            }

            if (timeSpan.TotalDays < 365)
            {
                var months = (int)(timeSpan.TotalDays / 30);
                return months == 1 ? "1 month ago" : $"{months} months ago";
            }

            var years = (int)(timeSpan.TotalDays / 365);
            return years == 1 ? "1 year ago" : $"{years} years ago";
        }
    }
}
