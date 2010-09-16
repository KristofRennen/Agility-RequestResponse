using System.Collections.Generic;

namespace Agility.RequestResponse.Common
{
    /// <summary>
    /// Container to return data from the BackEnd to the FrontEnd.
    /// </summary>
    public abstract class Response
    {
        protected Response()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// List of all errors occurred when generating this response.
        /// </summary>
        public List<string> Errors { get; private set; }
    }
}