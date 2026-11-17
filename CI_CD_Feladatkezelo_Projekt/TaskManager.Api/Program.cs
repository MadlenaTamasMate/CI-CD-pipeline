using TaskManager.Api;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TaskStore>();
var app = builder.Build();
app.MapGet("/", () => Results.Content("""<!doctype html><html lang="hu"><meta charset="utf-8"><title>Feladatkezelő</title><style>body{font:16px system-ui;max-width:700px;margin:50px auto;padding:20px}button,input{padding:10px;margin:4px}li{margin:12px}</style><h1>Feladatkezelő</h1><input id="name" placeholder="Új feladat"><button onclick="add()">Hozzáadás</button><ul id="items"></ul><script>async function load(){const a=await(await fetch('/api/tasks')).json();document.getElementById('items').innerHTML=a.map(t=>`<li>${escapeHtml(t.title)} ${t.completed?'✅':''} <button onclick="done(${t.id})">Kész</button><button onclick="del(${t.id})">Törlés</button></li>`).join('')}function escapeHtml(s){return s.replace(/[&<>"']/g,c=>({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[c]))}async function add(){const title=document.getElementById('name').value.trim();if(!title)return;await fetch('/api/tasks',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify({title})});document.getElementById('name').value='';load()}async function done(id){await fetch('/api/tasks/'+id+'/complete',{method:'PUT'});load()}async function del(id){await fetch('/api/tasks/'+id,{method:'DELETE'});load()}load()</script></html>""", "text/html; charset=utf-8"));
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/api/tasks", (TaskStore store) => Results.Ok(store.All()));
app.MapPost("/api/tasks", (CreateTask request, TaskStore store) => {
    if (string.IsNullOrWhiteSpace(request.Title)) return Results.BadRequest("A cím kötelező.");
    var task = store.Add(request.Title.Trim());
    return Results.Created($"/api/tasks/{task.Id}", task);
});
app.MapPut("/api/tasks/{id:int}/complete", (int id, TaskStore store) => store.Complete(id) ? Results.NoContent() : Results.NotFound());
app.MapDelete("/api/tasks/{id:int}", (int id, TaskStore store) => store.Delete(id) ? Results.NoContent() : Results.NotFound());
app.Run();
public partial class Program { }
