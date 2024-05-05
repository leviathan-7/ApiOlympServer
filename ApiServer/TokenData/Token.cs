using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace ApiServer.TokenData
{
    public partial class JWTToken
    {
        public string Login { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
