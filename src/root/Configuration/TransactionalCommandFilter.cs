using System;
using System.Linq;
using Infrastructure.NHibernate;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Root.Configuration
{
    public class TransactionalCommandFilter : IActionFilter
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
            if (!MustActionBeWrapped(context.ActionDescriptor) || !Transaction.IsActive)
            {
                return;
            }

            if (null != context.Exception)
            {
                Transaction.Rollback();
            }
            else
            {
                Transaction.Commit();                
            }

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