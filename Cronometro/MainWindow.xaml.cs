using Cronometro.Models;
using Cronometro.Services;
using Cronometro.ViewModels;
using System.Windows;

namespace Cronometro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ICronometroModel cronometroModel=new CronometroModel();
            ICronometroService cronometroService = new CronometroService(cronometroModel);
            this.DataContext = new CronometroVM(cronometroService);
        }
    }
}