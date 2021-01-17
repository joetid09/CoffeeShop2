using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using CoffeeShop.Models;

namespace CoffeeShop.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private readonly string _connectionString;
        public CoffeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<Coffee> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id as coffeeId, Title, BeanVarietyId, b.Name as [beanName], b.Region[beanRegion], b.Notes as Notes FROM Coffee c
          LEFT JOIN BeanVariety b on c.BeanVarietyId = b.id";
                    var reader = cmd.ExecuteReader();
                    var coffees = new List<Coffee>();
                    while (reader.Read())
                    {
                        var coffee = new Coffee()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("coffeeId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
                            BeanVariety = new BeanVariety()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
                                Name = reader.GetString(reader.GetOrdinal("beanName")),
                                Region = reader.GetString(reader.GetOrdinal("beanRegion"))
                            }
                        };
                        if (!reader.IsDBNull(reader.GetOrdinal("Notes")))
                        {
                            coffee.BeanVariety.Notes = reader.GetString(reader.GetOrdinal("Notes"));
                        }
                        coffees.Add(coffee);
                    }

                    reader.Close();

                    return coffees;
                }
            }
        }

        //Comment rest of CRUD out due to it not being needed at this time.
        //    public Coffee Get(int id)
        //    {
        //        using (var conn = Connection)
        //        {
        //            conn.Open();
        //            using (var cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = "SELECT Id, Title, BeanVarietyId, b.Name as [beanName], b.Region [beanRegion] FROM Coffee c" +
        //                    "               LEFT JOIN BeanVariety b on c.BeanVarietyId = b.id" +
        //                    "               WHERE Id = @id"
        //                    ;
        //                cmd.Parameters.AddWithValue("@id", id);
        //                var reader = cmd.ExecuteReader();
        //                Coffee coffee = null;
        //                if (reader.Read())
        //                {
        //                    coffee = new Coffee()
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                        Title = reader.GetString(reader.GetOrdinal("Title")),
        //                        BeanVarietyId = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
        //                        BeanVariety = new BeanVariety()
        //                        {
        //                            Id = reader.GetInt32(reader.GetOrdinal("BeanVarietyId")),
        //                            Name = reader.GetString(reader.GetOrdinal("beanName")),
        //                            Region = reader.GetString(reader.GetOrdinal("beanRegion"))
        //                        }
        //                    };
        //                    if (!reader.IsDBNull(reader.GetOrdinal("Notes")))
        //                    {
        //                        coffee.BeanVariety.Notes = reader.GetString(reader.GetOrdinal("Notes"));
        //                    }
        //                }

        //                reader.Close();

        //                return coffee;
        //            }
        //        }
        //    }

        //    public void Add(Coffee coffee)
        //    {
        //        using (var conn = Connection)
        //        {
        //            conn.Open();
        //            using (var cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = @"
        //                    INSERT INTO Coffee (Title, BeanVarietyId)
        //                    OUTPUT INSERTED.ID
        //                    VALUES (@title, @beanVarietyid)";
        //                cmd.Parameters.AddWithValue("@title", coffee.Title);
        //                cmd.Parameters.AddWithValue("@region", coffee.BeanVarietyId);
        //                coffee.Id = (int)cmd.ExecuteScalar();
        //            }
        //        }
        //    }

        //    public void Update(Coffee coffee)
        //    {
        //        using (var conn = Connection)
        //        {
        //            conn.Open();
        //            using (var cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = @"
        //                    UPDATE Coffee 
        //                       SET [Title] = @title, 
        //                           BeanVarietyId = @variety, 
        //                     WHERE Id = @id";
        //                cmd.Parameters.AddWithValue("@id", coffee.Id);
        //                cmd.Parameters.AddWithValue("@title", coffee.Title);
        //                cmd.Parameters.AddWithValue("@variety", coffee.BeanVarietyId);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }

        //    public void Delete(int id)
        //    {
        //        using (var conn = Connection)
        //        {
        //            conn.Open();
        //            using (var cmd = conn.CreateCommand())
        //            {
        //                cmd.CommandText = "DELETE FROM Coffee WHERE Id = @id";
        //                cmd.Parameters.AddWithValue("@id", id);

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
    }
}