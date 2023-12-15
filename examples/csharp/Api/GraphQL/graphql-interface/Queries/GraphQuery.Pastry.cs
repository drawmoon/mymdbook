using GraphQL.Types;
using HttpApi.Entities;
using HttpApi.Types;

namespace HttpApi.Queries
{
    public partial class GraphQuery
    {
        private void InitPastryQuery()
        {
            Field<ListGraphType<PastryGraphType>>("Pastrys", "获取 Pastry", resolve: context =>
            {
                List<Pastry> pastries = new();

                string[] chocolates = { "Gonzalez", "Beth", "Henry" };
                for (int i = 0; i < 3; i++)
                {
                    pastries.Add(new Chocolate
                    {
                        Id = Guid.NewGuid(),
                        Name = chocolates[i],
                        Tag = $"{nameof(Chocolate)} - {chocolates[i]}"
                    });
                }

                string[] donuts = { "Emma", "Vaughn" };
                for (int i = 0; i < 2; i++)
                {
                    pastries.Add(new Donut
                    {
                        Id = Guid.NewGuid(),
                        Name = donuts[i],
                        Tag = $"{nameof(Donut)} - {donuts[i]}"
                    });
                }

                return pastries;
            });
        }
    }
}
