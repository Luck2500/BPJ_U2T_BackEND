using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Settings;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BPJ_U2T.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext databaseContext;
        private readonly JwtSetting jwtSetting;
        private readonly IUploadFileService uploadFileService;

        public AccountService(DatabaseContext databaseContext, JwtSetting jwtSetting,IUploadFileService uploadFileService)
        {
            this.databaseContext = databaseContext;
            this.jwtSetting = jwtSetting;
            this.uploadFileService = uploadFileService;
        }

        public async Task<Account> Login(string email, string password)
        {
            var result = await databaseContext.Account.Include(a => a.Role).SingleOrDefaultAsync(e => e.Email == email);
            if (result != null && VerifyPassword(result.Password, password))
            {
                return result;
            }
            return null;
        }

        public async Task<object> Register(Account account)
        {
            if (account.RoleID == 0)
            {
                account.RoleID = 1;
            }
            var result = await databaseContext.Account.SingleOrDefaultAsync(e => e.Email == account.Email);
            if (result != null) return new { msg = "อีเมลซ้ำ" };
            //------------- Password ที่ไม่ผ่านการ Has ---------
            //await AddPassword(customer.ID, customer.Password);

            account.Password = CreateHashPassword(account.Password);
            //------------- Password ที่ผ่านการ Has ---------
            //customer.Password = CreateHashPassword(customer.Password);
            await databaseContext.Account.AddAsync(account);
            await databaseContext.SaveChangesAsync();
            return null;
        }
        public int GenerateID()
        {
            var result = databaseContext.Account.OrderByDescending(a => a.ID).FirstOrDefault();
            if (result != null)
            {
                int ID = Convert.ToInt32(result.ID);
                return ID + 1;
            }
            return 1;
        }

        public async Task<Account> GetByID(int id)
        {
            var result = await databaseContext.Account.Include(e => e.Role).AsNoTracking().FirstOrDefaultAsync(e => e.ID == id);
            if (result == null) return null;
            return result;
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;

            var imageName = string.Empty;
            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImages(fileName);
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await databaseContext.Account.Include(e => e.Role).ToListAsync();
        }

        private string CreateHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            var hashed = HashPassword(password, salt);

            var hpw = $"{Convert.ToBase64String(salt)}.{hashed}";
            return hpw;
        }
        private bool VerifyPassword(string saltAndHashFromDB, string password)
        {
            var parts = saltAndHashFromDB.Split('.', 2);
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var passwordHash = parts[1];

            var hashed = HashPassword(password, salt);

            return hashed == passwordHash;
        }
        private string HashPassword(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        

        public async Task Delete(Account account)
        {
            databaseContext.Remove(account);
            await databaseContext.SaveChangesAsync();
        }

        public async Task Update(Account account)
        {
            databaseContext.Account.Update(account);
            await databaseContext.SaveChangesAsync();
        }

        public string GenerateToken(Account account)
        {
            //payload หรือ claim ข้อมูลที่ต้องการเก็บ ใส่อะไรก็ได้//
            //Claim("Sub", account.Username) ใส่ค่าที่ที่ไม่ซ้ำเช่น Username
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,account.Email),
                new Claim("role",account.Role.Name),
                new Claim("additonal","TestSomething"),
                new Claim("todo day","10/10/99"),

            };

            return BuildToken(claims);
        }

        private string BuildToken(Claim[] claims)
        {
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSetting.Expire)); //ดึงข้อหมดอายุมา เเล้ว + วันที่
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key)); //ให้ทำการเข้ารหัสอีกครั้ง 1
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //สร้าง Token ของเเท้
            var token = new JwtSecurityToken(
                issuer: jwtSetting.Issuer,
                audience: jwtSetting.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            //เขียน Token ออกมา
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Account GetInfo(string accessToken)
        {
            //แปลงค่า Token (ถอดรหัส)
            var token = new JwtSecurityTokenHandler().ReadToken(accessToken) as JwtSecurityToken;

            //ค้นหาค่า key ขึ้นมา
            var email = token.Claims.First(claim => claim.Type == "sub").Value;
            var role = token.Claims.First(claim => claim.Type == "role").Value;

            var account = new Account
            {
                Email = email,
                Role = new Role
                {
                    Name = role
                }
            };

            return account;
        }
    }
}
