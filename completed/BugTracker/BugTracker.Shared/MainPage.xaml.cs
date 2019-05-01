using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BugTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static readonly DependencyProperty IssueItemProperty =
            DependencyProperty.Register(nameof(Item), typeof(IssueItem), typeof(MainPage), new PropertyMetadata(default(IssueItem)));

        public MainPage()
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
- Include a full issue description with support for MarkDown
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

            this.InitializeComponent();

            Editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, Item.Description);
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
            if(e.Parameter is IssueItem issue)
            {
                Item = issue;
            }

            Editor.Document.SetText(Windows.UI.Text.TextSetOptions.None, Item?.Description);
            DescriptionTabs.TabIndex = string.IsNullOrEmpty(Item?.Description) ? 0 : 1;
        }

        private void Editor_TextChanged(object sender, RoutedEventArgs e)
        {
            if(Item != null)
            {
                Editor.Document.GetText(Windows.UI.Text.TextGetOptions.UseLf, out var description);
                Item.Description = description;
            }
        }

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
    }
}
