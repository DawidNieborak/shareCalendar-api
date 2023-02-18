namespace shareCalendar_api.Settings;

public class DbConnection
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string DbName { get; set; }

    public string ConnectionString
    {
        get
        {
            return $"jdbc:postgresql://{Host}:{Port}/{DbName}";
        }
    }
}