namespace TestApp.Fundamentals;

// Boundary Test
public class ColorConverter // Adapter Design Pattern
{
    public static string ConvertColor(int percentage)
    {
        // Thresholds
        if (percentage >= 0 && percentage < 50)
        {
            return "Green";
        }
        else if (percentage >= 50 && percentage <= 80)
        {
            return "Yellow";
        }
        else
        {
            return "Red";
        }
    }
}
