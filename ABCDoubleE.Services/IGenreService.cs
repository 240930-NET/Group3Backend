using ABCDoubleE.Models;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public interface IGenreService
    {
        Task<Genre> AddGenreAsync(Genre genre);
        Task<List<Genre>> SearchGenresAsync(string search);
    }
}
