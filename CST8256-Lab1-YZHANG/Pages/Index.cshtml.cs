using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

[BindProperties]
public class InputModel : PageModel
{
    public string NumberOne { get; set; } = string.Empty;
    public string NumberTwo { get; set; } = string.Empty;
    public string NumberThree { get; set; } = string.Empty;
    public string NumberFour { get; set; } = string.Empty;

    public string? StatisticsResult { get; private set; }

    public int counter = 0;

    // Validate input data
    List<string> fieldNames = new List<string> { "NumberOne", "NumberTwo", "NumberThree", "NumberFour" };
    private List<double> ValidateInput(List<string> inputs)
    {
        List<double> result = new();
        for (int i = 0; i < inputs.Count; i++)
        {
            if (!string.IsNullOrEmpty(inputs[i]))
            {
                double value;
                if (double.TryParse(inputs[i], out value))
                {
                    counter += 1;
                    result.Add(value);
                }
                else
                {
                    // Console.WriteLine($"the error for {fieldNames[i]}  to be added");
                    ModelState.AddModelError(fieldNames[i], "Must be a number");
                }
            }
        }
        return result;
    }

    public void OnPost()
    {
        counter = 0; // Reset counter on each submission

        // Collect inputs from bound properties
        List<string> userInputs = new List<string> {NumberOne, NumberTwo, NumberThree, NumberFour };

        List<double> doubles = ValidateInput(userInputs );

        // Handle error where fewer than two numbers are entered
        if (counter < 2)
        {
            ModelState.AddModelError(string.Empty, "Must enter at least two numbers.");
            return; // Return early if not enough valid inputs
        }

        //foreach (var entry in ModelState)
        //{
        //    foreach (var error in entry.Value.Errors)
        //    {
        //        Console.WriteLine($"Field: {entry.Key}, Error: {error.ErrorMessage}");
        //    }
        //}

        // Perform calculations on valid input
        StatisticsResult = $"You have entered {counter} numbers.<ul>" +
                           $"<li>Maximum is {doubles.Max()}</li>" +
                           $"<li>Minimum is {doubles.Min()}</li>" +
                           $"<li>Total is {doubles.Sum()}</li>" +
                           $"<li>Average is {doubles.Average()}</li>" +
                           "</ul>";
    }
}
