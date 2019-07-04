namespace BTA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CommentId { get; set; }

        public long? ParentId { get; set; }

        public long? Traveler { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public int Rating { get; set; }

        public bool Active { get; set; }

        [StringLength(50)]
        public string TableName { get; set; }

        public virtual Traveler Traveler1 { get; set; }
    }
}
