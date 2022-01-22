using Akka.Actor;

namespace MovieStreaming.Common.Actors;

public class PlaybackStatisticsActor : ReceiveActor
{
    public PlaybackStatisticsActor()
    {
        Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
    }
        
    #region Lifecycle hooks
    protected override void PreStart()
    {
        Console.WriteLine("PlaybackStatistics actor pre start");
    }

    protected override void PostStop()
    {
        Console.WriteLine("PlaybackStatistics actor post stop");
    }

    protected override void PreRestart(Exception reason, object message)
    {
        Console.WriteLine($"PlaybackStatistics actor pre restart because: {reason.Message}");
            
        base.PreRestart(reason, message);   
    }

    protected override void PostRestart(Exception reason)
    {
        Console.WriteLine($"PlaybackStatistics actor post stop because {reason.Message}");
            
        base.PostRestart(reason);
    }

    #endregion
}