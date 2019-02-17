namespace TenTwentyFour.ReCaptcha.Interfaces
{
    public interface IReCaptchaService
    {
        bool Validate(string response);
    }
}
