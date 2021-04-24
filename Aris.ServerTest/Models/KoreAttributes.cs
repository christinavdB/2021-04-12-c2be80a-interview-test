using Newtonsoft.Json;

namespace Aris.ServerTest.Models
{
    //I only added lines and Free_Spins as the other Attributes are not used in the test as yet (just trying to save time)
    public class KoreAttributes
    {
        [JsonProperty("lines")]
        public string Lines { get; set; }

        [JsonProperty("free_spins")]
        public bool Free_Spins { get; set; }
    }
}
