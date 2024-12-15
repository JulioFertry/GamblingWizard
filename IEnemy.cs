namespace GamblingWizard;
using System.Threading.Tasks;


public interface IEnemy
{
    string MonsterName { get; }
    int Health { get; set; }

    void SetTarget(Player player);
    Task ReceiveDamage(int damage);
    Task Attack();
    void Die();
}