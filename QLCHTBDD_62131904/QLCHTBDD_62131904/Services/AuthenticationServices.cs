using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QLCHTBDD_62131904.Services
{
    public class AuthenticationServices
    {
        private const int SaltSize = 16; // Kích thước salt (16 bytes)
        private const int Iterations = 10000; // Số vòng băm (càng nhiều càng bảo mật hơn)
        // Method hash password
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(password));
            }

            // Tạo salt ngẫu nhiên (16 bytes)
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt); // Tạo salt ngẫu nhiên
            }

            // Chuyển đổi mật khẩu thành mảng byte
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Kết hợp mật khẩu và salt để băm
            byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
            passwordBytes.CopyTo(passwordWithSalt, 0);
            salt.CopyTo(passwordWithSalt, passwordBytes.Length);

            // Băm mật khẩu và salt với số vòng băm
            byte[] hashBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(passwordWithSalt);
                for (int i = 1; i < Iterations; i++)
                {
                    hashBytes = sha256.ComputeHash(hashBytes);
                }
            }

            // Kết hợp hash và salt thành một chuỗi để lưu vào cơ sở dữ liệu
            byte[] result = new byte[hashBytes.Length + salt.Length];
            hashBytes.CopyTo(result, 0);
            salt.CopyTo(result, hashBytes.Length);

            // Chuyển đổi thành chuỗi hex để lưu vào cơ sở dữ liệu
            return Convert.ToBase64String(result);
        }

        // Method verify password
        public static bool VerifyPassword(string storedHash, string password)
        {
            // Chuyển chuỗi base64 từ cơ sở dữ liệu thành mảng byte
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            // Lấy phần hash và phần salt
            byte[] hashBytes = new byte[32]; // SHA-256 hash length is 32 bytes
            byte[] salt = new byte[SaltSize];
            Array.Copy(storedHashBytes, 0, hashBytes, 0, 32);
            Array.Copy(storedHashBytes, 32, salt, 0, SaltSize);

            // Kết hợp mật khẩu và salt để băm
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
            passwordBytes.CopyTo(passwordWithSalt, 0);
            salt.CopyTo(passwordWithSalt, passwordBytes.Length);

            // Băm mật khẩu và salt với số vòng băm
            byte[] hashWithSaltBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashWithSaltBytes = sha256.ComputeHash(passwordWithSalt);
                for (int i = 1; i < Iterations; i++)
                {
                    hashWithSaltBytes = sha256.ComputeHash(hashWithSaltBytes);
                }
            }

            // So sánh hash đã tính toán với hash lưu trữ
            return hashWithSaltBytes.SequenceEqual(hashBytes);
        }
    }
}