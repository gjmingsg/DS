using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nepton.Models
{
    [MetadataType(typeof(NT_ContactMetadata))]
    public partial class NT_Contact
    {
        private class NT_ContactMetadata {

            [DisplayName("留言主键")]
            public Guid ContactID { get; set; }

            [Required(ErrorMessage="请填写姓名")]
            [StringLength(30,ErrorMessage="姓名不能超过30个字")]
            [DisplayName("姓名")]
            public string UserName { get; set; }

            [StringLength(50, ErrorMessage = "公司名不能超过50个字")]
            [DisplayName("公司")]
            public string Company { get; set; }

            [Required(ErrorMessage = "请填写电话")]
            [StringLength(30, ErrorMessage = "电话不能超过30个字")]
            //[RegularExpression(@"^(\d{3,4}-)?\d{6,8}$", ErrorMessage = "填写的电话号码不正确")]
            [DisplayName("电话")]
            public string Tel { get; set; }

            [Required(ErrorMessage = "请填写邮箱")]
            [StringLength(30, ErrorMessage = "邮箱不能超过50个字")]
            [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "填写的邮箱格式不正确")]
            [DisplayName("邮箱")]
            public string Email { get; set; }

            [DisplayName("留言")]
            public string Msg { get; set; }

            [DisplayName("留言时间")]
            public DateTime CreateTime { get; set; }

            [DisplayName("留言状态")]
            public string Status { get; set; }

        }
    }
}