using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

public sealed class UniversityName : ValueObject
{
    public string Value { get; }

    public UniversityName(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}