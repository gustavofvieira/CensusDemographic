using MongoDB.Driver;
using System.Linq.Expressions;

namespace CensusDemographic.Infra.Data.Repositories.Filters
{
    public class FilterBuilder<T>
    {
        private readonly FilterDefinitionBuilder<T> _builder;
        private FilterDefinition<T> _filter = default!;

        public FilterBuilder()
        {
            _builder = Builders<T>.Filter;
        }

        public FilterBuilder<T> Where<TField>(TField value, Expression<Func<T, bool>> expression)
        {
            if (value != null)
            {
                if (_filter == null)
                {
                    _filter = _builder.Where(expression);
                }
                else
                {
                    _filter &= _builder.Where(expression);
                }
            }

            return this;
        }

        public FilterDefinition<T> Build()
        {
            return _filter;
        }
    }
}
