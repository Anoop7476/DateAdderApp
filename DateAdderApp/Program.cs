using DateAdderApp.Interfaces;
using DateAdderApp.Services;
using DateAdderApp.Validation;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<IDateParser, DateParser>();
builder.Services.AddScoped<IDateCalculator, DateCalculator>();
builder.Services.AddScoped<IDateAddService, DateAddService>();
builder.Services.AddScoped<IDateAddRequestValidator, DateAddRequestValidator>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();
app.MapRazorPages();
app.MapControllers();
app.Run();