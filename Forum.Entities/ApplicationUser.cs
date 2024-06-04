using Microsoft.AspNetCore.Identity;

namespace Forum.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<TopicEntity> Topics { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
