using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using Core.Utilities.Result.Abstract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Business;

namespace Business.Repositories.UserRepository
{
    public interface IUserService
    {
        Task Add(RegisterAuthDto authDto);
        IResult Update(User user); 
        IResult Delete(User user);
        IDataResult<List<User>> GetList(); 
        User GetByEmailAddress(string email);
        IDataResult<User> GetById(int id);
        IResult ChangePassword(UserChangePasswordDto passwordDto);
        List<OperationClaim> GetUserOperationClaims(int userId);
        
    }
}
    