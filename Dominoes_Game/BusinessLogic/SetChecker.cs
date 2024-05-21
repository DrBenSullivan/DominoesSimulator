namespace Dominoes_Game.BusinessLogic
{
    internal class SetChecker
    {
        public static (bool, List<(int, int)>?) DominoChecker(List<(int, int)> dominoes)
        {

            // It is mathematically possible to arrange dominoes into a closed loop if the set fits criteria for a Eulerian loop.
            // Euler's Theorem: A connected graph has an Euler cycle if and only if every vertex has even degree.
            // See: https://en.wikipedia.org/wiki/Eulerian_path
            // We can exploit this to rapidly determine if some negative solutions is possible but this will not provide a solution.

            bool canFormLoop = CanFormEulerianLoop(dominoes);
            Console.WriteLine($"canFormLoop = {canFormLoop}");
            //if (CanFormEulerianLoop(dominoes))
            //{

                // If so, we will find the solution by recursive iteration. We search for a valid chain of dominoes, rather than a loop 
                // but this will only be returned if the first and last values of the chain are equal and, therefore, a Eulerian loop.
                (bool isChainable, List<(int, int)>? solution) = SearchForSolution(dominoes);
            Console.WriteLine($"isChainable = {isChainable}");
                if (isChainable && solution != null)
                {
                    return (true, solution);
                }
            //}
            return (false, null);
        }

        private static bool CanFormEulerianLoop(List<(int, int)> dominoes)
        {
            // Each domino has two values or "sides". Using graph theory we can represent each side value (i.e. 1-6)
            // by a vertex & the domino itself is represented as an edge i.e. a path between two vertices (or values).
            // To do this we'll iterate through the set & count the number of each vertex into a dictionary.
            var vertexCount = new Dictionary<int, int>();
            foreach (var domino in dominoes)
            {
                if (!vertexCount.ContainsKey(domino.Item1))
                {
                    vertexCount[domino.Item1] = 0;
                }
                if (!vertexCount.ContainsKey(domino.Item2))
                {
                    vertexCount[domino.Item2] = 0;
                }
                vertexCount[domino.Item1]++;
                vertexCount[domino.Item2]++;
            }

            // If all vertices have an even count, the set of dominoes contains a Eulerian loop...
            bool doAllVerticesHaveAnEvenCount = vertexCount.Values.All(degree => degree % 2 == 0);
            Console.WriteLine($"doAllVerticesHaveAnEvenCount = {doAllVerticesHaveAnEvenCount}");

            // But, this does not guarantee that the set can be arranged into a single, unbroken loop. Merely, that it contains one.
            // To do this we can use a depth-first search to confirm the loop is traversible without revisiting a vertex & that
            // there are no un-visitable vertices within the set of of edges. See: https://en.wikipedia.org/wiki/Depth-first_search
            var visitedVertices = new HashSet<int>();
            void DepthFirstSearch(int vertex)
            {
                // Guard clause, if vertex already visited, return, else add to HashSet for next iteration.
                if (visitedVertices.Contains(vertex)) return;
                visitedVertices.Add(vertex);

                // Iterate through the set, if either vertex of the next domino has equal value, run loop using that domino.
                foreach (var nextDomino in dominoes
                    .Where(domino => domino.Item1 == vertex || 
                                     domino.Item2 == vertex)
                    .Select(domino => domino.Item1 == vertex ? domino.Item2 : domino.Item1))
                {
                    DepthFirstSearch(nextDomino);
                }
            }

            // Starting at the first vertex, run the Depth-Firt Search.
            int startingVertex = dominoes[0].Item1;
            DepthFirstSearch(startingVertex);

            // If the the HashSet count is not equal to the total vertex count, then Hashset is incomplete and a single unbroken loop is not possible.
            if (visitedVertices.Count != vertexCount.Count) return false;

            return true;

        }

        private static (bool, List<(int, int)>?) SearchForSolution(List<(int, int)> dominoSet)
        {
            foreach (var domino in dominoSet)
            {
                var clonedDominoList = CloneList(dominoSet);
                clonedDominoList.Remove(domino);

                var WorkingSolutionsList = new List<(int, int)> { domino };

                (bool isSolutionFound, List<(int, int)>? PossibleSolution) = IterateForSolution(dominoSet.Count, clonedDominoList, WorkingSolutionsList);

                if (isSolutionFound && PossibleSolution is not null)
                {
                    if (ValidateSolution(dominoSet.Count, PossibleSolution))
                    {
                        return (true, PossibleSolution);
                    }
                }
            }

            return (false, null);
        }

        private static (bool, List<(int, int)>?) IterateForSolution(
            int solutionLengthRequired,
            List<(int, int)> workerDominoList,
            List<(int, int)> workerSolutionList)
        {
            if (workerSolutionList.Count == solutionLengthRequired)
            {
                return (true, workerSolutionList);
            }

            int nextValue = workerSolutionList[^1].Item2;

            foreach (var domino in workerDominoList)
            {
                if (domino.Item1 == nextValue || domino.Item2 == nextValue)
                {
                    var ClonedSolutionList = CloneList(workerSolutionList);
                    var ClonedDominoList = CloneList(workerDominoList);
                    ClonedDominoList.Remove(domino);

                    if (domino.Item1 == nextValue)
                    {
                        ClonedSolutionList.Add(domino);
                    }
                    else
                    {
                        ClonedSolutionList.Add((domino.Item2, domino.Item1));
                    }

                    (bool isSolutionFound, List<(int, int)>? PossibleSolution) = IterateForSolution(solutionLengthRequired, ClonedDominoList, ClonedSolutionList);
                    if (isSolutionFound)
                    {
                        return (true, PossibleSolution);
                    }
                }
            }

            return (false, null);
        }

        private static bool MatchExists(int value, List<(int, int)> dominoList)
        {
            return dominoList.Any(domino => domino.Item1 == value || domino.Item2 == value);
        }

        private static List<(int, int)> CloneList(List<(int, int)> dominoList)
        {
            return new List<(int, int)>(dominoList);
        }

        private static bool ValidateSolution(int requiredNumber, List<(int, int)> PossibleSolution)
        {
            return PossibleSolution.Count == requiredNumber &&
                   PossibleSolution[0].Item1 == PossibleSolution[^1].Item2;
        }
    }
}