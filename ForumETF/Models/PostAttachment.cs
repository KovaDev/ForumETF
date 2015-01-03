using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumETF;

namespace ForumETF.Models
{
    public class PostAttachment
    {
        public int PostAttachmentId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

        public virtual Post Post { get; set; }
    }
}