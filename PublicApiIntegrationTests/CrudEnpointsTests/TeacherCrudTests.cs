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
    public class TeacherCrudTests
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
        public async Task GetReturnsAllTeachers()
        {
            var getTeachersResponse = await _httpClient.GetAsync("/TeacherCrud");
            getTeachersResponse.EnsureSuccessStatusCode();

            var teachersResultContent = getTeachersResponse.Content;
            var apiResponse = await teachersResultContent.ReadFromJsonAsync<ApiResponse<IEnumerable<TeacherDto>>>();
            
            Assert.That(apiResponse.Data.Count, Is.GreaterThan(0));
        
        }

        [Test]
        public async Task GetWithIdReturnsTeacher()
        {
            var getTeacherWithIdResponse = await _httpClient.GetAsync("/TeacherCrud/1");
            getTeacherWithIdResponse.EnsureSuccessStatusCode();

            var teacherResultContent = getTeacherWithIdResponse.Content;
            var apiResponse = await teacherResultContent.ReadFromJsonAsync<ApiResponse<TeacherDto>>();
            
            Assert.That(apiResponse.Data, Is.Not.Null);
        }

        [Test]
        public async Task GetWithNonExistentIdReturnsApiResponseWithNotFoundStatusCode()
        {
            var getTeacherWithNonExistentIdResponse = await _httpClient.GetAsync("/TeacherCrud/100");
            var homeWorkResultContent = getTeacherWithNonExistentIdResponse.Content;
            var apiResponse = await homeWorkResultContent.ReadFromJsonAsync<ApiResponse<object>>();
            var statusCode = getTeacherWithNonExistentIdResponse.StatusCode;
            
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(apiResponse.Succeed, Is.EqualTo(false));            
        }
        
        [Test]
        public async Task PostCreatesTeacherReturnsSuccessApiResponse()
        {
            TeacherDto newTestTeacher = new() {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.edu",
                PhoneNumber = "+1(202)328-0001"
            };

            var postTeacherResponse = await _httpClient.PostAsJsonAsync("/TeacherCrud", newTestTeacher);
            postTeacherResponse.EnsureSuccessStatusCode();

            var postResult = postTeacherResponse.Content;
            var apiResponse = await postResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task PutUpdatesTeacherReturnsSuccessApiResponse()
        {
            TeacherDto updatedTeacher = new() {
                Id = 3,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.edu",
                PhoneNumber = "+1(202)328-0001"
            };

            var putTeacherResponse = await _httpClient.PutAsJsonAsync("/TeacherCrud", updatedTeacher);
            putTeacherResponse.EnsureSuccessStatusCode();

            var putResult = putTeacherResponse.Content;
            var apiResponse = await putResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteRemovesTeacherReturnsSuccessApiResponse()
        {
            var deleteTeacherWithIdResponse = await _httpClient.DeleteAsync("/TeacherCrud/2");
            deleteTeacherWithIdResponse.EnsureSuccessStatusCode();

            var putResult = deleteTeacherWithIdResponse.Content;
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