namespace Agility.RequestResponse.Common
{
    /// <summary>
    /// Contains the details about an error which occurs when processing requests.
    /// </summary>
    public class Error
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }
}