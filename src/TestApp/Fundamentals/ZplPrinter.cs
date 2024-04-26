using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals;

public class ZplPrinter
{
    private readonly StringBuilder commandBuilder = new StringBuilder();

    public void CreateLabel(int width, int height)
    {
        commandBuilder.Append("^XA");
    }

    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentNullException("text");

        commandBuilder.Append("^FD");
        commandBuilder.Append(text);
        commandBuilder.Append("^FS");
    }


    public void SetPosition(int x, int y)
    {
        commandBuilder.Append($"^FO{x},{y}");
    }

    public string Output
    {
        get
        {
            commandBuilder.Append("^XZ");

            return commandBuilder.ToString();
        }
    }


}
