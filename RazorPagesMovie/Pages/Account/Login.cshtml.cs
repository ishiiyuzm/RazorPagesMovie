using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        // submitボタンがクリックされてpostリクエストを受け付けたときに動く
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // 必須入力がないなどの場合ログインさせない。（ログインページに戻る）
            if (!ModelState.IsValid) return Page();

            // ==================================================
            // ダミーの認証処理（実際にはDBを使う:TODO）
            // ===================================================
            var mockDB = new[] {
                (email: "Test_1@example.com", password: "pazzword1"),
                (email: "Test_2@example.com", password: "pazzword2")
            };

            // 存在チェック
            var data = mockDB.AsEnumerable().Where(x => x.email == Input.Email && x.password == Input.Password);

            bool isValid = false;
            // 存在した場合、true
            if (data.Count() > 0)
            {
                isValid = true;
            }

            // Eメール・パスワードが間違っている場合ログインさせない。
            if (!isValid) return Page();

            // ★以下ログイン処理
            // 名前、電子メール アドレス、年齢、Sales ロールのメンバーシップなど、id 情報の一部
            Claim[] claims = {
                new Claim(ClaimTypes.NameIdentifier, Input.Email), // ユニークID
                new Claim(ClaimTypes.Name, Input.Email),
            };

            // 一意の ID 情報
            var claimsIdentity = new ClaimsIdentity(
              claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // ログイン
            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              new AuthenticationProperties
              {
                // Cookie をブラウザー セッション間で永続化するか？（ブラウザを閉じてもログアウトしないかどうか）
                IsPersistent = Input.RememberMe
              });

            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }
    }
}
