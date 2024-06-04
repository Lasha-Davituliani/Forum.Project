using Forum.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class CommentForGettingDto
    {
        public int Id { get; set; }    
        public string Content { get; set; }        
        public DateTime CreationDate { get; set; }        
        public string AuthorName { get; set; }       
        public int TopicId { get; set; }       
        public string UserId { get; set; }
        public UserDto User { get; set; }
    }
}
