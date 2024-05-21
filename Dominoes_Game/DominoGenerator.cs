namespace Dominoes_Game
{
    internal class DominoGenerator
    {
        public static void Play()
        {
            // Build randomised domino inputs and pass into CanChain.
            var rand = new Random();
            int TotalCount = 0;
            int ChainableCount = 0;
            int UnchainableCount = 0;

            for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                string outputString1 = "";
                string outputString2 = "";
                string outputString3 = "";
                string outputString4 = "";
                string outputString5 = "";

                var InputDominoes = new List<(int, int)>();
                for (int j = 0; j < 10; j++)
                {
                    int value1 = rand.Next(6) + 1;
                    int value2 = rand.Next(6) + 1;

                    outputString1 = String.Concat(outputString1, "+-+  ");
                    outputString2 = String.Concat(outputString2, $"|{value1}|  ");
                    outputString3 = String.Concat(outputString3, "+-+  ");
                    outputString4 = String.Concat(outputString4, $"|{value2}|  ");
                    outputString5 = String.Concat(outputString5, "+-+  ");
                    InputDominoes.Add((value1, value2));
                }
                Console.WriteLine($"Total Domino Sets Checked:   {TotalCount}\nNumber of Sets Chainable:   {ChainableCount}\nNumber of Sets Not Chainable:   {UnchainableCount}\n\n\n");
                Console.WriteLine("\n\n\nA random set of 10 dominoes has been produced:\n\n");
                Console.WriteLine(outputString1);
                Console.WriteLine(outputString2);
                Console.WriteLine(outputString3);
                Console.WriteLine(outputString4);
                Console.WriteLine(outputString5 + "\n\n");
                Console.WriteLine("Checking if these dominoes are chainable...\n\n\n");

                var result = CanChain(InputDominoes);

                if (result == null)
                {
                    TotalCount++;
                    UnchainableCount++;
                    Console.WriteLine("The domino set was NOT chainable!\n\n");
                }
                if (result != null)
                {
                    TotalCount++;
                    ChainableCount++;
                    Console.WriteLine("The domino set was chainable:\n\n");
                    string resultString = "";
                    foreach (var domino in result)
                    {
                        resultString = String.Concat(resultString, $"[{domino.Item1}|{domino.Item2}] ");
                    }
                }

                Console.WriteLine("\n\nPlay again? [Y/N]");
                var keyInfo = Console.ReadKey(true);
                while (keyInfo.KeyChar != 'y' &&
                         keyInfo.KeyChar != 'n')
                {
                    keyInfo = Console.ReadKey(true);
                }
                if (keyInfo.KeyChar == 'y')
                {
                    Console.WriteLine("Playing again...");
                }
                if (keyInfo.KeyChar == 'n')
                {
                    break;
                }
            }
            Console.Clear();
            Console.WriteLine($"GAME OVER!!!\n\n\nTotal Domino Sets Checked:   {TotalCount}\nNumber of Sets Chainable:   {ChainableCount}\nNumber of Sets Not Chainable:   {UnchainableCount}\n");
        }*/
    }
}
