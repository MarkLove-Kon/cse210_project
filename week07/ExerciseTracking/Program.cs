using System;

public abstract class ActivityBase
{
    private DateTime date;
    private int length; // length in minutes

    public ActivityBase(DateTime date, int length)
    {
        this.date = date;
        this.length = length;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    
    public string GetSummary()
    {
        return $"{date.ToString("dd MMM yyyy")} {this.GetType().Name} ({length} min) - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }

    public int GetLength()
    {
        return length;
    }
}

public class Running : ActivityBase
{
    private double distance; // distance in miles

    public Running(DateTime date, int length, double distance)
        : base(date, length)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return (distance / GetLength()) * 60;
    }

    public override double GetPace()
    {
        return GetLength() / distance;
    }
}

public class Cycling : ActivityBase
{
    private double speed; // speed in mph

    public Cycling(DateTime date, int length, double speed)
        : base(date, length)
    {
        this.speed = speed;
    }

    public override double GetDistance()
    {
        return (speed * GetLength()) / 60;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return 60 / speed;
    }
}
public class Swimming : ActivityBase
{
    private int laps;

    public Swimming(DateTime date, int length, int laps)
        : base(date, length)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0 * 0.62;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetLength()) * 60;
    }

    public override double GetPace()
    {
        return GetLength() / GetDistance();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<ActivityBase> activities = new List<ActivityBase>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 3), 30, 20.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
