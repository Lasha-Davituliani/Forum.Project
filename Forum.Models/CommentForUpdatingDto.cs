﻿using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class CommentForUpdatingDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int TopicId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
