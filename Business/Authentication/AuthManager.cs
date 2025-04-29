using Business.Authentication.Validation.FluentValidation;
using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserRepository;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<Token> Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmailAddress(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            List<OperationClaim> operationClaims = _userService.GetUserOperationClaims(user.Id);
            if (result == true)
            {
                Token token = new Token();
                token = _tokenHandler.CreateToken(user, operationClaims);
                return new SuccessDataResult<Token>(token, "kullanıcı girişi başarılı");
            }
            return new ErrorDataResult<Token>("kullanıcı adı veya şifre hatalı");



        }    
        [ValidationAspect(typeof(AuthValidator))]
        public IResult Register(RegisterAuthDto registerDto)
        {
            IResult result = BusinessRules.Run(CheckIfEmailExist(registerDto.Email),
                CheckIfImageTypeIsValid(registerDto.Image.FileName)
                );
            if (!result.Success)
            {
                return result;
            }
            _userService.Add(registerDto);
            return new SuccessResult("kullanıcı kaydı başarıyla tamamlandı");

        }
        private IResult CheckIfEmailExist(string email)
        {
            var list = _userService.GetByEmailAddress(email);
            if (list != null)
            {
                return new ErrorResult("bu email adresi zaten kayıtlı");
            }
            return new SuccessResult();
        }
        private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
            if (imgMbSize > 1)
            {
                return new ErrorResult("yüklediğiniz resim boyutu en fazla bir mb olmalı");
            }
            
            
            return new SuccessResult();
        }
        private IResult CheckIfImageTypeIsValid(string fileName)
        {
            var extension = fileName.Split('.').Last();
            var ext = extension.ToLower();
            var validTypes = new List<string> { "jpg", "jpeg", "png" };
            if (!validTypes.Contains(ext))
            {
                return new ErrorResult("geçersiz resim formatı");
            }
            return new SuccessResult();
        }
    }
}