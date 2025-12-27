namespace DefaultNamespace
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
    public enum PlayerState
    {
        Jump,
        Run,
        Fall,
        Die,
        Ideal
    }
}