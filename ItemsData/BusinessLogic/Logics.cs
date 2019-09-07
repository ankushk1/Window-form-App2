using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace TaskGroupBy
{
    class Logics
    {
        public static List<Item> localItem = new List<Item>();
        public static List<Category> localCategory = new List<Category>();
        static int saleId = 1;
        double sum = 0;
        public List<Item> ItemData()
        {
            localItem.Add(new Item { Id = 1, Name = "Pineapple", Price = 80, CategoryId = 3 });
            localItem.Add(new Item { Id = 2, Name = "Cup", Price = 50, CategoryId = 2 });
            localItem.Add(new Item { Id = 3, Name = "Laptop", Price = 55000, CategoryId = 4 });
            localItem.Add(new Item { Id = 4, Name = "Tshirt", Price = 450, CategoryId = 5 });
            localItem.Add(new Item { Id = 5, Name = "Car", Price = 800000, CategoryId = 1 });
            localItem.Add(new Item { Id = 6, Name = "Soap", Price = 40, CategoryId = 2 });
            localItem.Add(new Item { Id = 7, Name = "Apple", Price = 110, CategoryId = 3 });
            localItem.Add(new Item { Id = 8, Name = "Jeans", Price = 1000, CategoryId = 5 });
            localItem.Add(new Item { Id = 9, Name = "ToothPaste", Price = 65, CategoryId = 2 });
            localItem.Add(new Item { Id = 10, Name = "Shoes", Price = 1800, CategoryId = 5 });
            localItem.Add(new Item { Id = 11, Name = "iPhone", Price = 88000, CategoryId = 4 });
            localItem.Add(new Item { Id = 12, Name = "MotorCycle", Price = 95000, CategoryId = 1 });
            localItem.Add(new Item { Id = 13, Name = "Earphone", Price = 750, CategoryId = 4 });
            localItem.Add(new Item { Id = 14, Name = "Kiwi", Price = 85, CategoryId = 3 });
            return localItem;
        }

        public List<Category> CategoryData()
        {
            localCategory.Add(new Category { Id = 1, Name = "Automobiles" });
            localCategory.Add(new Category { Id = 2, Name = "General Items" });
            localCategory.Add(new Category { Id = 3, Name = "Fruits" });
            localCategory.Add(new Category { Id = 4, Name = "Electronics" });
            localCategory.Add(new Category { Id = 5, Name = "Clothes" });
            return localCategory;
        }
       
        public void Insert(DateTime saleDate, int itemId, int qty, double rate ,double amount)
        {
            Sale objSale = new Sale();
            objSale.Id = saleId;
            objSale.SaleDate = saleDate;
            objSale.ItemId = itemId;
            objSale.Quantity = qty;
            objSale.Rate = rate;
            objSale.Amount = amount;

            frmHome.sale.Add(objSale);
            saleId++;
        }

        public void RateKeypress(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || e.KeyChar == '.')
                    return;
                else
                    MessageBox.Show("Enter only Numbers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            e.Handled = true;
        }

        public void QtyKeyPress(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
                    return;
                else
                    MessageBox.Show("Enter only Numbers");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            e.Handled = true;
        }

        public List<DisplayResult> DisplaySale()
        {
            List<DisplayResult> display = new List<DisplayResult>();
            try
            {
                var values = from c in frmHome.category
                             join x in (from s in frmHome.sale
                                        join i in frmHome.item
                         on s.ItemId equals i.Id
                                        select new
                                        {
                                            i.CategoryId,
                                            s.Quantity,
                                            s.Amount,
                                            i.Name
                                        }) on c.Id equals x.CategoryId
                             select new DisplayValues
                             {
                                 CategoryName = c.Name,
                                 Quantity = x.Quantity,
                                 Amount = x.Amount,
                                 ItemName = x.Name
                             };

                var result = values.GroupBy(j => new
                {
                    j.CategoryName
                }).Select(i => new DisplayResult
                {
                    CategoryName = i.Key.CategoryName,
                    Quantity = i.Sum(x => x.Quantity),
                    Amount = i.Sum(x => x.Amount),
                    ItemCount = i.Distinct().Count()
                });

                display.AddRange(result);
            }
            catch
            {
                throw;
            }
            return display;
        }

        public double AmountSum()
        {
            try
            {
                sum = (from s in frmHome.sale
                       select s.Amount).Sum();

                return sum;
            }
            catch
            {
                throw;
            }
        }
    }
}
