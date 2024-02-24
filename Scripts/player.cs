using System.Collections.Generic;
using Godot;

public partial class Player : CharacterBody2D
{
    public float speed = 150f;
    public int experience = 0;
    private int currentLevel = 0;
    private List<int> experienceLevels = new List<int>(); // Start with an empty list

    public Player()
    {
        AddNextExperienceLevel();
    }

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
        // Since only one level can be gained at a time, check the next level's threshold
        if (currentLevel < experienceLevels.Count && experience >= experienceLevels[currentLevel])
        {
            currentLevel++;
            GD.Print("Level Up! Current Level: ", currentLevel);
            SpawnPowerUps(); // Method to spawn power-ups
            AddNextExperienceLevel(); // Add the next level threshold
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

    // private void AddNextExperienceLevel()
    // {
    //     // Simple linear growth formula; adjust as needed for your game's balance
    //     int nextLevelExp = 100 + (currentLevel * 200); // Example formula
    //     experienceLevels.Add(nextLevelExp);
    // }

    private int additionalIncrease = 100; // Start with 200 for the first level, will increase by 100 each time

    private void AddNextExperienceLevel()
    {
        // If it's the first level, set the base. Otherwise, increase the increase by 100 more than last time.
        if (experienceLevels.Count == 0)
        {
            experienceLevels.Add(100); // Base XP for the first level
        }
        else
        {
            // Calculate the next level's XP based on the last level's XP plus the current increase
            int nextLevelExp = experienceLevels[experienceLevels.Count - 1] + additionalIncrease;
            experienceLevels.Add(nextLevelExp);
        }

        // Increase the additionalIncrease for the next level
        additionalIncrease += 100;
    }
}