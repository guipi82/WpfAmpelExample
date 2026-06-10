using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAmpelExample.Controls;

namespace WpfAmpelExample.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly Random _random = new();

    private CancellationTokenSource? _automaticCancellationTokenSource;

    public MainViewModel()
    {
        GreenCommand = new RelayCommand(OnGreenCommand);
        YellowCommand = new RelayCommand(OnYellowCommand);
        RedCommand = new RelayCommand(OnRedCommand);
        AutomaticCommand = new RelayCommand(OnAutomaticCommand);
    }

    public AmpelLicht AktuellesAmpelLicht
    {
        get;
        set => SetField(ref field, value);
    }

    public ICommand GreenCommand { get; }
    public ICommand YellowCommand { get; }
    public ICommand RedCommand { get; }
    public ICommand AutomaticCommand { get; }

    private async void OnGreenCommand()
    {
        try
        {
            AktuellesAmpelLicht = AmpelLicht.Green;
            await StopAutomatic();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private async void OnYellowCommand()
    {
        try
        {
            AktuellesAmpelLicht = AmpelLicht.Yellow;
            await StopAutomatic();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private async void OnRedCommand()
    {
        try
        {
            AktuellesAmpelLicht = AmpelLicht.Red;
            await StopAutomatic();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private async void OnAutomaticCommand()
    {
        try
        {
            if (await StopAutomatic())
            {
                return;
            }

            // Starte neuen Task
            _automaticCancellationTokenSource = new CancellationTokenSource();
            var token = _automaticCancellationTokenSource.Token;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    // Zufälliges AmpelLicht auswählen (außer None)
                    var lights = new[] { AmpelLicht.Red, AmpelLicht.Yellow, AmpelLicht.Green };
                    AktuellesAmpelLicht = lights[_random.Next(lights.Length)];

                    // 1 Sekunde warten
                    await Task.Delay(1000, token);
                }
            }
            catch (OperationCanceledException)
            {
                // Task wurde abgebrochen
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private async Task<bool> StopAutomatic()
    {
        // Wenn bereits ein Task läuft, stoppe ihn
        if (_automaticCancellationTokenSource == null)
        {
            return false;
        }

        await _automaticCancellationTokenSource.CancelAsync();
        _automaticCancellationTokenSource.Dispose();
        _automaticCancellationTokenSource = null;
        return true;
    }
}