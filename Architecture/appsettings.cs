namespace Architecture
{
    public class Settings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public Blob Blob { get; set; }
    }

    public class ConnectionStrings
    {
        public string Default { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }

    public class Blob
    {
        public string ConnectionString { get; set; }
        public string Token { get; set; }
    }
}
