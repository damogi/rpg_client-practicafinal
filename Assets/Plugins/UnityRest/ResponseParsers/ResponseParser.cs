namespace UnityRest
{
    public abstract class ResponseParser
    {
        public abstract void OnResponse (string responseBody);
    }
}