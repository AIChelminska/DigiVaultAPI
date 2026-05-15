using Npgsql;

namespace DigiVaultAPI.Data;

// Render itp. często dają URI postgresql://…; Npgsql chce Host=…;Username=…;
public static class PostgresConnectionStringNormalizer
{
    public static string? ForNpgsql(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return raw;

        var s = raw.Trim();
        if (!s.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase)
            && !s.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
            return s;

        var uri = new Uri(s);
        var userInfo = Uri.UnescapeDataString(uri.UserInfo);
        var colonIdx = userInfo.IndexOf(':');
        var username = colonIdx >= 0 ? userInfo[..colonIdx] : userInfo;
        var password = colonIdx >= 0 ? userInfo[(colonIdx + 1)..] : string.Empty;

        var port = uri.Port > 0 ? uri.Port : 5432;
        var database = Uri.UnescapeDataString(uri.AbsolutePath.TrimStart('/'));

        var csb = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = port,
            Database = database,
            Username = username,
            Password = password,
            SslMode = SslMode.Require,
            TrustServerCertificate = true,
        };

        return csb.ConnectionString;
    }
}
