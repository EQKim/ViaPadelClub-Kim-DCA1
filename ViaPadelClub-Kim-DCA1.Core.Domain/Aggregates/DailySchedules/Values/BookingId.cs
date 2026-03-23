using System;
using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

public sealed class BookingId : ValueObject
{
    public Guid Value { get; }

    public BookingId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}