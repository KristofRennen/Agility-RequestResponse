using System;
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
            Errors = new List<Error>();
        }

        /// <summary>
        /// List of all errors occurred when generating this response.
        /// </summary>
        public IEnumerable<Error> Errors { get; private set; }

        /// <summary>
        /// Adds a new error based on the given exception <seealso cref="System.Exception"/>.
        /// </summary>
        /// <param name="exception">The exception to create the error from.</param>
        public void AddError(Exception exception)
        {
            var errors = Errors as List<Error>;
            errors.Add(new Error { Message = exception.Message, Source = exception.Source, Trace = exception.StackTrace });
        }
    }
}