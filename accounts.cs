using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementasi_database
{
    public class accounts
    {
        public string employee_id { get; set; }
        public string password { get; set; }
        public bool is_deleted { get; set; }
        public string otp { get; set; }
        public bool is_used { get; set; }
        public DateTime expired_time { get; set; }


    }
}
