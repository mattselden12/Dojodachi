using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dojodachi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
		[HttpGet]
        public IActionResult Index()
        {
			if(HttpContext.Session.GetString("startsession") == null)
			{
				Dachi this_dachi = new Dachi(20,20,3,50);
				HttpContext.Session.SetObjectAsJson("myDachi", this_dachi);
				HttpContext.Session.SetString("startsession", "true");
				TempData["Pic"] = "Start.gif";
				ViewBag.Response = "Look at your cute new Dojodachi!";
			}
			Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("myDachi");
			if(myDachi.Fullness <= 0 || myDachi.Happiness <= 0)
			{
				ViewBag.Pic = "Died.gif";
				ViewBag.Response = "Your Dojodachi has passed away...";
			}
			else if(myDachi.Energy>100 && myDachi.Fullness>100 && myDachi.Happiness>100)
			{
				ViewBag.Pic = "Won.gif";
				ViewBag.Response = "Congratulations! You Won!";
			}
			else{
				ViewBag.Response = TempData["Response"];
				ViewBag.Pic = TempData["Pic"];
			}
            return View(myDachi);
        }

		[HttpGet("feed")]
		public IActionResult Feed()
		{
			Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("myDachi");
			string response = myDachi.Feed();
			Console.WriteLine(myDachi.Fullness);
			HttpContext.Session.SetObjectAsJson("myDachi", myDachi);
			TempData["Response"] = response;
			TempData["Pic"] = "Eat.gif";
			return RedirectToAction("Index");
		}

		[HttpGet("play")]
		public IActionResult Play()
		{
			Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("myDachi");
			string response = myDachi.Play();
			HttpContext.Session.SetObjectAsJson("myDachi", myDachi);
			TempData["Response"] = response;
			TempData["Pic"] = "Play.gif";
			return RedirectToAction("Index");
		}

		[HttpGet("work")]
		public IActionResult Work()
		{
			Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("myDachi");
			string response = myDachi.Work();
			HttpContext.Session.SetObjectAsJson("myDachi", myDachi);
			TempData["Response"] = response;
			TempData["Pic"] = "Work.gif";
			return RedirectToAction("Index");
		}

		[HttpGet("sleep")]
		public IActionResult Sleep()
		{
			Dachi myDachi = HttpContext.Session.GetObjectFromJson<Dachi>("myDachi");
			string response = myDachi.Sleep();
			HttpContext.Session.SetObjectAsJson("myDachi", myDachi);
			TempData["Response"] = response;
			TempData["Pic"] = "Sleep.gif";
			return RedirectToAction("Index");
		}

		[HttpGet("restart")]
		public IActionResult Restart()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}
    }
}
























namespace Dojodachi
{
	public static class SessionExtensions
	{
		public static void SetObjectAsJson(this ISession session, string key, object value)
		{
			session.SetString(key, JsonConvert.SerializeObject(value));
		}
		public static T GetObjectFromJson<T>(this ISession session, string key)
		{
			string value = session.GetString(key);
			return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
		}
	}
}