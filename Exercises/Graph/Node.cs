/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

using static Exercises.Graph.Path;

namespace Exercises.Graph {
    // Understands its neighbors
    public class Node {
        private readonly List<Link> _links = new();

        public bool CanReach(Node destination) => Paths(destination).Count > 0;

        public int HopCount(Node destination) => Path(destination, FewestHopsPath).HopCount();

        public double Cost(Node destination) => Path(destination).Cost();

        public Path Path(Node destination) => Path(destination, LeastCostPath);

        public List<Path> Paths(Node destination) => Paths(destination, NoVisitedNodes);

        internal List<Path> Paths(Node destination, List<Node> visitedNodes) {
            if (this == destination) return new List<Path>{ new() };
            if (visitedNodes.Contains(this)) return new List<Path>();
            return _links.SelectMany(l => l.Paths(destination, CopyWithThis(visitedNodes))).ToList();
        }

        private Path Path(Node destination, Func<Path, double> strategy) => 
            Paths(destination).MinBy(strategy) ?? throw new ArgumentException("Destination cannot be reached");

        private List<Node> CopyWithThis(List<Node> originals) => new List<Node>(originals) { this };

        private static List<Node> NoVisitedNodes => new();

        public LinkBuilder Cost(double amount) => new LinkBuilder(amount, _links);

        public class LinkBuilder {
            private readonly double _cost;
            private readonly List<Link> _links;

            internal LinkBuilder(double cost, List<Link> links) {
                _cost = cost;
                _links = links;
            }

            public Node To(Node neighbor) {
                _links.Add(new Link(_cost, neighbor));
                return neighbor;
            }
        }
    }
}