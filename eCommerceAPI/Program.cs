using eCommerceAPI.DBContext;
using eCommerceAPI.Models;
using eCommerceAPI.Services.ProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //Enable CORS
        builder.Services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => {
                options.WithOrigins("http://localhost:5173")
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
        });
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();

        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<DbContext, EcommerceDBContext>(options =>
        {
            options.UseSqlServer(
                "Server=DESKTOP-5D2A9FB;" +
                "Database=eCommerceDB;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;");
        });
        
        builder.Services.AddIdentityApiEndpoints<ApplicationUser>
            (options => {
                options.SignIn.RequireConfirmedAccount = false;     
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<EcommerceDBContext>();

        var app = builder.Build();
        //Enable Cors
        app.UseCors("AllowOrigin");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapIdentityApi<ApplicationUser>();
        app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }).RequireAuthorization();

        app.MapGet("/pingauth", (ClaimsPrincipal user) =>
        {
            var email = user.FindFirstValue(ClaimTypes.Email); //get the user's email from the claim
            var role = user.FindFirstValue(ClaimTypes.Role);
            return Results.Json(new { Email = email, Role = role }); // return the email as a plain text response
        }).RequireAuthorization();

        app.UseHttpsRedirection();

        app.MapControllers();

        //seeding default role data
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { "Admin", "Member" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        //seeding default admin account data
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string email = "admin@admin.com";
            string password = "Test1234!";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser();
                user.UserName = email;
                user.Email = email;
                user.EmailConfirmed = true;

                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Admin");
            }

        }

        app.Run();

    }
}
