using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQLDemo.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
