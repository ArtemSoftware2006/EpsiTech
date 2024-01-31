using Task_1.Domain.validators;

namespace Task_1.Domain.ViewModels
{
    public class TaskCreateViewModel
    {
        [NotEmpty]
        public string Name { get; set; }
        [NotEmpty]
        public string Text { get; set; }
    }
}