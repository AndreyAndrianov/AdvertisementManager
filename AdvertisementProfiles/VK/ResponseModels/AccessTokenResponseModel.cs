using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdverticementManager.Utils
{
    public class AccessTokenResponseModel
    {
        public string access_token { get; set; }

        public long expires_in { get; set; }

        public long user_id { get; set; }
    }
}
