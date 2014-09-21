using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nepton.Models
{
    [MetadataType(typeof(NT_ConfigMetadata))]
    public partial class NT_Config
    {
        private class NT_ConfigMetadata {
            
            public Guid ConfigID { get; set; }

            [StringLength(15,ErrorMessage="配置名称不能超过15个字")]
            [DisplayName("配置Key")]
            public string Key { get; set; }

            [StringLength(30, ErrorMessage = "配置名称不能超过15个字")]
            [DisplayName("配置名称")]
            public string Name { get; set; }

            [StringLength(500, ErrorMessage = "配置不能超过500个字")]
            [DisplayName("值1")]
            public string Value1 { get; set; }

            [StringLength(1000, ErrorMessage = "配置不能超过1000个字")]
            [DisplayName("值2")]
            public string Value2 { get; set; }

            [StringLength(2000, ErrorMessage = "配置名称不能超过2000个字")]
            [DisplayName("值3")]
            public string Value3 { get; set; }

            [DisplayName("值4")]
            public string Value4 { get; set; }
        }
    }
}