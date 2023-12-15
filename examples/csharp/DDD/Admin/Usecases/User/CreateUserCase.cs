namespace DDDExample.Admin.Usecases.User;

public class CreateUserCase
{
    public class Request
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }

    public class Response
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
