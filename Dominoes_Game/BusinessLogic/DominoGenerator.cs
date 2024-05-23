namespace DominoesSimulator
{
    internal class DominoGenerator
    {
        // Iteratively generates a random domino set. If no setSize provided, defaults to 10 dominoes.
        public static List<(int, int)> GenerateDominoSet(int? setSize = 10)
        {
            var rand = new Random();
            var dominoSet = new List<(int, int)>();
            for (int i = 0; i < setSize; i++)
            {
                var domino = (rand.Next(6) + 1,
                              rand.Next(6) + 1);
                dominoSet.Add(domino);
            }
            return dominoSet;
        }
    }
}