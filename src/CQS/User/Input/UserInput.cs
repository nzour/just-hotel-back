using Newtonsoft.Json;

namespace app.CQS.User.Input
{
    public class UserInput
    {
        public string Name { get; }
        public string Login { get; }

        [JsonConstructor]
        public UserInput(string name, string login)
        {
            Name = name;
            Login = login;
        }
    }
}