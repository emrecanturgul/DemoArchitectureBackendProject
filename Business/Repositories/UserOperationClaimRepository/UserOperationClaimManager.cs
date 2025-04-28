using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository.Constants;
using Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using Business.Repositories.UserRepository;
using Core.Utilities.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal,
            IOperationClaimService operationClaimService,IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(
                IsUserExist(operationClaim.UserId),
                IsOperationClaimExist(operationClaim.OperationClaimId),
                IsOperationSetExist(operationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Add(operationClaim);
            return new SuccessResult(UserOperationClaimMessages.Added);

        }

        public IResult Delete(UserOperationClaim operationClaim)
        {
            _userOperationClaimDal.Delete(operationClaim);
            return new SuccessResult(UserOperationClaimMessages.Delete);

        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(x => x.Id == id));
        }

        public IDataResult<List<UserOperationClaim>> GetList()
        {

            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(
             IsUserExist(operationClaim.UserId),
              IsOperationClaimExist(operationClaim.OperationClaimId),
                IsOperationSetExist(operationClaim));
            if (result != null)
            {
                return result;
            }
            _userOperationClaimDal.Update(operationClaim);
            return new SuccessResult(UserOperationClaimMessages.Update);

        }
        private IResult IsUserExist(int userId)
        {
            var result = _userService.GetById(userId).Data;
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessages.UserNotFound);
            }   
            return new SuccessResult();
        }
        
        private IResult IsOperationClaimExist(int operationClaimId)
        {
            var result = _operationClaimService.GetById(operationClaimId).Data;
            if (result == null)
            {
                return new ErrorResult(UserOperationClaimMessages.OperationClaimNotFound);
            }
            return new SuccessResult();
        }
        private IResult IsOperationSetExistForAdd(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(x => x.UserId == userOperationClaim.UserId && x.OperationClaimId == userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(UserOperationClaimMessages.OperationClaimAlreadyExist);
            }
            return new SuccessResult();

        }
        private IResult IsOperationSetExistForUpdate(UserOperationClaim userOperationClaim)
        {
           var currentUserOperationClaim = _userOperationClaimDal.Get(x=>x.Id == userOperationClaim.Id);
            if(currentUserOperationClaim.UserId != userOperationClaim.UserId  || currentUserOperationClaim.OperationClaimId != userOperationClaim.OperationClaimId)
            {
                var result = _userOperationClaimDal.Get(p => p.UserId == userOperationClaim.UserId && p.OperationClaimId == userOperationClaim.OperationClaimId);
                if (result != null)
                {
                    return new ErrorResult(UserOperationClaimMessages.OperationClaimAlreadyExist);
                }
            }
            return new SuccessResult();
        }
    }
} 

