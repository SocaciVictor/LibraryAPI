using LibraryAPI.Core.Interfaces;
using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Repositories;
using LibraryAPI.Presentation.Mappings;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowLocalFrontend", policy =>
    {
        policy
          .WithOrigins("http://localhost:5173")   
          .AllowAnyHeader()                         
          .AllowAnyMethod()                        
          .AllowCredentials();                     
    });
});



var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options
      .UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 28)))
      .EnableSensitiveDataLogging()
      .LogTo(Console.WriteLine)
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(BookProfile),
                               typeof(AuthorProfile),
                               typeof(UserProfile),
                               typeof(LoanProfile));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();
