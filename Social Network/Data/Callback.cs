using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Network.Data
{
    public class Callback
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [NotMapped]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

