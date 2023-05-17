using System.Linq.Expressions;

namespace CensusDemographic.Domain.Filters
{
    public class FilterDomain<T>
    {
        private readonly List<Func<T, bool>> _filter = new List<Func<T, bool>>();


        //public FilterDomain<T> Where<TField>(TField value, Expression<Func<T, bool>> expression)
        public FilterDomain<T> Where<TField>(TField value, Func<T, bool> expression)
        {
            if (value != null)
            {
                _filter.Add(expression);
            }
            return this;
        }

        public Func<T, bool> Build()
        {
            return item => _filter.All(filter => filter(item));
        }

        public IEnumerable<T> Build(IEnumerable<T> source)
        {
            var query = source.AsEnumerable();

            foreach (var filter in _filter)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }

}
