using BLL.Interface.Interfaces;

namespace BLL.Interface.Entities
{
    public class BllRole : IEntityBLL
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
