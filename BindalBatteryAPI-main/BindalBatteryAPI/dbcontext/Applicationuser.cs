using System;
using System.Collections.Generic;

namespace BindalBatteryAPI.dbcontext
{
    public partial class Applicationuser
    {
      

        public int ApplicationUserId { get; set; }
        public string? PartyName { get; set; } = null!;
        public string? Passcode { get; set; }
        public string? Place { get; set; } = null!;
        public string? Address { get; set; }
        public long? Mobile { get; set; }
        public string? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }

        public string role { get; set; }
        public int IsApproved { get; set; }
        public string UserType { get; set; }
    }
}
