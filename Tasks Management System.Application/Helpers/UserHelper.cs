using System.Security.Claims;

namespace Tasks_Management_System.Application.Helpers
{
    public static class UserHelper
    {
        public static int GetUserId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return claim != null ? int.Parse(claim) : 0;
        }
    }
}