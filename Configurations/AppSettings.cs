namespace clerk.server.Configurations;

public class AppSettings
{
    public const string ConnectionStrings = "ConnectionStrings";
    public static DBConnectionOptions Connections { get; set; } = new();

}