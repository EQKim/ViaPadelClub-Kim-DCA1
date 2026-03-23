using System;
using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;

public sealed class TimeRange : ValueObject
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public TimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}