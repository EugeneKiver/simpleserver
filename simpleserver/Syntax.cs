using System;
using System.Collections.Generic;

namespace LearnCSharp
{
    public class Syntax
    {
        // BASIC SYNTAX - skip to INTERESTING FEATURES if you have used Java or C++ before
        public static void syntax()
        {
            // Sbyte - Signed 8-bit integer
            // (-128 <= sbyte <= 127)
            sbyte fooSbyte = 100;

            // Byte - Unsigned 8-bit integer
            // (0 <= byte <= 255)
            byte fooByte = 100;

            // Short - 16-bit integer
            // Signed - (-32,768 <= short <= 32,767)
            // Unsigned - (0 <= ushort <= 65,535)
            short fooShort = 10000;
            ushort fooUshort = 10000;

            // Integer - 32-bit integer
            int fooInt = 1; // (-2,147,483,648 <= int <= 2,147,483,647)
            uint fooUint = 1; // (0 <= uint <= 4,294,967,295)

            // Long - 64-bit integer
            long fooLong = 100000L; // (-9,223,372,036,854,775,808 <= long <= 9,223,372,036,854,775,807)
            ulong fooUlong = 100000L; // (0 <= ulong <= 18,446,744,073,709,551,615)
            // Numbers default to being int or uint depending on size.
            // L is used to denote that this variable value is of type long or ulong

            // Double - Double-precision 64-bit IEEE 754 Floating Point
            double fooDouble = 123.4; // Precision: 15-16 digits

            // Float - Single-precision 32-bit IEEE 754 Floating Point
            float fooFloat = 234.5f; // Precision: 7 digits
            // f is used to denote that this variable value is of type float

            // Decimal - a 128-bits data type, with more precision than other floating-point types,
            // suited for financial and monetary calculations
            decimal fooDecimal = 150.3m;

            // Boolean - true & false
            bool fooBoolean = true; // or false

            // Char - A single 16-bit Unicode character
            char fooChar = 'A';

            // Strings -- unlike the previous base types which are all value types,
            // a string is a reference type. That is, you can set it to null
            string fooString = "\"escape\" quotes and add \n (new lines) and \t (tabs)";
            Console.WriteLine(fooString);

            // You can access each character of the string with an indexer:
            char charFromString = fooString[1]; // => 'e'
            // Strings are immutable: you can't do fooString[1] = 'X';

            // Compare strings with current culture, ignoring case
            string.Compare(fooString, "x", StringComparison.CurrentCultureIgnoreCase);

            // Formatting, based on sprintf
            string fooFs = string.Format("Check Check, {0} {1}, {0} {1:0.0}", 1, 2);

            // Dates & Formatting
            DateTime fooDate = DateTime.Now;
            Console.WriteLine(fooDate.ToString("hh:mm, dd MMM yyyy"));

            // Verbatim String
            // You can use the @ symbol before a string literal to escape all characters in the string
            string path = "C:\\Users\\User\\Desktop";
            string verbatimPath = @"C:\Users\User\Desktop";
            Console.WriteLine(path == verbatimPath); // => true

            // You can split a string over two lines with the @ symbol. To escape " use ""
            string bazString = @"Here's some stuff on a new line! ""Wow!"", the masses cried";

            // Use const or read-only to make a variable immutable
            // const values are calculated at compile time
            const int HoursWorkPerWeek = 9001;

            ///////////////////////////////////////////////////
            // Data Structures
            ///////////////////////////////////////////////////

            // Arrays - zero indexed
            // The array size must be decided upon declaration
            // The format for declaring an array is follows:
            // <datatype>[] <var name> = new <datatype>[<array size>];
            int[] intArray = new int[10];

            // Another way to declare & initialize an array
            int[] y = {9000, 1000, 1337};

            // Indexing an array - Accessing an element
            Console.WriteLine("intArray @ 0: " + intArray[0]);
            // Arrays are mutable.
            intArray[1] = 1;

            // Lists
            // Lists are used more frequently than arrays as they are more flexible
            // The format for declaring a list is follows:
            // List<datatype> <var name> = new List<datatype>();
            List<int> intList = new List<int>();
            List<string> stringList = new List<string>();
            List<int> z = new List<int> {9000, 1000, 1337}; // initialize
            // The <> are for generics - Check out the cool stuff section

            // Lists don't default to a value;
            // A value must be added before accessing the index
            intList.Add(1);
            Console.WriteLine("intList @ 0: " + intList[0]);

            // Others data structures to check out:
            // Stack/Queue
            // Dictionary (an implementation of a hash map)
            // HashSet
            // Read-only Collections
            // Tuple (.Net 4+)

            ///////////////////////////////////////
            // Operators
            ///////////////////////////////////////
            Console.WriteLine("\n->Operators");

            int i1 = 1, i2 = 2; // Shorthand for multiple declarations

            // Arithmetic is straightforward
            Console.WriteLine(i1 + i2 - i1 * 3 / 7); // => 3

            // Modulo
            Console.WriteLine("11%3 = " + (11 % 3)); // => 2

            // Comparison operators
            Console.WriteLine("3 == 2? " + (3 == 2)); // => false
            Console.WriteLine("3 != 2? " + (3 != 2)); // => true
            Console.WriteLine("3 > 2? " + (3 > 2)); // => true
            Console.WriteLine("3 < 2? " + (3 < 2)); // => false
            Console.WriteLine("2 <= 2? " + (2 <= 2)); // => true
            Console.WriteLine("2 >= 2? " + (2 >= 2)); // => true

            // Bitwise operators!

            //~       Unary bitwise complement
            //<<      Signed left shift
            //>>      Signed right shift
            //&       Bitwise AND
            //^       Bitwise exclusive OR
            //|       Bitwise inclusive OR


            // Incrementations
            int i = 0;
            Console.WriteLine("\n->Inc/Dec-rementation");
            Console.WriteLine(i++); //Prints "0", i = 1. Post-Incrementation
            Console.WriteLine(++i); //Prints "2", i = 2. Pre-Incrementation
            Console.WriteLine(i--); //Prints "2", i = 1. Post-Decrementation
            Console.WriteLine(--i); //Prints "0", i = 0. Pre-Decrementation

            ///////////////////////////////////////
            // Control Structures
            ///////////////////////////////////////
            Console.WriteLine("\n->Control Structures");

            // If statements are c-like
            int j = 10;
            if (j == 10)
            {
                Console.WriteLine("I get printed");
            }
            else if (j > 10)
            {
                Console.WriteLine("I don't");
            }
            else
            {
                Console.WriteLine("I also don't");
            }

            // Ternary operators
            // A simple if/else can be written as follows
            // <condition> ? <true> : <false>
            int toCompare = 17;
            string isTrue = toCompare == 17 ? "True" : "False";

            // While loop
            int fooWhile = 0;
            while (fooWhile < 100)
            {
                // Iterated 100 times, fooWhile 0->99
                fooWhile++;
            }

            // Do While Loop
            int fooDoWhile = 0;
            do
            {
                // Start iteration 100 times, fooDoWhile 0->99
                if (false)
                    continue; // skip the current iteration

                fooDoWhile++;

                if (fooDoWhile == 50)
                    break; // breaks from the loop completely

            } while (fooDoWhile < 100);

            // for loop structure => for(<start_statement>; <conditional>; <step>)
            for (int fooFor = 0; fooFor < 10; fooFor++)
            {
                // Iterated 10 times, fooFor 0->9
            }

            // For Each Loop
            // foreach loop structure => foreach(<iteratorType> <iteratorName> in <enumerable>)
            // The foreach loop loops over any object implementing IEnumerable or IEnumerable<T>
            // All the collection types (Array, List, Dictionary...) in the .Net framework
            // implement one or both of these interfaces.
            // (The ToCharArray() could be removed, because a string also implements IEnumerable)
            foreach (char character in "Hello World".ToCharArray())
            {
                // Iterated over all the characters in the string
            }

            // Switch Case
            // A switch works with the byte, short, char, and int data types.
            // It also works with enumerated types (discussed in Enum Types),
            // the String class, and a few special classes that wrap
            // primitive types: Character, Byte, Short, and Integer.
            int month = 3;
            string monthString;
            switch (month)
            {
                case 1:
                    monthString = "January";
                    break;
                case 2:
                    monthString = "February";
                    break;
                case 3:
                    monthString = "March";
                    break;
                // You can assign more than one case to an action
                // But you can't add an action without a break before another case
                // (if you want to do this, you would have to explicitly add a goto case x
                case 6:
                case 7:
                case 8:
                    monthString = "Summer time!!";
                    break;
                default:
                    monthString = "Some other month";
                    break;
            }

            ///////////////////////////////////////
            // Converting Data Types And Typecasting
            ///////////////////////////////////////

            // Converting data

            // Convert String To Integer
            // this will throw a FormatException on failure
            int.Parse("123"); // returns an integer version of "123"

            // try parse will default to type default on failure
            // in this case: 0
            int tryInt;
            if (int.TryParse("123", out tryInt)) // Function is boolean
                Console.WriteLine(tryInt); // 123

            // Convert Integer To String
            // Convert class has a number of methods to facilitate conversions
            Convert.ToString(123);
            // or
            tryInt.ToString();

            // Casting
            // Cast decimal 15 to an int
            // and then implicitly cast to long
            long x = (int) 15M;
        }
    }
}