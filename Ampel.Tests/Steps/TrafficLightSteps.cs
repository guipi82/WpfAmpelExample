using Reqnroll;
using WpfAmpelExample.ViewModels;
using WpfAmpelExample.Controls;
using FluentAssertions;

namespace Ampel.Tests.Steps
{
    [Binding]
    public class TrafficLightSteps
    {
        private MainViewModel? _driver;

        [Given("the traffic light application is running")]
        public void GivenDieAmpelAppIstGestartet()
        {
            _driver = new MainViewModel();
        }

        [When("I click the {string} button")]
        public void WhenIchAufKlicke(string color)
        {
            _driver.Should().NotBeNull();

            switch (color)
            {
                case "Red":
                    _driver!.RedCommand.Execute(null);
                    break;

                case "Yellow":
                    _driver!.YellowCommand.Execute(null);
                    break;

                case "Green":
                    _driver!.GreenCommand.Execute(null);
                    break;

                default:
                    throw new ArgumentException($"Unbekannte Farbe: {color}");
            }
        }

        [When("I click the Automatic button")]
        public void WhenIchAufAutomaticKlicke()
        {
            _driver.Should().NotBeNull();
            _driver.AutomaticCommand.Execute(null);

        }

        [When("I click the Automatic button again")]
        public void WhenIchAufAutomaticNochmalKlicke()
        {
            _driver.Should().NotBeNull();
            //_driver.AutomaticCommand.Execute(null);
            _driver.AutomaticCommand.Execute(null); // stop automatic loop
        }

        [Then("the traffic light should show {string}")]
        public void ThenWirdDerAmpelzustandAngezeigt(string expectedColor)
        {
            var expected = expectedColor switch
            {
                "Red" => AmpelLicht.Red,
                "Yellow" => AmpelLicht.Yellow,
                "Green" => AmpelLicht.Green,
                _ => throw new ArgumentException($"Unbekannte Farbe: {expectedColor}")
            };

            _driver!.AktuellesAmpelLicht.Should().Be(expected);
        }

        [Then("the traffic light should show different colors in a loop")]
        public async Task ThenWirdDerAmpelzustandInLoopAngezeigt()
        {
            var first = _driver!.AktuellesAmpelLicht;

            await Task.Delay(1500);
            var second = _driver.AktuellesAmpelLicht;

            await Task.Delay(1500);
            var third = _driver.AktuellesAmpelLicht;

            var changed =
                first != second ||
                second != third ||
                first != third;

            changed.Should().BeTrue("the automatic mode should eventually change the traffic light color");

        }

        [Then("the traffic light should show stop changing colors")]
        public async Task ThenWirdDerAmpelzustandStopChangingColors()
        {
            var first = _driver!.AktuellesAmpelLicht;

            await Task.Delay(1500);

            var second = _driver.AktuellesAmpelLicht;

            await Task.Delay(1500);

            var third = _driver.AktuellesAmpelLicht;

            second.Should().Be(first);
            third.Should().Be(first);

        }

        [AfterScenario]
        public void Cleanup()
        {
            _driver = null;
        }
    }
}
