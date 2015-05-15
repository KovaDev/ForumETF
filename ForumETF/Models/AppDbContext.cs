using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumETF.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("DbContext")
        {
 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Post>().HasMany<Comment>(a => a.Comments).WithOptional().WillCascadeOnDelete(true);
            //modelBuilder.Entity<Post>().HasMany<Answer>(a => a.Answers).WithOptional().WillCascadeOnDelete(true);
            //modelBuilder.Entity<Post>().HasMany<PostAttachment>(a => a.Attachments).WithOptional().WillCascadeOnDelete(true);
            //modelBuilder.Entity<Post>().HasMany<Tag>(a => a.Tags).WithOptional().WillCascadeOnDelete();

            Database.SetInitializer<AppDbContext>(new DropCreateDatabaseAlways<AppDbContext>());

            //modelBuilder.Entity<Comment>().has
            //modelBuilder.Entity<Answer>().HasMany<Answer>(a => a.Answers).WithOptional().WillCascadeOnDelete();
            //modelBuilder.Entity<PostAttachment>().HasMany<PostAttachment>(a => a.Attachments).WithOptional().WillCascadeOnDelete();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerComment> AnswerComments { get; set; }
        public DbSet<PostAttachment> PostAttachments { get; set; }
    }
}