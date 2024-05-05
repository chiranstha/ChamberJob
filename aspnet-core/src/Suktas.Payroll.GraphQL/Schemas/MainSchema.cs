using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Suktas.Payroll.Queries.Container;
using System;

namespace Suktas.Payroll.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}