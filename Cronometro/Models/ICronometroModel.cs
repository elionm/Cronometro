
namespace Cronometro.Models
{
    public interface ICronometroModel
    {
        TimeSpan Elapsed { get; set; }

        void Reset();
    }
}