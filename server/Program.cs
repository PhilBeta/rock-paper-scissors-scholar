using supper_tool_api;

/*
Phil's Self-Generating Text Adventure Tool.


Q: I'm stuck on the issue of how to represent areas which are simply a logical grouping of sibling-areas and can't be entered by the player.
An example of this is a Golf Course that contains 18 starting points. You can stand at any of the starting points, but you cannot stand in the golf course on its own.

*/
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {

    options.AddDefaultPolicy(
    policy => {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});


// Add services to the container.

builder.Services.AddSingleton<IChatService>(provider=>new OpenAIChatService());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

