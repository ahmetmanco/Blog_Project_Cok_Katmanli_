using Blog_Project_Domain.Entities;
using Blog_Project_Domain.Enums;
using Blog_Project_Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Models.DTOs.AppUserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_Project_Application.Services.AppUserServices
{
    public class AppUserServices : IAppUserServices
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        //dependency işlemi gerçekleşti
        public AppUserServices(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //UserName ile appUser tablosunda bulunan (eğer varsa) appuser satırını çekeriz ve updateprofileDTO nesnesini doldururuz.
        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            UpdateProfileDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    UploadPath = x.UploadPath,
                },
                where: x => x.UserName == userName);

            return result;
        }

        //kullanıcının sisteme login olmasını sağlar.User bilgilerine ulaşırız.
        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
        }

        public async Task LogOut(LoginDTO model)
        {
            await _signInManager.SignOutAsync();
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            //gelen registerDTO,Create edilmesi gereken AppUser
            AppUser user = new AppUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.CreateDate = model.CreateDate;
           // user.PasswordHash
            IdentityResult result =await _userManager.CreateAsync(user , model.Password);

            if(result.Succeeded)
                await _signInManager.SignInAsync(user,isPersistent:false);

            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {

            AppUser user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(600, 550));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");

                user.ImagePath = $"/images/{guid}.jpg";

            }
            else
            {
                if (model.ImagePath != null)
                    user.ImagePath = model.ImagePath;
                else
                    user.ImagePath = $"/images/defaultuser.jpg";
            }
            user.Status = Status.Modified;
            user.UpdateDate = DateTime.Now;
            await _appUserRepository.Update(user);

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,model.Password);
                await _userManager.UpdateAsync(user);
            }

            if(model.Email != null)
            {
                AppUser isUserMailExists = await _userManager.FindByEmailAsync(model.Email);
                if(isUserMailExists == null)
                    await _userManager.SetEmailAsync(user, model.Email);
            }
        }
    }
}
