namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Line")]
    public partial class Line
    {
        public long LineId { get; set; }

        public long? PoiId { get; set; }

        public long? SourceCity { get; set; }

        public long? DestCity { get; set; }

        public bool Active { get; set; }

        public virtual City City { get; set; }

        public virtual City City1 { get; set; }

        public virtual POI POI { get; set; }
    }
}
