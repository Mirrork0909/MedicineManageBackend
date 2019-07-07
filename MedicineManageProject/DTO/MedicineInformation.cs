using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MedicineManageProject.DTO
{
    public class MedicineInformation
    {
        [Display(Name = "药品批准文号")]
        [Required(ErrorMessage = "{0} 是必填项")]
        [StringLength(13,MinimumLength =13,ErrorMessage ="{0}的长度应该为13")]
        public String MEDICINE_ID { set; get; }
        [Display(Name = "药品名称")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_NAME { set; get; }
        [Display(Name = "药品类别")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_TYPE { set; get; }
        [Display(Name = "药品成分")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_INGREDIENT { set; get; }
        [Display(Name = "药品性状")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_CHARACTER { set; get; }
        [Display(Name = "药品适应症")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_APPLICABILITY { set; get; }
        [Display(Name = "药品用法")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_USAGE { set; get; }
        [Display(Name = "药品注意事项")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_ATTENTION { set; get; }
        [Display(Name = "药品图片")]
        [Required(ErrorMessage = "{0} 是必填项")]
        public String MEDICINE_IMAGE { set; get; }
    }
}
