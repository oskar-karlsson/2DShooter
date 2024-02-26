using Godot;

public partial class Bomb : Area2D
{
    private void OnTimerTimeout()
    {
        var affectedBodies = GetOverlappingBodies();
        foreach (var body in affectedBodies)
        {
            // Use the constant from the Nodes class for node names
            if (body is Enemy)
            {
                var enemy = body as Enemy;
                enemy.GetNode<EnemyHealth>(NodeNames.EnemyHealth).Damage(50);
            }
            else if (body is Player)
            {
                var player = body as Player;
                player.GetNode<PlayerHealth>(NodeNames.PlayerHealth).Damage(50);
            }
        }
        
		QueueFree();
    }

    public void SetExplosionRadius(float radius)
    {
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        if (collisionShape.Shape is CircleShape2D circleShape)
        {
            circleShape.Radius = radius;
        }
        // Add similar adjustments for other shape types if needed
    }

}