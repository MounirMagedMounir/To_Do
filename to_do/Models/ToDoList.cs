using System.Text.Json.Serialization;

namespace to_do.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }
}
