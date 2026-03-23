using System;
using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;
namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
public sealed class ManagerId : ValueObject
{
    public Guid Value { get; }

    public ManagerId(Guid value)
    {
        Value = value;
    }

    public static ManagerId Create()
    {
        return new ManagerId(Guid.NewGuid());
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