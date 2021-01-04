using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace HiLo.EfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dataContext = new SampleDBContext())
            {
                dataContext.Categories.Add(new Category() { CategoryName = "Clothing" });
                dataContext.Categories.Add(new Category() { CategoryName = "Footwear" });
                dataContext.Categories.Add(new Category() { CategoryName = "Accessories" });
                dataContext.SaveChanges();
                dataContext.Products.Add(new Product() { ProductName = "TShirts" });
                dataContext.Products.Add(new Product() { ProductName = "Shirts" });
                dataContext.Products.Add(new Product() { ProductName = "Causal Shoes" });
                dataContext.SaveChanges();

                IEnumerable<Product> productsFromCsv = GetFromCsv();

                dataContext.Products.AddRange(productsFromCsv);
                dataContext.SaveChanges();

            }
        }

        static IEnumerable<Product> GetFromCsv()
        {
            var file = "Products.csv";

            using (var csvParser = new TextFieldParser(file))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = false;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    string name = fields[1];
                    yield return new Product() { ProductName = name };

                }
            }
        }
    }
}
