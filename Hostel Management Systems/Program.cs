using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// ========================================
// ADD SERVICES
// ========================================

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

// SESSION

builder.Services.AddSession(options =>
{
    options.IdleTimeout =
        TimeSpan.FromHours(5);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ========================================
// ERROR HANDLING
// ========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

// ========================================
// MIDDLEWARE
// ========================================
builder.Services.AddSession();


app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// SESSION

app.UseSession();

// AUTHORIZATION

app.UseAuthorization();

// ========================================
// DEFAULT ROUTE
// ========================================

app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Account}/{action=Login}/{id?}");

// ========================================

app.Run();