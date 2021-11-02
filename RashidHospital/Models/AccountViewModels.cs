using Hospital.DAL;
using RashidHospital.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace RashidHospital.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : BusinessBaseClass<AspNetUser, RegisterViewModel>
    {
        public DbBaseClass<AspNetRole> rolslist;
        public RegisterViewModel()
        {
            rolslist = new DbBaseClass<AspNetRole>();
        }
        public System.Guid Id   { get; set; }
        public string stringId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 11)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }
        [Required]
        [Display(Name = "Third Name")]
        public string ThirdName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.Guid Modifiedby { get; set; }
        public string SelectedRoles { get; set; }
        public string RolesString { get; set; }
        public MultiSelectList RoleList { get; set; }
        [Required]
        public List<string> RolesId { get; set; }
        public MultiSelectList ConvertRoles()
        {
            //AspNetRole _obj = new AspNetRole();
            var RoleList = rolslist.GetAll<AspNetRole>();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var role in RoleList)
            {
                SelectListItem _item = new SelectListItem();

                _item.Text = role.Name;


                _item.Value = role.Id.ToString();
                items.Add(_item);
            }
            MultiSelectList MultiRoleList = new MultiSelectList(items, "Value", "Text");
            return MultiRoleList;
        }

        internal override AspNetUser Convert(RegisterViewModel Obj)
        {
            if (Obj == null)
                return null;
            else
            {
                _Obj = new AspNetUser
                {
                    CreatedBy = Obj.CreatedBy,
                    CreatedDate = Obj.CreatedDate,
                    Email = Obj.Email,
                    FirstName = Obj.FirstName,
                    Id = Obj.Id,
                    IsDeleted = Obj.IsDeleted,
                    IsActive = Obj.IsActive,
                    ModifiedDate = Obj.ModifiedDate,
                    PhoneNumber = Obj.PhoneNumber,
                    SecondName = Obj.SecondName,
                    ThirdName = Obj.ThirdName,
                    PasswordHash = Obj.PasswordHash,
                    UserName=Obj.UserName,
                    ModifiedBy=Obj.Modifiedby,
                    

                };
            }
            return _Obj;
        }

        internal override RegisterViewModel Convert(AspNetUser Obj)
        {
            return new RegisterViewModel()
            {

                CreatedBy = Obj.CreatedBy,
                CreatedDate = Obj.CreatedDate,
                Email = Obj.Email,
                FirstName = Obj.FirstName,
                Id = Obj.Id,
                stringId=Obj.Id.ToString(),
                IsDeleted = Obj.IsDeleted,
                IsActive = Obj.IsActive,
                ModifiedDate = Obj.ModifiedDate,
                PhoneNumber = Obj.PhoneNumber,
                SecondName = Obj.SecondName,
                ThirdName = Obj.ThirdName,
                PasswordHash = Obj.PasswordHash,
                UserName = Obj.UserName
            };
        }
        public RegisterViewModel SelectObject(Guid Id)
        {
            AspNetUser _BClass = new AspNetUser();

            RegisterViewModel Object = Convert(_BClass.Getobject(Id));
            return Object;
        }
        public RegisterViewModel SelectObjectyEmail(string email)
        {
            AspNetUser _BClass = new AspNetUser();
            IEnumerable<AspNetUser> UsersList = _BClass.Where(a => a.Email == email);
            List<AspNetUser> Users = UsersList.ToList();
            RegisterViewModel Object = Convert(Users.FirstOrDefault());
            return Object;
        }
        public void Edit()
        {
            try
            {
                _Obj = Convert(this);
                _Obj.Edit();
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]

        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class RoleViewModel
    {
        [Display(Name = "RoleName")]
        [EmailAddress]
        [Required(ErrorMessage = "Role Name is Required")]
        public string Name { get; set; }

        public System.Guid Id { get; set; }


    }

}
