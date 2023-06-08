using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services.User_Service
{
    public class UserService : IUserService
    {
        private UserManager<User> _UserManager;
        private SignInManager<User> _SignInManager;

        public UserService(UserManager<User> userManager
            , SignInManager<User> signInManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        #region Get User by UserName Or Email
        public async Task<User> GetUserByUserNameOrEmailAsync(string Value)
        {
            try
            {
               var user =  await _UserManager.Users.Where(t=>t.UserName== Value
                || t.FullName==Value || t.Email==Value)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return null;
                }
                return user;
            }catch
            {
                return null;
            }
        }
        #endregion
        #region Authentication User
        public async Task<bool> IsAuthenticationUserAsync(string Value, string Password)
        {
            try
            {
                var user = await _UserManager.Users
                    .Where(t => t.UserName == Value || t.Email == Value)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }
                var Rsualt = await _SignInManager.PasswordSignInAsync(user, Password, false, false);
                if (Rsualt != null && Rsualt.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        #endregion
        #region Exist user
        public async Task<bool> IsExistUserAsync(string Value)
        {
            try
            {
                var User = await _UserManager.Users
                    .Where(t => t.UserName == Value
                || t.Email == Value
                || t.FullName == Value)
                    .FirstOrDefaultAsync();
                if (User == null)
                {
                    return false;
                }
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsExistUserAsync(int Id)
        {
            try
            {
                if (Id == 0)
                    return false;

                var user = await _UserManager.FindByIdAsync(Id.ToString());
                if (user == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
        #endregion
        #region Register User
        public async Task<bool> RegisterUser(DTOs.RegisterUser_DTO Data)
        {
            try
            {
                Domain.Entities.User newuser = new User()
                {
                    FullName = Data.FullName,
                    Email = Data.Email,
                    PhoneNumber = Data.PhoneNumber,
                    UserName = Data.UserName,
                };
                var Resualt_Add_User = await _UserManager.CreateAsync(newuser, Data.Passowrd);
                if(Resualt_Add_User!=null
                    && Resualt_Add_User.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }
}
