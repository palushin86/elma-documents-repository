using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocRepository.Models
{
    public class Users
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин")]
        public virtual string Login { get; set; }
        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [Display(Name = "Пароль")]
        public virtual string Password { get; set; }
    }
}