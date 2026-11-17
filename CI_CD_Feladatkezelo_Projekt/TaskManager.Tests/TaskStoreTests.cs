using TaskManager.Api;
using Xunit;
namespace TaskManager.Tests;
public class TaskStoreTests
{
    [Fact] public void Add_CreatesTask() { var store = new TaskStore(); var task = store.Add("Teszt"); Assert.Equal("Teszt", task.Title); Assert.Single(store.All()); }
    [Fact] public void Complete_MarksTaskDone() { var store = new TaskStore(); var task = store.Add("Teszt"); Assert.True(store.Complete(task.Id)); Assert.True(Assert.Single(store.All()).Completed); }
    [Fact] public void Delete_RemovesTask() { var store = new TaskStore(); var task = store.Add("Teszt"); Assert.True(store.Delete(task.Id)); Assert.Empty(store.All()); }
    [Fact] public void MissingTask_ReturnsFalse() { var store = new TaskStore(); Assert.False(store.Complete(999)); Assert.False(store.Delete(999)); }
}
