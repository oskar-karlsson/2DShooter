using Godot;
using System;

public partial class bullet : RigidBody2D
{
    [Export] public float damage = 10;

    public override void _Ready() {
        Timer timer = GetNode<Timer>("Timer");
        timer.Timeout += () => QueueFree();
    }

    public void OnBodyEntered(Node2D body){
        if (body.IsInGroup("attacker"))
        {
            body.GetNode<Health>("Health").Damage(damage);
        }

        QueueFree();
    }
}
