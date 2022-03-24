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
    public class SubjectCrudTests
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
        public async Task GetReturnsAllSubjects()
        {
            var getSubjectsResponse = await _httpClient.GetAsync("/SubjectCrud");
            getSubjectsResponse.EnsureSuccessStatusCode();

            var subjectsResultContent = getSubjectsResponse.Content;
            var apiResponse = await subjectsResultContent.ReadFromJsonAsync<ApiResponse<IEnumerable<SubjectDto>>>();

            Assert.That(apiResponse.Data.Count, Is.GreaterThan(0));
        
        }

        [Test]
        public async Task GetWithIdReturnsSubject()
        {
            var getSubjectWithIdResponse = await _httpClient.GetAsync("/SubjectCrud/1");
            getSubjectWithIdResponse.EnsureSuccessStatusCode();

            var subjectResultContent = getSubjectWithIdResponse.Content;
            var apiResponse = await subjectResultContent.ReadFromJsonAsync<ApiResponse<SubjectDto>>();

            Assert.That(apiResponse.Data, Is.Not.Null);
        }

        [Test]
        public async Task GetWithNonExistentIdReturnsApiResponseWithNotFoundStatusCode()
        {
            var getSubjectWithNonExistentIdResponse = await _httpClient.GetAsync("/SubjectCrud/100");
            var homeWorkResultContent = getSubjectWithNonExistentIdResponse.Content;
            var apiResponse = await homeWorkResultContent.ReadFromJsonAsync<ApiResponse<object>>();
            var statusCode = getSubjectWithNonExistentIdResponse.StatusCode;
            
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(apiResponse.Succeed, Is.EqualTo(false));            
        }

        [Test]
        public async Task PostCreatesSubjectReturnsSuccessApiResponse()
        {
            SubjectDto newTestSubject = new() {
                TeacherId = 1,
                Title = "How To Do Your Taxes"
            };

            var postSubjectResponse = await _httpClient.PostAsJsonAsync("/SubjectCrud", newTestSubject);
            postSubjectResponse.EnsureSuccessStatusCode();

            var postResult = postSubjectResponse.Content;
            var apiResponse = await postResult.ReadFromJsonAsync<ApiResponse<object>>();

            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task PutUpdatesSubjectReturnsSuccessApiResponse()
        {
            SubjectDto updatedSubject = new() {
                Id = 1,
                TeacherId = 3,
                Title = "New Title"
            };

            var putSubjectResponse = await _httpClient.PutAsJsonAsync("/SubjectCrud", updatedSubject);
            putSubjectResponse.EnsureSuccessStatusCode();

            var putResult = putSubjectResponse.Content;
            var apiResponse = await putResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteRemovesSubjectReturnsSuccessApiResponse()
        {
            var deleteSubjectWithIdResponse = await _httpClient.DeleteAsync("/SubjectCrud/2");
            deleteSubjectWithIdResponse.EnsureSuccessStatusCode();

            var putResult = deleteSubjectWithIdResponse.Content;
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