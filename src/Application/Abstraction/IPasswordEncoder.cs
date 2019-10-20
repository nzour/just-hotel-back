namespace app.Application.Abstraction
{
    public interface IPasswordEncoder
    {
        string Encrypt(string passwordToEncode);
    }
}