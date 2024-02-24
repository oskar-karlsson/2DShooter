using Godot;
using System.Collections.Generic;

public partial class PowerUps : Marker2D
{
    private List<Marker2D> powerUpSpawnLocations = new List<Marker2D>();

    public override void _Ready()
    {
        // Populate the list with your Marker2D nodes
        powerUpSpawnLocations.Add(GetNode<Marker2D>(NodeNames.PuEngine));
        powerUpSpawnLocations.Add(GetNode<Marker2D>(NodeNames.PuGun));
        powerUpSpawnLocations.Add(GetNode<Marker2D>(NodeNames.PuBullet));
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}