using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Task.Core.Entities;


namespace Task.Application.Interfaces.Token
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateToken(AppUser user);
    }
}
