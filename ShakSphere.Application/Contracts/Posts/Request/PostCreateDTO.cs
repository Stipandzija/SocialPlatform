﻿using ShakSphere.Domain.Aggregates.UserProfileAggregate.Definitions;
using System.ComponentModel.DataAnnotations;

namespace ShakSphere.Application.Contracts.Posts.Request
{
    public class PostCreateDTO
    {
        [Required]
        public string TextContent { get; set; }
    }
}
