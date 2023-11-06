﻿namespace AppIdCreatorTool.Model
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RecordName { get; set; }

        public Pager()
        {

        }
        public Pager(int totalItems, int page, int pageSize=5)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
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
                if(endPage > pageSize)
                {
                    startPage = endPage - (pageSize - 1);
                }

            }
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }

   

}
