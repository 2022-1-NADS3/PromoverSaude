using Newtonsoft.Json;
using Xamarin.Forms.Xaml;

namespace AppPSOne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class DadosUsuarioBase
    {
        public string nome { get; set; }
        [JsonProperty("nome")]
        public string email { get; set; }
        [JsonProperty("email")]
        public int senha { get; set; }
        [JsonProperty("senha")]
        public float sexo { get; set; }
    }
}