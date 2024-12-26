using System.ComponentModel.DataAnnotations;
using ATM.Data;
using ATM.DTO;
using ATM.RTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            // Check if a user with the same email already exists
            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            // Create a new ApplicationUser instance
            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                FullName = userDto.FullName
            };

            // Attempt to create the user
            var result = await dbContext.CreateAsync(user, userDto.Password);

            // If the operation fails, throw an exception with error details
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }
        }


        public async Task<IEnumerable<UserRTO>> GetallUserAsync()
        {
            return dbContext.Users.Select(user =>new UserRTO
            {
                Id = user.Id,
                UserName= user.UserName,
                Email = user.Email
            }).ToList();
        }

        //public async Task<UserRTO> GetUserByIdAsync(string id)
        //{
        //    var user =await dbContext.FindByIdAsync(id);
        //    if (user == null) return null;

        //    return new UserRTO
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        FullName = user.FullName,
        //    };
        //}

        //public async Task<UserRTO> CreateUserAsync(UserDto userDto)
        //{
        //    var user = new ApplicationUser
        //    {
        //        UserName = userDto.UserName,
        //        Email = userDto.Email,
        //        FullName = userDto.FullName,
        //    };
        //    var result = await dbContext.CreateAsync(user, userDto.Password);
        //    if (!result.Succeeded) return null;

        //    return new UserRTO
        //    {
        //        Id = userDto.Id,
        //        UserName = userDto.UserName,
        //        Email = userDto.Email,
        //        FullName = userDto.FullName,
        //    };
        //}



        //public async Task<bool> UpdatetUserAsync(stringid UserDto, userDto )
        //{
        //    var user = await dbContext.FindByIdAsync(id);
        //    if (user == null) return false;
        //    user.UserName = UserDto.UserName;
        //    user.Email = UserDto.Email;
        //    user.FullName = UserDto.FullName;

        //    var result = await dbContext.UpdateAsync(user);
        //    return result.Succeeded;

        //}

        //public async Task<bool> DeleteUserAsync(string id)
        //{
        //   var user =await dbContext.FindByAsync(id);
        //    if (user == null) return false;
        //}

        //var result = await dbContext.DeleteAsync(user);
        //return Result.Succeeded;



    }
}
