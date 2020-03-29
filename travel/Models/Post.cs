using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace travel.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long CategoryId { get; set; } //1 Post có 1 Category => Cái ni là khoá ngoại
        public virtual Category Category { get; set; } //1 Post có 1 Category
    }

    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ICollection<Post> Posts { get; set; } //1 Category có nhiều Post, được liên kết bởi khoá ngoại CategoryId
    }
}