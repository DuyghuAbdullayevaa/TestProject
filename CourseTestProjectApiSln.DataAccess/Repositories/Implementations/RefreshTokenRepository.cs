using CourseTestProjectApiSln.DataAccess.Data;
using CourseTestProjectApiSln.DataAccess.Entities;
using CourseTestProjectApiSln.DataAccess.Repositories.Abstactions;
using CourseTestProjectApiSln.DataAccess.Repositories.Implementations.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.DataAccess.Repositories.Implementations
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly ApiCourseSystem _context;

        public RefreshTokenRepository(ApiCourseSystem context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string userName)
        {
            // Fetch the most recent refresh token associated with the user and check if it's valid
            return await _context.RefreshTokens
                .Include(x => x.User)
                .Where(x => x.User.UserName == userName && !x.IsDeleted && x.ExpirationDate > DateTime.Now)
                .OrderByDescending(x => x.ExpirationDate) // Get the most recent one
                .FirstOrDefaultAsync();
        }
        public void Remove(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
        }
        public async Task<RefreshToken?> GetByUserIdAsync(int userId)
        {
            return await _context.RefreshTokens
                                          .FirstOrDefaultAsync(rt => rt.UserId == userId);
        }

      
    }

}

