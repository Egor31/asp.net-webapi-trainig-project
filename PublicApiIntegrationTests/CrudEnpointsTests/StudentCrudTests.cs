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
    public class StudentCrudControllerTests
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
        public async Task GetReturnsAllStudents()
        {
            var getStudentsResponse = await _httpClient.GetAsync("/StudentCrud");
            getStudentsResponse.EnsureSuccessStatusCode();
            
            var studentsResultContent = getStudentsResponse.Content;
            var apiResponse = await studentsResultContent.ReadFromJsonAsync<ApiResponse<IEnumerable<StudentDto>>>();
            
            Assert.That(apiResponse.Data.Count, Is.GreaterThan(0));
        
        }

        [Test]
        public async Task GetWithIdReturnsStudent()
        {
            var getStudentWithIdResponse = await _httpClient.GetAsync("/StudentCrud/1");
            getStudentWithIdResponse.EnsureSuccessStatusCode();

            var studentResultContent = getStudentWithIdResponse.Content;
            var apiResponse = await studentResultContent.ReadFromJsonAsync<ApiResponse<StudentDto>>();
            
            Assert.That(apiResponse.Data, Is.Not.Null);
        }

        [Test]
        public async Task GetWithNonExistentIdReturnsApiResponseWithNotFoundStatusCode()
        {
            var getStudentWithNonExistentIdResponse = await _httpClient.GetAsync("/StudentCrud/100");
            var homeWorkResultContent = getStudentWithNonExistentIdResponse.Content;
            var apiResponse = await homeWorkResultContent.ReadFromJsonAsync<ApiResponse<object>>();
            var statusCode = getStudentWithNonExistentIdResponse.StatusCode;

            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(apiResponse.Succeed, Is.EqualTo(false));            
        }

        [Test]
        public async Task PostCreatesStudentReturnsSuccessApiResponse()
        {
            StudentDto newTestStudent = new() {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.edu",
                PhoneNumber = "+1(202)328-0001"
            };

            var postStudentResponse = await _httpClient.PostAsJsonAsync("/StudentCrud", newTestStudent);
            postStudentResponse.EnsureSuccessStatusCode();

            var postResult = postStudentResponse.Content;
            var apiResponse = await postResult.ReadFromJsonAsync<ApiResponse<object>>();

            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task PutUpdatesStudentReturnsSuccessApiResponse()
        {
            StudentDto updatedStudent = new() {
                Id = 3,
                FirstName = "Test",
                LastName = "Test",
                Email = "test@test.edu",
                PhoneNumber = "+1(202)328-0001"
            };

            var putStudentResponse = await _httpClient.PutAsJsonAsync("/StudentCrud", updatedStudent);
            putStudentResponse.EnsureSuccessStatusCode();
            
            var putResult = putStudentResponse.Content;
            var apiResponse = await putResult.ReadFromJsonAsync<ApiResponse<object>>();
            
            Assert.That(apiResponse.Succeed, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteRemovesStudentReturnsSuccessApiResponse()
        {
            var deleteStudentWithIdResponse = await _httpClient.DeleteAsync("/StudentCrud/2");
            deleteStudentWithIdResponse.EnsureSuccessStatusCode();

            var putResult = deleteStudentWithIdResponse.Content;
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