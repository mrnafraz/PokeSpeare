using System.Threading.Tasks;

namespace PokeSpeare.Service.Services
{
    public interface IShakespeareTranslationService
    {
        Task<string> Translate(string text);
    }
}