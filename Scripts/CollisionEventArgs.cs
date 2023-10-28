using Collections;
using System;

public class CollisionEventArgs : EventArgs {
	private readonly ISceneService _sender;
	private readonly object _senderCollider;
	private readonly object _otherCollider;
	private readonly IReadOnlyBlackboard _args;

	public ISceneService Sender => _sender;
	public object SenderCollider => _senderCollider;
	public object OtherCollider => _otherCollider;
	public IReadOnlyBlackboard Args => _args;

	public CollisionEventArgs(ISceneService sender, object senderCollider, object otherCollider, IReadOnlyBlackboard args) {
		Exceptions.ArgumentNull.ThrowIfNull(sender, nameof(sender));
		Exceptions.ArgumentNull.ThrowIfNull(senderCollider, nameof(senderCollider));
		Exceptions.ArgumentNull.ThrowIfNull(otherCollider,nameof(otherCollider));
		Exceptions.ArgumentNull.ThrowIfNull(args, nameof(args));
	}
}