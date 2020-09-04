using System;
using MvvmHelpers;

namespace BugTracker.Models {
    public class IssueItem : ObservableObject {
        public IssueItem() {
            CreatedAt = DateTimeOffset.Now.ToLocalTime();
        }

        private int id;
        public int Id {
            get => id;
            set => SetProperty(ref id, value);
        }

        private IssueType type;
        public IssueType Type {
            get => type;
            set => SetProperty(ref type, value);
        }

        private string title;
        public string Title {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string description;
        public string Description {
            get => description;
            set => SetProperty(ref description, value);
        }

        private IssueStatus status;
        public IssueStatus Status {
            get => status;
            set => SetProperty(ref status, value);
        }

        private int effort;
        public int Effort {
            get => effort;
            set => SetProperty(ref effort, value);
        }

        public DateTimeOffset CreatedAt { get; set; }

        private DateTimeOffset? startedAt;
        public DateTimeOffset? StartedAt {
            get => startedAt;
            set => SetProperty(ref startedAt, value);
        }

        private DateTimeOffset? completedAt;
        public DateTimeOffset? CompletedAt {
            get => completedAt;
            set => SetProperty(ref completedAt, value);
        }
    }

    public enum IssueType {
        Bug,
        Issue,
        Task,
        Feature
    }

    public enum IssueStatus {
        Icebox,
        Planned,
        WIP,
        Done,
        Removed
    }
}
