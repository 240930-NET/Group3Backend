using ABCDoubleE.Models;
using System.Threading.Tasks;

namespace ABCDoubleE.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthorAsync(Author author);
        Task<List<Author>> SearchAuthorsAsync(string search);
    }
}
