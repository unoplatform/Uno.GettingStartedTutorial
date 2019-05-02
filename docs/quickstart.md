---
title: Quickstart - Creating a Single Page App with Uno
description: How to get started developing Cross Platform apps with Uno
---

# Create a Single Page App with Uno

[Download Sample](https://github.com/nventive/Uno.QuickStarts)

In this quickstart you will learn how to:

- Add the Uno Project Templates to Visual Studio
- Create a new Project with Uno
- Learn basics on Model Binding
- Create a stylish UI with the Windows Community Toolkit

The quickstart walks through creating cross platform application with Uno, which enables you to see a single Issue entry.

### Prerequisites

- Visual Studio 2019 (latest release), with the **Mobile development with .NET** workload installed.
- Knowledge of C#.
- (optional) A paired Mac to build the iOS project.

For more information about these prerequisites, see [Installing Xamarin](https://docs.microsoft.com/en-us/xamarin/get-started/installation/index.md). For information about connecting Visual Studio 2019 to a Mac build host, see [Pair to Mac for Xamarin.iOS development](https://docs.microsoft.com/en-us/xamarin/ios/get-started/installation/windows/connecting-to-mac/index.md).

## Installing the App Templates with Visual Studio 2019

1. Launch Visual Studio 2019, then click `Continue without code`. Click `Extensions` -> `Manage Extensions` from the Menu Bar.

    ![](../images/manage-extensions.png)

1. In the Extension Manager expand the Online node and search for `Uno`. Download the `Uno Platform Solution Templates` extension and restart Visual Studio.

    ![](../images/uno-extensions.PNG)

## Getting Started

1. Open Visual Studio 2019 and click on `Create new project`. 

    ![](../images/newproject1.PNG)

1. Search for the `Uno` templates, select the `Cross-Platform App (Uno Platform)` then click `Next`.

    ![](../images/newproject2.PNG)

1. In the `Configure your new project` window, set the `Project name` to `BugTracker`, choose where you would like to save your project and click the `Create` button.

    ![](../images/newproject3.PNG)

    > [!IMPORTANT]
    > The C# and XAML snippets in this quickstart requires that the solution is named **BugTracker**. Using a different name will result in build errors when you copy code from this quickstart into the solution.

1. Right click on the Solution and select `Manage NuGet Packages for Solution` from the context menu.

    - Click on the Updates tab, and update any of the packages that may need to update.
    - Click back on the Browse tab and install the following NuGet Packages to each of the projects in your solution:
      - Uno.Microsoft.Toolkit.Uwp.UI.Controls
      - Refactored.MvvmHelpers

### Setting Up Our Model

1. Add a Models folder in the Shared Project.

    ![](../images/create-models-folder.png)

1. Add a new class and then paste in the following code:

    ```cs
    public class IssueItem : ObservableObject
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private IssueType type;
        public IssueType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private IssueStatus status;
        public IssueStatus Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        private int effort;
        public int Effort
        {
            get => effort;
            set => SetProperty(ref effort, value);
        }

        private DateTimeOffset createdAt = DateTimeOffset.Now.ToLocalTime();
        public DateTimeOffset CreatedAt
        {
            get => createdAt;
            set => SetProperty(ref createdAt, value);
        }

        private DateTimeOffset? startedAt;
        public DateTimeOffset? StartedAt
        {
            get => startedAt;
            set => SetProperty(ref startedAt, value);
        }

        private DateTimeOffset? completedAt;
        public DateTimeOffset? CompletedAt
        {
            get => completedAt;
            set => SetProperty(ref completedAt, value);
        }
    }

    public enum IssueType
    {
        Bug,
        Issue,
        Task,
        Feature
    }

    public enum IssueStatus
    {
        Icebox,
        Planned,
        WIP,
        Done,
        Removed
    }
    ```

    > [!IMPORTANT]
    > Because we want to be able to respond to changes in our model we'll want to bring in the ObservableObject in the MvvmHelpers namespace, from the Refactored.MvvmHelpers NuGet pacakge we installed earlier.

### Setting up our Page

1. Let's start by opening the code behind and adding some base properties to bind to in our XAML. In the **Solution Explorer**, double-click **MainPage.xaml.cs** to open, then add the following code.

    ```cs
    public sealed partial class MainPage : Page
    {
        public static readonly DependencyProperty IssueItemProperty =
            DependencyProperty.Register(nameof(Item), typeof(IssueItem), typeof(MainPage), new PropertyMetadata(default(IssueItem)));

        public MainPage()
        {
            this.InitializeComponent();
        }

        public IssueItem Item
        {
            get => (IssueItem)GetValue(IssueItemProperty);
            set => SetValue(IssueItemProperty, value);
        }

        public IssueStatus[] StatusList => new[]
        {
            IssueStatus.Icebox,
            IssueStatus.Planned,
            IssueStatus.WIP,
            IssueStatus.Done,
            IssueStatus.Removed
        };

        public IssueType[] IssueTypeList => new[]
        {
            IssueType.Bug,
            IssueType.Feature,
            IssueType.Issue,
            IssueType.Task
        };

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Item = new IssueItem
            {
                Id = 1232,
                Title = "Getting Started",
                Description = @"Create a page to enter Issues that we need to work on.

    ## Acceptance Criteria

    - Display the issue Id
    - Provide an ability to select the issue Type (i.e. Bug, Feature, etc)
    - Include an Issue Title
    - Include a full issue description with support for Markdown
    - Include an issue effort
    - Include an ability for a developer to update the Status (i.e Icebox, WIP, etc)

    ## Additional Comments

    We would like to have a visual indicator for the type of issue as well as something to visualize the effort involved",
                Effort = 3,
                Status = IssueStatus.WIP,
                Type = IssueType.Feature,
                CreatedAt = new DateTimeOffset(2019, 04, 03, 08, 0, 0, TimeSpan.FromHours(-8)),
                StartedAt = new DateTimeOffset(2019, 04, 30, 08, 0, 0, TimeSpan.FromHours(-8))
            };
        }
    }
    ```

1. Now that we have some basic data to bind to, in the **Solution Explorer**, double-click **MainPage.xaml** to open, then add the following code. We will start by adding a XML Namespace for the Converters and Controls from the Microsoft Community Toolkit as shown below:

    ```xml
    <Page x:Class="BugTracker.MainPage"
          xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
          xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
          xmlns:converters="using:Microsoft.Toolkit.Uwp.UI.Converters"
          xmlns:controls="using:Microsoft.Toolkit.Uwp.UI.Controls"
          xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
          xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
          mc:Ignorable="d">
    ```

1. Now we will add the `StringFormatConverter` from the Windows Community Toolkit to our Page Resources as shown below:

    ```xml
    <Page.Resources>
        <converters:StringFormatConverter x:Key="StringFormatConverter" />
    </Page.Resources>
    ```

1. Now we will update the Grid so that we define 6 rows with a small spacing between the rows to add a little padding between the row elements.

    ```xml
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}" RowSpacing="8">
      <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
      </Grid.RowDefinitions>

    </Grid>
    ```

1. Inside the Grid, beneath the RowDefinitions, we will now add our first row that will contain a Header with a TextBlock containing the Issue Id, and a ComboBox to select what type of issue we are working on. Because our model is a property of the Page we will use **x:Bind** to bind to our Item property. You will notice that you can dot into the property to Bind directly to a property of the Item model.

    ```xml
    <StackPanel Grid.Row="0" Orientation="Horizontal" Background="LightGray" Padding="5">
      <Canvas Background="Blue" Width="10" x:Name="IssueTypeIndicator" />
      <TextBlock Text="{x:Bind Item.Id}" Margin="6,0" VerticalAlignment="Center" />
      <ComboBox x:Name="IssueTypeBox"
                ItemsSource="{x:Bind IssueTypeList}"
                SelectedItem="{x:Bind Item.Type}"
                SelectionChanged="IssueType_SelectionChanged"
                PlaceholderText="Enter the Issue Type"
                HorizontalAlignment="Stretch"
                Margin="10,0,0,0"/>
    </StackPanel>
    ```

    > [!IMPORTANT]
    > Take note that we have added a reference to an event handler on the ComboBox. We will add this later in the code behind.

1. Now after the StackPanel we added in the last step, we will add a TabView from the Community Toolkit. In here we will have a Markdown editor with a toolbar on the first tab, and a preview of the Markdown on the second tab.

    ```xml
    <controls:HeaderedTextBlock Text="Description" Grid.Row="1" Margin="10,0" />
    <controls:TabView x:Name="DescriptionTabs"
                      Grid.Row="2"
                      Margin="10,0"
                      SelectedTabWidth="150"
                      TabWidthBehavior="Equal">
      <controls:TabViewItem Header="Edit">
        <StackPanel>
          <controls:TextToolbar Editor="{x:Bind Editor}" Format="MarkDown"/>
          <RichEditBox x:Name="Editor"
                       PlaceholderText="Enter Text Here"
                       TextChanged="Editor_TextChanged"
                       Height="200" />
        </StackPanel>
      </controls:TabViewItem>
      <controls:TabViewItem Header="Preview" Padding="10">
        <controls:MarkdownTextBlock Text="{x:Bind Item.Description}"
                                    Margin="6" />
      </controls:TabViewItem>
    </controls:TabView>
    ```

    > [!IMPORTANT]
    > Take note that we have added a reference to an event handler on the RichEditBox. We will add this later in the code behind.

1. Finally we will add the last section to our layout. In here we will have an ability to visualize the Effort of the Issue and see when an issue was created, started, and completed.

    ```xml
    <controls:HeaderedTextBlock Text="Planning" Grid.Row="3" Margin="10,0" />
    <controls:WrapPanel Grid.Row="4" Margin="10,0" HorizontalSpacing="20">
      <StackPanel Background="LightGray" Padding="20">
        <controls:HeaderedTextBlock Text="Effort" />
        <controls:RadialGauge x:Name="EffortGauge"
                              Value="{x:Bind Item.Effort,Mode=OneWay}"
                              Minimum="0"
                              Maximum="15"
                              Unit="Story Points"
                              TickSpacing="2"
                              TickLength="0"
                              ScaleWidth="26"
                              TrailBrush="Green"
                              NeedleWidth="2"
                              NeedleBrush="Black"
                              NeedleLength="85"
                              MaxWidth="150"/>
          <StackPanel Orientation="Horizontal">
            <TextBox Text="{x:Bind Item.Effort,Mode=TwoWay}"
                     HorizontalTextAlignment="Center"
                     HorizontalAlignment="Center"
                     HorizontalContentAlignment="Center"
                     BorderBrush="Transparent"
                     Background="Transparent"/>
            <Slider Value="{x:Bind Item.Effort,Mode=TwoWay}" ValueChanged="Slider_ValueChanged" Width="100" Minimum="0" Maximum="15" />
          </StackPanel>
        </StackPanel>
        <StackPanel Background="LightGray"
                    Padding="20">
          <controls:HeaderedTextBlock Text="Status" />
          <ComboBox ItemsSource="{x:Bind StatusList}"
                    SelectedItem="{x:Bind Item.Status}"
                    HorizontalAlignment="Stretch"
                    SelectionChanged="StatusPicker_SelectionChanged" />
          <TextBlock Text="{x:Bind Item.StartedAt,Converter={StaticResource StringFormatConverter},ConverterParameter='Started: {0:MMM dd, yyyy hh:mm tt}',Mode=OneWay}" />
          <TextBlock Text="{x:Bind Item.CompletedAt,Converter={StaticResource StringFormatConverter},ConverterParameter='Completed: {0:MMM dd, yyyy hh:mm tt}',Mode=OneWay}" />
        </StackPanel>
      </controls:WrapPanel>
      <TextBlock Text="{x:Bind Item.CreatedAt, Converter={StaticResource StringFormatConverter}, ConverterParameter='Created: {0:MMM dd, yyyy hh:mm tt}'}" Grid.Row="5"
               Margin="10,0"/>
    </Grid>
    ```

    > [!IMPORTANT]
    > Take note that we have added a reference to an event handler on the Slider and a ComboBox. We will add this in the next step in the code behind.

1. Now that our Page is complete we can go back and add the event handlers in our code behind. This will allow us to handle changes and make necessary updates. In the **Solution Explorer**, double-click **MainPage.xaml.cs** to open, then add the following code.

    ```cs
    // Update the IssueItem's Description when the Editor's text value changes
    private void Editor_TextChanged(object sender, RoutedEventArgs e)
    {
        if(Item != null)
        {
            Editor.Document.GetText(Windows.UI.Text.TextGetOptions.UseLf, out var description);
            Item.Description = description;
        }
    }

    // Make sure that we add a timestamp when an Issue is changed to WIP, Done or Removed
    private void StatusPicker_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        switch(Item.Status)
        {
            case IssueStatus.Removed:
            case IssueStatus.Done:
                Item.CompletedAt = DateTimeOffset.Now.ToLocalTime();
                break;
            case IssueStatus.WIP:
                Item.StartedAt = DateTimeOffset.Now.ToLocalTime();
                break;
            default:
                Item.StartedAt = null;
                Item.CompletedAt = null;
                break;
        }
    }

    // Provide a unique color to visually indicate what type of issue we are looking at
    private void IssueType_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        var color = Color.FromArgb(0xFF, 0xFF, 0x00, 0x00);
        switch(IssueTypeBox.SelectedItem)
        {
            case IssueType.Feature:
                color = Color.FromArgb(0xFF, 0x00, 0xFF, 0x00);
                break;
            case IssueType.Issue:
                color = Color.FromArgb(0xFF, 0x00, 0x00, 0xFF);
                break;
            case IssueType.Task:
                color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0x00);
                break;
        }
        IssueTypeIndicator.Background = new SolidColorBrush(color);
    }

    // Change the color of the RadialGauge depending on the effort
    private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs args)
    {
        var color = Color.FromArgb(0xFF, 0x00, 0xFF, 0x00);
        if(args.NewValue > 4 & args.NewValue <= 10)
        {
            color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0x00);
        }
        else if(args.NewValue > 10)
        {
            color = Color.FromArgb(0xFF, 0xFF, 0x00, 0x00);
        }

        EffortGauge.TrailBrush = new SolidColorBrush(color);
    }
    ```

    As one final change be sure to add the following line in the `OnNavigatedTo` method that we created earlier. This will let us set the text in the `RichEditBox` with the Description from our `IssueItem`

    ```cs
    // Initialize the Editor after we have set the Issue Item
    Editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, Item.Description);
    ```

1. Build and run the project on each platform.

    You will notice as you make changes to the effort slider the Radial Gauge both moves and updates the color of the Trail. As you make changes to the Issue type you will also see the indicator in the upper left hand corner changing colors as well.

## Next steps

In this quickstart, you have learned how to:

- Add the Uno Project Templates to Visual Studio
- Create a new Project with Uno
- Learn basics on Model Binding
- Create a stylish UI with the Windows Community Toolkit

To see how we can create a list of pages, navigate between them passing parameters, and store our issues continue to the next quickstart.

> [!div class="nextstepaction"]
> [Next](quickstart-navigation.md)