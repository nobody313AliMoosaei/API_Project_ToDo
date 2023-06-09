using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Web_API.Hellper.AuthorizationHandler.ResourceAuthorization
{
    public class CardRequirement : IAuthorizationRequirement
    {
    }

    public class IsCardForUserAuthorization : AuthorizationHandler<CardRequirement
        , Hellper.AuthorizationHandler.ResourceAuthorization.CardResourceAuthorization_DTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CardRequirement requirement, CardResourceAuthorization_DTO resource)
        {
            var userId = context.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                int Id = int.Parse(userId);
                if(Id==resource.User_Id)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
