using Core.DataAccess;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
            List<OperationClaim> userOperationClaims(int userId)
    }
}
