namespace GamblingWizard;

public interface IEnemy
{
    string MonsterName { get; }
    int Health { get; set; }

    void ReceiveDamage(int damage);
    void Die();
}
