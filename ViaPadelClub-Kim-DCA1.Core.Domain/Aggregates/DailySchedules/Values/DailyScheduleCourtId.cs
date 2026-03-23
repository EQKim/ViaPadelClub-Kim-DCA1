using System;
using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

public sealed class DailyScheduleCourtId : ValueObject
{
    public Guid Value { get; }

    public DailyScheduleCourtId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}