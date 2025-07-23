
namespace ETMS.Service.DTOs;
public class EmailConfiguration
{
    public string From { get; set; } = string.Empty;
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string AppPassword { get; set; } = string.Empty;
}