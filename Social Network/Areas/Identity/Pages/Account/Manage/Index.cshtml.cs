using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Social_Network.Data;

namespace Social_Network.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }
        public string CreatedAt { get; set; }
       


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Username must contains only letters")]
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            [DataType(DataType.Text)]
            [Display(Name = "Full Name")]
            public string Fullname { get; set; }

            [Display(Name = "Profile Image")]
            public string ProfileUrl { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "Status")]
            [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
            public string Status { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Description")]
            [StringLength(125, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 0)]
            public string Desription { get; set; }

        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var fullname = user.FullName;
            var profileurl = user.ProfileUrl;
            var status = user.Status;
            var description = user.Desription;

            var createdAt = user.CreatedAt.ToString();

            Username = userName;
            CreatedAt = createdAt;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Fullname = fullname,
                ProfileUrl = profileurl,
                Status = status,
                Desription = description
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile img)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if ( Input.PhoneNumber == phoneNumber && Input.Fullname == user.FullName && Input.Desription == user.Desription&& Input.Status == user.Status && img == null)
            {
                StatusMessage = "You nothing changed";
                return RedirectToPage();
            }

            if (img != null)
            {
                var newProfileUrl = Helpers.Media.UploadProfilePictures(img, "Profile_Pictures");
                if (string.IsNullOrEmpty(newProfileUrl))
                {
                    StatusMessage = "Something went wrong due uploading new avatar.";
                    return RedirectToPage();
                }
                else
                {
                    user.ProfileUrl = newProfileUrl;
                }
            }

            user.FullName = Input.Fullname;
            user.Status = Input.Status;
            user.PhoneNumber = Input.PhoneNumber;
            user.Desription = Input.Desription;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                StatusMessage = "Oooops, there was error in uploading your informtion. Try gain";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
