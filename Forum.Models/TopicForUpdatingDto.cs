﻿using Forum.Entities;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class TopicForUpdatingDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        public int CommentCount { get; set; }
        [Required]
        [MaxLength(100)]
        public string AuthorName { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
