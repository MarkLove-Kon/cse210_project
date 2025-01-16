using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                Random random = new Random();
                int magicNumber = random.Next(1, 101);
                int guess = 0; 
            
                int attempts = 0; 

                while (guess != magicNumber)
                {
                    Console.Write("What is your guess? ");
                    guess = int.Parse(Console.ReadLine());
                    attempts++;

                    if (guess < magicNumber)
                    {
                        Console.WriteLine("Higher");
                    }
                    else if (guess > magicNumber)
                    {
                        Console.WriteLine("Lower");
                    }
                    else
                    {
                        Console.WriteLine($"You guessed it! It took you {attempts} attempts.");
                    }
                }
                
                Console.Write("Do you want to play again? (yes/no) ");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                {
                    playAgain = false;
                }
            }
        }
    }
}
