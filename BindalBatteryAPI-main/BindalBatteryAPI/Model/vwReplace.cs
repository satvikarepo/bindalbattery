namespace BindalBatteryAPI.Model
{


    public class vwReplace
    {
        public int ReplaceId { get; set; }
        public int Productid { get; set; }
        public int? SaleId { get; set; }
        public string? NewSrNo { get; set; }
        public string? ReplacementDate { get; set; }
        public int? PartyId { get; set; }

        public string? PartyName { get; set; }
        public string? BatterType { get; set; }

        public string? OkdSrNo { get; set; }

        public string? SaleDate { get; set; }
    }

    public class vwReplaceSub
    {
        public int ReplaceId { get; set; }     
    }
}
