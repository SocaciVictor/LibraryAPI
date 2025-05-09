using LibraryAdminPage.Components;
using LibraryAdminPage.Components.Interfaces;
using LibraryAdminPage.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
var apiBase = new Uri(builder.Configuration["ApiBaseUrl"]!);

builder.Services
    .AddHttpClient<IBookService, BookService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = apiBase;
    });

builder.Services
    .AddHttpClient<IUserService, UserService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = apiBase;
    });

builder.Services
    .AddHttpClient<IAuthorService, AuthorService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = apiBase;
    });

builder.Services
    .AddHttpClient<ILoanService, LoanService>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = apiBase;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
