using System.Threading;
using System.Threading.Tasks;
using NHibernate.Event;

namespace app.Infrastructure.NHibernate
{
    public class MappingConversationListener : IPreLoadEventListener
    {
        public Task OnPreLoadAsync(PreLoadEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void OnPreLoad(PreLoadEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}