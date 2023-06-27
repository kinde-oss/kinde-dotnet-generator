﻿using Kinde.Api.Client;
using Kinde.Api.Models.Configuration;
using Kinde.Api.Models.Utils;

namespace Kinde.Api
{
    /// <summary>
    /// Provide thread safe dictionary to save client instances
    /// </summary>
    public class KindeClientFactory : AuthorizationCodeStore<string, KindeClient>
    {
        private static KindeClientFactory _factory;

        /// <summary>
        /// Constructor for kinde client factory
        /// </summary>
        protected KindeClientFactory()
        {
        }

        /// <summary>
        /// Provide singleton instance for kinde client factory
        /// </summary>
        public static KindeClientFactory Instance
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new KindeClientFactory();
                }
                return _factory;
            }
        }

        /// <summary>
        /// Use session Id for instance to persist users authentication
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="identityProviderConfiguration"></param>
        /// <returns></returns>
        public KindeClient GetOrCreate(string instanceId, IApplicationConfiguration identityProviderConfiguration)
        {
            return GetOrCreate(instanceId, identityProviderConfiguration, new KindeHttpClient());
        }

        /// <summary>
        /// Use session Id for instance to persist users authentication
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="identityProviderConfiguration"></param>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public KindeClient GetOrCreate(string instanceId, IApplicationConfiguration identityProviderConfiguration, HttpClient httpClient)
        {
            if (_dictionary.TryGetValue(instanceId, out var cached))
            {
                return cached;
            }

            var client = new KindeClient(identityProviderConfiguration, httpClient);
            _dictionary.Add(instanceId, client);
            return Get(instanceId);
        }
    }
}