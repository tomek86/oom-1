using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace task2
    {
        class Program
        {
            static void Main(string[] args)
            {
                Book newBook = new Book("13978-3-499-22916-9", "C#", 4.90m);
                Schedule newSchedule = new Schedule(newBook);

                Console.WriteLine(newSchedule.displayStatus());
                newSchedule.updateStatus(true);
                Console.WriteLine(newSchedule.displayStatus());
                newSchedule.updateStatus(false);
                Console.WriteLine(newSchedule.displayStatus());
            }
        }

        public class Schedule
        {
            private DateTime start;
            private DateTime end;
            private Boolean borrowStatus;
            private Book borrowedBook;
            private IItem borrowedItem;

            public Schedule(Book pBook)
            {
                borrowStatus = false;
            }

            public DateTime Start
            {
                get // this code is executed when the value of a property is read
                {
                    return start;
                }
                set // this code is executed when a value is assigned to the property
                {
                    // the keyword 'value' represents the value that is assigned
                    if (value < DateTime.Now) throw new Exception("Start and End Time must be in the future.");
                    start = value;
                    end = start.AddDays(7);
                }
            }

            public DateTime End
            {
                get
                {
                    return end;
                }

                set
                {
                    if (value < DateTime.Now) throw new Exception("Start and End Time must be in the future.");
                    end = value;
                }
            }

            public void updateStatus(Boolean pBorrowed)
            {
                borrowStatus = pBorrowed;

                if (borrowStatus == true)
                {
                    start = DateTime.Now;
                    end = start.AddDays(7);
                }
                else
                {
                    end = DateTime.Now;
                }
            }

            public string displayStatus()
            {

                if (borrowStatus == true)
                {
                    return "ISBN: " + borrowedBook.ISBN + " Title: " + borrowedBook.Title + " Price: " + borrowedBook.Price + " borrowed from: " + Start + " to " + End + "!";
                }
                else
                {
                    return "ISBN: " + borrowedBook.ISBN + " Title: " + borrowedBook.Title + " Price: " + borrowedBook.Price + " is available!";
                }
            }
        }

        public class Movie : IItem
        {
            private decimal price;
            private string title;

            public Movie (string title, decimal price)
            {

            }

            public decimal Price
            {
                get // this code is executed when the value of a property is read
                {
                    return price;
                }
                set // this code is executed when a value is assigned to the property
                {
                    // the keyword 'value' represents the value that is assigned
                    if (value < 0) throw new Exception("Price must not be negative.");
                    price = value;
                }
            }

            public string Title
            {
                get
                {
                    return title;
                }

                set
                {
                    title = value;
                }
            }

            public void UpdatePrice(decimal pPrice)
            {
                price = pPrice;
            }
        }

        public class Book : IItem
        {
            private decimal m_price;
            private string isbn;
            private string title;

            public Book (string pIsbn, string pTitle, decimal pPrice)
            {
                isbn = pIsbn;
                title = pTitle;
                m_price = pPrice;
            }

            private Book(decimal pPrice)
            {
                m_price = pPrice;
            }

            public decimal Price
            {
                get // this code is executed when the value of a property is read
                {
                    return m_price;
                }
                set // this code is executed when a value is assigned to the property
                {
                    // the keyword 'value' represents the value that is assigned
                    if (value < 0) throw new Exception("Price must not be negative.");
                    m_price = value;
                }
            }

            public string ISBN
            {
                get
                {
                    return isbn;
                }

                set
                {
                    isbn = value;
                }
            }

            public string Title
            {
                get
                {
                    return title;
                }

                set
                {
                    title = value;
                }
            }

            public void UpdatePrice(decimal pPrice)
            {
                m_price = pPrice;
            }
        }
    }

    public interface IItem
    {
        string Title { get; set; }
        decimal Price { get; set; }

        void UpdatePrice(decimal pPrice);
    }


}
