using System.Linq;
using System.Threading.Tasks;
using Domain.User;
using NHibernate;

namespace Infrastructure.NHibernate.Repository
{
    public class UserRepository : EntityRepository<UserEntity>, IUserRepository
    {
        public UserRepository(Transactional transactional, ISessionFactory sessionFactory) : base(transactional,
            sessionFactory)
        {
        }

        public async Task CreateUserAsync(UserEntity userEntity)
        {
            await Task.Run(() => Transactional.Action(session => session.SaveOrUpdate(userEntity)));
        }

        public void CreateUser(UserEntity userEntity)
        {
            Save(userEntity);
        }

        public UserEntity FindUserWithLoginAndPassword(string login, string encryptedPassword)
        {
            return Transactional.Func(session => session.Query<UserEntity>()
                .SingleOrDefault(u => login == u.Login && encryptedPassword == u.Password));
        }

        public bool IsLoginBusy(string login)
        {
            return Transactional.Func(session => null != session.Query<UserEntity>().SingleOrDefault(u => u.Login == login));
        }

        public IQueryable<UserEntity> FindAllUsers()
        {
            var session = SessionFactory.OpenSession();
            return session.Query<UserEntity>();
        }
    }
}