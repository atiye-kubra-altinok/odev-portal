using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class Ders
    {
        [Key]
        public int Id { get; set; }
        public string DersAdi { get; set; }
        
    }
}
