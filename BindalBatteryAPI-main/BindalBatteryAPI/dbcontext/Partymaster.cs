using System;
using System.Collections.Generic;

namespace BindalBatteryAPI.dbcontext
{
    public partial class Partymaster
    {
    
        public int PartyMasterId { get; set; }
       
        public int GracePeriod { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }

        public string? Partyname { get; set; }

        public string? place { get; set; }
        public long? Mobile { get; set; }

        public virtual ICollection<Warantymaster> Warantymasters { get; } = new List<Warantymaster>();
        public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
        public virtual ICollection<Replace> Replaces { get; } = new List<Replace>();

    }
}
