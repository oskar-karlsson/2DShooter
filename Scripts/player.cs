using System.Collections.Generic;
using Godot;

public partial class Player : CharacterBody2D
{
    public float speed = 150f;
    public int experience = 0;
    private int currentLevel = 0;
    // private List<int> experienceLevels = new List<int>{100, 300, 600, 1000, 1500}; // Example thresholds
    private List<int> experienceLevels = new List<int>{20, 60, 120, 200, 300}; // Example thresholds


    public override void _Process(double delta)
{
    if (Input.IsActionJustPressed("rightClick"))
    {
        PlaceBomb();
    }
}

    public override void _PhysicsProcess(double delta){
        LookAt(GetGlobalMousePosition());

        var move_input = Input.GetVector(InputActions.Left, InputActions.Right, InputActions.Up, InputActions.Down);

        Velocity = move_input * speed;

        MoveAndSlide();
    }

    public void AddExperience(int xp)
    {
        experience += xp;
        CheckForLevelUp();
        UpdateUI();
    }

    private void UpdateUI()
	{
		var xpLabel = GetNode<Label>(NodePaths.XP);

		xpLabel.Text = $"XP: {experience}";
	}

    private void PlaceBomb()
    {
        var bombScene = (PackedScene)ResourceLoader.Load(Scenes.Bomb);
        Bomb bomb = (Bomb)bombScene.Instantiate();
        bomb.Position = Position; // Or a specified offset
        GetParent().AddChild(bomb); // Assuming the player and bombs are on the same node level
    }

        private void CheckForLevelUp()
    {
        // Check if the player's experience has reached the next level's threshold
        if (currentLevel < experienceLevels.Count && experience >= experienceLevels[currentLevel])
        {
            // Level up!
            currentLevel++;
            SpawnPowerUps(); // Method to spawn power-ups
        }
    }

    private void SpawnPowerUps()
    {
        // Assuming you have set up paths or references to your power-up scenes
        PackedScene puSpeedScene = (PackedScene)ResourceLoader.Load("res://Entities/PuEngine.tscn");
        PackedScene puFireRateScene = (PackedScene)ResourceLoader.Load("res://Entities/PuGun.tscn");
        PackedScene puRangeScene = (PackedScene)ResourceLoader.Load("res://Entities/PuBullet.tscn");

        // Instantiate power-ups
        Node2D puEngine = (Node2D)puSpeedScene.Instantiate();
        Node2D puGun = (Node2D)puFireRateScene.Instantiate();
        Node2D puBullet = (Node2D)puRangeScene.Instantiate();

        puEngine.AddToGroup(Groups.PowerUps);
        puGun.AddToGroup(Groups.PowerUps);
        puBullet.AddToGroup(Groups.PowerUps);

        var mainGameNode = GetTree().Root.GetNode(NodeNames.MainGame);

        // Set positions
        puEngine.Position = GetNode<Marker2D>("/root/MainGame/PowerUps/PuEngine").Position;
        puGun.Position = GetNode<Marker2D>("/root/MainGame/PowerUps/PuGun").Position;
        puBullet.Position = GetNode<Marker2D>("/root/MainGame/PowerUps/PuBullet").Position;

        mainGameNode.CallDeferred("add_child", puEngine);
        mainGameNode.CallDeferred("add_child", puGun);
        mainGameNode.CallDeferred("add_child", puBullet);
    }
}