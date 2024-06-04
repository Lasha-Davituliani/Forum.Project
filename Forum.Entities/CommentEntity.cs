using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Forum.Entities
{
    public class CommentEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [ForeignKey(nameof(Topic))]
        public int TopicId { get; set; }
        public TopicEntity Topic { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
