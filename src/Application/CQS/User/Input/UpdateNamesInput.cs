namespace Application.CQS.User.Input
{
    public class UpdateNamesInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UpdateNamesInput(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
