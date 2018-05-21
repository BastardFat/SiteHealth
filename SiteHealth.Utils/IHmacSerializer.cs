namespace BotMagic.Utils
{
    public interface IHmacSerializer<T>
    {
        T Deserialize(string data);
        string Serialize(T obj);
    }
}