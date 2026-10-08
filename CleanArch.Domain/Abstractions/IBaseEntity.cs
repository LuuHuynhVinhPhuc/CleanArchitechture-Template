using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Abstractions
{
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; init; }
    }

    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
    }

    public interface IAuditable
    {
        DateTimeOffset CreatedAt { get; set; }
        string? CreatedBy { get; set; }
        DateTimeOffset? LastModifiedAt { get; set; }
        string? LastModifiedBy { get; set; }
    }
}
