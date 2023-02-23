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
        public Int64 idSubGroup { get; set; }
        //**********************************************************************
        [Display(Name = "نام گروه")]
        public Int64 idGroup { get; set; }
        //**********************************************************************
        [Display(Name = "نام زیرگروه")]
        [Required(ErrorMessage = "لطفا نام زیر گروه را وارد نمایید")]
        public string? nameSubGroup { get; set; }
        //**********************************************************************
        [Display(Name = "تصویر")]
        public byte[]? picSubGroup { get; set; }
        //**********************************************************************
       
    }
}
