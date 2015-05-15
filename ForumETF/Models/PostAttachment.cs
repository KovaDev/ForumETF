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