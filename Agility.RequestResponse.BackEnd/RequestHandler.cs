using System;
using Agility.RequestResponse.Common;

namespace Agility.RequestResponse.BackEnd
{
    /// <summary>
    /// Contract for each handler which can process a request and return a response.
    /// </summary>
    /// <typeparam name="TRequest">The request to process <seealso cref="Agility.RequestResponse.Common.Request"/>.</typeparam>
    /// <typeparam name="TResponse">The response to return <seealso cref="Agility.RequestResponse.Common.Response"/>.</typeparam>
    public interface IRequestHandler<TRequest, TResponse> 
        where TRequest : Request
        where TResponse : Response
    {
        /// <summary>
        /// Handles a request and returns a response.
        /// </summary>
        /// <param name="request">The request to process <seealso cref="Agility.RequestResponse.Common.Request"/>.</param>
        /// <returns>A response <seealso cref="Agility.RequestResponse.Common.Response"/></returns>
        Response Handle(Request request);
    }

    /// <summary>
    /// Handler which can process a request and return a response.
    /// </summary>
    /// <typeparam name="TRequest">The request to process <seealso cref="Agility.RequestResponse.Common.Request"/>.</typeparam>
    /// <typeparam name="TResponse">The response to return <seealso cref="Agility.RequestResponse.Common.Response"/>.</typeparam>
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response
    {
        /// <summary>
        /// Handles all requests and returns any response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Response Handle(Request request)
        {
            try
            {
                return Handle(request as TRequest);
            }
            catch (Exception e)
            {
                var response = Activator.CreateInstance<TResponse>();
                response.Errors.Add(e.Message);
                return response;
            }
        }

        /// <summary>
        /// Handles a request and returns a response.
        /// </summary>
        /// <param name="request">The request to process <seealso cref="Agility.RequestResponse.Common.Request"/>.</param>
        /// <returns>A response <seealso cref="Agility.RequestResponse.Common.Response"/>.</returns>
        protected abstract TResponse Handle(TRequest request);

        /// <summary>
        /// Creates a new instance for the given response.
        /// </summary>
        /// <returns>A new response <seealso cref="Agility.RequestResponse.Common.Response"/>.</returns>
        protected TResponse CreateResponse()
        {
            return Activator.CreateInstance<TResponse>();
        }
    }
}