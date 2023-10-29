using Collections;

public class AreaEventArgs
{
    private readonly object _areaCollider;
    private readonly object _otherCollider;
    private readonly IReadOnlyBlackboard _args;

    public object AreaCollider => _areaCollider;
    public object OtherCollider => _otherCollider;
    public IReadOnlyBlackboard Args => _args;

    public AreaEventArgs( object areaCollider, object otherCollider, IReadOnlyBlackboard args)
    {
        Exceptions.ArgumentNull.ThrowIfNull(areaCollider, nameof(areaCollider));
        Exceptions.ArgumentNull.ThrowIfNull(otherCollider, nameof(otherCollider));
        Exceptions.ArgumentNull.ThrowIfNull(args, nameof(args));

        _areaCollider = areaCollider;
        _otherCollider = otherCollider;
        _args = args;
    }
}