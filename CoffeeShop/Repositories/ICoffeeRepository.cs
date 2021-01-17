using CoffeeShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace CoffeeShop.Repositories
{
    public interface ICoffeeRepository
    {
        SqlConnection Connection { get; }
        List<Coffee> GetAll();
        //following code is being held in reserve for future update
        //void Add(Coffee coffee);
        //void Delete(int id);
        //Coffee Get(int id);
  
        //void Update(Coffee coffee);
    }
}