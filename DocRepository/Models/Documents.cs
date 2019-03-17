using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocRepository.Models
{
    public class Documents
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Введите наименование документа")]
        [Display(Name = "Наименование документа")]
        public virtual string DocName { get; set; }
        //обрезка имени свыше 30 символов
        public virtual string ShortDocName
        {
            get
            {
                return Truncate(DocName, 30);
            }
        }

        [Required]
        [Display(Name = "Время создания")]
        public virtual DateTime DocDate { get; set; }
        [Required]
        [Display(Name = "Автор документа")]
        public virtual string DocAuthor { get; set; }
        [Required(ErrorMessage = "Укажите файл документа")]
        [Display(Name = "Ссылка на файл")]
        public virtual string DocFileName { get; set; }

        public virtual HttpPostedFileBase file { get; set; }

        //функция обрезки
        private string Truncate(string DocName, int len)
        {
            if (DocName != null)
            {
                if (DocName.Length > len)
                {
                    DocName = DocName.Substring(0, len);
                    DocName += "...";
                }
            }
            return DocName;
        }
    }
    
}
