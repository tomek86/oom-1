using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace lesson2
{
	public class Book
	{
		private decimal m_price;

		/// <summary>
		/// Creates a new book object.
		/// </summary>
		/// <param name="title">Title must not be empty.</param>
		/// <param name="isbn">International Standard Book Number.</param>
		/// <param name="price">Price must not be negative.</param>
		public Book(string title, string isbn, decimal price, Currency currency)
		{
			if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title must not be empty.", nameof(title));
			if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN must not be empty.", nameof(isbn));

			Title = title;
			ISBN = isbn;
			UpdatePrice(price, currency);
		}

		/// <summary>
		/// Gets the book title.
		/// </summary>
		public string Title { get; }

		/// <summary>
		/// Gets the International Standard Book Number.
		/// </summary>
		public string ISBN { get; }

		/// <summary>
		/// Gets the currency of this book's price.
		/// </summary>
		public Currency Currency { get; private set;}

		/// <summary>
		/// Gets the book's price in the given currency.
		/// </summary>
		public decimal GetPrice(Currency currency)
		{
			// if the price is requested in it's own currency, then simply return the stored price
			if (currency == Currency) return m_price;

            var from = Currency.ToString();
            var to = currency.ToString();

            // use web service to query current exchange rate
            // request : https://api.fixer.io/latest?base=EUR&symbols=USD
            // response: {"base":"EUR","date":"2018-01-24","rates":{"USD":1.2352}}
            var url = $"https://api.fixer.io/latest?base={from}&symbols={to}";
            // download the response as string
            var data = new WebClient().DownloadString(url);
            // parse JSON
            var json = JObject.Parse(data);
            // convert the exchange rate part to a decimal 
            var rate = decimal.Parse((string)json["rates"][to], CultureInfo.InvariantCulture);

            return m_price * rate;
        }

		/// <summary>
		/// Updates the book's price.
		/// </summary>
		/// <param name="newPrice">Price must not be negative.</param>
		/// <param name="newCurrency">Currency.</param>
		public void UpdatePrice(decimal newPrice, Currency currency)
		{
			if (newPrice < 0) throw new ArgumentException("Price must not be negative.", nameof(newPrice));
			m_price = newPrice;
			Currency = currency;
		}
	}
}

