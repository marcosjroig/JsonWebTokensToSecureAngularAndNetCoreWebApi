using PtcApi.Model;

namespace PtcApi.Security
{
    public interface ISecurityManager
    {
        AppUserAuth ValidateUser(AppUser user);
    }
}
