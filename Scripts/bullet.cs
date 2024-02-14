using Godot;

public partial class bullet : RigidBody2D
{
    [Export] public float damage = 10;

    public override void _Ready() {
        Timer timer = GetNode<Timer>(NodePaths.Timer);
        timer.Timeout += () => QueueFree();
    }

    public void OnBodyEntered(Node2D body){
        if (body.IsInGroup(Groups.Attacker))
        {
            body.GetNode<Health>(NodePaths.Health).Damage(damage);
        }

        QueueFree();
    }
}