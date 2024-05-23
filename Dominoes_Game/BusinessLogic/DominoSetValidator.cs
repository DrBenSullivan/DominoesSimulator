namespace DominoesSimulator.BusinessLogic
{
    internal class DominoSetValidator
    {
        public static List<(int, int)>? SetValidator(List<(int, int)> dominoes)
        {

            // Mathematically determine if a domino set contains a closed loop. See method documentation.
            if (!CanFormEulerianLoop(dominoes)) return null;

            // If Euler's Theorem applies, we can find a solution through recursive iteration by searching for a valid
            // chain of dominoes, rather than a loop & only returning it if the first and last values of the chain are equal.
            List<(int, int)>? solution = SearchForSolution(dominoes);
            if (solution is not null && ValidateSolution(dominoes.Count, solution))
            {
                return solution;
            }

            return null;
        }

        private static bool CanFormEulerianLoop(List<(int, int)> dominoes)
        {

            // A domino set contains a closed loop if it conforms to Euler's Theorem: A connected graph only contains an Euler cycle 
            // if every vertex has an even count. We can exploit this to rapidly discount sets but it will not provide a solution.
            // Read more: https://en.wikipedia.org/wiki/Eulerian_path

            // A domino has two values, one on either side. In graph theory, each possible value (i.e. 1-6) can be represented as a
            // vertex. The domino itself is an edge i.e. a "path" or line between two vertices.
            // To do this we iterate through the domino set & count the number of each vertex into a dictionary.

            var vertexCounts = new Dictionary<int, int>();
            foreach (var domino in dominoes)
            {

                if (!vertexCounts.TryGetValue(domino.Item1, out int _))
                {
                    vertexCounts[domino.Item1] = 0;
                }
                vertexCounts[domino.Item1]++;

                if (!vertexCounts.TryGetValue(domino.Item2, out int _))
                {
                    vertexCounts[domino.Item2] = 0;
                }
                vertexCounts[domino.Item2]++;

            }

            // If Euler's Theorem does not apply, there cannot be a closed loop present in the set.
            if (!vertexCounts.Values.All(count => count % 2 == 0))
            {
                return false;
            }

            // But, this does not guarantee that the set can be arranged into a single, unbroken loop; just that it could contain one.
            // To do this we can use a depth-first search to confirm that a loop is traversible without revisiting a vertex & that
            // there are no un-visitable vertices within the set of edges. See: https://en.wikipedia.org/wiki/Depth-first_search
            var visitedVerticesSet = new HashSet<int>();
            void DepthFirstSearch(int vertex)
            {
                // Guard clause, if vertex already visited, return, else add to HashSet for next iteration.
                if (visitedVerticesSet.Contains(vertex)) return;
                visitedVerticesSet.Add(vertex);

                // Iterate through the set, if either vertex (value) of the next edge (domino) is equal to the current vertex,
                // it becomes the nextVertex and the loop is called again on it.
                foreach (var nextVertex in dominoes
                    .Where(domino => domino.Item1 == vertex || domino.Item2 == vertex)
                    .Select(domino => domino.Item1 == vertex ? domino.Item2 : domino.Item1))
                {
                    DepthFirstSearch(nextVertex);
                }
            }

            // Starting at the first vertex, run the recursive Depth-First Search loop.
            int startingVertex = dominoes[0].Item1;
            DepthFirstSearch(startingVertex);

            // If all vertices were reached, the domino set can be arranged into a closed loop or, a Eulerian cycle.
            if (!vertexCounts.Keys.All(vertex => visitedVerticesSet.Contains(vertex)))
            {
                return false;
            };

            return true;
        }

        private static List<(int, int)>? SearchForSolution(List<(int, int)> dominoSet)
        {
            // Start recursion by iterating through each domino value in the set and running a recursive function on it.
            foreach (var domino in dominoSet)
            {
                // Use cloned lists to preserve data integrity.
                var clonedDominoList = CloneList(dominoSet);
                clonedDominoList.Remove(domino);

                var workingSolutionsList = new List<(int, int)> { domino };
                List<(int, int)>? possibleSolution = IterateForSolution(dominoSet.Count,
                                                                        clonedDominoList,
                                                                        workingSolutionsList);

                // If a solution is found, return it.
                if (ValidateSolution(dominoSet.Count, possibleSolution))
                {
                    return possibleSolution;
                }
            }

            return null;
        }

        private static List<(int, int)>? IterateForSolution(
            int solutionLengthRequired,
            List<(int, int)> workerDominoList,
            List<(int, int)> workerSolutionList)
        {
            // Guard clause to ensure valid solution not already found.
            if (ValidateSolution(solutionLengthRequired, workerSolutionList))
            {
                return workerSolutionList;
            }

            // Find the last value in the domino chain and iterate through remaining dominoes to find matches.
            int nextValue = workerSolutionList[^1].Item2;
            foreach (var domino in workerDominoList.Where(domino => domino.Item1 == nextValue ||
                                                                    domino.Item2 == nextValue))
            {
                var ClonedSolutionList = CloneList(workerSolutionList);
                var ClonedDominoList = CloneList(workerDominoList);
                ClonedDominoList.Remove(domino);

                // Ensure domino is correctly orientated.
                if (domino.Item1 == nextValue)
                {
                    ClonedSolutionList.Add(domino);
                }
                else
                {
                    ClonedSolutionList.Add((domino.Item2, domino.Item1));
                }

                // Restart loop with each found match.
                List<(int, int)>? possibleSolution = IterateForSolution(solutionLengthRequired, ClonedDominoList, ClonedSolutionList);
                if (possibleSolution is not null && ValidateSolution(solutionLengthRequired, possibleSolution))
                {
                    return possibleSolution;
                }
            }

            return null;
        }

        // Simple method for cloning lists of dominoes.
        private static List<(int, int)> CloneList(List<(int, int)> dominoList)
        {
            return new List<(int, int)>(dominoList);
        }

        // Simple solution validator ensuring correct length of solution and that first and last values are equal.
        private static bool ValidateSolution(int requiredNumber, List<(int, int)> possibleSolution)
        {
            return possibleSolution.Count == requiredNumber &&
                   possibleSolution[0].Item1 == possibleSolution[^1].Item2;
        }
    }
}