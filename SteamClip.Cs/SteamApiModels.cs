using System.Collections.Generic;

namespace SteamClip
{
    public class SteamAppDetails
    {
        public bool success { get; set; }
        public SteamAppData data { get; set; }
    }

    public class SteamAppData
    {
        public string name { get; set; }
    }

    public class SteamApiResponse
    {
        public Dictionary<string, SteamAppDetails> apps { get; set; }
    }
}
