using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Application.Common.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public long ExpiresInSeconds { get; set; }
        public string TokenType { get; set; }
        public bool EnableAudiences { get; set; }
        public bool ValidateAudience { get; set; }
        public bool Enabled { get; set; }
    }
}
