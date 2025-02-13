using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; protected set; }

    public abstract void RecordEvent();
    public virtual string GetDetailsString() => $"{Name} - Points: {Points}";
}

public class SimpleGoal : Goal
{
    public override void RecordEvent()
    {
        Points += 100; // Example point value
        IsComplete = true;
    }

    public override string GetDetailsString() => $"{Name} [X] - Points: {Points}";
}

public class EternalGoal : Goal
{
    public override void RecordEvent()
    {
        Points += 50; // Example point value
    }
}

public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, int targetCount, int bonusPoints)
    {
        Name = name;
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        Points += 10; // Example point value
        CurrentCount++;
        if (CurrentCount >= TargetCount)
        {
            Points += BonusPoints;
            IsComplete = true;
        }
    }

    public override string GetDetailsString() => $"{Name} [{CurrentCount}/{TargetCount}] - Points: {Points}";
}

public class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int totalScore = 0;

    public void DisplayMenu()
    {
        Console.WriteLine("1. Create Goal");
        Console.WriteLine("2. Record Event");
        Console.WriteLine("3. Display Goals");
        Console.WriteLine("4. Display Score");
        Console.WriteLine("5. Save Goals");
        Console.WriteLine("6. Load Goals");
        Console.WriteLine("7. Quit");
    }

    public void CreateGoal()
    {
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        string choice = Console.ReadLine();
        Goal goal = null;

        switch (choice)
        {
            case "1":
                goal = new SimpleGoal { Name = "Run a marathon" };
                break;
            case "2":
                goal = new EternalGoal { Name = "Read scriptures daily" };
                break;
            case "3":
                goal = new ChecklistGoal("Attend the temple", 10, 500);
                break;
        }

        if (goal != null)
        {
            goals.Add(goal);
            Console.WriteLine("Goal created successfully!");
        }
    }

    public void RecordEvent()
    {
        Console.WriteLine("Select a goal to record:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        int index = int.Parse(Console.ReadLine()) - 1;
        goals[index].RecordEvent();
        totalScore += goals[index].Points;
    }

    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {totalScore}");
    }

    public void SaveGoals()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(goals, options);
        File.WriteAllText("goals.json", jsonString);
        Console.WriteLine("Goals saved successfully!");
    }

    public void LoadGoals()
    {
        if (File.Exists("goals.json"))
        {
            string jsonString = File.ReadAllText("goals.json");
            goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();

        while (true)
        {
            goalManager.DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                case "2":
                    goalManager.RecordEvent();
                    break;
                case "3":
                    goalManager.DisplayGoals();
                    break;
                case "4":
                    goalManager.DisplayScore();
                    break;
                case "5":
                    goalManager.SaveGoals();
                    break;
                case "6":
                    goalManager.LoadGoals();
                    break;
                case "7":
                    return;
            }
        }
    }
}
