using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalMessage : IEntity
    {
        public int Id { get; set; }
        public string TextMessage { get; set; }
        public DateTime? Date { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }

        public virtual DalUserProfile FromUser { get; set; }
        public virtual DalUserProfile ToUser { get; set; }
    }
}
