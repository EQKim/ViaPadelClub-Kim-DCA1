using System;
using System.Collections.Generic;
using ViaPadelClub_Kim_DCA1.Core.Domain.Common.Bases;

namespace ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Players.Values;

public sealed class PlayerId : ValueObject
{
    public Guid Value { get; }

    public PlayerId(Guid value)
    {
        Value = value;
    }

    public static PlayerId Create()
    {
        return new PlayerId(Guid.NewGuid());
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