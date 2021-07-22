# AndcultureCode.CSharp.Sitefinity.Testing

![build status](https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/actions/workflows/build.yaml/badge.svg)<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-3-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

Base classes, utilities, and extensions to facilitate writing tests for Sitefinity.

## Sitefinity Version Support

- Compatibility: `13+`
- Tested with: `13.3 (build 7625)`

## Example

Below is an example of an integration test suite for the Blog Post Sitefinity service.

```csharp
using AndcultureCode.CSharp.Sitefinity.Testing;
using AndcultureCode.CSharp.Sitefinity.Testing.Extensions;
using OData.Services.Models.Blogs;
using OData.Services.Services.Blogs;
using Shouldly;
using System;
using Xunit;
using Xunit.Abstractions;

namespace OData.Services.Tests.Services.Blogs
{
    [Collection("OData Session Collection")]
    public class BlogPostODataServicesTests : ODataServiceTestsBase<BlogPostODataServices, BlogPostDto>, IDisposable
    {
        #region Members

        private BlogODataServices _blogService;
        private BlogDto _blog;

        #endregion Members

        #region Setup and Teardowns

        public BlogPostODataServicesTests(
            ODataSessionFixture fixture,
            ITestOutputHelper output) : base(fixture, output)
        {
            var session = fixture.Session;
            _blogService = new BlogODataServices(session.ODataConnectionSettings, session);
            _blog = new BlogDto();
            Model = new BlogPostDto();
            Sut = new BlogPostODataServices(session.ODataConnectionSettings, session);
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeOfExistingModel(_blogService, _blog);
        }

        #endregion Setup and Teardown

        #region Tests

        [Fact]
        public void CreateDraft_When_Data_Provided_Then_Returns_Created_Status_Code_With_Returned_Data_Object_With_Same_Data()
        {
            // Arrange & Act
            var responseModel = CreateDraft("CreateDraft");

            // Assert
            responseModel.ShouldNotBeNull();
        }

        [Fact]
        public void Modify_When_Updating_Draft_Then_Returns_NoContent_Status_Code_And_Updates_Data_Object_With_Same_Data()
        {
            // Arrange
            CreateDraft("Modify");

            Model.Title += "Updated";
            Model.Description += "Updated";
            Model.Content += "Updated";
            Model.AllowComments = !Model.AllowComments;
            Model.Summary += "Updated";

            // Act
            var response = Sut.Modify(Model);

            // Assert
            response.ShouldBeExpectedStatusCode();

            response = Sut.GetItem(Model.Id.Value);
            var responseModel = response.ResultObject;
            responseModel.Title.ShouldBe(Model.Title);
            responseModel.Description.ShouldBe(Model.Description);
            responseModel.Content.ShouldBe(Model.Content);
            responseModel.AllowComments.ShouldBe(Model.AllowComments);
            responseModel.Summary.ShouldBe(Model.Summary);
        }

        [Fact]
        public void Delete_When_Deleting_Draft_Then_Returns_NoContent_Status_Code()
        {
            // Arrange
            var responseModel = CreateDraft("Delete");

            // Act
            var response = Sut.Delete(responseModel.Id.Value);

            // Assert
            response.ShouldBeExpectedStatusCode();
        }

        [Fact]
        public void Publish_When_Publishing_Draft_Then_Returns_Ok_Status_Code_And_Published_Response()
        {
            // Arrange
            var responseModel = CreateDraft("Publish");

            // Act
            var response = Sut.Publish(responseModel.Id.Value);

            // Assert
            response.RestResponse.ShouldEqualPublishedContentResponse();
        }

        [Fact]
        public void GetItem_When_Retrieving_Item_Then_Returns_Ok_Status_Code_And_Item()
        {
            // Arrange
            var responseModel = CreateDraft("GetItem");

            // Act
            var response = Sut.GetItem(responseModel.Id.Value);

            // Assert
            response.ShouldBeExpectedStatusCode();
            response.ResultObject.Id.ShouldBe(responseModel.Id);
        }

        [Fact]
        public void GetCount_When_Retrieving_Count_Then_Returns_Ok_Status_Code_And_Count()
        {
            // Arrange
            CreateDraft("GetCount");

            // Act
            var response = Sut.GetCount();

            // Assert
            response.ShouldBeExpectedStatusCode();
            response.ResultObject.ShouldBeGreaterThan(0);
        }

        [Fact]
        public void Get_When_Retrieving_Items_Then_Returns_Ok_Status_Code_And_Items()
        {
            // Arrange
            CreateDraft("Get");

            // Act
            var response = Sut.Get();

            // Assert
            response.ShouldBeExpectedStatusCode();
            response.ResultObject.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        #endregion Tests

        #region Overrides

        protected BlogPostDto CreateDraft(string methodBeingTested)
        {
            _blog.Title = GetRandomString("Title");
            var newBlogResponse = _blogService.CreateDraft(_blog);

            _blog = newBlogResponse.ResultObject;

            Model.Title = GetRandomString("Title");
            Model.Description = GetRandomString("Description");
            Model.Content = GetRandomString("Content");
            Model.AllowComments = false;
            Model.Summary = GetRandomString("Summary");
            Model.ParentId = _blog.Id;

            var response = Sut.CreateDraft(Model);
            response.ShouldBeExpectedStatusCode();

            var responseModel = response.ResultObject;
            Model.Id = responseModel.Id;

            responseModel.Title.ShouldBe(Model.Title);
            responseModel.Description.ShouldBe(Model.Description);
            responseModel.Content.ShouldBe(Model.Content);
            responseModel.AllowComments.ShouldBe(Model.AllowComments);
            responseModel.Summary.ShouldBe(Model.Summary);
            responseModel.ParentId.ShouldBe(Model.ParentId);

            return responseModel;
        }

        #endregion Overrides
    }
}
```

## Contributors ✨

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://github.com/KevinBusch"><img src="https://avatars.githubusercontent.com/u/775414?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Kevin Busch</b></sub></a><br /><a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/commits?author=KevinBusch" title="Code">💻</a> <a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/pulls?q=is%3Apr+reviewed-by%3AKevinBusch" title="Reviewed Pull Requests">👀</a> <a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/commits?author=KevinBusch" title="Tests">⚠️</a></td>
    <td align="center"><a href="https://github.com/cspath1"><img src="https://avatars.githubusercontent.com/u/26265706?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Cody Spath</b></sub></a><br /><a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/pulls?q=is%3Apr+reviewed-by%3Acspath1" title="Reviewed Pull Requests">👀</a> <a href="#ideas-cspath1" title="Ideas, Planning, & Feedback">🤔</a></td>
    <td align="center"><a href="https://github.com/Stefanie899"><img src="https://avatars.githubusercontent.com/u/37462028?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Stefanie Leitch</b></sub></a><br /><a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/commits?author=Stefanie899" title="Code">💻</a> <a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/commits?author=Stefanie899" title="Tests">⚠️</a> <a href="https://github.com/AndcultureCode/AndcultureCode.CSharp.Sitefinity.Testing/pulls?q=is%3Apr+reviewed-by%3AStefanie899" title="Reviewed Pull Requests">👀</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
