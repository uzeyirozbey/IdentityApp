﻿using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityBlogApp.Models.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {

        }
        public SignUpViewModel(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }

        [Required(ErrorMessage = "Kullanıcı ad alanı boş bırakılamaz!!")]
        [Display(Name = "Kullanıcı Adı : ")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Mail alanı boş bırakılamaz!!")]
        [Required(ErrorMessage = "Mail alanı boş bırakılamaz!!")]
        [Display(Name = "Email : ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz!!")]
        [Display(Name = "Telefon : ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!!")]
        [Display(Name = "Şifre : ")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifre aynı değildir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz!!")]
        [Display(Name = "Şifre Tekrar : ")]
        public string PasswordConfirm { get; set; }

    }
}
