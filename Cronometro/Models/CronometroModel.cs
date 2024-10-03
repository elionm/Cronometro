
namespace Cronometro.Models
{
    public class CronometroModel : ICronometroModel
    {
        public TimeSpan Elapsed { get; set; }

        public void Reset()
        {
            Elapsed = TimeSpan.Zero;
        }
    }
}
