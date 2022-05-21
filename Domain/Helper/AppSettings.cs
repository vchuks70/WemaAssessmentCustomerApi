﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string AlatKey { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string GetBanksUrl { get; set; }
    }
}
    