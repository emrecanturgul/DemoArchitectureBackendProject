using Business.Repositories.UserRepository;
using Business.Repositories.Utilities.File;
using Core.Utilities.Hashing;
using Core.Utilities.Result;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Result.Abstract;
using Business.Repositories.UserRepository.Constants;
using Core.Utilities.Aspects.Validation;
using Business.Repositories.UserRepository.Validation.FluentValidation;

namespace Business.Repositories.UserRepository
{
    public class UserManager : IUserService 
    {    
        private readonly IFileService _fileService;
        private readonly IUserDal _userDal;
        
        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }
        public async Task Add(RegisterAuthDto registerDto)
        {  
            string fileName = await Task.Run(() => _fileService.FileSaveToServer(registerDto.Image, "./content/img/"));
            //byte[] fileByteArray = _fileService.FileConvertByteArrayToDatabase(registerDto.Image);
            var user = CreateUser(registerDto, fileName);
            _userDal.Add(user);
        }
        private  User CreateUser(RegisterAuthDto registerDto, string imageUrl) 
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out passwordHash, out passwordSalt);
            
            return new User
            {
                Id = 0,
                Name = registerDto.Name,
                Email = registerDto.Email,
                ImageUrl = imageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }

        public IDataResult<List<User>> GetList() {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),UserMessages.GetListMessage);
        }
        

        public User GetByEmailAddress(string email)
        {   
            var result = _userDal.Get(u => u.Email == email);
            return result;   
        }


        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user) { 
            _userDal.Update(user); 
            return new SuccessResult(UserMessages.UpdatedUser); 
        }
        public IResult Delete(User user) {
            _userDal.Delete(user); 
            return new SuccessResult(UserMessages.DeletedUser); 
        }
        public IDataResult<User> GetById(int id) {
            return new SuccessDataResult<User>(_userDal.Get(p=>p.Id == id));
        }
        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public IResult ChangePassword(UserChangePasswordDto passwordDto)
        {   
            var user = _userDal.Get(u => u.Id == passwordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(passwordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if(!result)
            {
                return new ErrorResult(UserMessages.WrongCurrentPassword);
            }
            byte[] passwordHash, passwordSalt; 
            HashingHelper.CreatePassword(passwordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedPassword);
        }
    }
}
