namespace WebAPI.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CarRentalSystem";

        public static class Roles
        {
            public const string UserRoleName = "user";

            public const string AdministratorRoleName = "admin";
        }

        public static class ConfigurationKeys
        {
            public const string DbConnectionStringKey = "DefaultConnection";

            public const string ClientUrlKey = "ClientUrl";
            public const string ApplicationUrlKey = "ApplicationUrl";

            private static string GetChildKey(string parentKey, string childKey)
            {
                return $"{parentKey}:{childKey}";
            }

            public static class Admin
            {
                public static readonly string UsernameKey = GetChildKey(ParentObject, "Username");

                public static readonly string PasswordKey = GetChildKey(ParentObject, "Password");

                private const string ParentObject = "admin";
            }

            public static class JWT
            {
                public static readonly string AudienceKey = GetChildKey(ParentObject, "Audience");
                public static readonly string IssuerKey = GetChildKey(ParentObject, "Issuer");
                public static readonly string SecretKey = GetChildKey(ParentObject, "Key");

                private const string ParentObject = "JWT_Settings";
            }

            public static class Gmail
            {
                public static readonly string Email = GetChildKey(ParentObject, "Email");

                public static readonly string Password = GetChildKey(ParentObject, "Password");

                private const string ParentObject = "Gmail";
            }
        }
    }
}
