using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Domain.Tests.Models
{
    [TestFixture]
    public class ManifestTets
    {
        private Manifest _manifest;
        
        [SetUp]
        public void Setup()
        {
            _manifest = new Manifest(){Id=Guid.NewGuid(),Docket = "CT",SiteCode = 10000,Name = "Demo"};
        }
        [Test]
        public void should_Set_Handshake()
        {
            _manifest.SetHandshake();  
            Assert.That(_manifest.Status, Is.EqualTo("Queued"));
        }

        [Test] 
        public void should_Update_Status()
        {
            _manifest.UpdateStatus("Incomplete");  
            Assert.That(_manifest.Status, Is.EqualTo("Incomplete"));
        }
    }
}
