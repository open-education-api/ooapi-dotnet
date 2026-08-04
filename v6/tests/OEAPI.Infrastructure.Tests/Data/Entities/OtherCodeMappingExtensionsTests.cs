using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.Infrastructure.Tests.Data.Entities;

public class OtherCodeMappingExtensionsTests
{
    [Fact]
    public void ToApiModel_EmptyCollection_ReturnsNull()
    {
        ICollection<OtherCodeEntity> otherCodes = [];

        Assert.Null(otherCodes.ToApiModel());
    }

    [Fact]
    public void ToApiModel_NonEmptyCollection_MapsEveryEntry()
    {
        ICollection<OtherCodeEntity> otherCodes =
        [
            new() { CodeType = "system_id", Code = "R123" },
            new() { CodeType = "institution_code", Code = "B456" }
        ];

        IdentifierEntry[]? result = otherCodes.ToApiModel();

        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.Contains(result, e => e.CodeType == "system_id" && e.Code == "R123");
        Assert.Contains(result, e => e.CodeType == "institution_code" && e.Code == "B456");
    }

    [Fact]
    public void SyncFrom_NullModel_LeavesCollectionUntouched()
    {
        ICollection<OtherCodeEntity> otherCodes = [new() { CodeType = "system_id", Code = "R123" }];

        otherCodes.SyncFrom(null);

        Assert.Single(otherCodes);
    }

    [Fact]
    public void SyncFrom_NonNullModel_ClearsAndReplacesExistingEntries()
    {
        ICollection<OtherCodeEntity> otherCodes = [new() { CodeType = "identifier", Code = "OLD1" }];

        otherCodes.SyncFrom([new IdentifierEntry { CodeType = "system_id", Code = "R123" }]);

        OtherCodeEntity entry = Assert.Single(otherCodes);
        Assert.Equal("system_id", entry.CodeType);
        Assert.Equal("R123", entry.Code);
    }

    [Fact]
    public void SyncFrom_EmptyArray_ClearsExistingEntries()
    {
        ICollection<OtherCodeEntity> otherCodes = [new() { CodeType = "identifier", Code = "OLD1" }];

        otherCodes.SyncFrom([]);

        Assert.Empty(otherCodes);
    }
}
