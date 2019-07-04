namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("POI")]
    public partial class POI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POI()
        {
            Lines = new HashSet<Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PoiId { get; set; }

        public long? CityId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(2083)]
        public string Website { get; set; }

        [StringLength(2083)]
        public string PoiImg { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public double Rating { get; set; }

        public double? Lon { get; set; }

        public double? Lat { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public long? CategoryId { get; set; }

        public string POIDescription { get; set; }

        public bool Active { get; set; }

        public bool? Transport { get; set; }

        public virtual Category Category { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line> Lines { get; set; }
    }
}
