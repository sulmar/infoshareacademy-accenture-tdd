using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.Mocking;

public class MessageProcessor
{
    private string[] whiteList;
    private readonly DbMessageRepository repository;
    public MessageProcessor(string[] whiteList, DbMessageRepository repository)
    {
        this.whiteList = whiteList;
        this.repository = repository;
    }

    public void Process(Message message)
    {
        if (!whiteList.Contains(message.From))
        {
            throw new InvalidOperationException();
        }

        if (!message.Title.Contains("Order"))
        {
            throw new FormatException();
        }

        var amount = decimal.Parse(message.Body);

        if (amount > 1000)
        {
            repository.Add(message);
        }        
    }
}

public class Message
{
    public string From { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }


    public Message(string from, string title, string body)
    {
        From = from;
        Title = title;
        Body = body;
    }       
}

public class DbMessageRepository
{
    private readonly List<Message> messages = new List<Message>();

    public void Add(Message message)
    {
        messages.Add(message);
    }
}
