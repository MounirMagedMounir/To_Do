using to_do.Models;

namespace to_do.Dto
{
    public record ListRespones(string? Id, string? Name, List<ToDoList>? toDoList, int? result);
}
