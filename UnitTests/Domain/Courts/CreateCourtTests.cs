using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Courts.Values;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Domain.Courts;

public class CreateCourtTests
{
    [Fact]
    public void Create_WithValidName_ShouldCreateCourt()
    {
        CourtId courtId = new CourtId(Guid.NewGuid());
        string courtName = "Court 1";

        Result<Court> result = Court.Create(courtId, courtName);

        Assert.True(result.IsSuccess);

        Court court = result.Value!;

        Assert.Equal(courtId, court.Id);
        Assert.Equal(courtName, court.CourtName);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldFail()
    {
        CourtId courtId = new CourtId(Guid.NewGuid());

        Result<Court> result = Court.Create(courtId, "");

        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "court.name.empty");
    }
}