using Collections;

public class MessageEventArgs
{
    public IReadOnlyBlackboard Blackboard { get; }
    public MessageEventArgs(IReadOnlyBlackboard blackboard)
    {
        Blackboard = blackboard;
    }
}