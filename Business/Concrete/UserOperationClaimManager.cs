﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal; 

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        { 
            _userOperationClaimDal = userOperationClaimDal; 

        }
        public void Add(UserOperationClaim userOperationClaim)
        {
                _userOperationClaimDal.Add(userOperationClaim);
        }
    }
}
