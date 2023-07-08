using System;
using System.Collections.Generic;

namespace BindalBatteryAPI.dbcontext
{
    public partial class Productmaster
    {
    
        public int ProductId { get; set; }
        public string? BrandName { get; set; }
        public string? BatteryType { get; set; }        
        public int? GauranteePeriod { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }
        public virtual ICollection<Warantymaster> Warantymasters { get; } = new List<Warantymaster>();

        public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
        public virtual ICollection<Replace> Replaces { get; } = new List<Replace>();
    }
}
