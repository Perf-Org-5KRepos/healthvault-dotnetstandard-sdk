﻿using Microsoft.HealthVault.Authentication;

namespace Microsoft.HealthVault.Connection
{
    /// <summary>
    /// Not yet flushed
    /// </summary>
    public interface ICryptographer
    {
        /// <summary>
        /// Gets the hash algorithm.
        /// </summary>
        /// <value>
        /// The hash algorithm.
        /// </value>
        string HashAlgorithm { get; }

        /// <summary>
        /// Gets the hmac algorithm.
        /// </summary>
        /// <value>
        /// The hmac algorithm.
        /// </value>
        string HmacAlgorithm { get; }

        /// <summary>
        /// Hmacs the specified key material.
        /// </summary>
        /// <param name="keyMaterial">The key material.</param>
        /// <param name="text">The text.</param>
        /// <returns>CryptoHmac</returns>
        CryptoHmac Hmac(string keyMaterial, string text);

        /// <summary>
        /// Hashes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>CryptoHash</returns>
        CryptoHash Hash(string text);
    }
}