using System;

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
