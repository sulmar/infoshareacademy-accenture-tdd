using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TestApp.Fundamentals;

// Wzorzec Budowniczy (Builder)
public class ZplCommandBuilder
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

    public string Create()
    {
        commandBuilder.Append("^XZ");

        return commandBuilder.ToString();

    }
}

public interface IZplPrinter
{
    void Print(string content);
}

public class FakeZplPrinter : IZplPrinter
{
    public void Print(string content)
    {
        Console.WriteLine(content);
    }
}

public class TcpZplPrinter : IZplPrinter
{
    public void Print(string content)
    {
        throw new NotImplementedException();
    }
}

public class LaberyZplPrinter : IZplPrinter
{
    public void Print(string content)
    {
        TcpClient tcpClient = new TcpClient();
      //  tcpClient.Connect(ipAdress, port);

        var stream = new StreamWriter(tcpClient.GetStream());
        stream.Write(content);
        stream.Flush();
        stream.Close();
        tcpClient.Close();
    }


}
