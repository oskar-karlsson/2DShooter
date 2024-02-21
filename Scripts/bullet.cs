using Godot;

public partial class Bullet : RigidBody2D
{
    public float damage = 10;

    public override void _Ready() {
        Timer timer = GetNode<Timer>(NodeNames.Timer);
        timer.Timeout += () => QueueFree();
    }

    public void OnBodyEntered(Node2D body){
        if (body.IsInGroup(Groups.Attacker))
        {
            body.GetNode<EnemyHealth>(NodeNames.EnemyHealth).Damage(damage);
        }

        QueueFree();
    }
}