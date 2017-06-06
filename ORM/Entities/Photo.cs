using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Entities
{
    public class Photo
    {
        public Photo()
        {
            Users = new HashSet<User>();
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Data { get; set; }
        [Column(TypeName = "datetime2")]    
        public DateTime Date { get; set; }
        //public int UserId { get; set; }
        public string MimeType { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
