namespace StrategiAlgoritma;

public abstract class Program
{
    public static void Main()
    {
        var coins = new int[] { 50, 10, 2 };
        const int amount = 190;
        
        var resultGreedy = CoinChangeProblemGreedy(coins, amount);
        var resultDp = CoinChangeProblemDp(coins, amount);
        
        Console.WriteLine("Using Greedy:");
        Console.WriteLine($"Coin used: {string.Join(", ", resultGreedy)}");
        Console.WriteLine($"Total coin used: {resultGreedy.Length}");

        Console.WriteLine();
        
        Console.WriteLine("Using Dynamic Programming:");
        Console.WriteLine($"Coin used: {string.Join(", ", resultDp)}");
        Console.WriteLine($"Total coin used: {resultDp.Length}");
    }

    private static int[] CoinChangeProblemGreedy(int[] coins, int amount)
    {
        var result = new List<int>();
        coins = coins.OrderDescending().ToArray();

        foreach (var coin in coins)
        {
            while (amount >= coin)
            {
                amount -= coin;
                result.Add(coin);
            }
        }
        return result.ToArray();
    }

    private static int[] CoinChangeProblemDp(int[] coins, int amount)
    {
        coins = coins.OrderDescending().ToArray();
        
        var dp = new int[amount + 1];
        Array.Fill(dp, int.MaxValue - 1);
        dp[0] = 0;

        for (var i = 1; i <= amount; i++)
        {
            foreach (var coin in coins)
            {
                if (coin <= i && dp[i - coin] + 1 < dp[i])
                {
                    dp[i] = dp[i - coin] + 1;
                }
            }
        }

        if (dp[amount] == int.MaxValue - 1)
        {
            throw new Exception("No solution found");
        }

        var solution = new int[dp[amount]];
        var remaining = amount;
        for (var i = solution.Length - 1; i >= 0; i--)
        {
            foreach (var coin in coins)
            {
                if (coin <= remaining && dp[remaining - coin] + 1 == dp[remaining])
                {
                    solution[i] = coin;
                    remaining -= coin;
                    break;
                }
            }
        }

        return solution;
    }
}