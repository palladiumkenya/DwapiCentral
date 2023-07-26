using Hangfire.Dashboard;
using Microsoft.Owin;

namespace DwapiCentral.Ct
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {            
            return true;
        }

        
    }
}
