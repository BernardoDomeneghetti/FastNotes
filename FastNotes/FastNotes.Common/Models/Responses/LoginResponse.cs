using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastNotes.Common.Models.Responses
{
    public class LoginResponse
    {
        public string UserNickname { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string JwtToken { get; set; } = string.Empty;
    }
}
