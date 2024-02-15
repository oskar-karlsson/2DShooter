using Godot;

public partial class EnemyHealth : Node2D
{
    [Export] public float maxHealth = 20f;

    private float health;

    public override void _Ready()
    {
        health = maxHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GetParent().QueueFree();
        }
    }
}