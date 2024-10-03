
namespace Cronometro.Services
{
    public interface ICronometroService
    {
        bool CanStart { get; }
        bool CanPause { get; }
        bool CanStop { get; }
        
        void Start();
        void Pause();
        void Stop();
        
        TimeSpan Elapsed { get; }
        StateCronometro State { get; }
        bool IsRunning { get ; }
    }

}
