using Godot;

public partial class EnemyHealth : Node2D
{
    public float maxHealth = 20f;
    private float health;
    
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