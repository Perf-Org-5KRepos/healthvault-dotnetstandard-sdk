// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.HealthVault.RestApi.Generated.Models
{
    using Microsoft.HealthVault;
    using Microsoft.HealthVault.RestApi;
    using Microsoft.HealthVault.RestApi.Generated;
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An instance of an action a user should complete.
    /// Adherence to a plan is measured by completion statistics against tasks
    /// </summary>
    public partial class ActionPlanTaskInstance
    {
        /// <summary>
        /// Initializes a new instance of the ActionPlanTaskInstance class.
        /// </summary>
        public ActionPlanTaskInstance()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ActionPlanTaskInstance class.
        /// </summary>
        /// <param name="name">The friendly name of the task</param>
        /// <param name="shortDescription">The short description of the
        /// task</param>
        /// <param name="longDescription">The detailed description of the
        /// task</param>
        /// <param name="imageUrl">The image URL of the task. Suggested
        /// resolution is 200 x 200</param>
        /// <param name="thumbnailImageUrl">The thumbnail image URL of the
        /// task. Suggested resolution is 90 x 90</param>
        /// <param name="taskType">The type of the task, used to choose the UI
        /// editor for the task. Possible values include: 'Unknown',
        /// 'BloodPressure', 'Other'</param>
        /// <param name="trackingPolicy">The tracking policy</param>
        /// <param name="associatedPlanId">The ID of the associated plan. This
        /// is not needed when adding a task as part of a new plan</param>
        /// <param name="associatedObjectiveIds">The list of objective IDs the
        /// task is associated with</param>
        /// <param name="completionType">The Completion Type of the Task.
        /// Possible values include: 'Unknown', 'Frequency',
        /// 'Scheduled'</param>
        /// <param name="id">The Id of the task instance</param>
        /// <param name="status">The status of the task. Possible values
        /// include: 'Unknown', 'Archived', 'InProgress', 'Recommended',
        /// 'Completed', 'Template'</param>
        /// <param name="startDate">The date that the task was started.
        /// Read-only</param>
        /// <param name="endDate">The date that the task was ended.
        /// Read-only</param>
        /// <param name="organizationId">The ID of the organization that owns
        /// this task. Read-only</param>
        /// <param name="organizationName">The name of the organization that
        /// owns this task. Read-only</param>
        /// <param name="taskKey">The task key a provider sets and maintains
        /// for a user's created task</param>
        /// <param name="signupName">The text shown during task signup.</param>
        /// <param name="frequencyTaskCompletionMetrics">Completion metrics for
        /// frequency based tasks</param>
        /// <param name="schedules">Schedules for when a task should be
        /// completed.</param>
        public ActionPlanTaskInstance(string name, string shortDescription, string longDescription, string imageUrl, string thumbnailImageUrl, string taskType, ActionPlanTrackingPolicy trackingPolicy, System.Guid associatedPlanId, IList<System.Guid?> associatedObjectiveIds, string completionType, System.Guid? id = default(System.Guid?), string status = default(string), System.DateTime? startDate = default(System.DateTime?), System.DateTime? endDate = default(System.DateTime?), string organizationId = default(string), string organizationName = default(string), System.Guid? taskKey = default(System.Guid?), string signupName = default(string), ActionPlanFrequencyTaskCompletionMetrics frequencyTaskCompletionMetrics = default(ActionPlanFrequencyTaskCompletionMetrics), IList<Schedule> schedules = default(IList<Schedule>))
        {
            Id = id;
            Status = status;
            StartDate = startDate;
            EndDate = endDate;
            OrganizationId = organizationId;
            OrganizationName = organizationName;
            TaskKey = taskKey;
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            ImageUrl = imageUrl;
            ThumbnailImageUrl = thumbnailImageUrl;
            TaskType = taskType;
            TrackingPolicy = trackingPolicy;
            SignupName = signupName;
            AssociatedPlanId = associatedPlanId;
            AssociatedObjectiveIds = associatedObjectiveIds;
            CompletionType = completionType;
            FrequencyTaskCompletionMetrics = frequencyTaskCompletionMetrics;
            Schedules = schedules;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the Id of the task instance
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public System.Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the status of the task. Possible values include:
        /// 'Unknown', 'Archived', 'InProgress', 'Recommended', 'Completed',
        /// 'Template'
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the date that the task was started. Read-only
        /// </summary>
        [JsonProperty(PropertyName = "startDate")]
        public System.DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date that the task was ended. Read-only
        /// </summary>
        [JsonProperty(PropertyName = "endDate")]
        public System.DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the ID of the organization that owns this task.
        /// Read-only
        /// </summary>
        [JsonProperty(PropertyName = "organizationId")]
        public string OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the organization that owns this task.
        /// Read-only
        /// </summary>
        [JsonProperty(PropertyName = "organizationName")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the task key a provider sets and maintains for a
        /// user's created task
        /// </summary>
        [JsonProperty(PropertyName = "taskKey")]
        public System.Guid? TaskKey { get; set; }

        /// <summary>
        /// Gets or sets the friendly name of the task
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short description of the task
        /// </summary>
        [JsonProperty(PropertyName = "shortDescription")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the task
        /// </summary>
        [JsonProperty(PropertyName = "longDescription")]
        public string LongDescription { get; set; }

        /// <summary>
        /// Gets or sets the image URL of the task. Suggested resolution is 200
        /// x 200
        /// </summary>
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail image URL of the task. Suggested
        /// resolution is 90 x 90
        /// </summary>
        [JsonProperty(PropertyName = "thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the type of the task, used to choose the UI editor for
        /// the task. Possible values include: 'Unknown', 'BloodPressure',
        /// 'Other'
        /// </summary>
        [JsonProperty(PropertyName = "taskType")]
        public string TaskType { get; set; }

        /// <summary>
        /// Gets or sets the tracking policy
        /// </summary>
        [JsonProperty(PropertyName = "trackingPolicy")]
        public ActionPlanTrackingPolicy TrackingPolicy { get; set; }

        /// <summary>
        /// Gets or sets the text shown during task signup.
        /// </summary>
        [JsonProperty(PropertyName = "signupName")]
        public string SignupName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated plan. This is not needed when
        /// adding a task as part of a new plan
        /// </summary>
        [JsonProperty(PropertyName = "associatedPlanId")]
        public System.Guid AssociatedPlanId { get; set; }

        /// <summary>
        /// Gets or sets the list of objective IDs the task is associated with
        /// </summary>
        [JsonProperty(PropertyName = "associatedObjectiveIds")]
        public IList<System.Guid?> AssociatedObjectiveIds { get; set; }

        /// <summary>
        /// Gets or sets the Completion Type of the Task. Possible values
        /// include: 'Unknown', 'Frequency', 'Scheduled'
        /// </summary>
        [JsonProperty(PropertyName = "completionType")]
        public string CompletionType { get; set; }

        /// <summary>
        /// Gets or sets completion metrics for frequency based tasks
        /// </summary>
        [JsonProperty(PropertyName = "frequencyTaskCompletionMetrics")]
        public ActionPlanFrequencyTaskCompletionMetrics FrequencyTaskCompletionMetrics { get; set; }

        /// <summary>
        /// Gets or sets schedules for when a task should be completed.
        /// </summary>
        [JsonProperty(PropertyName = "schedules")]
        public IList<Schedule> Schedules { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
            if (ShortDescription == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ShortDescription");
            }
            if (LongDescription == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "LongDescription");
            }
            if (ImageUrl == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ImageUrl");
            }
            if (ThumbnailImageUrl == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ThumbnailImageUrl");
            }
            if (TaskType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TaskType");
            }
            if (TrackingPolicy == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TrackingPolicy");
            }
            if (AssociatedObjectiveIds == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "AssociatedObjectiveIds");
            }
            if (CompletionType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "CompletionType");
            }
            if (Schedules != null)
            {
                foreach (var element in Schedules)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}
