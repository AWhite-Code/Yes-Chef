namespace Yes_Chef.Helpers
{
    public static class AppSettings
    {
        public static TimeSpan DeletionInterval { get; set; } = TimeSpan.FromMinutes(5); // For testing purposes
    }
}
