using Godot;

public partial class PuGun : Area2D
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

			// Directly increase bulletsPerSecond or call a method to do so
			playerGun.bulletsPerSecond += 0.2f; // Adjust the value as needed
			playerGun.bulletSpeed += 150;
            playerGun.UpdateGunStatusUi();
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