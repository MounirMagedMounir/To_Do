

namespace to_do.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public StatesEntities State { get; set; } = StatesEntities.NotComplet;



    }
}
