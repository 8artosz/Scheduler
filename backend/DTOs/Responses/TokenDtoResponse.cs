using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs.Responses
{
    public class TokenDtoResponse
    {
        public TokenDtoResponse(string one, string two)
        {
            this.accessToken = one;
            this.refreshToken = two;
        }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
