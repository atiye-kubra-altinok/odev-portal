using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class OgrenciDersleri
    {
        [Key]
        public int Id { get; set; }
        public bool Durum { get; set; }
        public int AppUserId { get; set; }
        public virtual Kullanici AppUser { get; set; }

        public int DersId { get; set; }
        public virtual Ders Ders { get; set; }

        public int? OdevId { get; set; }
        public virtual Odev Odev { get; set; }
    }
}
