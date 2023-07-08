using System;
using System.Collections.Generic;

namespace BindalBatteryAPI.dbcontext
{
    public partial class Sale
    {


        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? PartyId { get; set; }

        public DateTime SaleDate { get; set; }
        public string? BatterySrNo { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreatedBy { get; set; }

        public virtual Partymaster? Party { get; set; }

        public virtual Productmaster? Product { get; set; }
        public virtual ICollection<Replace> Replaces { get; } = new List<Replace>();

    }
}
