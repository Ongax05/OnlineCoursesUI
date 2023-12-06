namespace OnlineCoursesUI.Services
{
    public class CourseComponentService
    {
        public int SelectedCourseId { get; private set; } = 0;
        public event Action ChangeCourseId;
        public void SetSelectedCourseId(int Id)
        {
            SelectedCourseId = Id;
            ChangeCourseId?.Invoke();
            ExecuteAction();
        }
        private void ExecuteAction () => ChangeCourseId?.Invoke();
    }
}
