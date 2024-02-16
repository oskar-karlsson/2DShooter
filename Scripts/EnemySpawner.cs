using Godot;

public partial class EnemySpawner : Node2D
{
	[Export] public PackedScene enemyScene;
	[Export] public Node2D[] spawnPoints;
	[Export] public float enemiesPerSecond = 0.4f;

	float timeBetweenSpawns;
	float timeUntilSpawns = 0;
	private int killCount = 0;

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

		// Connect the EnemyKilled signal to a method to handle the kill count
		var enemyHealth = enemy.GetNode<EnemyHealth>(NodePaths.EnemyHealth);

		enemyHealth.Connect(Signals.EnemyKilled, new Callable(this, nameof(OnEnemyKilled)));

		GetTree().Root.AddChild(enemy);
	}

	private void OnEnemyKilled()
	{
		killCount++;
		UpdateKillCountLabel();
	}

	private void UpdateKillCountLabel()
	{
		var killCountLabel = (Label)GetTree().Root.GetNode(NodePaths.MainGame).GetNode(NodePaths.KillCount);

		killCountLabel.Text = $"Kills: {killCount}";
	}
}