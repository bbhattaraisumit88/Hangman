﻿namespace Hangman.Service.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }

            public static class DefaultRoles
            {
                public const string Superuser = "superuser";
                public const string Guest = "guest";
            }
        }
    }
}