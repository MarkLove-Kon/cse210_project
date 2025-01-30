using System;
using System.Collections.Generic;

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}


public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Video Title 1", "Author 1", 300);
        Video video2 = new Video("Video Title 2", "Author 2", 450);
        Video video3 = new Video("Video Title 3", "Author 3", 120);

        // Add comments to video1
        video1.AddComment(new Comment("User1", "Great video!"));
        video1.AddComment(new Comment("User2", "Very informative."));
        video1.AddComment(new Comment("User3", "Loved it!"));

        // Add comments to video2
        video2.AddComment(new Comment("User4", "Thanks for sharing."));
        video2.AddComment(new Comment("User5", "I learned a lot."));
        video2.AddComment(new Comment("User6", "Keep it up!"));

        // Add comments to video3
        video3.AddComment(new Comment("User7", "Not what I expected."));
        video3.AddComment(new Comment("User8", "Could use more details."));
        
        // Create a list of videos
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video information
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"{comment.Name}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}