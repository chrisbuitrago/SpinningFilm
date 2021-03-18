using Microsoft.AspNetCore.Authorization;
using SpinningFilm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpinningFilm.Extensions;
using Microsoft.AspNetCore.Http;

namespace SpinningFilm.Auth
{
    public class MediaAuthorizationHandler : AuthorizationHandler<SameUserRequirement, PhysicalMedia>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, PhysicalMedia resource)
        {
            if (context.User.Identity.GetNameIdGuid() == resource.AppUserId)
            {
                context.Succeed(requirement);
            }           

            return Task.CompletedTask;
        }
    }
}
