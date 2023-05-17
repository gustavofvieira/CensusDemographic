namespace CensusDemographic.Domain.PagedList
{
    public class PagedList<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public List<T> List { get; set; }
        public List<int> Pages { get; set; } = new List<int>();
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            GetPages(TotalPages);

            List = items;
        }

        private void GetPages(int totalPages)
        {

            for (int numberPage = 1; numberPage <= TotalPages; numberPage++)
            {
                this.Pages.Add(numberPage);
            }
        }

        

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}
