using Microsoft.AspNetCore.Mvc;
using ooapi.v5.Controllers;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;
using ooapi.v5.Models.Params;

namespace ooapi.v5.UnitTests.Controllers;

public class NewsControllerTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void NewsFeedsGet_Success_ReturnsNewsFeeds()
    {
        //arrange
        var sut = CreateSut(out var newsFeedsService, out var _);
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var newsFeedType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<NewsFeed>();

        DataRequestParameters? dataRequestParameters = null;

        newsFeedsService.GetAll(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), out var errorResponse).Returns(response);

        //act
        var result = sut.NewsFeedsGet(filterParams, pagingParams, newsFeedType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var newsFeeds = result.Value as Pagination<NewsFeed>;
        newsFeeds.Should().NotBeNull();
        newsFeeds.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void NewsFeedsNewsFeedIdGet_Success_ReturnsNewsFeed()
    {
        //arrange
        var sut = CreateSut(out var newsFeedsService, out var _);
        var newsFeedId = _fixture.Create<Guid>();

        var response = new NewsFeed();

        newsFeedsService.Get(newsFeedId, out var errorResponse).Returns(response);

        //act
        var result = sut.NewsFeedsNewsFeedIdGet(newsFeedId) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var newsFeed = result.Value as NewsFeed;
        newsFeed.Should().NotBeNull();
        newsFeed.Should().Be(response);
    }

    [Test]
    public void NewsFeedsNewsFeedIdNewsItemsGet_Success_ReturnsNewsItems()
    {
        //arrange
        var sut = CreateSut(out var _, out var newsItemsService);
        var newsFeedId = _fixture.Create<Guid>();
        var filterParams = _fixture.Create<FilterParams>();
        var pagingParams = _fixture.Create<PagingParams>();
        var newsFeedType = _fixture.Create<string?>();
        var sort = _fixture.Create<string?>();

        var response = new Pagination<NewsItem>();

        DataRequestParameters? dataRequestParameters = null;

        newsItemsService.GetNewsItemsByNewsFeedId(Arg.Do<DataRequestParameters>(x => dataRequestParameters = x), newsFeedId, out var errorResponse).Returns(response);

        //act
        var result = sut.NewsFeedsNewsFeedIdNewsItemsGet(newsFeedId, filterParams, pagingParams, newsFeedType, sort) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var newsItems = result.Value as Pagination<NewsItem>;
        newsItems.Should().NotBeNull();
        newsItems.Should().Be(response);

        dataRequestParameters.Should().NotBeNull();
        dataRequestParameters!.SearchTerm.Should().Be(filterParams.q);
        dataRequestParameters.Consumer.Should().Be(filterParams.consumer);
        dataRequestParameters.PageNumber.Should().Be(pagingParams.PageNumber);
    }

    [Test]
    public void NewsItemsNewsItemIdGet_Success_ReturnsNewsItem()
    {
        //arrange
        var sut = CreateSut(out var _, out var newsItemsService);
        var newsItemId = _fixture.Create<Guid>();
        var expand = _fixture.Create<List<string>>();

        var response = new NewsItem();

        newsItemsService.Get(newsItemId, out var errorResponse).Returns(response);

        //act
        var result = sut.NewsItemsNewsItemIdGet(newsItemId, expand) as OkObjectResult;

        //assert
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);

        errorResponse.Should().BeNull();

        var newsItem = result.Value as NewsItem;
        newsItem.Should().NotBeNull();
        newsItem.Should().Be(response);
    }

    private static NewsController CreateSut(out INewsFeedsService newsFeedsService, out INewsItemsService newsItemsService)
    {
        newsFeedsService = Substitute.For<INewsFeedsService>();
        newsItemsService = Substitute.For<INewsItemsService>();
        return new NewsController(newsFeedsService, newsItemsService);
    }
}