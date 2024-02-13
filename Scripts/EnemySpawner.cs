using Godot;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene enemyScene;
	[Export] public Node2D[] spawnPoints;
	[Export] public float enemiesPerSecond = 1f;

	float timeBetweenSpawns; // spawnRate
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
		RandomNumberGenerator rng = new RandomNumberGenerator();
		Vector2 location = spawnPoints[rng.Randi() % spawnPoints.Length].GlobalPosition;
		Enemy enemy = (Enemy)enemyScene.Instantiate();
		enemy.GlobalPosition = location;
		GetTree().Root.AddChild(enemy);
	}
}
