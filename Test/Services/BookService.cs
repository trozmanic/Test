namespace Test.Service
{
    using System.Text.Json;

    public interface IBookService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="requestedAmount"></param>
        /// <returns></returns>
        List<Order> PurchaseCoins(int requestedAmount);

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestedAmount"></param>
        /// <returns></returns>
        List<Order> SellCoins(int requestedAmount);
    }
    public  class BookService : IBookService
    {
        public List<Book> Books;

        public BookService()
        {
            DesierializeJSON();
        }

        private void DesierializeJSON()
        {
            //    var text = System.IO.File.ReadAllText("C:\\Users\\TineRozmanic\\source\\repos\\Test\\Test\\Files\\order_books_data_1.json");
            var text = System.IO.File.ReadAllText("C:\\Users\\TineRozmanic\\source\\repos\\Test\\Test\\Files\\order_books.json");
            this.Books = JsonSerializer.Deserialize<List<Book>>(text);
        }

        public List<Order> PurchaseCoins(int requestedAmount)
        {
            var totalAmount = 0.0;
            var orderList = new List<Order>();
            foreach (var book in this.Books)
            {
                foreach (var item in book.Asks)
                {
                    if ((totalAmount < requestedAmount)
                        || (orderList.Last().Price > item.Order.Price))
                    {
                        totalAmount += item.Order.Amount;
                        BinInsertAsc(0, orderList.Count, orderList, item.Order);
                    }

                    if((totalAmount - orderList.Last().Amount) >= requestedAmount)
                    {
                        totalAmount -= orderList.Last().Amount;
                        orderList.RemoveAt(orderList.Count - 1);
                    }
                }
            }

            return orderList;
        }

        public List<Order> SellCoins(int requestedAmount)
        {
            var totalAmount = 0.0;
            var sellList = new List<Order>();
            foreach (var book in this.Books)
            {
                foreach (var item in book.Bids)
                {
                    if ((totalAmount < requestedAmount)
                        || (sellList.Last().Price < item.Order.Price))
                    {
                        totalAmount += item.Order.Amount;
                        BinInsertDesc(0, sellList.Count, sellList, item.Order);
                    }

                    if ((totalAmount - sellList.Last().Amount) >= requestedAmount)
                    {
                        totalAmount -= sellList.Last().Amount;
                        sellList.RemoveAt(sellList.Count - 1);
                    }
                }
            }

            return sellList;
        }

        private void BinInsertAsc(int start, int finish, List<Order> items, Order item)
        {

            var middleIndex = (start + finish) / 2;
            if (start >= finish)
            {
                items.Insert(start, item);
            }
            else if (item.Price > items[middleIndex].Price)
            {
                BinInsertAsc(middleIndex + 1, finish, items, item);
            }
            else
            {
                BinInsertAsc(start, middleIndex, items, item);
            }
        }

        private void BinInsertDesc(int start, int finish, List<Order> items, Order item)
        {
            var middleIndex = (start + finish) / 2;
            if (start >= finish)
            {
                items.Insert(start, item);
            }
            else if (item.Price < items[middleIndex].Price)
            {
                BinInsertDesc(middleIndex + 1, finish, items, item);
            }
            else
            {
                BinInsertDesc(start, middleIndex, items, item);
            }
        }
    }
}
