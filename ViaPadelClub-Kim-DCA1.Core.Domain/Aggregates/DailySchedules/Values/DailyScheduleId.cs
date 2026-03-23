using System;
using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

public sealed class DailyScheduleId : ValueObject
{
    public Guid Value { get; }

    public DailyScheduleId(Guid value)
    {
        Value = value;
    }

    public static DailyScheduleId Create()
    {
        return new DailyScheduleId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}