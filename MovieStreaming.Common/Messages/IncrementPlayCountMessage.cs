namespace Akka001.Common.Messages;
public class IncrementPlayCountMessage
{
    public string MovieTitle { get; }

    public IncrementPlayCountMessage(string movieTitle)
    {
        MovieTitle = movieTitle;
    }
}