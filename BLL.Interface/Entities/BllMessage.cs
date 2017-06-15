using BLL.Interface.Interfaces;
using System;

namespace BLL.Interface.Entities
{
    public class BllMessage : IEntityBLL
    {
        public int Id { get; set; }
        public string TextMessage { get; set; }
        public DateTime? Date { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }

        public virtual BllUserProfile UserFrom { get; set; }
        public virtual BllUserProfile UserTo { get; set; }
    }
}
