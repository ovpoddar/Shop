namespace Checkout.Helpers
{
    public interface IUserhelper
    {
        string CheckUserValidToken();
        bool CheckCookie();
    }
}
