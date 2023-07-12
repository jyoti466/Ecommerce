using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ecommerce.ViewModels
{
    public class UserRoleMaping
    {
       
        public string UserId { get; set; }
       
        public string RoleId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }

    }
}
