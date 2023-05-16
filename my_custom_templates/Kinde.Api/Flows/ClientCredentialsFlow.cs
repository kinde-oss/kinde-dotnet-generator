﻿using Kinde.Api.Enums;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.User;

namespace Kinde.Api.Flows
{
    public class ClientCredentialsFlow : BaseAuthorizationFlow<ClientCredentialsConfiguration>, IAuthorizationFlow
    {
        public override bool RequiresRedirection => false;

        public override IUserActionResolver UserActionsResolver => new DefaultUserActionResolver();

        public ClientCredentialsFlow(IApplicationConfiguration identityProviderConfiguration, ClientCredentialsConfiguration configuration) : base(identityProviderConfiguration, configuration)
        {

        }

        public override async Task<AuthorizationStates> Authorize(HttpClient httpClient, bool register = false)
        {
            var parameters = CreateBaseRequestParameters(register);
            parameters.Add("client_secret", Configuration.ClientSecret);
            parameters.Add("grant_type", "client_credentials");
            return await SendRequest(httpClient, parameters);
        }

        protected override async Task RenewInternal(HttpClient client)
        {
            //Client credentials doesn't return refresh token
            // So just re-auth it, when needed
            await Authorize(client);
        }

        public override void OnCodeReceived(HttpClient httpClient, string key, string value)
        {
            throw new NotImplementedException("Code is not applicable for this flow");
        }
    }
}
