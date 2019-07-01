using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WarframeTrackerLib.Utilities {
    public class Hasher {
        public KeyValuePair<string, string> CreateSaltAndHash(string rawString) {
            byte[] saltBytes = CreateSalt();
            string hash = CreateHash(rawString, saltBytes);
            return new KeyValuePair<string, string>(Convert.ToBase64String(saltBytes), hash);
        }

        public bool Validate(string value, string rawString, string salt) {
            byte[] saltBytes = Convert.FromBase64String(salt);
            return value == CreateHash(rawString, saltBytes);
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
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(bytes);
                return bytes;
            }
        }
    }
}
