using GiftPicker.Db.Models;
using GiftPicker.Db.Models.Search.Users;
using System.ComponentModel.DataAnnotations;

namespace GiftPicker.Web.ViewModels
{
    public class UsersSearchModel : BaseSearchModel<Response, User>
    {
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}