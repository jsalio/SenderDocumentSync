using System.Collections.Generic;

namespace DocumentSender.RestManagers
{
    /// <summary>
    /// Responsible of Send request.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRestRequestResolver<T>
    {
        /// <summary>
        /// Validate that if information on request are valid
        /// </summary>
        /// <returns>A <see cref="BasicOperationResponse{T}"/></returns>
        BasicOperationResponse<T> ValidateResolver();

        /// <summary>
        /// Send the request using some mechanism of sending HTTP.
        /// </summary>
        /// <returns></returns>
        CaptureClientRestResponse SendRequest();

        /// <summary>
        /// Add headers parameters in current request
        /// </summary>
        /// <param name="headerPair"> A instance of  <see cref="KeyValuePair{TKey,TValue}"/></param>
        void AddHeaders(KeyValuePair<string, string> headerPair);

        /// <summary>
        /// Indicate that current request required body or not
        /// </summary>
        /// <param name="requiredBody">A <see cref="bool"/></param>
        void RequiredBody(bool requiredBody);

        /// <summary>
        /// Add Authentication on request
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void AddAuthentication(string username, string password);
    }
}