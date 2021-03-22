namespace RestAPI101_Back {
    public static class APIRoutes {
        // Auth
        public const string AuthController = "api/auth";
        public static class Auth {
            public const string Login = "login";
            public const string Register = "register";
            public const string Delete = "delete";
        }
        
        // Todos
        public const string TodosController = "api/todos";
        public static class Todos {
            public const string GetAll = "";
            public const string GetSpecified = "{id}";
            public const string Create = "";
            public const string Update = "{id}";
            public const string PartialUpdate = "{id}";
            public const string Delete = "{id}";
        }
    }
}