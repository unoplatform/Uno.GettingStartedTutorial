namespace Counter;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this
            .DataContext(
                new MainViewModel(),
                (page, vm) => page
                    .Background(Theme.Brushes.Background.Default)
                    .Content(new StackPanel()
                        .VerticalAlignment(VerticalAlignment.Center)
                        .Children(
                            new Image()
                                .Width(150)
                                .Height(150)
                                .CenterAndSpace()
                                .Source("ms-appx:///Counter/Assets/logo.png"),
                            new TextBox()
                                .CenterAndSpace()
                                //.HorizontalTextAlignment(Microsoft.UI.Xaml.TextAlignment.Center),
                                .PlaceholderText("Step Size")
                                .Text(x => x.Bind(() => vm.Step).TwoWay()),
                            new TextBlock()
                                .CenterAndSpace()
                                .HorizontalTextAlignment(Microsoft.UI.Xaml.TextAlignment.Center)
                                .Text(() => vm.Count, txt => $"Counter: {txt}"),
                            new Button()
                                .CenterAndSpace()
                                .Command(() => vm.IncrementCommand)
                                .Content("Click me to increment Counter by Step Size")
                        )
                    )
        );
    }
}

public static class MainPageHelpers
{
    public static T CenterAndSpace<T>(this T element) where T : FrameworkElement
        => element
            .HorizontalAlignment(HorizontalAlignment.Center)
            .Margin(12);

}
