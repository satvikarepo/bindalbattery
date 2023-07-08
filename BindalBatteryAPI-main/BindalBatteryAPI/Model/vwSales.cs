namespace BindalBatteryAPI.Model
{
    public class vwSales
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public int? PartyId { get; set; }
        public string? BatterySrNo { get; set; }

        public string? BatteryType { get; set; }
        public string SaleDate { get; set; }
        public string? BrandName { get; set; }
        public string? PartyName { get; set; }

    }

    public class vwSalesSub
    {
        public int Id { get; set; }
        public string? BatterSrNo { get; set; }
        public string? SaleDate { get; set; }
    }
}
