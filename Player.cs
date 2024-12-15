namespace GamblingWizard;
using Godot;
using System;


public partial class Player : Node
{
    [Signal]
    public delegate void PlayerAttackedEventHandler();

    public string PlayerName { get; } = "Kiko";
    private int _maxHealth = 100;
    private int _currentHealth;

    private int Health
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Math.Max(0, value);
            UpdateStatsLabels();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private int _damage = 4;

    private int Power
    {
        get => _damage;
        set
        {
            _damage = value;
            UpdateStatsLabels();
        }
    }

    private Label PlayerHealthLabel { get; set; }
    private Label PlayerNameLabel { get; set; }
    private Label PlayerDamageLabel { get; set; }
    private IEnemy _target;
    private Button _attackButton;


    public override void _Ready()
    {
        _attackButton = GetNodeOrNull<Button>("PlayerUI/Actions/AttackButton");
        if (_attackButton != null)
        {
            _attackButton.Pressed += _onAttackButtonPressed;
        }

        _currentHealth = _maxHealth;

        PlayerHealthLabel = GetNodeOrNull<Label>("PlayerUI/PlayerStats/StatsContainer/PlayerHpLabel");
        PlayerNameLabel = GetNodeOrNull<Label>("PlayerUI/PlayerStats/StatsContainer/PlayerNameLabel");
        PlayerDamageLabel = GetNodeOrNull<Label>("PlayerUI/PlayerStats/StatsContainer/PlayerDmgLabel");

        if (PlayerNameLabel != null) PlayerNameLabel.Text = PlayerName;
        UpdateStatsLabels();

        GD.Print($"Bienvenido a la grieta de la ludopatía, {PlayerName}!");
    }

    
    private void UpdateStatsLabels()
    {
        if (PlayerHealthLabel != null)
        {
            PlayerHealthLabel.Text = $"{_currentHealth} HP";
        }

        if (PlayerDamageLabel != null)
        {
            PlayerDamageLabel.Text = $"{_damage} DMG";
        }
    }

    
    public void SetTarget(IEnemy target)
    {
        _target = target;
    }

    
    public void SetAttackButtonState(bool enabled)
    {
        if (_attackButton != null)
        {
            _attackButton.Disabled = !enabled;
        }
    }

    
    private void _onAttackButtonPressed()
    {
        SetAttackButtonState(false);
        
        if (_target == null || !IsInstanceValid(_target as Node))
        {
            GD.PrintErr("Target don't exists anymore");
            return;
        }

        Node2D targetNode = _target as Node2D;
        if (targetNode != null)
        {
            GD.Print($"{PlayerName} ataca a {_target.MonsterName}");
            CreateSlash(targetNode.GlobalPosition);
            _target.ReceiveDamage(_damage);
            EmitSignal(nameof(PlayerAttacked));
        }
        else
        {
            GD.PrintErr("Enemy is not a Node2D!");
        }
    }

    
    public void ReceiveDamage(int damage)
    {
        if (Health > 0)
        {
            Health -= damage;
        }
    }

    
    private void Die()
    {
        GD.Print($"Has muerto, {PlayerName}");
        GetTree().Quit();
    }

    
    private void CreateSlash(Vector2 position)
    {
        PackedScene slashScene = GD.Load<PackedScene>("res://slash.tscn");
        if (slashScene != null)
        {
            Node2D slash = slashScene.Instantiate<Node2D>();
            if (slash != null)
            {
                Node main = GetTree().CurrentScene;
                if (main != null)
                {
                    main.AddChild(slash);
                    slash.GlobalPosition = position;
                }
                else
                {
                    GD.PrintErr("There's no scene");
                }
            }
            else
            {
                GD.PrintErr("Slash was not initialized");
            }
        }
        else
        {
            GD.PrintErr("Slash node could not be created");
        }
    }
    
}