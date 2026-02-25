namespace Tracking.Application.Common.Interface
{
    public interface ICryptography
    {
        string Encrypt(string Texto);
        string Decrypt(string cipherText);
    }
}
