using Godot;

public partial class PuBullet : Area2D
{
    public override void _Ready()
    {
        // Connect the body_entered signal to a method
        Connect(Signals.body_entered, new Callable(this, nameof(OnBodyEntered)));
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Player)
        {
            // Assuming the player has a child node named "Gun" that uses the Gun.cs script
            var playerGun = body.GetNode<Gun>(NodeNames.Gun);
			playerGun.bulletSpeed += 900;

			RemoveOtherPowerUps();

            // Remove the power-up from the scene
            QueueFree();
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
