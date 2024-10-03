using Cronometro.Commands;
using Cronometro.Services;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Cronometro.ViewModels
{
    public class CronometroVM : ViewModelBase
    {
        private string _tiempoMostrado= "00:00:00";
        private Timer _timer;

        public DelegateCommand StartCommand { get; }
        public DelegateCommand PauseCommand { get; }
        public DelegateCommand StopCommand { get; }

        private readonly ICronometroService _cronometroService;

        public CronometroVM(ICronometroService cronometroService)
        {
            _cronometroService = cronometroService;

            StartCommand = new DelegateCommand(Start, CanStart);
            PauseCommand = new DelegateCommand(Pause, CanPause);
            StopCommand = new DelegateCommand(Stop, CanStop);

            _timer = new Timer(100);
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            ShowTime();
        }

        private void ShowTime()
        {
            var elapsed = _cronometroService.Elapsed;
            ShowTime(elapsed);
        }

        private void ShowTime(TimeSpan elapsed)
        {
            TiempoMostrado = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";
        }

        public string TiempoMostrado 
        { 
            get => _tiempoMostrado; 
            set { _tiempoMostrado = value; OnPropertyChanged(); }
        }

        // 
        private bool CanStart(object? parameter)
        {
            return _cronometroService.CanStart;
        }

        private bool CanPause(object? parameter)
        {
            return _cronometroService.CanPause;
        }

        private bool CanStop(object? parameter)
        {
            return _cronometroService.CanStop;
        }
        //
        private void Start(object? parameter)
        {
            _cronometroService.Start();
            _timer.Start();
            RaiseAllCanExecuteChanged();
        }

        private void Pause(object? parameter)
        {
            _cronometroService.Pause();
            
            if(_cronometroService.IsRunning) 
                _timer.Start(); 
            else 
                _timer.Stop();
            
            RaiseAllCanExecuteChanged();
        }

        private void Stop(object? parameter)
        {
            _cronometroService.Stop();
            _timer.Stop();
            ShowTime();
            RaiseAllCanExecuteChanged();
        }

        private void RaiseAllCanExecuteChanged()
        {
            StartCommand?.RaiseCanExecuteChanged();
            PauseCommand?.RaiseCanExecuteChanged();
            StopCommand?.RaiseCanExecuteChanged();
        }

    }
}
