using ProductApi.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace ProductApi.Commands
{
    public static class SellerHashHelperCommand
    {
        public static SellerRegisterViewModel HashPasswordReturnModel(SellerRegisterViewModel model)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(model.password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                model.password = builder.ToString();
                return model;
            }
        }

        //public static bool VerifyPassword(string password, string hashedPassword)
        //{
        //    string hashOfInput = HashPassword(password);
        //    return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hashedPassword) == 0;
        //}
    }
}
