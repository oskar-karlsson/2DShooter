using Godot;

public partial class EnemyHealth : Node2D
{
    [Export] public float maxHealth = 20f;

    private float health;

    // Declare a signal
    [Signal]
    public delegate void EnemyKilledEventHandler();

    public override void _Ready()
    {
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EmitSignal(Signals.EnemyKilled);
            GetParent().QueueFree();
        }
    }
}