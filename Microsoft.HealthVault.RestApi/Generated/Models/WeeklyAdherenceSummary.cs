// Code generated by Microsoft (R) AutoRest Code Generator 1.0.1.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.HealthVault.RestApi.Generated.Models
{
    using Microsoft.HealthVault;
    using Microsoft.HealthVault.RestApi;
    using Microsoft.HealthVault.RestApi.Generated;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A summary of a tasks weekly adherence
    /// </summary>
    public partial class WeeklyAdherenceSummary
    {
        /// <summary>
        /// Initializes a new instance of the WeeklyAdherenceSummary class.
        /// </summary>
        public WeeklyAdherenceSummary()
        {
          CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the WeeklyAdherenceSummary class.
        /// </summary>
        /// <param name="weekStart">The start of the week</param>
        /// <param name="completions">The # of completions that occurred this
        /// week</param>
        /// <param name="intendedCompletions">The # of completions that were
        /// intended for this week</param>
        /// <param name="intendedOccurrences">The # of occurrences that were
        /// intended for this week</param>
        /// <param name="manualTrackedOccurrences">The # of occurrences that
        /// were manually tracked this week</param>
        /// <param name="automaticTrackedOccurrences">The # of occurrences that
        /// were automatically tracked this week</param>
        /// <param name="automaticTrackedOccurrenceEvidence">A list of evidence
        /// (HealthVault Thing IDs) that were automatically tracked</param>
        public WeeklyAdherenceSummary(System.DateTime? weekStart = default(System.DateTime?), int? completions = default(int?), int? intendedCompletions = default(int?), int? intendedOccurrences = default(int?), int? manualTrackedOccurrences = default(int?), int? automaticTrackedOccurrences = default(int?), IList<string> automaticTrackedOccurrenceEvidence = default(IList<string>))
        {
            WeekStart = weekStart;
            Completions = completions;
            IntendedCompletions = intendedCompletions;
            IntendedOccurrences = intendedOccurrences;
            ManualTrackedOccurrences = manualTrackedOccurrences;
            AutomaticTrackedOccurrences = automaticTrackedOccurrences;
            AutomaticTrackedOccurrenceEvidence = automaticTrackedOccurrenceEvidence;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the start of the week
        /// </summary>
        [JsonProperty(PropertyName = "weekStart")]
        public System.DateTime? WeekStart { get; set; }

        /// <summary>
        /// Gets or sets the # of completions that occurred this week
        /// </summary>
        [JsonProperty(PropertyName = "completions")]
        public int? Completions { get; set; }

        /// <summary>
        /// Gets or sets the # of completions that were intended for this week
        /// </summary>
        [JsonProperty(PropertyName = "intendedCompletions")]
        public int? IntendedCompletions { get; set; }

        /// <summary>
        /// Gets or sets the # of occurrences that were intended for this week
        /// </summary>
        [JsonProperty(PropertyName = "intendedOccurrences")]
        public int? IntendedOccurrences { get; set; }

        /// <summary>
        /// Gets or sets the # of occurrences that were manually tracked this
        /// week
        /// </summary>
        [JsonProperty(PropertyName = "manualTrackedOccurrences")]
        public int? ManualTrackedOccurrences { get; set; }

        /// <summary>
        /// Gets or sets the # of occurrences that were automatically tracked
        /// this week
        /// </summary>
        [JsonProperty(PropertyName = "automaticTrackedOccurrences")]
        public int? AutomaticTrackedOccurrences { get; set; }

        /// <summary>
        /// Gets or sets a list of evidence (HealthVault Thing IDs) that were
        /// automatically tracked
        /// </summary>
        [JsonProperty(PropertyName = "automaticTrackedOccurrenceEvidence")]
        public IList<string> AutomaticTrackedOccurrenceEvidence { get; set; }

    }
}
