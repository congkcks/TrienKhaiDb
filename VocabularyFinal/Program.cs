using Microsoft.EntityFrameworkCore;
using VocabularyFinal.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LuyenTuVungDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()    // ✅ Cho phép tất cả domain
              .AllowAnyMethod()    // ✅ Cho phép tất cả HTTP method (GET, POST, PUT, DELETE, PATCH, v.v.)
              .AllowAnyHeader());  // ✅ Cho phép tất cả header (Authorization, Content-Type, v.v.)
});
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
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
