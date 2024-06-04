using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Forum.Entities
{
    public class TopicEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        public int CommentCount { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        public Status Status { get; set; }
        
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
