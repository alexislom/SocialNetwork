using BLL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllPhoto : IEntityBLL
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }

        public string MimeType { get; set; }

        public DateTime Date { get; set; }
    }
}
