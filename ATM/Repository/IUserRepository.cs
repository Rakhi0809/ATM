using ATM.DTO;
using ATM.RTO;

namespace ATM.Repository
{
    public interface IUserRepository
    {
        Task CreateUserAsync(UserDto userDto);
        Task<IEnumerable<UserRTO>> GetallUserAsync();
        //Task<UserRTO>GetUserByIdAsync(string id);
       
        //Task<bool> UpdatetUserAsync(stringid UserDto userDto);
        //Task<bool> DeleteUserAsync(string id);
    }
}
