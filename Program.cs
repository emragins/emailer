using System;
using System.Text;

namespace EmailTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Console.ReadLine();
        }

        private static void Test1()
        {
            string name = "AlertUsers Sync With NO Dispose";
            try
            {
                MyLogger.Info(name + ": Starting...");
                var files = new FileManager();
                string myFile = files.CreateTestFile();

                var emailer = new Emailer();
                emailer.AlertUsers(GetEmails, name, "Hello, email friend", new[] { myFile }, false);

                files.DeleteFile(myFile);
                MyLogger.Info(name + ": Successful!");
            }
            catch (Exception e)
            {
                MyLogger.Error(name + ":  Error", e);
            }
        }

        private static void Test2()
        {
            string name = "AlertUsers Sync With Dispose";
            try
            {
                MyLogger.Info(name + ": Starting...");
                var files = new FileManager();
                string myFile = files.CreateTestFile();

                var emailer = new Emailer();
                emailer.AlertUsers(GetEmails, name, "Hello, email friend", new[] { myFile });

                files.DeleteFile(myFile);
                MyLogger.Info(name + ": Successful!");
            }
            catch (Exception e)
            {
                MyLogger.Error(name + ":  Error", e);
            }
        }

        private static void Test3()
        {
            string name = "AlertUsers Async With NO Dispose";
            try
            {
                MyLogger.Info(name + ": Starting...");
                var files = new FileManager();
                string myFile = files.CreateTestFile();

                var emailer = new Emailer();
                emailer.AlertUsersAsync(GetEmails, name, "Hello, email friend", new[] { myFile }, false);

                files.DeleteFile(myFile);
                MyLogger.Info(name + ": Successful!");
            }
            catch (Exception e)
            {
                MyLogger.Error(name + ":  Error", e);
            }
        }

        private static void Test4()
        {
            string name = "AlertUsers Async With Dispose";
            try
            {
                MyLogger.Info(name + ": Starting...");
                var files = new FileManager();
                string myFile = files.CreateTestFile();

                var emailer = new Emailer();
                emailer.AlertUsersAsync(GetEmails, name, "Hello, email friend", new[] { myFile });

                files.DeleteFile(myFile);
                MyLogger.Info(name + ": Successful!");
            }
            catch (Exception e)
            {
                MyLogger.Error(name + ":  Error", e);
            }
        }


        static string[] GetEmails()
        {
            return new[] { "user@todomain.com" };
        }
    }
}
