using Microsoft.AspNetCore.Session;
// יוצר אובייקט שמכיל את כל ההגדרות והשירותים של האפליקציה
var builder = WebApplication.CreateBuilder(args);

// מוסיף תמיכה ב־MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// ===== Session – Services =====

// יוצר זיכרון זמני בשרת שבו נשמרים נתוני ה־Session
// בלי זה ל־Session אין איפה להישמר
builder.Services.AddDistributedMemoryCache();

// מוסיף את מנגנון ה־Session לאפליקציה
builder.Services.AddSession(options =>
{
    // קובע אחרי כמה זמן ללא פעילות ה־Session יימחק
    options.IdleTimeout = TimeSpan.FromMinutes(30);

    // מונע גישה ל־Session מצד JavaScript (אבטחה)
    options.Cookie.HttpOnly = true;

    // אומר שה־Session חיוני גם בלי אישור cookies מהמשתמש
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=HomePage}/{id?}");

app.MapControllerRoute(
    name: "catalog",
    pattern: "{controller=Guest}/{action=MachineCatalog}/{id?}");

// מפעיל את האפליקציה
app.Run();
