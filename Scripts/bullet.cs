using Godot;

public partial class bullet : RigidBody2D
{
    public float damage = 10;

    public override void _Ready() {
        Timer timer = GetNode<Timer>(Nodes.Timer);
        timer.Timeout += () => QueueFree();
    }

    public void OnBodyEntered(Node2D body){
        if (body.IsInGroup(Groups.Attacker))
        {
            body.GetNode<EnemyHealth>(Nodes.EnemyHealth).Damage(damage);
        }

        QueueFree();
    }
}