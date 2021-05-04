using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckKintal
{
    class UserAccount
    {
        public int index { get; set; }
        public bool chkSelected { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AutoCheckIn { get; set; }
        public string UserAgent { get; set; }
        public string lblShow { get; set; }
        public string updated_at { get; set; }
    }
}
