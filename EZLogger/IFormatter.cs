namespace EZLogger
{
    public interface IFormatter<T>
    {
        T FormatMessage(LogMessage message);
    }
}
