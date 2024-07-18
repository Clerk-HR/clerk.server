namespace clerk.server.Configurations;

public class AppSettings
{
    public const string ConnectionStrings = "ConnectionStrings";
    public const string JwtConfig = "JwtConfig";
    public static DBConnectionOptions Connections { get; set; } = new();
    public static JwtOptions JwtOptions { get; set; } = new();

}