namespace AppIdCreatorTool.Model
{
    public class PagerModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public static int PageSize { get; set; } = 5;
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RecordName { get; set; }

        public PagerModel()
        {

        }
        public PagerModel(int totalItems, int page)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)PageSize);
            int currentPage = page;
            int startPage = currentPage - 3;
            int endPage = currentPage + 2;

            if(startPage <= 0)
            {
                endPage = endPage -(startPage-1);
                startPage = 1;
            }
            if(endPage > totalPages)
            {
                endPage = totalPages;
                if(endPage > PageSize)
                {
                    startPage = endPage - (PageSize - 1);
                }

            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = PageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }

   

}
