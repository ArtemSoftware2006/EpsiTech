using Task_1.Domain.validators;

namespace Task_1.Domain.ViewModels
{
    public class TaskUpdateViewModel
    {
        public int Id { get; set; }
        [NotEmpty]
        public string Name { get; set; }
        [NotEmpty]
        public string Text { get; set; }
    }
}