using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

class MindfulnessProgram
{
    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                int duration = GetDuration("Breathing Activity");
                BreathActivity(duration);
            }
            else if (choice == "2")
            {
                int duration = GetDuration("Reflection Activity");
                ReflectionActivity(duration);
            }
            else if (choice == "3")
            {
                int duration = GetDuration("Listing Activity");
                ListingActivity(duration);
            }
            else if (choice == "4")
            {
                Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                SaveLog();
                break;
            }
            else if (choice == "5")
            {
                DisplayLog();
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose again.");
                Thread.Sleep(2000);
            }
        }
    }

    static List<string> activityLog = new List<string>();
    static List<string> usedPrompts = new List<string>();
    static List<string> usedQuestions = new List<string>();

    static void DisplayMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("             Mindfulness Program        ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Quit");
        Console.WriteLine("5. Display Activity Log");
        Console.WriteLine("========================================");
        Console.Write("Select an activity (1-5): ");
    }

    static int GetDuration(string activityName)
    {
        int duration;
        while (true)
        {
            Console.Write($"Enter the duration in seconds for {activityName}: ");
            if (int.TryParse(Console.ReadLine(), out duration))
            {
                activityLog.Add($"Started {activityName} for {duration} seconds.");
                return duration;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }

    static void Pause(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i}... ");
            Thread.Sleep(1000);
            Console.Write("\r".PadRight(Console.WindowWidth));
        }
    }

    static void BreathActivity(int duration)
    {
        Console.WriteLine("This activity will help you relax by guiding you through slow breathing. Clear your mind and focus on your breathing.");
        Thread.Sleep(2000);

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            AnimateBreathing(4);
            Console.WriteLine("Breathe out...");
            AnimateBreathing(4);
        }

        Console.WriteLine("Good job! You have completed the breathing activity.");
        activityLog.Add("Completed Breathing Activity.");
        Pause(2);
    }

    static void ReflectionActivity(int duration)
    {
        var prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        var questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done something similar again?",
            "What did this experience teach you about yourself?",
            "How can this experience help you in the future?"
        };

        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
        Thread.Sleep(2000);

        string prompt = GetUniquePrompt(prompts);
        string question = GetUniqueQuestion(questions);

        Console.WriteLine(prompt);
        Pause(5);

        foreach (string q in questions)
        {
            Console.WriteLine(q);
            Pause(5);
        }

        Console.WriteLine("Good job! You have completed the reflection activity.");
        activityLog.Add("Completed Reflection Activity.");
        Pause(2);
    }

    static void ListingActivity(int duration)
    {
        Console.WriteLine("This activity will help you think broadly by listing things related to your strengths and positivity.");
        Thread.Sleep(2000);

        var strengths = new List<string>();
        Console.WriteLine("Take a moment to list as many strengths or positive attributes about yourself as you can.");
        Pause(5);

        for (int i = 0; i < duration / 5; i++)
        {
            Console.Write($"Strength #{strengths.Count + 1}: ");
            string strength = Console.ReadLine();
            strengths.Add(strength);
        }

        Console.WriteLine("You listed the following strengths:");
        foreach (string strength in strengths)
        {
            Console.WriteLine($"- {strength}");
        }

        Console.WriteLine("Good job! You have completed the listing activity.");
        activityLog.add("Completed Listing Activity.");
        Pause(2);
    }

    static void AnimateBreathing(int seconds)
    {
        for (int i = 1; i <= seconds; i++)
        {
            int length = (int)(i / (float)seconds * 10);
            Console.WriteLine(new string('=', length));
            Thread.Sleep(1000);
        }
    }

    static string GetUniquePrompt(List<string> prompts)
    {
        Random random = new Random();
        string prompt;
        do
        {
            prompt = prompts[random.Next(prompts.Count)];
        } while (usedPrompts.Contains(prompt) && usedPrompts.Count < prompts.Count);

        usedPrompts.Add(prompt);
        return prompt;
    }

    static string GetUniqueQuestion(List<string> questions)
    {
        Random random = new Random();
        string question;
        do
        {
            question = questions[random.Next(questions.Count)];
        } while (usedQuestions.Contains(question) && usedQuestions.Count < questions.Count);

        usedQuestions.Add(question);
        return question;
    }

    static void SaveLog()
    {
        File.WriteAllLines("activity_log.txt", activityLog);
        Console.WriteLine("Activity log saved.");
    }

    static void DisplayLog()
    {
        if (File.Exists("activity_log.txt"))
        {
            string[] log = File.ReadAllLines("activity_log.txt");
            Console.WriteLine("Activity Log:");
            foreach (string entry in log)
            {
                Console.WriteLine(entry);
            }
        }
        else
        {
            Console.WriteLine("No activity log found.");
        }
    }
}
