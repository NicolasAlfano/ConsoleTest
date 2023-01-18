using ConsoleTest.Utilities;

namespace ConsoleTest;
class Program
{
    static void Main(string[] args)
    {
        // String line;
        try
        {   
            //
            string path = "/Users/nicolasalfano/Projects/ConsoleTest/students.txt";
            Console.WriteLine("Start");
            TxtReaderUtilities readTxt = new TxtReaderUtilities(path);
            GetDataUtilities data = new GetDataUtilities(readTxt);
            data.GetData();
            
            Console.WriteLine("Finish");
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Finally block.");
        }
    }
}
