using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Abstractions
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public TKey Id { get; init; } = default!;

        protected BaseEntity() { }

        protected BaseEntity(TKey id)
        {
            Id = id;
        }
    }

    public abstract class AuditableEntity<TKey> : BaseEntity<TKey>, IAuditable, ISoftDelete
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
