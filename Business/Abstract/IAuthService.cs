﻿using Core.Entities.Concreate;
using Core.Utilities.Result;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);

        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
