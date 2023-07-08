using System;
using System.Collections.Generic;

namespace BindalBatteryAPI.dbcontext
{
    public partial class Warantymaster
    {
        public int WarrantyId { get; set; }
        public int? ProductId { get; set; }
        public int? PartyMatserId { get; set; }
        public int? GracePeriod { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }
        public virtual Partymaster? PartyMatser { get; set; }

        public virtual Productmaster? ProductMaster { get; set; }



    }
}
