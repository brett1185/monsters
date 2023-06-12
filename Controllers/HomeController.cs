// Using statements
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Monsters.Models;
namespace YourProjectName.Controllers;    
public class HomeController : Controller
{    
    private readonly ILogger<HomeController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;         
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public HomeController(ILogger<HomeController> logger, MyContext context)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;    
    }         
 

    [HttpPost("monsters/create")]
    public IActionResult CreateMonster(Monster newMon){
        if(ModelState.IsValid){
            _context.Add(newMon);
            _context.SaveChanges();
            return RedirectToAction("monsters/view");
        }
        else{
            return View("");
        }
    }

    [HttpGet("")]    
public IActionResult Index()    
{        
    // Get all Monsters
    List<Monster> AllMonsters = _context.Monsters.ToList();            
    
    // Get Monsters with the Name "Mike"
    ViewBag.AllMikes = _context.Monsters.Where(n => n.Name == "Mike");     	
    
    // Get the 5 most recently added Monsters        
    ViewBag.MostRecent = _context.Monsters.OrderByDescending(u => u.CreatedAt).Take(5).ToList(); 	
    
    // Get one Monster who has a certain description
    ViewBag.MatchedDesc = _context.Monsters.FirstOrDefault(u => u.Description == "Super scary");
    return View();  
}
[HttpPost("monsters/{MonsterId}/destroy")]
public IActionResult DestroyMonster(int MonsterId)
{
    Monster? MonToDelete = _context.Monsters.SingleOrDefault(i => i.MonsterId == MonsterId);
    // Once again, it could be a good idea to verify the monster exists before deleting
    _context.Monsters.Remove(MonToDelete);
    _context.SaveChanges();
    return RedirectToAction("Index");
}
[HttpGet("monsters/{MonsterId}/edit")]
public IActionResult EditMonster(int MonsterId)
{
    Monster? MonsterToEdit = _context.Monsters.FirstOrDefault(i => i.MonsterId == MonsterId);
    // Tip: it would be good to add a check here to ensure what you are grabbing will not return a null item
    return View(MonsterToEdit);
}
// 1. Trigger a post request that contains the updated instance from our form and the ID of that instance
[HttpPost("monsters/{MonsterId}/update")]
public IActionResult UpdateMonster(Monster newMon, int MonsterId)
{
    // 2. Find the old version of the instance in your database
    Monster? OldMonster = _context.Monsters.FirstOrDefault(i => i.MonsterId == MonsterId);
    // 3. Verify that the new instance passes validations
    if(ModelState.IsValid)
    {
        // 4. Overwrite the old version with the new version
    	// Yes, this has to be done one attribute at a time
    	OldMonster.Name = newMon.Name;
        OldMonster.LocatedAt = newMon.LocatedAt;
        OldMonster.LastSeen = newMon.LastSeen;
        OldMonster.Description = newMon.Description;
    	// You updated it, so update the UpdatedAt field!
        OldMonster.UpdatedAt = DateTime.Now;
    	// 5. Save your changes
        _context.SaveChanges();
    	// 6. Redirect to an appropriate page
        return RedirectToAction("Index");
    } else {
    	// 3.5. If it does not pass validations, show error messages
    	// Be sure to pass the form back in so you don't lose your changes
        // It should be the old version so we can keep the ID
        return View("EditMonster", OldMonster);
    }
}

}