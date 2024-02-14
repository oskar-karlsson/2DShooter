using Godot;
using System;
using System.Diagnostics;

public partial class Enemy : CharacterBody2D
{
    player playerCharacter;

    [Export] public float speed = 250f;
    [Export] public float damage = 25f;
    [Export] public float attacksPerSecond = 2f;

    float timeBetweenAttacks;
    float timeUntilAttack;
    bool withinAttackRange = false;

    public override void _Ready()
    {
        playerCharacter = (player)GetTree().Root.GetNode(NodePaths.MainGame).GetNode(NodePaths.Player);

        timeBetweenAttacks = 1 / attacksPerSecond;
        timeUntilAttack = timeBetweenAttacks;
    }

    public override void _Process(double delta)
    {
        if (withinAttackRange && timeUntilAttack <= 0)
        {
            Attack();
            timeUntilAttack = timeBetweenAttacks;
        }
        else
        {
            timeUntilAttack -= (float)delta;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerCharacter != null)
        {
            LookAt(playerCharacter.GlobalPosition);
            var direction = (playerCharacter.GlobalPosition - GlobalPosition).Normalized();
            Velocity = direction * speed;
        }
        else
        {
            Velocity = Vector2.Zero;
        }

        MoveAndSlide();
    }

    public void Attack()
    {
        playerCharacter.GetNode<Health>(NodePaths.Health).Damage(damage);
    }

    public void OnAttackRangeBodyEnter(Node2D body)
    {
        if (body.IsInGroup(Groups.Player))
        {
            withinAttackRange = true;
        }
    }

    public void OnAttackRangeBodyExit(Node2D body)
    {
        if (body.IsInGroup(Groups.Player))
        {
            withinAttackRange = false;
            timeUntilAttack = timeBetweenAttacks;
        }
    }
}