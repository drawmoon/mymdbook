using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConsoleAppSample.Models
{
    [Table("organization")]
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [ForeignKey(nameof(Parent))]
        [Column("parent_id")]
        public Guid ParentId { get; set; }

        public virtual Organization Parent { get; set; }
    }
}
