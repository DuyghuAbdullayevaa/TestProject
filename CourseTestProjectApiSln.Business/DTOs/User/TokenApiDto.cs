using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTestProjectApiSln.Business.DTOs.User
{
    public class TokenApiDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
