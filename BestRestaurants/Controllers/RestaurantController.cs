using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
using System.Collections.Generic;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller
  {

    [HttpGet("/cuisines/{cuisineId}/restaurants/new")]
    public ActionResult New(int cuisineId)
    {
     Cuisine cuisine = Cuisine.Find(cuisineId);
     return View(cuisine);
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/")]
    public ActionResult Index(int cuisineId)
    {
      List<Restaurant> allCuisineRestaurants = Restaurant.GetAllRestaurants(cuisineId);
      return View(allCuisineRestaurants);
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}")]
    public ActionResult Show(int cuisineId, int restaurantId)
    {
      Restaurant restaurant = Restaurant.Find(restaurantId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("restaurant", restaurant);
      model.Add("cuisine", cuisine);
      return View(model);
    }

    [HttpPost("/restaurants/delete")]
    public ActionResult DeleteAll()
    {
      Restaurant.ClearAll();
      return View();
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}/edit")]
    public ActionResult Edit(int cuisineId, int restaurantId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("cuisine", cuisine);
      Restaurant restaurant = Restaurant.Find(restaurantId);
      model.Add("restaurant", restaurant);
      return View(model);
    }

    [HttpPost("/cuisines/{cuisineId}/restaurants/{restaurantId}")]
    public ActionResult Update(int cuisineId, int restaurantId, string name)
    {
      Restaurant restaurant = Restaurant.Find(restaurantId);
      restaurant.Edit(name);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(cuisineId);
      model.Add("cuisine", cuisine);
      model.Add("restaurant", restaurant);
      return View("Show", model);
    }


  }
}
