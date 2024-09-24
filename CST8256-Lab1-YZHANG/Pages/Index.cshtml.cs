using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging; // Add this for logging

// set up  a plain data model for the form that accepts four numbers
public class InputModel
{
    public string? NumberOne { get; set; }

    public string? NumberTwo { get; set; }

 
    public string? NumberThree { get; set; }

    
    public string? NumberFour { get; set; }

}
public class IndexModel : PageModel
{
    // Using the BindProperty directive corresponding data from page form is bind to an instance of the input model

    [BindProperty]
    public InputModel InputModel { get; set; } = new InputModel();

    public string? StatisticsResult { get; private set; }


    // validate input data
    public void OnPost()
    {

        
        // implement custom validation logic
        // first check that at least two numbers are entered
        double one = 0.0;
        double two = 0.0;
        double three = 0.0;
        double four = 0.0;
        // use list instead of array for unknown size of enums
        List<double> doubles = new List<double>();
        int counter = 0;
        // NOTE: a generic method with three parameters can be used to dynamically 
        // validate input method(input string, output double, errorMessage string)

        if (double.TryParse(InputModel.NumberOne, out one)) 
        {
            counter++;
            doubles.Add(one);
        }
        else
        {
            ModelState.AddModelError(nameof(InputModel.NumberOne), "Must be a number");
        }
        if (double.TryParse(InputModel.NumberTwo, out two))
        {
            counter++;
            doubles.Add(two);
        }
        else
        {
            ModelState.AddModelError(nameof(InputModel.NumberTwo), "Must be a number");
        }
        if (double.TryParse (InputModel.NumberThree, out three))
        {
            counter++;
            doubles.Add(three);
        }
        else
        {
            ModelState.AddModelError(nameof(InputModel.NumberThree), "Must be a number");
        }
        if (double.TryParse(InputModel.NumberFour, out four))
        {
            counter++;
            doubles.Add(four);
        }
        else
        {
            ModelState.AddModelError(nameof(InputModel.NumberFour), "Must be a number");
        }
        // TODO : has to be a way NOT to hard code the counter logic

        // debugger CONFIRMED correct key value binding for each field
        foreach (var entry in ModelState)
        {
            foreach (var error in entry.Value.Errors)
            {
                Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
            }
        }

        // debugger end

        // debugger ModelState is passed to view correctly 
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is invalid");  // This should print when there's an error.
        }

        // handle error where entire form has fewer than two numbers
        if (counter <2)
        {
            ModelState.AddModelError(String.Empty, "Must enter at least two numbers.");

            return;
        }

        

        // do calculation on valid input
        else
        {

            StatisticsResult = $"You have entered {counter} numbers.<ul>" +
            $"<li>Maximum is {doubles.Max()}</li>" +
            $"<li>Minimum is {doubles.Min()}</li>" +
            $"<li>Total is {doubles.Sum()}</li>" +
            $"<li>Average is {doubles.Average()}</li>" +
            "</ul>";

        }



    }

}



