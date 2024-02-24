using Godot;

public partial class PuEngine : Area2D
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
            // Apply the power-up effect
            ApplyEffect(body as Player);

			// var player = (Player)body;
			// var player = body as Player;
			// player.speed += 500;

			RemoveOtherPowerUps();

            // Remove the power-up from the scene
            QueueFree();
        }
    }

    private void ApplyEffect(Player player)
    {
        // Example effect: Increase player speed
        player.speed += 500; // Adjust according to how you've implemented speed in Player.cs
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