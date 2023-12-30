namespace UpTRobot.Client.Pages.monitors
{
    public partial class Create
    {

        public string Name { get; set; }
        public string HostAddress { get; set; }

        public int Interval { get; set; }
        public int Timeout { get; set; }

        public bool IsSslCheck { get; set; }

        public bool IsDomainExpierCheck { get; set; }

    }
}
