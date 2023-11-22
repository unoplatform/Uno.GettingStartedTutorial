namespace Counter;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this
            .DataContext(new MainViewModel(), (page, vm) => page
            .Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
            .Content(new StackPanel()
            .VerticalAlignment(VerticalAlignment.Center)
            .HorizontalAlignment(HorizontalAlignment.Center)
            .Children(
                new Image()
                    .Width(150)
                    .Height(150)
                    .Source("ms-appx:///Counter/Assets/logo.png"),
                new TextBox()
                    .Margin(12)
                    //.HorizontalTextAlignment(Microsoft.UI.Xaml.TextAlignment.Center),
                    .PlaceholderText("Step Size")
                    .Text(x => x.Bind(() => vm.StepSize).Mode(BindingMode.TwoWay)),
                new TextBlock()
                    .Margin(12)
                    .HorizontalTextAlignment(Microsoft.UI.Xaml.TextAlignment.Center)
                    .Text(() => vm.CounterValue, txt => $"Counter: {txt}"),
                new Button()
                    .Margin(12)
                    .HorizontalAlignment(HorizontalAlignment.Center)
                    .Command(() => vm.IncrementCommand)
                    .Content("Click me to increment Counter by Step Size")
            )));
    }
}
