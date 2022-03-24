using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BusinessLogic.ModelsDto;
using NUnit.Framework;
using PublicApi;

namespace PublicApiIntegrationTests
{
    public class AttendanceReportTests
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
        public async Task GetJsonReportByStudentId()
        {
            var getJsonReportResponse = await _httpClient.GetAsync("/AttendanceReport/student1.json");
            getJsonReportResponse.EnsureSuccessStatusCode();

            var jsonReportResultContent = getJsonReportResponse.Content;
            var report = await jsonReportResultContent.ReadFromJsonAsync<IEnumerable<RollBookRecordDto>>();
            
            Assert.That(report.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetXmlReportByStudentId()
        {
            var getXmlReportResponse = await _httpClient.GetAsync("/AttendanceReport/student1.xml");
            getXmlReportResponse.EnsureSuccessStatusCode();

            var xmlReportResponse = await getXmlReportResponse.Content.ReadAsStreamAsync();
            var xmlSerializer = new XmlSerializer(typeof(List<RollBookRecordDto>));
            var report = (List<RollBookRecordDto>)xmlSerializer.Deserialize(xmlReportResponse);

            Assert.That(report.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetJsonReportBySubjectId()
        {
            var getJsonReportResponse = await _httpClient.GetAsync("/AttendanceReport/subject1.json");
            getJsonReportResponse.EnsureSuccessStatusCode();

            var jsonReportResultContent = getJsonReportResponse.Content;
            var report = await jsonReportResultContent.ReadFromJsonAsync<IEnumerable<RollBookRecordDto>>();
            
            Assert.That(report.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetXmlReportBySubjectId()
        {
            var getXmlReportResponse = await _httpClient.GetAsync("/AttendanceReport/subject1.xml");
            getXmlReportResponse.EnsureSuccessStatusCode();

            var xmlReportResponse = await getXmlReportResponse.Content.ReadAsStreamAsync();
            var xmlSerializer = new XmlSerializer(typeof(List<RollBookRecordDto>));
            var report = (List<RollBookRecordDto>)xmlSerializer.Deserialize(xmlReportResponse);

            Assert.That(report.Count, Is.GreaterThan(0));
        }

        [OneTimeTearDown]
        public void TearDownFactoryAndClient()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }
    }
}
