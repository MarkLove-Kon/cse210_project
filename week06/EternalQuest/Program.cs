using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Base class for different goal types
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; protected set; }

    public abstract void RecordEvent();
    public virtual string GetDetailsString() => $"{Name} - Points: {Points}";
}

// Simple goal that can be marked complete
public class SimpleGoal : Goal
{
    public override void RecordEvent()
    {
        Points += 100; // Example point value
        IsComplete = true;
    }

    public override string GetDetailsString() => $"{Name} [X] - Points: {Points}";
}

// Eternal goal that is never complete
public class EternalGoal : Goal
{
    public override void RecordEvent()
    {
        Points += 50; // Example point value
    }
}

// Checklist goal that requires multiple completions
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

// Progress goal that tracks incremental progress
public class ProgressGoal : Goal
{
    public int Progress { get; private set; }
    public int TargetProgress { get; private set; }

    public ProgressGoal(string name, int targetProgress)
    {
        Name = name;
        TargetProgress = targetProgress;
    }

    public override void RecordEvent()
    {
        Points += 20; // Example point value
        Progress++;
        if (Progress >= TargetProgress)
        {
            IsComplete = true;
        }
    }

    public override string GetDetailsString() => $"{Name} [{Progress}/{TargetProgress}] - Points: {Points}";
}

// Negative goal that deducts points
public class NegativeGoal : Goal
{
    public NegativeGoal(string name)
    {
        Name = name;
    }

    public override void RecordEvent()
    {
        Points -= 20; // Example point deduction
        if (Points < 0) Points = 0;
    }

    public override string GetDetailsString() => $"{Name} - Points: {Points}";
}

// Class to manage goals and user interactions
public class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int totalScore = 0;
    private int level = 1;
    private int pointsForNextLevel = 1000;
    private List<string> activityLog = new List<string>();

    // Display the main menu
    public void DisplayMenu()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("             Eternal Quest              ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Create Goal");
        Console.WriteLine("2. Record Event");
        Console.WriteLine("3. Display Goals");
        Console.WriteLine("4. Display Score");
        Console.WriteLine("5. Save Goals");
        Console.WriteLine("6. Load Goals");
        Console.WriteLine("7. Display Activity Log");
        Console.WriteLine("8. Quit");
        Console.WriteLine("========================================");
        Console.Write("Select an option (1-8): ");
    }

    // Create a new goal based on user input
    public void CreateGoal()
    {
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Progress Goal");
        Console.WriteLine("5. Negative Goal");
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
            case "4":
                goal = new ProgressGoal("Complete a project", 5);
                break;
            case "5":
                goal = new NegativeGoal("Reduce screen time");
                break;
        }

        if (goal != null)
        {
            goals.Add(goal);
            Console.WriteLine("Goal created successfully!");
        }
    }

    // Record an event for a selected goal
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
        activityLog.Add($"Recorded event for: {goals[index].Name}");

        if (totalScore >= pointsForNextLevel)
        {
            level++;
            pointsForNextLevel += 1000;
            Console.WriteLine($"Congratulations! You've leveled up to Level {level}!");
            activityLog.Add($"Leveled up to Level {level}");
        }
    }

    // Display the details of all goals
    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    }

    // Display the user's total score and level
    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {totalScore}");
        Console.WriteLine($"Level: {level}");
        Console.WriteLine($"Points for next level: {pointsForNextLevel - totalScore}");
    }

    // Save goals and activity log to files
    public void SaveGoals()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(goals, options);
        File.WriteAllText("goals.json", jsonString);
        Console.WriteLine("Goals saved successfully!");

        File.WriteAllLines("activity_log.txt", activityLog);
        Console.WriteLine("Activity log saved successfully!");
    }

    // Load goals and activity log from files
    public void LoadGoals()
    {
        if (File.Exists("goals.json"))
        {
            string jsonString = File.ReadAllText("goals.json");
            goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
            Console.WriteLine("Goals loaded successfully!");

            activityLog = new List<string>(File.ReadAllLines("activity_log.txt"));
            Console.WriteLine("Activity log loaded successfully!");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }

    // Display the activity log
    public void DisplayLog()
    {
        if (activityLog.Count == 0)
        {
            Console.WriteLine("No activities recorded yet.");
        }
        else
        {
            Console.WriteLine("Activity Log:");
            foreach (var entry in activityLog)
            {
                Console.WriteLine(entry);
            }
        }
    }
}

// Main program class
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
                    goalManager.DisplayLog();
                    break;
                case "8":
                    return;
            }
        }
    }
}
