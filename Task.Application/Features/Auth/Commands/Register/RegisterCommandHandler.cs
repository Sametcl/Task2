using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, IdentityResult>
    {
        private readonly UserManager<AppUser> userManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IdentityResult> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Böyle bir kullanıcı zaten var!"
                });
            }
            var user = new AppUser
            {
                FullName = request.FullName,
                UserName= request.FullName,
                Email = request.Email
            };

            var result = await userManager.CreateAsync(user, request.Password);
            return result;
        }
    }
}
