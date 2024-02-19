using Godot;

public partial class Bomb : Area2D
{
    private void OnTimerTimeout()
    {
        // Handle the explosion here (e.g., apply effects, damage)
        
		QueueFree();
    }
}
