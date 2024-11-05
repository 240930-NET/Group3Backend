using ABCDoubleE.Models;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public interface IAuthorService
    {
        Task<Author> AddAuthorAsync(Author author);
        Task<List<Author>> SearchAuthorsAsync(string search);
    }
}
