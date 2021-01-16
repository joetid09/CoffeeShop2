using CoffeeShop.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace CoffeeShop.Repositories
{
    public interface IBeanVarietyRepository
    {
        SqlConnection Connection { get; }

        void Add(BeanVariety variety);
        void Delete(int id);
        BeanVariety Get(int id);
        List<BeanVariety> GetAll();
        void Update(BeanVariety variety);
    }
}