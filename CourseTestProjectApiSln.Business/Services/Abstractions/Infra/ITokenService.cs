using CourseTestProjectApiSln.Business.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseTestProjectApiSln.DataAccess.Entities;
using System.Security.Claims;


namespace CourseTestProjectApiSln.Business.Services.Abstractions.Infra
{
    public interface ITokenService
    {
        TokenResponseDto GetToken(User appUser);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
