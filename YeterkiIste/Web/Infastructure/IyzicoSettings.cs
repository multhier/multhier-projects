namespace Web.Infastructure
{
    /// <summary>
    /// appsettings.json içindeki "Iyzico" bölümü. Anahtarlar koda yazılmaz;
    /// User Secrets ya da ortam değişkeni (Iyzico__ApiKey, Iyzico__SecretKey) ile verilir.
    /// </summary>
    public class IyzicoSettings
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string BaseUrl { get; set; } = "https://sandbox-api.iyzipay.com";
    }
}
