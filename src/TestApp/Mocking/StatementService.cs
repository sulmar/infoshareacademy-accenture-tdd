using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace TestApp.Mocking.Edu;

public interface IEmployeeRepository
{
    IQueryable<Employee> GetAll();
}


public interface IDateTime
{
    DateTime Now { get; }
}

public interface IMessageService
{
    void Send(string message);
}

public class SmtpMessageService : IMessageService
{
    public void Send(string message)
    {
        throw new NotImplementedException();
    }
}

public class RealDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;
}

public class DbEmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationContext db;
    public DbEmployeeRepository(ApplicationContext db)
    {
        this.db = db;
    }

    public IQueryable<Employee> GetAll()
    {
        return db.Employees;
    }
}



public class StatementService
{
    IEmployeeRepository employeeRepository;
    private readonly IDateTime dateTime;


    public StatementService(IEmployeeRepository employeeRepository, IDateTime dateTime)
    {
        this.employeeRepository = employeeRepository;
        this.dateTime = dateTime;
    }

    public bool SendEmails()
    {
        var employees = employeeRepository.GetAll();

        employees = employees.Where(e => !string.IsNullOrEmpty(e.Email));

        foreach (var employee in employees)
        {
            var statementFilename = SaveStatementReport(employee.FullName, dateTime.Now);

            try
            {
                EmailFile(employee.Email, $"Hello {employee.FullName}", statementFilename, "Your statement");
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", employee.Email), MessageBoxStyle.Error, MessageBoxButtons.Ok);

                return false;
            }
        }

        return true;

    }

    private static string SaveStatementReport(string name, DateTime statementDate)
    {
        var report = new StatementReport(name, statementDate);

        if (!report.HasData)
            return string.Empty;

        report.CreateDocument();

        var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            $"Statement {statementDate:yyyy-MM} {name}.pdf");


        report.ExportToPdf(filename);

        return filename;
    }

    private static void EmailFile(string emailAddress, string body, string filename, string subject)
    {
        SmtpOptions smtpOptions = new SmtpOptions();

        var client = new SmtpClient(smtpOptions.SmtpHost, smtpOptions.SmtpPort)
        {
            Credentials = new NetworkCredential(smtpOptions.SmtpUsername, smtpOptions.SmtpPassword)
        };

        var from = new MailAddress(smtpOptions.EmailFrom, smtpOptions.NameFrom);

        var to = new MailAddress(emailAddress);

        var message = new MailMessage(from, to)
        {
            Subject = subject,
            SubjectEncoding = Encoding.UTF8,
            Body = body,
            BodyEncoding =  Encoding.UTF8,
        };

        // Add attachment
        if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
        {
            Attachment attachment = new Attachment(filename);
            message.Attachments.Add(attachment);
        }

        client.Send(message);
        message.Dispose();
        File.Delete(filename);

    }

    public class StatementReport
    {
        public string Name { get; set; }
        public DateTime StatementDate { get; set; }

        public StatementReport(string name, DateTime statementDate)
        {
            Name = name;
            StatementDate = statementDate;
        }

        public bool HasData => !string.IsNullOrEmpty(Name);

        public void CreateDocument()
        {

        }

        public void ExportToPdf(string filename)
        {
            File.Create(filename);
        }
    }
}


public class Employee
{
    public string FullName { get; set; }
    public string Email { get; set; }

}

public enum MessageBoxButtons { Ok }
public enum MessageBoxStyle { Info, Error }
public class XtraMessageBox
{
    public static void Show(string title, string message, MessageBoxStyle messageBoxStyle, MessageBoxButtons messageBoxButtons)
    {

    }
}

public abstract class DbContext
{
}

public abstract class DbSet<T> : IQueryable<T>
    where T : class
{
    public Type ElementType => throw new NotImplementedException();

    public Expression Expression => throw new NotImplementedException();

    public IQueryProvider Provider => throw new NotImplementedException();

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class ApplicationContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
}

public class SmtpOptions
{
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
    public string EmailFrom { get; set; }
    public string NameFrom { get; set; }
}