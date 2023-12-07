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
builder.Services.AddScoped<IInstructorServices, InstructorServices>();
builder.Services.AddScoped<AuthenticationStateProvider,AuthenticationService>();
builder.Services.AddScoped<IQualificationServices, QualificationServices>();
builder.Services.AddScoped<ICommentServices,CommentServices>();
builder.Services.AddHostedService<RefreshTokenService>();
builder.Services.AddSingleton<CourseComponentService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
