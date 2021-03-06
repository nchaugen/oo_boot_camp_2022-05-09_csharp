/*
 * Copyright (c) 2022 by Fred George
 * May be used freely except for training; license required for training.
 * @author Fred George  fredgeorge@acm.org
 */

namespace Exercises.Graph {
    // Understands a connection from one Node to another
    internal class Link {        
        internal static double Cost(List<Link> links) => links.Sum(l => l._cost);

        private readonly double _cost;
        private readonly Node _target;

        public Link(double cost, Node target) {
            _cost = cost;
            _target = target;
        }

        internal List<Path> Paths(Node destination, List<Node> visitedNodes) => 
            _target.Paths(destination, visitedNodes).Select(p =>p.Prepend(this)).ToList();
    }
}