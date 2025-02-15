using System;
using System.Text.Json;

public abstract class GoalBase
{
    protected string name;
    protected string description;
    protected int points;
    protected bool isCompleted;

    public GoalBase(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        this.isCompleted = false;
    }

    public virtual void RecordEvent()
    {
        // Default behavior for recording an event
    }

    public abstract string GetStatus();

    public int GetPoints()
    {
        return points;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public override string ToString()
    {
        return $"{name} - {description} - {points} points - {(isCompleted ? "[X]" : "[ ]")}";
    }
}

public class SimpleGoal : GoalBase
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        isCompleted = true;
    }

    public override string GetStatus()
    {
        return isCompleted ? "[X] Completed" : "[ ] Not Completed";
    }
}

public class EternalGoal : GoalBase
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        // No change in isCompleted for eternal goals
    }

    public override string GetStatus()
    {
        return "[∞] Eternal Goal";
    }
}

public class ChecklistGoal : GoalBase
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints) 
        : base(name, description, points)
    {
        this.targetCount = targetCount;
        this.currentCount = 0;
        this.bonusPoints = bonusPoints;
    }

    public override void RecordEvent()
    {
        currentCount++;
        if (currentCount >= targetCount)
        {
            isCompleted = true;
        }
    }

    public override string GetStatus()
    {
        return $"{currentCount}/{targetCount} Completed - {(isCompleted ? $"[X] Completed with {bonusPoints} bonus points" : "[ ] Not Completed")}";
    }

    public int GetBonusPoints()
    {
        return isCompleted ? bonusPoints : 0;
    }
}

public class Program
{
    private static List<GoalBase> goals = new List<GoalBase>();
    private static int totalScore = 0;

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. Record an Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    SaveGoals();
                    break;
                case "6":
                    LoadGoals();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CreateGoal()
    {
        Console.Clear();
        Console.WriteLine("Create a New Goal");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choose a goal type: ");
        string type = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter goal points: ");
        int points = int.Parse(Console.ReadLine());

        GoalBase goal = null;

        switch (type)
        {
            case "1":
                goal = new SimpleGoal(name, description, points);
                break;
            case "2":
                goal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonusPoints = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, description, points, targetCount, bonusPoints);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                return;
        }

        goals.Add(goal);
        Console.WriteLine("Goal created successfully!");
    }

    private static void RecordEvent()
    {
        Console.Clear();
        DisplayGoals();
        Console.Write("Enter the number of the goal to record an event: ");
        int goalIndex = int.Parse(Console.ReadLine()) - 1;

        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            totalScore += goals[goalIndex].GetPoints();

            if (goals[goalIndex] is ChecklistGoal checklistGoal && checklistGoal.IsCompleted())
            {
                totalScore += checklistGoal.GetBonusPoints();
            }

            Console.WriteLine("Event recorded successfully!");
        }
        else
        {
            Console.WriteLine("Invalid goal number. Please try again.");
        }
    }

    private static void DisplayGoals()
    {
        Console.Clear();
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i]} - {goals[i].GetStatus()}");
        }
    }

    private static void DisplayScore()
    {
        Console.Clear();
        Console.WriteLine($"Total Score: {totalScore} points");
    }

    private static void SaveGoals()
    {
        using (FileStream fileStream = new FileStream("goals.json", FileMode.Create))
        {
            JsonSerializer.Serialize(fileStream, goals);
            JsonSerializer.Serialize(fileStream, totalScore);
        }
        Console.WriteLine("Goals saved successfully!");
    }

    private static void LoadGoals()
    {
        if (File.Exists("goals.dat"))
        if (File.Exists("goals.json"))
        {
            using (FileStream fileStream = new FileStream("goals.json", FileMode.Open))
            {
                goals = JsonSerializer.Deserialize<List<GoalBase>>(fileStream);
                totalScore = JsonSerializer.Deserialize<int>(fileStream);
            }
            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}

