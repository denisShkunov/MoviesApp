using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters;

public class ActorAge : Attribute, IActionFilter
{
    private readonly int minAge;
    private readonly int maxAge;
    public ActorAge(int minAge, int maxAge)
    {
        this.minAge = minAge;
        this.maxAge = maxAge;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            var age = DateTime.Parse(context.HttpContext.Request.Form["Birthday"]).Year;
            if (DateTime.Now.Year - age < minAge || DateTime.Now.Year - age > maxAge) 
                context.Result = new BadRequestResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
    }
        
    public void OnActionExecuted(ActionExecutedContext context)
    {
           
    }
        
}