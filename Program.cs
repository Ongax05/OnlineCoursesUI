using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineCoursesUI;
using OnlineCoursesUI.Interfaces;
using OnlineCoursesUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5031") });
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICourseServices, CourseServices>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider,AuthenticationService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
