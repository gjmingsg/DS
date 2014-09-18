using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Nepton.Models
{
    [MetadataType(typeof(NT_ArticleMetadata))]
    public partial class NT_Article
    {
        private class NT_ArticleMetadata {

            public Guid ArticleID { get; set; }

            [Required(ErrorMessage="请输入标题")]
            [StringLength(100,ErrorMessage="标题不能超过100个字")]
            [DisplayName("标题")]
            public string Title { get; set; }
            [DisplayName("创建人")]
            public string Creator { get; set; }
            [DisplayName("发表时间")]
            public DateTime CreateTime { get; set; }

            [DisplayName("内容")]
            public string Content { get; set; }

            [Required(ErrorMessage = "请输选择状态")]
            [Range(1,2,ErrorMessage="只能选择草稿，或者发布2种状态")]
            [DisplayName("状态")]
            public int Status { get; set; }

        }
    }
}