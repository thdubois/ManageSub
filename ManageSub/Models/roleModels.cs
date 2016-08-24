using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManageSub.Models
{
    public class RoleModels: IdentityRole
    {
            public RoleModels() : base() { }
            public RoleModels(string name) : base(name) { }
    }
}