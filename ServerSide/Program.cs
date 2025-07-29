using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using TodoApi;
using System.Collections;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.Extensions.ObjectPool;
int i = 1;//משתנה גלובלי שמגדיר את האיידי של המשימות
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoDbContext>(option =>
    option.UseMySql(builder.Configuration.GetConnectionString("ToDoDb"),
    ServerVersion.Parse("8.0.41-myfirst"))
);//הוספת התקשרות עם המסד נתונים
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});//הוספת התקשרות עם צד שרת
var app = builder.Build();

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", async (ToDoDbContext x) =>
   await x.Items.ToListAsync()
);//הצגת כל המשימות
 app.MapPost("/{e}", async (string e,ToDoDbContext x) =>{

    Item r=new Item();
  r.Name =e;
    r.IsComplete=false;
    r.Id=i++;
   x.Items.Add(r);
    await x.SaveChangesAsync();
 
} );//הוספת משימה חדשה
app.MapPut("/{id}/{t}",async (int id,bool t,ToDoDbContext x) => 
  {
            

    var itemToUpdate=  await x.Items.FindAsync(id);
    itemToUpdate.IsComplete=t;
     await x.SaveChangesAsync();

  });//עדכון משימה

 
  app.MapDelete("/{id}",async (int id,ToDoDbContext s) => 
   {
      var itemToDelete=await s.Items.FindAsync(id);
      s.Remove(itemToDelete);
           await s.SaveChangesAsync();

   });//מחיקת משימה
  


app.Run();
