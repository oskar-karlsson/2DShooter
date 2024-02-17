using Godot;

public partial class PlayerHealth : Node2D
{
    public float maxHealth = 100f;

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