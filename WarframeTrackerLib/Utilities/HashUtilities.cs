using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WarframeTrackerLib.Utilities {
    public class HashUtilities {
        /// <summary>
        /// Return the KeyValuePair<salt, hash> for the supplied string
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public KeyValuePair<string, string> CreateSaltAndHash(string rawString) {
            byte[] saltBytes = CreateSalt();
            string hash = CreateHash(rawString, saltBytes);
            return new KeyValuePair<string, string>(Convert.ToBase64String(saltBytes), hash);
        }

        /// <summary>
        /// Validate the supplied string against the hash and salt
        /// </summary>
        /// <param name="stringToValidate"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public bool Validate(string stringToValidate, string hash, string salt) {
            byte[] saltBytes = Convert.FromBase64String(salt);
            return hash == CreateHash(stringToValidate, saltBytes);
        }

        private string CreateHash(string rawString, byte[] saltBytes) {
            string hash = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: rawString,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                ));
            return hash;
        }

        private byte[] CreateSalt() {
            byte[] bytes = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return bytes;
        }
    }
}
