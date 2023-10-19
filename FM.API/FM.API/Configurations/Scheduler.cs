namespace FM.API.Configurations
{
    /// <summary>
    /// Información de Intervalos de tiempo para la tarea programada 
    /// basada en la sección "Scheduler" dentro del appsettings.json
    /// </summary>
    public class Scheduler
    {
        public const string SchedulerSection = "Scheduler";
        public int Interval { get; set; }
    }
}
