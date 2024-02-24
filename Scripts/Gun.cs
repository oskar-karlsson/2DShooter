using Godot;

public partial class Gun : Node2D
{
    [Export] public PackedScene bulletScene;
    public float bulletSpeed = 300f;
    public float bulletLifetimeModifier = 0f; // New variable to modify bullet lifetime
    public float _bulletsPerSecond = 1f;
    float timeBetweenShots;
    float timeSinceFired = 0f;

    public float bulletsPerSecond
    {
        get => _bulletsPerSecond;
        set
        {
            _bulletsPerSecond = value;
            timeBetweenShots = 1 / _bulletsPerSecond; // Recalculate timeBetweenShots whenever bulletsPerSecond changes
        }
    }

    public override void _Ready()
    {
        bulletsPerSecond = _bulletsPerSecond;
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