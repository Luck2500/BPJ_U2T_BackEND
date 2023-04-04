using BPJ_U2T.DTOS.Account;
using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;
using BPJ_U2T.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BPJ_U2T.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccountAll()
        {
            return Ok((await accountService.GetAll()).Select(AccountResponse.FromAccount));
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAccountByID(int id)
        {
            var result = await accountService.GetByID(id);
            if (result == null)
            {
                return Ok(new { msg = "ไม่มีผู้ใช้งานนี้" });
            }
            return Ok(AccountResponse.FromAccount(result));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginAccount([FromForm] LoginRequest loginRequest)
        {
            var result = await accountService.Login(loginRequest.Email, loginRequest.Password);
            if (result == null)
            {
                return Ok(new { msg = "เข้าสู่ระบบไม่สำเร็จ" });
            }
            var token = accountService.GenerateToken(result);

            return Ok(new { msg = "OK", data = AccountResponse.FromAccount(result), token});
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Account>> UpdateAccount(int id, [FromForm] RegisterRequest registerRequest)
        {
            var result = await accountService.GetByID(id);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบผู้ใช้" });
            }

            #region จัดการรูปภาพ
            if (registerRequest.FormFiles != null)
            {
                (string erorrMesage, string imageName) = await accountService.UploadImage(registerRequest.FormFiles);
                if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);

                if (!string.IsNullOrEmpty(imageName))
                {
                    await accountService.DeleteImage(result.Image);
                    result.Image = imageName;
                }
            }
            #endregion
            var account = registerRequest.Adapt(result);
            if (registerRequest.FormFiles != null)
            {
                account.Image = result.Image;
            }
            await accountService.Update(account);
            return Ok(new { msg = "OK", data = AccountResponse.FromAccount(account) });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAccount([FromForm] RegisterRequest registerRequest)
        {
            #region จัดการรูปภาพ
            (string erorrMesage, string imageName) = await accountService.UploadImage(registerRequest.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);
            #endregion

            var account = registerRequest.Adapt<Account>();
            account.Image = imageName;

            var data = await accountService.Register(account);

            if (data != null) return Ok(data);
            return Ok(new { msg = "OK", data = account });
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await accountService.GetByID(id);
            if (result == null) return Ok(new { msg = "ไม่พบผู้ใช้" });
            await accountService.Delete(result);
            await accountService.DeleteImage(result.Image);
            return Ok(new { msg = "OK", data = result });
        }
    }
}
