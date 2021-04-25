using System;
using System.ComponentModel.DataAnnotations;
using RestAPI101.Domain.Models;

namespace RestAPI101.Domain.DTOs
{
    public class AuthTokenReadDTO
    {
        [Required]
        public string Token { get; }
        [Required]
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
