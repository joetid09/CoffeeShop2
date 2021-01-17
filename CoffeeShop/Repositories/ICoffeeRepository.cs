using CoffeeShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace CoffeeShop.Repositories
{
    public interface ICoffeeRepository
    {
        SqlConnection Connection { get; }

        void Add(Coffee coffee);
        void Delete(int id);
        Coffee Get(int id);
        List<Coffee> GetAll();
        void Update(Coffee coffee);
    }
}