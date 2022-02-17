using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Social_Network.Data
{
    public class Friend
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool IsFriend { get; set; }
        //public User UserTo { get; set; }
        public Guid UserToId { get; set; }
        //public User UserFrom { get; set; }
        public Guid UserFromId { get; set; } 
    }
}
