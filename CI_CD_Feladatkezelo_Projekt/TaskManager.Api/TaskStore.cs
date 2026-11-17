namespace TaskManager.Api;
public record CreateTask(string Title);
public record TaskItem(int Id, string Title, bool Completed);
public sealed class TaskStore
{
    private readonly List<TaskItem> tasks = new();
    private int nextId;
    private readonly object gate = new();
    public IReadOnlyList<TaskItem> All() { lock (gate) return tasks.ToList(); }
    public TaskItem Add(string title) { lock (gate) { var task = new TaskItem(++nextId, title, false); tasks.Add(task); return task; } }
    public bool Complete(int id) { lock (gate) { var i = tasks.FindIndex(t => t.Id == id); if (i < 0) return false; tasks[i] = tasks[i] with { Completed = true }; return true; } }
    public bool Delete(int id) { lock (gate) return tasks.RemoveAll(t => t.Id == id) > 0; }
}
