using IdentityModel.Client;
using System.Threading.Tasks;

namespace NaKun.WebAPI.Client.Services
{
    public interface IIdentityServerService
    {
        Task<TokenResponse> GetToken(string apiScope);
    }
}
