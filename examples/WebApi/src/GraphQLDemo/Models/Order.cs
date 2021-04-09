using System.Collections.Generic;

namespace GraphQLDemo.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
