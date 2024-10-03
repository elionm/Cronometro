
using Cronometro.Models;
using System.Diagnostics;

namespace Cronometro.Services
{

    public class CronometroService : ICronometroService
    {
        private readonly ICronometroModel _cronometroModel;
        private readonly Stopwatch _stopwatch;
        private StateCronometro _state = StateCronometro.Stoped;

        public CronometroService(ICronometroModel cronometroModel)
        {
            _cronometroModel = cronometroModel;
            _stopwatch = new Stopwatch();
        }

        public bool CanStart => _state == StateCronometro.Stoped; 
        public bool CanPause => _state != StateCronometro.Stoped; 
        public bool CanStop => _state != StateCronometro.Stoped; 

        public StateCronometro State { get => _state;}
        public bool IsRunning { get => _state== StateCronometro.Running; }

        public void Start()
        {
            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Start();
            }

            _state = StateCronometro.Running;
        }

        public void Pause()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
                _state= StateCronometro.Paused;
                return;
            }

            // Start();
            _stopwatch.Start();
            _state = StateCronometro.Running;
        }

        public void Stop()
        {
            _stopwatch.Reset();
            _cronometroModel.Reset();
            _state = StateCronometro.Stoped;
        }

        public TimeSpan Elapsed
        {
            get
            {
                _cronometroModel.Elapsed = _stopwatch.Elapsed;
                return _cronometroModel.Elapsed;
            }
        }
    }

}
