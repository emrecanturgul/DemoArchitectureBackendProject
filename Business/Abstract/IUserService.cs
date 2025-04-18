﻿using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(RegisterAuthDto authDto);
        List<User> GetList(); 
        User GetByEmailAddress(string email);
        void Delete(LoginAuthDto loginDto);
    }
}
    