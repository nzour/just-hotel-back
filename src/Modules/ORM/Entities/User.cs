namespace app.Modules.ORM.Entities
{
    public class User : AbstractEntity
    {
        public virtual string Login { get; protected set; }
        public virtual string Name { get; protected set; }

        public User()
        {   
        }

        public virtual User ChangeLogin(string login)
        {
            Login = login;
            return this;
        }
        
        public virtual User ChangeName(string name)
        {
            Name = name;
            return this;
        }
    }
}