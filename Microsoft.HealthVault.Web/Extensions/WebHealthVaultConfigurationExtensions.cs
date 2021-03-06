﻿// Copyright (c) Microsoft Corporation.  All rights reserved.
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Microsoft.HealthVault.Web.Configuration;
using static Microsoft.HealthVault.Web.Constants.HealthVaultWebConstants;

namespace Microsoft.HealthVault.Web.Extensions
{
    internal static class WebHealthVaultConfigurationExtensions
    {
        private static readonly string s_authTarget = "AUTH";

        /// <summary>
        /// Gets the URL to/from which BLOBs get streamed, for
        /// the configured default instance of the HealthVault web-service.
        /// </summary>
        ///
        /// <remarks>
        ///  When reading from web.config, this property corresponds to the "HealthVaultUrl" configuration
        /// value with the path modified to the appropriate handler.
        /// </remarks>
        public static Uri BlobStreamUrl(this WebHealthVaultConfiguration config) => config.DefaultHealthVaultUrl?.ReplacePath(Urls.BlobStreamUrlSuffix);

        /// <summary>
        /// Gets the HealthVault client service URL for
        /// the configured default instance of the HealthVault web-service,
        /// from the application or web configuration file.
        /// </summary>
        ///
        /// <remarks>
        ///  When reading from web.config, this property corresponds to the "HV_HealthVaultUrl" configuration
        /// value with the path modified to the appropriate handler.
        /// </remarks>
        public static Uri HealthClientServiceUrl(this WebHealthVaultConfiguration config) => config.DefaultHealthVaultUrl?.Append(Urls.HealthClientServiceSuffix);

        /// <summary>
        /// Gets the URL of the HealthVault Shell authentication page.
        /// </summary>
        ///
        /// <remarks>
        /// When reading from web.config, this property uses the "HV_ShellUrl" configuration value to construct the
        /// redirector URL with a target of "AUTH".
        /// </remarks>
        ///
        public static Uri HealthVaultShellAuthenticationUrl(this WebHealthVaultConfiguration config)
        {
            var redirect = new ShellRedirectParameters(config.DefaultHealthVaultShellUrl.OriginalString)
            {
                TargetLocation = s_authTarget,
                ApplicationId = config.MasterApplicationId,
            };

            return redirect.ConstructRedirectUrl();
        }

        /// <summary>
        /// Gets the HealthVault type schema root URL for
        /// the configured default instance of the HealthVault web-service.
        /// </summary>
        ///
        /// <remarks>
        /// This property corresponds to the "HV_HealthServiceUrl" configuration value when reading from web.config.
        /// </remarks>
        ///
        public static Uri HealthVaultTypeSchemaUrl(this WebHealthVaultConfiguration config) => new Uri(config.DefaultHealthVaultUrl, Urls.TypeSchemaSuffix);

        /// <summary>
        /// Shell redirector url, derived from web config.
        /// </summary>
        ///
        public static Uri ShellRedirectorUrl(this WebHealthVaultConfiguration config) => config.DefaultHealthVaultShellUrl?.Append(ConfigDefaults.ShellRedirectorLocation);

        /// <summary>
        /// Gets the URL of the page corresponding to the action.
        /// </summary>
        ///
        /// <remarks>
        /// This property corresponds to the "HV_Action*" configuration values when reading from web.config.
        /// Returns null in case Action is not specified in web.config.
        /// </remarks>
        public static Uri TryGetActionUrl(this WebHealthVaultConfiguration config, string action)
        {
            Uri actionUri;

            Dictionary<string, Uri> actionPageUrls = config.ActionPageUrls;
            actionPageUrls.TryGetValue(action, out actionUri);

            return actionUri;
        }

        // Helpers

        private static Uri ReplacePath(this Uri original, string newPathWithLeadingSlash)
        {
            if (original == null)
            {
                return null;
            }

            return new Uri(original.GetLeftPart(UriPartial.Authority) + newPathWithLeadingSlash);
        }

        private static Uri Append(this Uri baseUrl, string path)
        {
            if (baseUrl == null)
            {
                return null;
            }

            return new Uri(baseUrl, path);
        }
    }
}
