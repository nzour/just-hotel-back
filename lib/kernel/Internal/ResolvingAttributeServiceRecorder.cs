using System;
using System.Collections.Generic;
using System.Linq;
using common.Extensions;
using kernel.Abstraction;
using kernel.Attribute;
using kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace kernel.Internal
{
    internal class ResolvingAttributeServiceRecorder : AbstractServiceRecorder
    {
        private TypeFinder TypeFinder { get; }

        public ResolvingAttributeServiceRecorder(TypeFinder typeFinder)
        {
            TypeFinder = typeFinder;
        }

        protected override void Execute(IServiceCollection services)
        {
            RecordMarkedServices<TransientAttribute>((@interface, impl) => services.AddTransient(@interface, impl));
            RecordMarkedServices<ScopedAttribute>((@interface, impl) => services.AddScoped(@interface, impl));
            RecordMarkedServices<SingletonAttribute>((@interface, impl) => services.AddSingleton(@interface, impl));
        }

        private void RecordMarkedServices<TAttribute>(Action<Type, Type> recordAction)
        {
            FindMarkedByAttributeServices<TAttribute>()
                .Foreach(service =>
                {
                    var attribute = (TransientAttribute) service.GetCustomAttributes(true)
                        .First(attr => attr is TransientAttribute);

                    recordAction.Invoke(attribute.Interface ?? service, service);
                });
        }

        private IEnumerable<Type> FindMarkedByAttributeServices<TAttribute>()
        {
            return TypeFinder.FindTypes(t =>
                t.CustomAttributes.Select(attributeData => attributeData.AttributeType)
                    .Contains(typeof(TAttribute)));
        }
    }
}