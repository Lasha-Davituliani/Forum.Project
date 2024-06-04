using Forum.Entities;
using Forum.Models.Identity;

namespace Forum.Models
{
    public class TopicForGettingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CommentCount { get; set; }
        public string AuthorName { get; set; }
        public State State { get; set; }
        public Status Status { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
    }
}
