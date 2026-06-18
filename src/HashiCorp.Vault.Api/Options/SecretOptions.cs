namespace Hashicorp.Vault.API.Options;

public class SecretOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string SqlConnectionString { get; set; } = string.Empty;
    public string KestrelCertificatesPassword { get; set; } = string.Empty;
}
