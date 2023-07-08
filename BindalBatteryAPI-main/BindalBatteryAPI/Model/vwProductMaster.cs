namespace BindalBatteryAPI.Model
{
    public class vwProductMaster
    {
        public int ProductId { get; set; }
        public string? BrandName { get; set; }
        public string? BatteryType { get; set; }
        public int? GauranteePeriod { get; set; }
    }

    public class vwProductMasterNameAuto
    {
        public string BrandName { get; set; }

    }
}
