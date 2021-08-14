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

        // submit�{�^�����N���b�N�����post���N�G�X�g���󂯕t�����Ƃ��ɓ���
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // �K�{���͂��Ȃ��Ȃǂ̏ꍇ���O�C�������Ȃ��B�i���O�C���y�[�W�ɖ߂�j
            if (!ModelState.IsValid) return Page();

            // ==================================================
            // �_�~�[�̔F�؏����i���ۂɂ�DB���g��:TODO�j
            // ===================================================
            var mockDB = new[] {
                (email: "Test_1@example.com", password: "pazzword1"),
                (email: "Test_2@example.com", password: "pazzword2")
            };

            // ���݃`�F�b�N
            var data = mockDB.AsEnumerable().Where(x => x.email == Input.Email && x.password == Input.Password);

            bool isValid = false;
            // ���݂����ꍇ�Atrue
            if (data.Count() > 0)
            {
                isValid = true;
            }

            // E���[���E�p�X���[�h���Ԉ���Ă���ꍇ���O�C�������Ȃ��B
            if (!isValid) return Page();

            // ���ȉ����O�C������
            // ���O�A�d�q���[�� �A�h���X�A�N��ASales ���[���̃����o�[�V�b�v�ȂǁAid ���̈ꕔ
            Claim[] claims = {
                new Claim(ClaimTypes.NameIdentifier, Input.Email), // ���j�[�NID
                new Claim(ClaimTypes.Name, Input.Email),
            };

            // ��ӂ� ID ���
            var claimsIdentity = new ClaimsIdentity(
              claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // ���O�C��
            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity),
              new AuthenticationProperties
              {
                // Cookie ���u���E�U�[ �Z�b�V�����Ԃŉi�������邩�H�i�u���E�U����Ă����O�A�E�g���Ȃ����ǂ����j
                IsPersistent = Input.RememberMe
              });

            return LocalRedirect(returnUrl ?? Url.Content("~/"));
        }
    }
}
