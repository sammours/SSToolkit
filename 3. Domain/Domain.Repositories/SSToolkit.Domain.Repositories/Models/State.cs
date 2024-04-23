namespace SSToolkit.Domain.Repositories.Model
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;
    using SSToolkit.Fundamental.Extensions;

    [JsonObject(MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class State
    {
        [JsonProperty(nameof(CreatedBy))]
        public string? CreatedBy { get; private set; }

        [JsonProperty(nameof(CreatedDate))]
        public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.UtcNow;

        [JsonProperty(nameof(CreatedDescription))]
        public string? CreatedDescription { get; private set; }

        [JsonProperty(nameof(UpdatedBy))]
        public string? UpdatedBy { get; private set; }

        [JsonProperty(nameof(UpdatedDate))]
        public DateTimeOffset UpdatedDate { get; private set; } = DateTimeOffset.UtcNow;

        [JsonProperty(nameof(UpdatedDescription))]
        public string? UpdatedDescription { get; set; }

        [JsonProperty(nameof(UpdatedReasons))]
        public IEnumerable<string>? UpdatedReasons { get; private set; }

        [JsonProperty(nameof(Deleted))]
        public bool? Deleted { get; private set; }

        [JsonProperty(nameof(DeletedBy))]
        public string? DeletedBy { get; private set; }

        [JsonProperty(nameof(DeletedDate))]
        public DateTimeOffset? DeletedDate { get; private set; }

        [JsonProperty(nameof(DeletedReason))]
        public string? DeletedReason { get; private set; }

        [JsonProperty(nameof(DeletedDescription))]
        public string? DeletedDescription { get; set; }

        /// <summary>
        /// Get last action date
        /// </summary>
        public DateTimeOffset? LastActionDate =>
            new List<DateTimeOffset?> { this.CreatedDate, this.UpdatedDate, this.DeletedDate ?? default(DateTimeOffset?) }
            .Where(d => d != null).Safe().Max();

        /// <summary>
        /// Gets a value indicating whether determines whether this instance is deleted.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsDeleted() =>
            (this.Deleted != null && (bool)this.Deleted) || !this.DeletedReason.IsNullOrEmpty();

        /// <summary>
        /// Sets the created information.
        /// </summary>
        /// <param name="by">Name of the account of the creater.</param>
        /// <param name="description">The description for the creation.</param>
        public virtual void SetCreated(string? by = null, string? description = null)
        {
            this.CreatedDate = DateTimeOffset.UtcNow;
            if (!by.IsNullOrEmpty())
            {
                this.CreatedBy = by;
            }

            if (!description.IsNullOrEmpty())
            {
                this.CreatedDescription = description;
            }
        }

        /// <summary>
        /// Sets the updated information.
        /// </summary>
        /// <param name="by">Name of the account of the updater.</param>
        /// <param name="reason">The reason of the update.</param>
        public virtual void SetUpdated(string? by = null, string? reason = null)
        {
            this.UpdatedDate = DateTimeOffset.UtcNow;

            if (!by.IsNullOrEmpty())
            {
                this.UpdatedBy = by;
            }

            if (!reason.IsNullOrEmpty())
            {
                if (this.UpdatedReasons.IsNullOrEmpty())
                {
                    this.UpdatedReasons = new List<string>();
                }

                this.UpdatedReasons = this.UpdatedReasons?.Concat(new[]
                {
                    $"{by}: ({this.UpdatedDate.ToString(CultureInfo.InvariantCulture)}) {reason}".Trim()
                });
            }
        }

        /// <summary>
        /// Sets the deleted information.
        /// </summary>
        /// <param name="by">Name of the deleter.</param>
        /// <param name="reason">The reason.</param>
        public virtual void SetDeleted(string? by = null, string? reason = null)
        {
            this.Deleted = true;
            this.DeletedDate = DateTimeOffset.UtcNow;
            this.UpdatedDate = this.DeletedDate.Value;

            if (!by.IsNullOrEmpty())
            {
                this.DeletedBy = by;
            }

            if (!reason.IsNullOrEmpty())
            {
                this.DeletedReason = $"{by}: ({this.DeletedDate.Value.ToString(CultureInfo.InvariantCulture)}) {reason}".Trim();
            }
        }
    }
}
