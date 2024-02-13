using Godot;

public partial class Gun : Node2D
{
    [Export] public PackedScene bulletScene;
    [Export] public float bulletSpeed = 600f;
    [Export] public float bulletsPerSecond = 5f;
    [Export] public float bulletDamage = 30f;

    float timeBetweenShots;
    float timeSinceFired = 0f;

    public override void _Ready()
    {
        timeBetweenShots = 1 / bulletsPerSecond;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("click") && timeSinceFired > timeBetweenShots)
        {
            RigidBody2D bullet = bulletScene.Instantiate<RigidBody2D>();

            bullet.Rotation = this.GlobalRotation;
            bullet.Position = this.GlobalPosition;

            Vector2 bulletDirection = bullet.Transform.X;
            bullet.LinearVelocity = bulletDirection * bulletSpeed;

            this.GetTree().Root.AddChild(bullet);

            timeSinceFired = 0f;
        }
        else
        {
            timeSinceFired += (float)delta;
        }
    }
}