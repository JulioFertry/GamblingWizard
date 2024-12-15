namespace GamblingWizard.scenes;
using System.Threading.Tasks;
using Godot;
using System;


public abstract partial class BaseEnemy : Node2D, IEnemy
{
    [Signal]
    public delegate void EnemyDiedEventHandler();

    public abstract string MonsterName { get; }
    protected Player Target;
    protected int HealthPoints;
    protected int Damage;
    protected Label HealthLabel { get; set; }
    protected AnimationPlayer Animations { get; set; }

    public int Health
    {
        get => HealthPoints;
        set
        {
            HealthPoints = Math.Max(0, value);
            UpdateHealthLabel();
            if (HealthPoints <= 0)
            {
                GD.Print($"{MonsterName} ha muerto!");
                Die();
            }
        }
    }

    
    public override void _Ready()
    {
        HealthLabel = GetNode<Label>("HealthLabel");
        Animations = GetNode<AnimationPlayer>("AnimationPlayer");
        UpdateHealthLabel();
        GD.Print($"Un {MonsterName} con {Health} puntos de vida ha aparecido!");
    }

    
    protected void UpdateHealthLabel()
    {
        if (HealthLabel != null)
        {
            HealthLabel.Text = $"{HealthPoints} hp";
        }
    }

    
    public void SetTarget(Player player)
    {
        Target = player;
    }

    
    public async Task Attack()
    {
        if (HealthPoints > 0 && Animations != null && Target != null)
        {
            GD.Print($"{MonsterName} ataca a {Target.PlayerName} causando {Damage} puntos de daño");
            Animations.Play("attack");
            await ToSignal(Animations, "animation_finished");
            Target.ReceiveDamage(Damage);
        }
        else
        {
            GD.PrintErr("Animations not found!");
        }
    }

    
    public async Task ReceiveDamage(int damage)
    {
        if (HealthPoints > 0)
        {
            GD.Print($"{Name} recibe {damage} de daño");
            Animations.Play("shake");
            await ToSignal(Animations, "animation_finished");
            Health -= damage;
        }
    }

    
    public void Die()
    {
        EmitSignal(nameof(EnemyDied));
        QueueFree();
    }
    
}