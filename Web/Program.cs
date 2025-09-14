using Core.Interfaces;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;
using Data;
using Data.Interceptors;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Web;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddDbContext<AppDbContext>(conf =>
			conf.UseSqlServer(
				builder.Configuration.GetConnectionString("dev"))
			.AddInterceptors(new SoftDeleteInterceptor())
			);

		builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
			{
				o.Password.RequireDigit = true;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequireUppercase = false;

				o.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<AppDbContext>();

		builder.Services.AddScoped<IPostRepository, PostRepository>();
		builder.Services.AddScoped<ICommentRepository, CommentRepository>();

		builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

		builder.Services.AddScoped<ICommentService, CommentService>();
		builder.Services.AddScoped<IPostService, PostService>();
		builder.Services.AddScoped<ICommunityService, CommunityService>();
		builder.Services.AddScoped<IUserService, UserService>();


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			// app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			"default",
			"{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}