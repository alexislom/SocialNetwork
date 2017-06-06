using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalPhoto : IEntity
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public string MimeType { get; set; }

        public DateTime Date { get; set; }
    }
}
