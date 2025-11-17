namespace SmartStock.Responses
{
    /// <summary>
    /// Generic API response envelope that wraps a message and a typed payload.
    /// </summary>
    /// <typeparam name="T">Type of the response payload.</typeparam>
    public sealed class ApiResponse<T>
    {
        /// <summary>
        /// Message describing the result of the operation.
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// The data payload returned by the operation.
        /// </summary>
        public T Data { get; init; }
    }
}
