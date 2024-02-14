using Godot;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene enemyScene;
	[Export] public Node2D[] spawnPoints;
	[Export] public float enemiesPerSecond = 1f;

	float timeBetweenSpawns;
	float timeUntilSpawns = 0;

	public override void _Ready()
	{
		timeBetweenSpawns = 1 / enemiesPerSecond;
	}


	public override void _Process(double delta)
	{
		if (timeUntilSpawns > timeBetweenSpawns)
		{
			Spawn();

			timeUntilSpawns = 0;
		}
		else
		{
			timeUntilSpawns += (float)delta;
		}
	}

	private void Spawn()
	{
		var rng = new RandomNumberGenerator();
		var location = spawnPoints[rng.Randi() % spawnPoints.Length].GlobalPosition;
		var enemy = (Enemy)enemyScene.Instantiate();
		enemy.GlobalPosition = location;
		GetTree().Root.AddChild(enemy);
	}
}