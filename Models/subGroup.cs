using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace frushgah.Models
{
    public class subGroup
    {
        [Key]
        public Int64 id { get; set; }
        //**********************************************************************
        [Display(Name = "نام زیرگروه")]
        [Required(ErrorMessage = "لطفا نام زیر گروه را وارد نمایید")]
        public string? name { get; set; }
        //**********************************************************************
        [Display(Name = "تصویر")]
        public byte[]? pic { get; set; }
        //**********************************************************************
       
    }
}
