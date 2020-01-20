namespace EZLogger.Test
{
    class TestMessageFormatter : IFormatter<string>
    {
        public string FormatMessage(LogMessage message) => string.Empty;
    }
}