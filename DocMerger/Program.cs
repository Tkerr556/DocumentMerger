using System;
using System.IO;
using System.Text;
using System.Data;

namespace DocumentMerger
{
    class Program
    {

        static void runOnce()
        {
            Console.WriteLine("Welcome to Document Merger:   ");
            Console.WriteLine();
            String first = ""; // first filename variab
            String second = ""; // second filename variab
            bool firstPrompt = true; // indicated whether prompted for the first time
            do
            {
                if (firstPrompt) // do not display "Invalid filename" if this is the first prompt
                {
                    firstPrompt = false; // disable first prompt
                }
                else
                {
                    Console.WriteLine("Invalid filename."); //"Invalid filename"
                }
                Console.WriteLine("Please enter the name of the first text file to be merged:"); // prompt for first filename
                first = Console.ReadLine(); // read first filename
            } while (first.Length > 0 && !File.Exists(first)); // check if first file exists
            firstPrompt = true; // set first prompt to true for second filename input
            do
            {
                if (firstPrompt) // do not display "Invalid filename" if this is the first prompt
                {
                    firstPrompt = false; // disable first prompt
                }
                else
                {
                    Console.WriteLine("Invalid filename."); // print "Invalid filename"
                }
                Console.WriteLine("Please enter the name of the second text file to be merged:"); // prompt for second filename
                second = Console.ReadLine(); // read second filename
            } while (second.Length > 0 && !File.Exists(second));

            String merged = first.Substring(0, first.Length - 4) + second.Substring(0, second.Length - 4) + ".txt"; // merged filename variable

            StreamWriter sw = null; // Stream for writing into merged
            StreamReader sr1 = null; // Stream for reading from first
            StreamReader sr2 = null; // Stream for reading from second

            bool success = false; // indicates whether merging was successful

            int count = 0; // counts the number of characters

            try
            {
                sw = new StreamWriter(merged);
                sr1 = new StreamReader(first);
                sr2 = new StreamReader(second);

                String line = sr1.ReadLine();
                while (line != null) // loop until end of file
                {
                    sw.WriteLine(line); // write line into merged
                    count += line.Length; // update total number of characters
                    line = sr1.ReadLine(); // next linee
                }
                line = sr2.ReadLine(); // set line to first line in the second file
                while (line != null) // loop until end of file
                {
                    sw.WriteLine(line); // write line into merged
                    count += line.Length; // update total number of characters
                    line = sr2.ReadLine(); // read the next line
                }
                success = true; // merging successful
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // print error message
            }
            finally
            {
                if (sw != null)
                    sw.Close(); // close merged
                if (sr1 != null)
                    sr1.Close(); // close first
                if (sr2 != null)
                    sr2.Close(); // close second
                if (success)
                    Console.WriteLine(merged + " was successfully saved. The document contains " + count + " characters."); // print successful message
            }
        }

        static void Main(string[] args)
        {
            do
            {
                runOnce();
                Console.WriteLine("Would you like to merge two more files? (y/n)"); // prompt for running again
                char c = Console.ReadLine()[0]; // reading first character
                if (c == 'n') // checking for 'no' option
                    break; // break the loop
            } while (true); // run again
        }
    }
}