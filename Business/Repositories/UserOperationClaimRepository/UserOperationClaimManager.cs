using Business.Repositories.UserOperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository.Constants;
using Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using Core.Utilities.Aspects.Validation;
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

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        { 
            _userOperationClaimDal = userOperationClaimDal; 
        }
        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim operationClaim)
        {
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
            _userOperationClaimDal.Update(operationClaim);
            return new SuccessResult(UserOperationClaimMessages.Update);

        }
    }
    }

