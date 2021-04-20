namespace RestAPI101_Back
{
    public static class APIRoutes
    {
        // Auth
        public const string AuthController = "api/auth";
        public static class Auth
        {
            public const string Login = "login";
            public const string Register = "register";
        }

        // User
        public const string UserController = "api/user";
        public static class User
        {
            public const string Get = "";
            public const string Delete = "";
            public const string ChangeName = "username";
            public const string ChangePassword = "password";
        }

        // Labels
        public const string LabelsController = "api/labels";
        public static class Labels
        {
            public const string GetAll = "";
            public const string GetSpecified = "{id}";
            public const string Create = "";
            public const string Update = "{id}";
            public const string Delete = "{id}";
        }

        // Todos
        public const string TodosController = "api/todos";
        public static class Todos
        {
            public const string GetAll = "";
            public const string GetSpecified = "{id}";
            public const string Create = "";
            public const string Update = "{id}";
            public const string PartialUpdate = "{id}";
            public const string Delete = "{id}";
            public const string Reorder = "reorder/{id}:{newOrder}";
            public const string AddLabel = "label/{id}:{labelId}";
            public const string RemoveLabel = "label/{id}:{labelId}";
        }
    }
}
