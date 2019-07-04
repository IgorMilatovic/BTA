namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [Table("City")]
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            Lines = new HashSet<Line>();
            Lines1 = new HashSet<Line>();
            POIs = new HashSet<POI>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CityId { get; set; }

        [Required]
        [StringLength(20)]
        public string CityName { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        public double? Lon { get; set; }

        public double? Lat { get; set; }

        public int? Population { get; set; }

        [StringLength(2083)]
        public string ImgUrl { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line> Lines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line> Lines1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POI> POIs { get; set; }
    }
}
