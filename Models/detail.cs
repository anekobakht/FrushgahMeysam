using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace frushgah.Models
{
    public class detail
    {
        [Key]
        public Int64 idDetail { get; set; }
        //**********************************************************************
        public Int64 idSubGroup { get; set; }
        //**********************************************************************
        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "لطفا نام کالا را وارد نمایید")]
        public string? nameDetail { get; set; }
        //**********************************************************************
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا قیمت کالا را وارد نمایید")]
        public Int64 price { get; set; }
        //**********************************************************************
        [Display(Name = "تصویر اول")]
        public byte[]? pic1 { get; set; }
        //**********************************************************************
        [Display(Name = "تصویر دوم")]
        public byte[]? pic2 { get; set; }
        //**********************************************************************
        [Display(Name = "تصویر سوم")]
        public byte[]? pic3 { get; set; }
        //**********************************************************************

    }
}
