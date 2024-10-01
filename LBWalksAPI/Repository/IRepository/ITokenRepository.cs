using Microsoft.AspNetCore.Identity;

namespace LBWalksAPI.Repository.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
