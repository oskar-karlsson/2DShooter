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

			var player = (Player)body;
			player.speed += 50;

			RemoveOtherPowerUps();
        }
    }

	private void RemoveOtherPowerUps()
	{
		// Get all power-ups in the "power_ups" group
		var powerUps = GetTree().GetNodesInGroup(Groups.PowerUps);

		foreach (var powerUp in powerUps)
		{
            powerUp.QueueFree();
		}
	}
}