namespace EZLogger
{
    public interface IFormatter<out T>
    {
        T FormatMessage(LogMessage message);
    }
}
