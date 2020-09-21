# AndcultureCode.CSharp.Sitefinity.Testing

.NET Core wrapper objects representing built-in Sitefinity module models and associated service files to interact and write integration tests to communicate with the Sitefinity OData API.  This should be used to create integration tests for built-in module content types, custom fields in those built-in module content types, and any dynamic modules built on top of Sitefinity.

NOTE: This is a rewrite of the Sitefinity CMS test framework found [here](https://github.com/Sitefinity/test-framework-core).  This repo was generated instead of using the Sitefinity repo for several reasons:

1. The Sitefinity repo uses .NET Framework (not .NET Core)
2. The Sitefinity repo was using less than desirable code for instance...
   - dictionaries in content models
   - no base test service
   - content models had name of service contained with it
3. The Sitefinity repo had problems with it
   - until recently, tests for publishing were failing
   - some of the tests were failing when asserting on successful creation of content
4. The Sitefinity repo wasn't a comprehensive suite of tests covering all of their built-in content types

This project is stand alone without any associated tests intentionally.  This is meant to serve as a base for generating your own project specific testing project.  

## Setup 

Before referencing this project, please ensure you have a Sitefinity instance readily available and an account set up in your Sitefinity instance.  See the **Installation** section found [here](https://github.com/Sitefinity/test-framework-core) for more details on the setup of the Sitefinity instance account.

Copy the contents of `appsettings.json.sample` and paste them into your test project's `appsettings.json` file.  Update the settings to match th account for your Sitefinity instance.

When generating your own project specific test suite using this as a dependency, you should consider having a *testing* environment configured which is available to run your tests against before continuing code changes through your CI/CD pipeline.

## Example

Below is an example of an integration test suite for the Blog Post Sitefinity service.

```csharp
using Andculture.Sitefinity.Testing.Extensions;
using Andculture.Sitefinity.Testing.Models.Content.Blogs;
using Andculture.Sitefinity.Testing.Services.Blogs;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace OData.Integration.Tests.Services.Blogs
{
    [Collection("OData Session Collection")]
    public class BlogPostServiceTests : ODataServiceTestsBase<BlogPostService, BlogPost>, IDisposable
    {
        #region Members

        BlogService _blogService;
        Blog _blog;

        #endregion Members

        #region Setup and Teardowns

        public BlogPostServiceTests(
            ODataSessionFixture fixture,
            ITestOutputHelper output) : base(fixture, output)
        {
            var session = fixture.Session;
            _blogService = new BlogService(session.ODataTestSettings, session.AccessToken);
            _blog = new Blog();
            Model = new BlogPost();
            Sut = new BlogPostService(session.ODataTestSettings, session.AccessToken);
        }

        public void Dispose()
        {
            if (Model.ID.HasValue && Sut.GetItem(Model.ID.Value).StatusCode == HttpStatusCode.OK)
            {
                Sut.Delete(Model.ID.Value);
            }

            if (_blog.ID.HasValue && _blogService.GetItem(_blog.ID.Value).StatusCode == HttpStatusCode.OK)
            {
                _blogService.Delete(_blog.ID.Value);
            }
        }

        #endregion Setup and Teardown

        #region Tests

        [Fact]
        public void CreateDraft_When_Data_Provided_Then_Returns_Created_Status_Code_With_Returned_Data_Object_With_Same_Data()
        {
            // Arrange & Act
            var responseModel = CreateDraft();

            // Assert
            responseModel.ShouldNotBeNull();
        }

        [Fact]
        public void Modify_When_Updating_Draft_Then_Returns_NoContent_Status_Code_And_Updates_Data_Object_With_Same_Data()
        {
            // Arrange
            CreateDraft();

            Model.Title += "Updated";
            Model.Description += "Updated";
            Model.Content += "Updated";
            Model.AllowComments = !Model.AllowComments;
            Model.Summary += "Updated";

            // Act
            var response = Sut.Modify(Model);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

            response = Sut.GetItem(Model.ID.Value);
            var responseModel = JsonConvert.DeserializeObject<BlogPost>(response.Content);
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
            var responseModel = CreateDraft();

            // Act
            var response = Sut.Delete(responseModel.ID.Value);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public void Publish_When_Publishing_Draft_Then_Returns_Ok_Status_Code_And_Published_Response()
        {
            // Arrange
            var responseModel = CreateDraft();

            // Act
            var response = Sut.Publish(responseModel.ID.Value);

            // Assert
            response.ShouldEqualPublishedContentResponse();
        }

        #endregion Tests

        #region Private Methods

        private BlogPost CreateDraft()
        {
            _blog.Title = "Title";
            var newBlogResponse = _blogService.CreateDraft(_blog);

            _blog = JsonConvert.DeserializeObject<Blog>(newBlogResponse.Content);

            Model.Title = "Title";
            Model.Description = "Description";
            Model.Content = "Content";
            Model.AllowComments = false;
            Model.Summary = "Summary";
            Model.ParentId = _blog.ID;

            var response = Sut.CreateDraft(Model);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            var responseModel = JsonConvert.DeserializeObject<BlogPost>(response.Content);
            Model.ID = responseModel.ID;

            responseModel.Title.ShouldBe(Model.Title);
            responseModel.Description.ShouldBe(Model.Description);
            responseModel.Content.ShouldBe(Model.Content);
            responseModel.AllowComments.ShouldBe(Model.AllowComments);
            responseModel.Summary.ShouldBe(Model.Summary);
            responseModel.ParentId.ShouldBe(Model.ParentId);

            return responseModel;
        }

        #endregion Private Methods
    }
}
