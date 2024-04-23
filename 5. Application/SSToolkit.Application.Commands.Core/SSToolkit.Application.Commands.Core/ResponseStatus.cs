namespace SSToolkit.Application.Commands.Core
{
    public enum ResponseStatus
    {
        /// <summary>
        /// Finished
        /// </summary>
        Completed = 0,

        /// <summary>
        /// Completed with errors
        /// </summary>
        CompletedWithErrors = 1,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 2
    }
}
