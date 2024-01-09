using Microsoft.AspNetCore.Identity;

namespace portal.Models
{
    public class Kullanici:IdentityUser<int>
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }      
        
    }
}
