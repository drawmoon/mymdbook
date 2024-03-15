import networkx as nx


class Table:
    def __init__(self, id: int, name: str) -> None:
        self.id = id
        self.name = name


class Relationship:
    def __init__(self, id: int, left_table_id: int, right_table_id: int) -> None:
        self.id = id
        self.left_table_id = left_table_id
        self.right_table_id = right_table_id


class MetaSet:
    def __init__(self, tables: list[Table], relationships: list[Relationship]):
        self._graph = nx.Graph()
        self._init_graph(tables, relationships)

    def _init_graph(self, tables: list[Table], relationships: list[Relationship]):
        table_map = {table.id: table for table in tables}

        first = relationships[0]
        left_table = table_map[first.left_table_id]

        for relationship in relationships:
            right_table = table_map[relationship.right_table_id]

            self._graph.add_edge(left_table, right_table, relationship=relationship)
            left_table = right_table

    def show_shortest_path(self, s: Table, t: Table):
        arr = nx.shortest_path(self._graph, s, t)
        for table in arr:
            print(table.name)


a = Table(1, "a")
b = Table(2, "b")
c = Table(3, "c")
d = Table(4, "d")

r1 = Relationship(1, a.id, b.id)
r2 = Relationship(2, b.id, d.id)
r3 = Relationship(3, a.id, c.id)
r4 = Relationship(4, c.id, d.id)

mate_set = MetaSet([a, b, c, d], [r1, r2, r3, r4])
mate_set.show_shortest_path(a, d)
