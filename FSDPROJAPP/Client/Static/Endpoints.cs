using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FSDPROJAPP.Client.Static
{
    public class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string HobbysEndpoint = $"{Prefix}/hobbys";
        public static readonly string DetailsEndpoint = $"{Prefix}/details";
        public static readonly string DislikesEndpoint = $"{Prefix}/dislikes";
        public static readonly string LikesEndpoint = $"{Prefix}/likes";
        public static readonly string ProfilesEndpoint = $"{Prefix}/profiles";
        public static readonly string SubscriptionsEndpoint = $"{Prefix}/subscriptions";
    }
}
