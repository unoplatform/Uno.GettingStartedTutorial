namespace Counter;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this
            .DataContext(
                new BindableMainModel(), 
                (page, vm) => page
                    .Background(Theme.Brushes.Background.Default)
                    .Content(new StackPanel()
                        .VerticalAlignment(VerticalAlignment.Center)
                        .Children(
                            CenterAndSpace(
                                new Image()
                                    .Width(150)
                                    .Height(150)
                                    .Source("ms-appx:///Counter/Assets/logo.png"),
                                new TextBox()
                                    //.HorizontalTextAlignment(Microsoft.UI.Xaml.TextAlignment.Center),
                                    .PlaceholderText("Step Size")
                                    .Text(x => x.Bind(() => vm.StepSize).TwoWay()),
                                new TextBlock()
                                    .HorizontalTextAlignment(Microsoft.UI.Xaml.TextAlignment.Center)
                                    .Text(() => vm.CounterValue, txt => $"Counter: {txt}"),
                                new Button()
                                    .Command(() => vm.IncrementCommand)
                                    .Content("Click me to increment Counter by Step Size")
                            )
                        )
                    )
        );
    }

    public static UIElement[] CenterAndSpace(params FrameworkElement[] elements)
        => elements.Select(element => element
            .HorizontalAlignment(HorizontalAlignment.Center)
            .Margin(12)).ToArray();
}
