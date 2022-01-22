namespace MovieStreaming.Common.Messages;

public class StopMovieMessage
{
    public Guid UserId { get; }
        
    public StopMovieMessage(Guid userId)
    {
        UserId = userId;
    }
}