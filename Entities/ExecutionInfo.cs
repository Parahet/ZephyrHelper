namespace ZephyrHelper.Entities
{
    public class ExecutionInfo
    {
        public int Id;
        public int OrderId;
        public string ExecutionStatus;
        public string ExecutedOn;
        public string ExecutedBy;
        public string ExecutedByDisplay;
        public string Comment;
        public string HtmlComment;
        public int CycleId;
        public string CycleName;
        public int VersionId;
        public string VersionName;
        public int ProjectId;
        public string CreatedBy;
        public string ModifiedBy;
        public int IssueId;
        public string IssueKey;
        public string Summary;
        public string IssueDescription;
        public string Label;
        public string Component;
        public string ProjectKey;
        public bool CanViewIssue;
        public int ExecutionDefectCount;
        public int StepDefectCount;
        public int TotalDefecCount;
    }
}
