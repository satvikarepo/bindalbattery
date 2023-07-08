using System;
using System.Collections.Generic;

namespace BindalBatteryAPI.dbcontext
{
    public partial class Replace
    {


        public int ReplaceId { get; set; }

        public int? SaleId { get; set; }

        public string? NewSrNo { get; set; }

        public DateTime ReplacementDate { get; set; }

        public int? PartyId { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? CreatedBy { get; set; }

        public int ProductId { get; set; }

        public virtual Partymaster? Party { get; set; }

        public virtual Productmaster Product { get; set; } = null!;

        public virtual Sale? Sale { get; set; }


    }
}
