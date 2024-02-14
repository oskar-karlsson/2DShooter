using Godot;

public partial class Gun : Node2D
{
    [Export] public PackedScene bulletScene;
    [Export] public float bulletSpeed = 600f;
    [Export] public float bulletsPerSecond = 1f;

    float timeBetweenShots;
    float timeSinceFired = 0f;

    public override void _Ready()
    {
        timeBetweenShots = 1 / bulletsPerSecond;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed(InputActions.Click) && timeSinceFired > timeBetweenShots)
        {
            var bullet = bulletScene.Instantiate<RigidBody2D>();

            bullet.Rotation = GlobalRotation;
            bullet.Position = GlobalPosition;

            var bulletDirection = bullet.Transform.X;
            bullet.LinearVelocity = bulletDirection * bulletSpeed;

            GetTree().Root.AddChild(bullet);

            timeSinceFired = 0f;
        }
        else
        {
            timeSinceFired += (float)delta;
        }
    }
}