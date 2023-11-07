namespace FM.API.Configurations
{
    /// <summary>
    /// Time Interval information for the scheduled task
    /// based on the "Scheduler" section within the appsettings.json
    /// </summary>
    public class Scheduler
    {
        public const string SchedulerSection = "Scheduler";
        public int Interval { get; set; }
    }
}
