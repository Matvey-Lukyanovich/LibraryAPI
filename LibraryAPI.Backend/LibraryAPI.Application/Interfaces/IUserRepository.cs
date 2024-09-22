using LibraryAPI.Domain;

namespace LibraryAPI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username, string password);
        Task<User> FindByUsernameAndPassword(string username, string password);

    }
}
