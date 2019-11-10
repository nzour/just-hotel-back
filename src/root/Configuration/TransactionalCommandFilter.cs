using System;
using System.Linq;
using Application.Abstraction;
using Infrastructure.NHibernate;
using Kernel.Abstraction;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Root.Configuration
{
    public class TransactionalCommandFilter : IActionFilter, IGlobalFilter
    {
        private const string CommandNamespace = "Application.CQS";
        private const string CommandPostfix = "Command";

        private TransactionFacade Transaction { get; }

        public TransactionalCommandFilter(TransactionFacade transaction)
        {
            Transaction = transaction;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!MustActionBeWrapped(context.ActionDescriptor))
            {
                return;
            }

            Transaction.Begin();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!MustActionBeWrapped(context.ActionDescriptor) || !Transaction.IsActive || null != context.Exception)
            {
                return;
            }

            Transaction.Commit();
            Transaction.Dispose();
        }

        private bool MustActionBeWrapped(ActionDescriptor descriptor)
        {
            return null != descriptor.Parameters
                       .Select(param => param.ParameterType)
                       .FirstOrDefault(IsCommand);
        }

        private bool IsCommand(Type param)
        {
            var @namespace = param.Namespace;

            return null != @namespace &&
                   @namespace.StartsWith(CommandNamespace) &&
                   @namespace.EndsWith(CommandPostfix) &&
                   !param.CustomAttributes
                       .Select(attrData => attrData.AttributeType)
                       .Contains(typeof(NotTransactionalAttribute));
        }
    }
}