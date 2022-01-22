using Akka.Actor;

namespace MovieStreaming.Common.Actors;

public class PlaybackActor : ReceiveActor
{
    public PlaybackActor()
    {
        Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
        Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
    }

    #region Lifecycle hooks
    protected override void PreStart()
    {
        Console.WriteLine("Playback actor pre start");
    }

    protected override void PostStop()
    {
        Console.WriteLine("Playback actor post stop");
    }

    protected override void PreRestart(Exception reason, object message)
    {
        Console.WriteLine($"Playback actor pre restart because: {reason.Message}");
            
        base.PreRestart(reason, message);   
    }

    protected override void PostRestart(Exception reason)
    {
        Console.WriteLine($"Playback actor post stop because {reason.Message}");
            
        base.PostRestart(reason);
    }

    #endregion
}