using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessLogic.ModelsDto;
using NUnit.Framework;
using PublicApi;

namespace PublicApiIntegrationTests.CrudEndpointsTests
{
    public class HomeWorkCrudTests
    {
        private WebApplicationTestFactory<Startup> _factory;
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void MakeWebAppAndHttpClientFromTestFactory()
        {
            _factory = new WebApplicationTestFactory<Startup>();
            _httpClient = _factory.CreateDefaultClient();
        }

        [Test]
        public async Task GetReturnsAllHomeWorks()
        {
            var getHomeWorksResponse = await _httpClient.GetAsync("/HomeWorkCrud");
            getHomeWorksResponse.EnsureSuccessStatusCode();

            var homeWorksResultContent = getHomeWorksResponse.Content;
            var apiResponse = await homeWorksResultContent.ReadFromJsonAsync<ApiResponse<IEnumerable<HomeWorkDto>>>();

            Assert.That(apiResponse.Data.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetWithIdReturnsHomeWork()
        {
            var getHomeWorkWithIdResponse = await _httpClient.GetAsync("/HomeWorkCrud/1");
            getHomeWorkWithIdResponse.EnsureSuccessStatusCode();

            var homeWorkResultContent = getHomeWorkWithIdResponse.Content;
            var apiResponse = await homeWorkResultContent.ReadFromJsonAsync<ApiResponse<HomeWorkDto>>();

            Assert.That(apiResponse.Data, Is.Not.Null);
        }

        [Test]
        public async Task GetWithNonExistentIdReturnsApiResponseWithNotFoundStatusCode()
        {
            var getHomeWorkWithNonExistentIdResponse = await _httpClient.GetAsync("/HomeWorkCrud/100");

            var homeWorkResultContent = getHomeWorkWithNonExistentIdResponse.Content;
            var apiResponse = await homeWorkResultContent.ReadFromJsonAsync<ApiResponse<object>>();
            var statusCode = getHomeWorkWithNonExistentIdResponse.StatusCode;

            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(apiResponse.Succeed, Is.EqualTo(false));            
        }

        [Test]
        public async Task PostCreatesHomeWorkReturnsSuccessApiResponse()
        {
            HomeWorkDto newTestHomeWork = new() {
                StudentId = 1,
                SubjectId = 3,
                Grade = 4
            };

            var postHomeWorkResponse = await _httpClient.PostAsJsonAsync("/HomeWorkCrud", newTestHomeWork);
            postHomeWorkResponse.EnsureSuccessStatusCode();
            var postResult = postHomeWorkResponse.Content;
            var apiResponse = await postResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task PutUpdatesHomeWorkReturnsSuccessApiResponse()
        {
            HomeWorkDto updatedHomeWork = new() {
                Id = 1,
                StudentId = 1,
                SubjectId = 2,
                Grade = 5
            };

            var putHomeWorkResponse = await _httpClient.PutAsJsonAsync("/HomeWorkCrud", updatedHomeWork);
            putHomeWorkResponse.EnsureSuccessStatusCode();

            var putResult = putHomeWorkResponse.Content;
            var apiResponse = await putResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteRemovesHomeWorkReturnsSuccessApiResponse()
        {
            var deleteHomeWorkWithIdResponse = await _httpClient.DeleteAsync("/HomeWorkCrud/2");
            deleteHomeWorkWithIdResponse.EnsureSuccessStatusCode();
            
            var putResult = deleteHomeWorkWithIdResponse.Content;
            var apiResponse = await putResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [OneTimeTearDown]
        public void TearDownFactoryAndClient()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }
    }
}