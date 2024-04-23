namespace SSToolkit.Fundamental.Extensions
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static partial class ExtensionHelpers
    {
        /// <summary>
        /// Accept any awaiter used to await <see cref="Task"/>. Sets continueOnCapturedContext to false
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <returns>An object used to await this task.</returns>
        [DebuggerStepThrough]
        public static ConfiguredTaskAwaitable<TResult> AnyContext<TResult>(this Task<TResult> task)
        {
            return task.ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Accept any awaiter used to await <see cref="Task"/>. Sets continueOnCapturedContext to false
        /// </summary>
        /// <param name="task"></param>
        /// <returns>An object used to await this task.</returns>
        [DebuggerStepThrough]
        public static ConfiguredTaskAwaitable AnyContext(this Task task)
        {
            return task.ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
