using System.Windows;
using System.Windows.Controls;

namespace WpfAmpelExample.Controls;

public class AmpelControl : Control
{
    public static readonly DependencyProperty AktuellesAmpelLichtProperty =
        DependencyProperty.Register(
            nameof(AktuellesAmpelLicht), 
            typeof(AmpelLicht), 
            typeof(AmpelControl), 
            new PropertyMetadata(default(AmpelLicht)));

    public AmpelLicht AktuellesAmpelLicht
    {
        get => (AmpelLicht)GetValue(AktuellesAmpelLichtProperty);
        set => SetValue(AktuellesAmpelLichtProperty, value);
    }
}