using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OData.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
