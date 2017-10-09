// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.HealthVault.RestApi.Generated
{
    using Microsoft.HealthVault;
    using Microsoft.HealthVault.RestApi;
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for ActionPlanTasks.
    /// </summary>
    public static partial class ActionPlanTasksExtensions
    {
            /// <summary>
            /// Get a task by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTaskId'>
            /// The unique identifer of the task.
            /// </param>
            public static ActionPlanTaskInstance GetById(this IActionPlanTasks operations, System.Guid actionPlanTaskId)
            {
                return operations.GetByIdAsync(actionPlanTaskId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get a task by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTaskId'>
            /// The unique identifer of the task.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ActionPlanTaskInstance> GetByIdAsync(this IActionPlanTasks operations, System.Guid actionPlanTaskId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.GetByIdWithHttpMessagesAsync(actionPlanTaskId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Delete a task by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTaskId'>
            /// The unique identifer of the task.
            /// </param>
            public static void Delete(this IActionPlanTasks operations, System.Guid actionPlanTaskId)
            {
                operations.DeleteAsync(actionPlanTaskId).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete a task by id
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTaskId'>
            /// The unique identifer of the task.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteAsync(this IActionPlanTasks operations, System.Guid actionPlanTaskId, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                (await operations.DeleteWithHttpMessagesAsync(actionPlanTaskId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Get a collection of task definitions
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTaskStatus'>
            /// An optional status used to filter the results. Possible values include:
            /// 'Unknown', 'Archived', 'InProgress', 'Recommended', 'Completed', 'Template'
            /// </param>
            public static ActionPlanTasksResponseActionPlanTaskInstance Get(this IActionPlanTasks operations, string actionPlanTaskStatus = default(string))
            {
                return operations.GetAsync(actionPlanTaskStatus).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get a collection of task definitions
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTaskStatus'>
            /// An optional status used to filter the results. Possible values include:
            /// 'Unknown', 'Archived', 'InProgress', 'Recommended', 'Completed', 'Template'
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ActionPlanTasksResponseActionPlanTaskInstance> GetAsync(this IActionPlanTasks operations, string actionPlanTaskStatus = default(string), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(actionPlanTaskStatus, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Put an update for an action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTask'>
            /// The updated action plan task.
            /// </param>
            public static ActionPlanTaskInstance Replace(this IActionPlanTasks operations, ActionPlanTaskInstance actionPlanTask)
            {
                return operations.ReplaceAsync(actionPlanTask).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Put an update for an action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTask'>
            /// The updated action plan task.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ActionPlanTaskInstance> ReplaceAsync(this IActionPlanTasks operations, ActionPlanTaskInstance actionPlanTask, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ReplaceWithHttpMessagesAsync(actionPlanTask, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Post a new action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTask'>
            /// The action plan task to create.
            /// </param>
            public static ActionPlanTaskInstance Create(this IActionPlanTasks operations, ActionPlanTask actionPlanTask)
            {
                return operations.CreateAsync(actionPlanTask).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Post a new action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTask'>
            /// The action plan task to create.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ActionPlanTaskInstance> CreateAsync(this IActionPlanTasks operations, ActionPlanTask actionPlanTask, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.CreateWithHttpMessagesAsync(actionPlanTask, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Patch an update for an action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTask'>
            /// The updated task
            /// </param>
            public static ActionPlanTaskInstance Update(this IActionPlanTasks operations, ActionPlanTaskInstance actionPlanTask)
            {
                return operations.UpdateAsync(actionPlanTask).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Patch an update for an action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='actionPlanTask'>
            /// The updated task
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ActionPlanTaskInstance> UpdateAsync(this IActionPlanTasks operations, ActionPlanTaskInstance actionPlanTask, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.UpdateWithHttpMessagesAsync(actionPlanTask, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Validate tracking for an action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='trackingValidation'>
            /// The tracking validation information.
            /// </param>
            public static ActionPlanTaskTrackingResponseActionPlanTaskTracking ValidateTracking(this IActionPlanTasks operations, TrackingValidation trackingValidation)
            {
                return operations.ValidateTrackingAsync(trackingValidation).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Validate tracking for an action plan task
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='trackingValidation'>
            /// The tracking validation information.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<ActionPlanTaskTrackingResponseActionPlanTaskTracking> ValidateTrackingAsync(this IActionPlanTasks operations, TrackingValidation trackingValidation, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ValidateTrackingWithHttpMessagesAsync(trackingValidation, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
