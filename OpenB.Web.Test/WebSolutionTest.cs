using NUnit.Framework;
using OpenB.Web.Elements;
using OpenB.Web.Theming;
using Rhino.Mocks;

namespace OpenB.Web.Test
{
    [TestFixture]
    public class WebSolutionTest
    {
        [Test]
        public void WebSolution_Initialize()
        {
            MockRepository mockRepository = new MockRepository();
            IWebThemePackage mockWebThemePackage = mockRepository.StrictMock<IWebThemePackage>();

            WebSolution webSolution = new WebSolution(mockWebThemePackage, new RenderContext(new RequestManager()));
            webSolution.Initialize();
        }


    }
}