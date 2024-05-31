public class ToJumpTransition : Transition
{
    public ToJumpTransition(State nextState) : base(nextState)
    {
    }

    public override void Disable()
    {
        throw new System.NotImplementedException();
    }

    public override void Enable()
    {
        throw new System.NotImplementedException();
    }
}