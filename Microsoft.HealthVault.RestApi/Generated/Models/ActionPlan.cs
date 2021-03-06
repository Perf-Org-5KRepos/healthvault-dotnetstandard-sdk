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
    /// An action plan being created for a user
    /// </summary>
    public partial class ActionPlan
    {
        /// <summary>
        /// Initializes a new instance of the ActionPlan class.
        /// </summary>
        public ActionPlan()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ActionPlan class.
        /// </summary>
        /// <param name="imageUrl">An HTTPS URL to an image for the plan.
        /// Suggested resolution is 212x212 with a 25px margin in the
        /// image.</param>
        /// <param name="thumbnailImageUrl">An HTTPS URL to a thumbnail image
        /// for the plan. Suggested resolution is 212x212 with a 25px margin in
        /// the image.</param>
        /// <param name="category">The category of the plan. Possible values
        /// include: 'Unknown', 'Health', 'Sleep', 'Activity', 'Stress'</param>
        /// <param name="objectives">The Collection of objectives for the
        /// plan</param>
        /// <param name="name">The name of the plan, localized</param>
        /// <param name="description">The description of the plan,
        /// localized</param>
        /// <param name="associatedTasks">The Tasks associated with this
        /// plan</param>
        public ActionPlan(string imageUrl, string thumbnailImageUrl, string category, IList<Objective> objectives, string name = default(string), string description = default(string), IList<ActionPlanTask> associatedTasks = default(IList<ActionPlanTask>))
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            ThumbnailImageUrl = thumbnailImageUrl;
            Category = category;
            Objectives = objectives;
            AssociatedTasks = associatedTasks;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the name of the plan, localized
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the plan, localized
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets an HTTPS URL to an image for the plan. Suggested
        /// resolution is 212x212 with a 25px margin in the image.
        /// </summary>
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets an HTTPS URL to a thumbnail image for the plan.
        /// Suggested resolution is 212x212 with a 25px margin in the image.
        /// </summary>
        [JsonProperty(PropertyName = "thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the category of the plan. Possible values include:
        /// 'Unknown', 'Health', 'Sleep', 'Activity', 'Stress'
        /// </summary>
        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the Collection of objectives for the plan
        /// </summary>
        [JsonProperty(PropertyName = "objectives")]
        public IList<Objective> Objectives { get; set; }

        /// <summary>
        /// Gets or sets the Tasks associated with this plan
        /// </summary>
        [JsonProperty(PropertyName = "associatedTasks")]
        public IList<ActionPlanTask> AssociatedTasks { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ImageUrl == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ImageUrl");
            }
            if (ThumbnailImageUrl == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ThumbnailImageUrl");
            }
            if (Category == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Category");
            }
            if (Objectives == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Objectives");
            }
            if (Objectives != null)
            {
                foreach (var element in Objectives)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (AssociatedTasks != null)
            {
                foreach (var element1 in AssociatedTasks)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
        }
    }
}
