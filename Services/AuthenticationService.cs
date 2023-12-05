using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Dtos;
using Microsoft.AspNetCore.Components.Authorization;

namespace OnlineCoursesUI.Services
{
    public class AuthenticationService(ISessionStorageService sessionStorageService)
        : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _SessionStorage = sessionStorageService;
        private ClaimsPrincipal _EmpityClaim = new(new ClaimsIdentity());

        #nullable enable
        public async Task UpdateAuthenticationState(DataUserDto? dataUser)
        {
            ClaimsPrincipal claimsPrincipal;

            if (dataUser != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, dataUser.UserName),
                    new(ClaimTypes.Email, dataUser.Email)
                };

                if (dataUser.Roles != null && dataUser.Roles.Count != 0)
                {
                    claims.AddRange(
                        dataUser.Roles.Select(role => new Claim(ClaimTypes.Role, role))
                    );
                }

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,"JwtAuth"));

                await _SessionStorage.SaveStorage("sessionStorage",dataUser);
            } else
            {
                claimsPrincipal = _EmpityClaim;
                await _SessionStorage.RemoveItemAsync("sessionStorage");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var dataUser = await _SessionStorage.GetStorage<DataUserDto>("sessionStorage");

            if(dataUser == null)
                return await Task.FromResult(new AuthenticationState(_EmpityClaim));

            var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, dataUser.UserName),
                    new(ClaimTypes.Email, dataUser.Email)
                };

                if (dataUser.Roles != null && dataUser.Roles.Count != 0)
                {
                    claims.AddRange(
                        dataUser.Roles.Select(role => new Claim(ClaimTypes.Role, role))
                    );
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,"JwtAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
