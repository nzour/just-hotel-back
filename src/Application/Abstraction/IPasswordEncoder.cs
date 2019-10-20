namespace App.Application.Abstraction
{
    public interface IPasswordEncoder
    {
        string Encrypt(string passwordToEncode);
    }
}