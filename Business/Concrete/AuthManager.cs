using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {   
        private readonly IUserService _userService;
        public AuthManager(IUserService userService)
        { 
          _userService = userService;     
        }

        public string Login(LoginAuthDto loginDto)
        {
          var user = _userService.GetByEmailAddress(loginDto.Email);
          var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (result == true)
            {
                return "kullanıcı girişi başarılı";
            }
          return "kullanıcı bilgileri hatalı";


        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(RegisterAuthDto registerDto)
        {
            int imgSize = 2; 
            IResult result = BusinessRules.Run(CheckIfEmailExist(registerDto.Email),CheckIfImageSizeIsLessThanOneMb(imgSize)); 
            if(!result.Success)
            {
                return result; 
            }
            _userService.Add(registerDto); 
            return new SuccessResult("kullanıcı kaydı başarıyla tamamlandı"); 

        }
        private IResult CheckIfEmailExist(string email)
        {
            var list = _userService.GetByEmailAddress(email); 
            if(list!=null)
            {
               return new ErrorResult("bu email adresi zaten kayıtlı");
            }
            return new SuccessResult(); 
        }
        private IResult CheckIfImageSizeIsLessThanOneMb(int imgSize)
        {
            if (imgSize > 1) 
            {
                return new ErrorResult("yüklediğiniz resim boyutu en az bir mb olmalı");
            }
            return new SuccessResult(); 
        }
    }
}
