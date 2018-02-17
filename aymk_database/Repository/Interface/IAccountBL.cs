using aymk_database.Database;
using aymk_database.Repository.Base;
using aymk_library.Library.Models;

namespace aymk_database.Repository.Interface
{
    public interface IAccountBL : IRepositoryBase<Account>
    {
        aymkResponse GetAllData(int Id);
        aymkResponse Login(string username, string password);
        aymkResponse Register(Account item);
    }
}
