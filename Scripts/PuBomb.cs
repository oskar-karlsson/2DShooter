using Godot;

public partial class PuBomb : Area2D
{
    public override void _Ready()
    {
        // Connect the body_entered signal to a method
        Connect(Signals.body_entered, new Callable(this, nameof(OnBodyEntered)));
    }

	private void OnBodyEntered(Node body)
	{
		// Check if the entered body is the player
		if (body is Player player)
		{
			// Check if BombCooldown can be reduced
			if (player.BombCooldown > 4)
			{
				player.BombCooldown -= 2;

				player.ResetBombPlacement();

				// Since the value is reduced, remove all power-ups
				RemoveOtherPowerUps();
				QueueFree();
			}
		}
	}

	private void RemoveOtherPowerUps()
	{
		// Get all power-ups in the "power_ups" group
		var powerUps = GetTree().GetNodesInGroup(Groups.PowerUps);

		foreach (var powerUp in powerUps)
		{
			// Ensure we're only removing other power-ups, not the one currently being collected
			if (powerUp != this)
			{
				// Cast to Node if necessary and remove it
				powerUp?.QueueFree();
			}
		}
	}
}
