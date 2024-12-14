namespace Nsu.Contest.Web.Common.Util;

// POCO
public class ConfigurationContest
{
    public int NRounds { get; set; }
    public int NJuniorsTeamleads { get; set; }
    public string JuniorsPath { get; set; } = string.Empty;
    public string TeamleadsPath { get; set; } = string.Empty;
}
