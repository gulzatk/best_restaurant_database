using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
    public class Cuisine
    {
      private string _name;
      private int _id;

      public Cuisine(string name, int id = 0)
      {
        _id = id;
        _name = name;
      }

      public string GetName()
      {
        return _name;
      }

      public int GetId()
      {
        return _id;
      }

      public static void ClearAll()
      {
         MySqlConnection conn = DB.Connection();
         conn.Open();
         var cmd = conn.CreateCommand() as MySqlCommand;
         cmd.CommandText = @"DELETE FROM cuisines;";
         cmd.ExecuteNonQuery();
         conn.Close();
         if (conn != null)
         {
           conn.Dispose();
         }
       }

      public static List<Cuisine> GetAll()
      {
        List<Cuisine> allCuisine = new List<Cuisine> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM cuisines;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int CuisineId = rdr.GetInt32(0);
          string CuisineName = rdr.GetString(1);
          Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
          allCuisine.Add(newCuisine);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allCuisine;
      }

      public static Cuisine Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();
     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM cuisines WHERE id = (@searchId);";
     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);
     var rdr = cmd.ExecuteReader() as MySqlDataReader;
     int cuisineId = 0;
     string cuisineName = "";
     while(rdr.Read())
     {
       cuisineId = rdr.GetInt32(0);
       cuisineName = rdr.GetString(1);
     }
     Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
     conn.Close();
     if (conn != null)
     {
       conn.Dispose();
     }
     return newCuisine;
    }

    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = @cuisine_id;";
      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@cuisine_id";
      cuisineId.Value = this._id;
      cmd.Parameters.Add(cuisineId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        int restaurantRating = rdr.GetInt32(2);
        int restaurantCuisineId = rdr.GetInt32(3);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantRating, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

      public override bool Equals(System.Object otherCuisine)
      {
        if (!(otherCuisine is Cuisine))
        {
          return false;
        }
        else
        {
          Cuisine newCuisine = (Cuisine) otherCuisine;
          bool idEquality = this.GetId().Equals(newCuisine.GetId());
          bool nameEquality = this.GetName().Equals(newCuisine.GetName());
          return (idEquality && nameEquality);
        }
      }

      public void Save()
     {
       MySqlConnection conn = DB.Connection();
       conn.Open();
       var cmd = conn.CreateCommand() as MySqlCommand;
       cmd.CommandText = @"INSERT INTO cuisines (name) VALUES (@name);";
       MySqlParameter name = new MySqlParameter();
       name.ParameterName = "@name";
       name.Value = this._name;
       cmd.Parameters.Add(name);
       cmd.ExecuteNonQuery();
       _id = (int) cmd.LastInsertedId;
       conn.Close();
       if (conn != null)
       {
         conn.Dispose();
       }
    }

        public static void DeleteCuisine(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisines WHERE id = (@thisId);";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }
}
}
