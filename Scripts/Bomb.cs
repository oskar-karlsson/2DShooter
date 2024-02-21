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
}