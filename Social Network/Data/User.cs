using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Network.Data
{
    public class User : IdentityUser
    {
        public List<Post> Friends { get; set; }
        public List<Post> Followers { get; set; }
        public List<Post> Following { get; set; }

        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TelegramId { get; set; }
        public string Desription { get; set; }
        public string Status { get; set; }
        public string ProfileUrl { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }
        public DateTime BirthYear { get; set; }
    }
}
