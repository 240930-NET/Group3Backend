using ABCDoubleE.Models;
using System.Threading.Tasks;

namespace ABCDoubleE.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> AddGenreAsync(Genre genre);
        Task<List<Genre>> SearchGenresAsync(string search);
    }
}
