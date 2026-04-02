public class NginxLogEntry
{
    public long Id { get; set; }
    public long Epoch { get; set; }
    public string RemoteIpAddress { get; set; } = string.Empty;
    public string RemoteUser { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; }
    public string RequestPath { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public long BytesSent { get; set; }
}