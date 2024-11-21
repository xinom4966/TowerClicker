public interface IpoolInterface<U> where U : class, IpoolInterface<U>
{
    void SetPool(Pool<U> pool);
    void SetActive(bool active);
}