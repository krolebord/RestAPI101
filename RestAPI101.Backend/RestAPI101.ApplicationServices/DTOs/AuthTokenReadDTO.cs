using System;
using RestAPI101.Domain.Entities;

namespace RestAPI101.ApplicationServices.DTOs
{
    public class AuthTokenReadDTO
    {
        public string Token { get; }
        public DateTime Expires { get; }

        public AuthTokenReadDTO(string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
    }

    public static class AuthTokenReadDTOMapper
    {
        public static AuthTokenReadDTO ToReadDTO(this AuthToken token) =>
            new AuthTokenReadDTO(token.Token, token.Expires);
    }
}
