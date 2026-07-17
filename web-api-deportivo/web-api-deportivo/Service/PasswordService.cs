using Microsoft.AspNetCore.Identity;

namespace web_api_deportivo.Service
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();
        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool Verify(string password, string hash)
        {
            var result = _hasher.VerifyHashedPassword(null!, hash, password);

            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
