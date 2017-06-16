using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class Photo
    {
        public Photo()
        {
            Date = DateTime.Now;
        }

        public int Id { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Data { get; set; }
        [Column(TypeName = "datetime2")]    
        public DateTime Date { get; set; }
        public string MimeType { get; set; }
    }
}
