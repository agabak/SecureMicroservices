using IdentityModel.Client;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MVC.Client.HttpHandlers
{
    public class AuthenticationDelegatingHandler: DelegatingHandler
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ??
                    throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override async Task<HttpResponseMessage> 
            SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContextAccessor
                 .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            if(!string.IsNullOrWhiteSpace(accessToken))
            {
                request.SetBearerToken(accessToken);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
