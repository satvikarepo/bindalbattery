namespace BindalBatteryAPI.Model
{
    public class vwWarranty
    {
        public int WarrantyId { get; set; }
        public int? ProductId { get; set; }
        public int? PartyMatserId { get; set; }
        public int? GracePeriod { get; set; }

        public string? ProductType  { get;set; }
        public string? PartyName { get; set;}
    }
}
