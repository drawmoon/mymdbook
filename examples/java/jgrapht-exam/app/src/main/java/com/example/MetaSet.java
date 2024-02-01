package com.example;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.jgrapht.Graph;
import org.jgrapht.GraphPath;
import org.jgrapht.alg.shortestpath.DijkstraShortestPath;
import org.jgrapht.graph.DefaultDirectedGraph;
import org.jgrapht.graph.DefaultEdge;

public class MetaSet {
    private Graph<Table, DefaultEdge> graph = new DefaultDirectedGraph<>(DefaultEdge.class);

    public MetaSet(Table[] tables, Relationship[] relationships) {
        this.initGraph(tables, relationships);
    }

    private void initGraph(Table[] tables, Relationship[] relationships) {
        Map<Integer, Table> tableMap = new HashMap<>();
        for (Table table : tables) {
            this.graph.addVertex(table);
            tableMap.put(table.getId(), table);
        }

        Relationship first = relationships[0];
        Table source = tableMap.get(first.getLeftTableId());

        for (Relationship relationship : relationships) {
            Table target = tableMap.get(relationship.getRightTableId());

            this.graph.addEdge(source, target);
            source = target;
        }
    }

    public List<Table> shortestPath(Table source, Table target) {
        DijkstraShortestPath<Table, DefaultEdge> dijkstraShortestPath = new DijkstraShortestPath<>(this.graph);

        GraphPath<Table, DefaultEdge> graphPath = dijkstraShortestPath.getPath(source, target);
        return graphPath.getVertexList();
    }
}
