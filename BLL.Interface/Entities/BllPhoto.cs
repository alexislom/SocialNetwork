using BLL.Interface.Interfaces;
using System;

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
