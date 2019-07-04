namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Traveler")]
    public partial class Traveler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Traveler()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public long UserId { get; set; }

        [StringLength(128)]
        public string IdentityId { get; set; }

        public bool Active { get; set; }

        [StringLength(150)]
        public string ImgUrl { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
