using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDTOs
{
    public record AuthenticatedUserDto
    {
        public int UserId { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }
    }
}
