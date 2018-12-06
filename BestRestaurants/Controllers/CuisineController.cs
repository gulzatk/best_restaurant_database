using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
  public class CuisinesController : Controller
  {

    [HttpGet("/cuisines")]
    public ActionResult Index()
    {
      List<Cuisine> allCuisines = Cuisine.GetAll();
      return View(allCuisines);
    }

    [HttpGet("/cuisines/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/cuisines")]
    public ActionResult Create(string cuisineName)
    {
      Cuisine newCuisine = new Cuisine(cuisineName);
      newCuisine.Save();

      List<Cuisine> allCuisines = Cuisine.GetAll();
      return View("Index", allCuisines);
    }

    [HttpGet("/cuisines/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine selectedCuisine = Cuisine.Find(id);
      List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
      model.Add("cuisine", selectedCuisine);
      model.Add("restaurants", cuisineRestaurants);
      return View(model);
    }

    [HttpGet("/cuisines/{id}/delete")]
     public ActionResult Delete(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine cuisine = Cuisine.Find(id);
      List<Restaurant> cuisineRestaurants = cuisine.GetRestaurants();

      foreach(Restaurant restaurant in cuisineRestaurants)
      {
        Restaurant.DeleteRestaurants(restaurant.GetId());
      }

      Cuisine.DeleteCuisine(id);

      model.Add("cuisine", cuisine);
      model.Add("restaurants", cuisineRestaurants);
      return View("Delete", model);

    }


    //This one creates new Restaurants within a given Cuisine, not new Cuisines:
    [HttpPost("/cuisines/{cuisineId}/restaurants")]
    public ActionResult Create(string restaurantName, int restaurantRating, int cuisineId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine foundCuisine = Cuisine.Find(cuisineId);
      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantRating, cuisineId);
      newRestaurant.Save();
      List<Restaurant> cuisineRestaurants = foundCuisine.GetRestaurants();
      model.Add("restaurants", cuisineRestaurants);
      model.Add("cuisine", foundCuisine);
      return View("Show", model);
    }

    [HttpGet("/cuisines/{cuisineId}/restaurants/{restaurantId}/delete")]
     public ActionResult Delete(int cuisineId, int restaurantId)
     {
       Restaurant restaurant = Restaurant.Find(restaurantId);
       Restaurant.DeleteRestaurants(restaurant.GetId());
       Dictionary<string, object> model = new Dictionary<string, object>();
       Cuisine cuisine = Cuisine.Find(cuisineId);
       List<Restaurant> cuisineRestaurants = cuisine.GetRestaurants();
       model.Add("cuisine", cuisine);
       model.Add("restaurants", cuisineRestaurants);
       return View("Show", model);

     }



  }
}
