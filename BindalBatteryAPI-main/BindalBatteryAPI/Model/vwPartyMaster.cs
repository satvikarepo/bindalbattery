namespace BindalBatteryAPI.Model
{
    public class vwPartyMaster
    {
        public int PartyMasterId { get; set; }

        public int GracePeriod { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreatedBy { get; set; }

        public string? Partyname { get; set; }

        public string? place { get; set; }
        public long? Mobile { get; set; }
    }

    public class vwPartyMasterName
    {
      
        public string? Partyname { get; set; }
        public int PartyMasterId { get; set; }

    }

    public class vwPartyMasterNameAuto
    {
        public string Partyname { get; set; }       

    }
}
