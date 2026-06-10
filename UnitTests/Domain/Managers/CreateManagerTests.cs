using System;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.DailySchedules.Values;
using ViaPadelClub_Kim_DCA1.Core.Domain.Aggregates.Managers;
using ViaPadelClub_Kim_DCA1.Core.Tools.OperationResult;
using Xunit;

namespace UnitTests.Domain.Managers;

public class CreateManagerTests
{
    [Fact]
    public void Create_WithValidCompanyName_ShouldCreateManager()
    {
        ManagerId managerId = new ManagerId(Guid.NewGuid());
        string companyName = "Via Padel Club";

        Result<Manager> result = Manager.Create(managerId, companyName);

        Assert.True(result.IsSuccess);

        Manager manager = result.Value!;

        Assert.Equal(managerId, manager.Id);
        Assert.Equal(companyName, manager.PadelCompanyName);
    }

    [Fact]
    public void Create_WithEmptyCompanyName_ShouldFail()
    {
        ManagerId managerId = new ManagerId(Guid.NewGuid());

        Result<Manager> result = Manager.Create(managerId, "");

        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, error => error.Code == "manager.padel_company_name.empty");
    }
}