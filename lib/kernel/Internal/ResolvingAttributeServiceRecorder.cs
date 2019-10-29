using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Kernel.Abstraction;
using Kernel.Attribute;
using Kernel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Kernel.Internal
{
    internal class ResolvingAttributeServiceRecorder : AbstractServiceRecorder
    {
        private TypeFinder[] TypeFinders { get; }

        public ResolvingAttributeServiceRecorder(params TypeFinder[] typeFinders)
        {
            TypeFinders = typeFinders;
        }

        protected override void Execute(IServiceCollection services)
        {
            RecordMarkedServices<TransientAttribute>((@interface, impl) => services.AddTransient(@interface, impl));
            RecordMarkedServices<ScopedAttribute>((@interface, impl) => services.AddScoped(@interface, impl));
            RecordMarkedServices<SingletonAttribute>((@interface, impl) => services.AddSingleton(@interface, impl));
        }

        private void RecordMarkedServices<TAttribute>(Action<Type, Type> recordAction)
        {
            foreach (var finder in TypeFinders)
            {
                FindMarkedByAttributeServices<TAttribute>(finder)
                    .Foreach(service =>
                    {
                        var attribute = (TransientAttribute) service.GetCustomAttributes(true)
                            .First(attr => attr is TransientAttribute);

                        recordAction.Invoke(attribute.Interface ?? service, service);
                    });
            }
        }

        private IEnumerable<Type> FindMarkedByAttributeServices<TAttribute>(TypeFinder finder)
        {
            return finder.FindTypes(t =>
                t.CustomAttributes.Select(attributeData => attributeData.AttributeType)
                    .Contains(typeof(TAttribute)));
        }
    }
}