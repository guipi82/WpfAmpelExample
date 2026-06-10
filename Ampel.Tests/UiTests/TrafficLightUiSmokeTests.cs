using FlaUI.Core;
using FlaUI.UIA3;
using FluentAssertions;
using NUnit.Framework;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using System.IO;

[TestFixture]
[Category("UI")]
public class TrafficLightUiSmokeTests
{
    private Application? _app;
    private UIA3Automation? _automation;
    private Window? _window;

    [SetUp]
    public void SetUp()
    {
        var appPath = Path.GetFullPath(
            Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                @"..\..\..\..\WpfAmpelExample\bin\Debug\net10.0-windows\WpfAmpelExample.exe"));

        _app = Application.Launch(appPath);
        _automation = new UIA3Automation();
        //_window = _app.GetMainWindow(_automation);
        var windowResult = Retry.WhileNull(() => _app.GetMainWindow(_automation),TimeSpan.FromSeconds(10));
        _window = windowResult.Result;
        _window.Should().NotBeNull();
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            TakeScreenshot("FAILED_" + TestContext.CurrentContext.Test.Name);
        }
        _app?.Close();
        _automation?.Dispose();

        _window = null;
        _app = null;
        _automation = null;
    }

    [Test]
    public void Application_Should_Start_And_Show_MainWindow()
    {
        _window!.Title.Should().NotBeNullOrWhiteSpace();
        //var redButton = _window.FindFirstDescendant(cf => cf.ByAutomationId("RedButton"));
        var redButton = Retry.WhileNull(() => _window.FindFirstDescendant(cf => cf.ByAutomationId("RedButton")), TimeSpan.FromSeconds(5));
        redButton.Should().NotBeNull();
        var ampelcontrol = Retry.WhileNull(() => _window.FindFirstDescendant(cf => cf.ByAutomationId("AmpelControl")), TimeSpan.FromSeconds(5));
    }

    [Test]
    public void Application_Start_And_Click_The_Green_Button()
    {
        var greenButton = Retry.WhileNull(() => _window.FindFirstDescendant(cf => cf.ByAutomationId("GreenButton")),TimeSpan.FromSeconds(5));
        greenButton.Result.Should().NotBeNull();
        TakeScreenshot("Before_Green_Click");
        Thread.Sleep(1000);
        greenButton.Result!.AsButton().Invoke();
        //Warten bis der Ampelcontrol angezeigt wird
        var ampelcontrol = Retry.WhileNull(() => _window.FindFirstDescendant(cf => cf.ByAutomationId("AmpelControl")), TimeSpan.FromSeconds(5));
        ampelcontrol.Should().NotBeNull();
        TakeScreenshot("After_Green_Click");
    }





    private string TakeScreenshot(string name)
    {
        var screenshotDirectory = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "Screenshots");

        Directory.CreateDirectory(screenshotDirectory);

        var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{name}.png";
        var filePath = Path.Combine(screenshotDirectory, fileName);

        //Console.WriteLine($"Screenshot Directory: {screenshotDirectory}");
        var screenshot = _window!.Capture();
        screenshot.Save(filePath);

        TestContext.AddTestAttachment(filePath);

        return filePath;
    }

}
