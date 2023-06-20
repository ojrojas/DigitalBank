using System;
namespace DigitalBank.Web.Data;

public class OptionToken
{
    public string SecretPhrase { get; set; } = null!;
    public string UrlApi { get; set; } = null!;
}

