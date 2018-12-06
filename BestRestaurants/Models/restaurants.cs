using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
  public class Restaurant
  {
    private string _rName;
    private int _id;
    private string _city;
    private string _state;
    private string _bestFood;
    private int _cuisine_id;


    public Restaurant (string name, bestFood, string city, string state, int cuisineId, int id = 0)
    {
      _rName = name;
      _city = city;
      _state = state;
      _bestFood = bestFood;
      _cuisine_id = cuisineId;
      _id = id;

    }

    public string GetName()
    {
      return _rName;
    }

    public void SetName(string newName)
    {
      _rName = newName;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetCity()
    {
      return _city;
    }

    public int GetState()
    {
      return _state;
    }

    public string GetBestFood()
    {
      return _bestFood;
    }

    public int GetCuisineId()
    {
      return _cuisine_id;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantCity = rdr.GetString(2);
        string restaurantState = rdr.GetString(3);
        string restaurantBestFood = rdr.GetString(4);
        int cuisineId = rdr.GetInt32(5);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantBestFood, restaurantCity, restaurantState, restaurantId, cuisineId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Restaurant> GetAllRestaurants(int id)
    {
      List<Restaurant> restaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = '" + id + "';";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantBestFood = rdr.GetString(2);
        string restaurantCity = rdr.GetString(3);
        string restaurantState = rdr.GetString(4);
        int cuisineId = rdr.GetInt32(5);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantBestFood, restaurantCity, restaurantState, cuisineId, restaurantId);
        restaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return restaurants;
    }

    public static void DeleteRestaurants(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurants WHERE id = (@thisId);";

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

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurants WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int restaurantId = 0;
      string restaurantName = "";
      string restaurantBestFood = "";
      string restaurantCity = "";
      string restaurantState = "";
      int cuisineId = 0;
      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantBestFood = rdr.GetString(2);
        string restaurantCity = rdr.GetString(3);
        string restaurantState = rdr.GetString(4);
        int cuisineId = rdr.GetInt32(5);
      }
      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantBestFood, restaurantCity, restaurantState, cuisineId, restaurantId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool bestFoodEquality = this.GetBestFood() == newRestaurant.GetBestFood();
        bool cityEquality = this.GetCity() == newRestaurant.GetCity();
        bool stateEquality = this.GetState() == newRestaurant.GetState();
        bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
        return (idEquality && nameEquality && bestFoodEquality && cityEquality && stateEquality && cuisineEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurants (name, bestFood, city, state, cuisine_id) VALUES (@name, @bestFood, @city, @state, @cuisine_id);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._rName;
      cmd.Parameters.Add(name);
      MySqlParameter bestFood = new MySqlParameter();
      bestFood.ParameterName = "@bestFood";
      bestFood.Value = this._bestFood;
      cmd.Parameters.Add(bestFood);
      MySqlParameter city = new MySqlParameter();
      city.ParameterName = "@city";
      city.Value = this._city;
      cmd.Parameters.Add(city);
      MySqlParameter state = new MySqlParameter();
      state.ParameterName = "@state";
      state.Value = this._state;
      cmd.Parameters.Add(state);
      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@cuisine_id";
      cuisineId.Value = this._cuisine_id;
      cmd.Parameters.Add(cuisineId);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurants SET name = @newName WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = _rName;
      cmd.Parameters.Add(name);
      cmd.ExecuteNonQuery();
      _rName = newName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
