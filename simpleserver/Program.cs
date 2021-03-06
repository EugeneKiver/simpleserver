﻿// Specify the namespaces this source code will be using
// The namespaces below are all part of the standard .NET Framework Class Library
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

// But this one is not:
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
// In order to be able to use it, you need to add a dll reference
// This can be done with the NuGet package manager: `Install-Package EntityFramework`

// Namespaces define scope to organize code into "packages" or "modules"
// Using this code from another source file: using Learning.CSharp;
namespace simpleserver
{
    // Each .cs file should at least contain a class with the same name as the file.
    // You're allowed to do otherwise, but shouldn't for sanity.
    public class Simpleserver
    {
        ///////////////////////////////////////
        // CLASSES - see definitions at end of file
        ///////////////////////////////////////
        public static void Classes()
        {
            // See Declaration of objects at end of file

            // Use new to instantiate a class
            Bicycle trek = new Bicycle();

            // Call object methods
            trek.SpeedUp(3); // You should always use setter and getter methods
            trek.Cadence = 100;

            // ToString is a convention to display the value of this Object.
            Console.WriteLine("trek info: " + trek.Info());

            // Instantiate a new Penny Farthing
            PennyFarthing funbike = new PennyFarthing(1, 10);
            Console.WriteLine("funbike info: " + funbike.Info());

            //Console.Read();
            
            OtherInterestingFeatures();
        }

        //
        // INTERESTING FEATURES
        //

        // DEFAULT METHOD SIGNATURES

        public // Visibility
        static // Allows for direct call on class without object
        int // Return Type,
        MethodSignatures(
            int maxCount, // First variable, expects an int
            int count = 0, // will default the value to 0 if not passed in
            int another = 3,
            params string[] otherParams // captures all other parameters passed to method
        )
        {
            return -1;
        }

        // Methods can have the same name, as long as the signature is unique
        // A method that differs only in return type is not unique
        public static void MethodSignatures(
            ref int maxCount, // Pass by reference
            out int count)
        {
            // the argument passed in as 'count' will hold the value of 15 outside of this function
            count = 15; // out param must be assigned before control leaves the method
        }

        // GENERICS
        // The classes for TKey and TValue is specified by the user calling this function.
        // This method emulates the SetDefault of Python
        public static TValue SetDefault<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultItem)
        {
            TValue result;
            if (!dictionary.TryGetValue(key, out result))
                return dictionary[key] = defaultItem;
            return result;
        }

        // You can narrow down the objects that are passed in
        public static void IterateAndPrint<T>(T toPrint) where T : IEnumerable<int>
        {
            // We can iterate, since T is a IEnumerable
            foreach (var item in toPrint)
                // Item is an int
                Console.WriteLine(item.ToString());
        }

        // YIELD
        // Usage of the "yield" keyword indicates that the method it appears in is an Iterator
        // (this means you can use it in a foreach loop)
        
        /// <summary>
        /// This is an XML documentation comment which can be used to generate external
        /// documentation or provide context help within an IDE
        /// </summary>
        /// <param name="limit">This is some parameter documentation for firstParam</param>
        /// <returns>Information on the returned value of a function</returns>
        public static IEnumerable<int> YieldCounter(int limit = 10)
        {
            for (var i = 0; i < limit; i++)
            {
                yield return i;
            }
        }

        // which you would call like this :
        public static void PrintYieldCounterToConsole()
        {
            foreach (var counter in ManyYieldCounter())
                Console.WriteLine(counter);
        }

        // you can use more than one "yield return" in a method
        public static IEnumerable<int> ManyYieldCounter()
        {
            yield return 0;
            yield return 1;
            yield return 2;
            yield return 3;
        }

        // you can also use "yield break" to stop the Iterator
        // this method would only return half of the values from 0 to limit.
        public static IEnumerable<int> YieldCounterWithBreak(int limit = 10)
        {
            for (var i = 0; i < limit; i++)
            {
                if (i > limit / 2) yield break;
                yield return i;
            }
        }

        public static void OtherInterestingFeatures()
        {
            // OPTIONAL PARAMETERS
            MethodSignatures(3, 1, 3, "Some", "Extra", "Strings");
            MethodSignatures(3, another: 3); // explicitly set a parameter, skipping optional ones

            // BY REF AND OUT PARAMETERS
            int maxCount = 0, count; // ref params must have value
            MethodSignatures(ref maxCount, out count);

            // EXTENSION METHODS
            int i = 3;
            i.Print(); // Defined below
            
            // NULLABLE TYPES - great for database interaction / return values
            // any value type (i.e. not a class) can be made nullable by suffixing a ?
            // <type>? <var name> = <value>
            int? nullable = null; // short hand for Nullable<int>
            Console.WriteLine("Nullable variable: " + nullable);
            bool hasValue = nullable.HasValue; // true if not null

            // ?? is syntactic sugar for specifying default value (coalesce)
            // in case variable is null
            int notNullable = nullable ?? 0; // 0

            // ?. is an operator for null-propagation - a shorthand way of checking for null
            nullable?.Print(); // Use the Print() extension method if nullable isn't null

            // IMPLICITLY TYPED VARIABLES - you can let the compiler work out what the type is:
            var magic = "magic is a string, at compile time, so you still get type safety";
            // magic = 9; will not work as magic is a string, not an int

            // GENERICS
            //
            var phonebook = new Dictionary<string, string>() {
                {"Sarah", "212 555 5555"} // Add some entries to the phone book
            };

            // Calling SETDEFAULT defined as a generic above
            Console.WriteLine(SetDefault<string, string>(phonebook, "Shaun", "No Phone")); // No Phone
            // nb, you don't need to specify the TKey and TValue since they can be
            // derived implicitly
            Console.WriteLine(SetDefault(phonebook, "Sarah", "No Phone")); // 212 555 5555

            // LAMBDA EXPRESSIONS - allow you to write code in line
            Func<int, int> square = (x) => x * x; // Last T item is the return value
            Console.WriteLine(square(3)); // 9

            // ERROR HANDLING - coping with an uncertain world
            try
            {
                var funBike = PennyFarthing.CreateWithGears(6);
                // will no longer execute because CreateWithGears throws an exception
                string some = "";
                if (true) some = null;
                some.ToLower(); // throws a NullReferenceException
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Not so much fun now!");
            }
            catch (Exception ex) // catch all other exceptions
            {
                //throw new ApplicationException("It hit the fan", ex);
                // throw; // A rethrow that preserves the callstack
            }
            // catch { } // catch-all without capturing the Exception
            finally
            {
                // executes after try or catch
            }

            // DISPOSABLE RESOURCES MANAGEMENT - let you handle unmanaged resources easily.
            // Most of objects that access unmanaged resources (file handle, device contexts, etc.)
            // implement the IDisposable interface. The using statement takes care of
            // cleaning those IDisposable objects for you.
            using (StreamWriter writer = new StreamWriter("log.txt"))
            {
                writer.WriteLine("Nothing suspicious here");
                // At the end of scope, resources will be released.
                // Even if an exception is thrown.
            }

            // PARALLEL FRAMEWORK
            // https://devblogs.microsoft.com/csharpfaq/parallel-programming-in-net-framework-4-getting-started/

            var words = new List<string> { "dog", "cat", "horse", "pony" };

            Parallel.ForEach(words,
                new ParallelOptions() { MaxDegreeOfParallelism = 4 },
                word =>
                {
                    Console.WriteLine(word);
                }
            );

            // Running this will produce different outputs
            // since each thread finishes at different times.
            // Some example outputs are:
            // cat dog horse pony
            // dog horse pony cat

            // DYNAMIC OBJECTS (great for working with other languages)
            dynamic student = new ExpandoObject();
            student.FirstName = "First Name"; // No need to define class first!

            // You can even add methods (returns a string, and takes in a string)
            student.Introduce = new Func<string, string>(
                (introduceTo) => string.Format("Hey {0}, this is {1}", student.FirstName, introduceTo));
            Console.WriteLine(student.Introduce("Beth"));

            // IQUERYABLE<T> - almost all collections implement this, which gives you a lot of
            // very useful Map / Filter / Reduce style methods
            var bikes = new List<Bicycle>();
            
            /*
             * this.Gear = 1; // you can access members of the object with the keyword this
             * Cadence = 50;  // but you don't always need it
             * _speed = 5;
             * Name = "Bontrager";
             * Brand = BikeBrand.AIST;
             * BicyclesCreated++;
             */
            bikes.Add(new Bicycle(5, 1, Bicycle.BikeBrand.Electra));
            bikes.Add(new Bicycle(5, 2, Bicycle.BikeBrand.Gitane));
            bikes.Add(new Bicycle(5, 3, Bicycle.BikeBrand.BMC));
            //Console.Write("bikes[1].Brand l.274 " + bikes[1].Brand + "\n");
            //bikes.Sort(); // Sorts the array
            //Console.Write("bikes[1].Brand l.276 " + bikes[1].Brand + "\n");

            bikes.Sort((b1, b2) => b1.Brand.CompareTo(b2.Brand)); // Sorts based on brand
            //Console.Write("bikes[1].Brand l.279 " + bikes[1].Brand + "\n");
            var result = bikes
                .Where(b => b.Brand == Bicycle.BikeBrand.BMC) // Filters - chainable (returns IQueryable of previous type)
                .Where(b => !b.IsBroken && !b.HasTassles)
                .Select(b => b.ToString()); // Map - we only this selects, so result is a IQueryable<string>
            //Console.Write("bikes[1].Brand l.284 " + bikes[1].Brand + "\n");

            //foreach (Bicycle bike in result) {Console.Write("Bike " + bike);}

            //var sum = bikes.Sum(b => b.Wheels); // Reduce - sums all the wheels in the collection
                
            //Console.Write("bikes[1].Brand l.290 " + bikes[1].Brand + "\n");

            // Create a list of IMPLICIT objects based on some parameters of the bike
            var bikeSummaries = bikes.Select(b => new { Name = b.Name, IsAwesome = !b.IsBroken && b.HasTassles });
            // Hard to show here, but you get type ahead completion since the compiler can implicitly work
            // out the types above!
            foreach (var bikeSummary in bikeSummaries.Where(b => b.IsAwesome))
                Console.WriteLine(bikeSummary.Name);
            //Console.Write("bikes[1].Brand l.298 " + bikes[1].Brand + "\n");

            // ASPARALLEL
            // And this is where things get wicked - combine linq and parallel operations
            var threeWheelers = bikes.AsParallel().Where(b => b.Wheels == 3).Select(b => b.Name);
            // this will happen in parallel! Threads will automagically be spun up and the
            // results divvied amongst them! Amazing for large datasets when you have lots of
            // cores

            // LINQ - maps a store to IQueryable<T> objects, with delayed execution
            // e.g. LinqToSql - maps to a database, LinqToXml maps to an xml document
            
            /* Working with db will be better to learn with separate db tutor
            
             var db = new BikeRepository();

            // execution is delayed, which is great when querying a database
            var filter = db.Bikes.Where(b => b.HasTassles); // no query run
            if (42 > 6) // You can keep adding filters, even conditionally - great for "advanced search" functionality
                filter = filter.Where(b => b.IsBroken); // no query run

            var query = filter
                .OrderBy(b => b.Wheels)
                .ThenBy(b => b.Name)
                .Select(b => b.Name); // still no query run

            // Now the query runs, but opens a reader, so only populates as you iterate through
            foreach (string bike in query)
                Console.WriteLine(result);
            */


        }

    } // End LearnCSharp class

    // You can include other classes in a .cs file

    public static class Extensions
    {
        // EXTENSION METHODS
        public static void Print(this object obj)
        {
            Console.WriteLine(obj.ToString());
        }
    }


    // DELEGATES AND EVENTS
    public class DelegateTest
    {
        public static int count = 0;
        public static int Increment()
        {
            // increment count then return it
            return ++count;
        }

        // A delegate is a reference to a method.
        // To reference the Increment method,
        // first declare a delegate with the same signature,
        // i.e. takes no arguments and returns an int
        public delegate int IncrementDelegate();

        // An event can also be used to trigger delegates
        // Create an event with the delegate type
        public static event IncrementDelegate MyEvent;

        public static void NotMain(string[] args)
        {
            // Refer to the Increment method by instantiating the delegate
            // and passing the method itself in as an argument
            IncrementDelegate inc = new IncrementDelegate(Increment);
            Console.WriteLine(inc());  // => 1

            // Delegates can be composed with the + operator
            IncrementDelegate composedInc = inc;
            composedInc += inc;
            composedInc += inc;

            // composedInc will run Increment 3 times
            Console.WriteLine(composedInc());  // => 4


            // Subscribe to the event with the delegate
            MyEvent += new IncrementDelegate(Increment);
            MyEvent += new IncrementDelegate(Increment);

            // Trigger the event
            // ie. run all delegates subscribed to this event
            Console.WriteLine(MyEvent());  // => 6
        }
    }


    // Class Declaration Syntax:
    // <public/private/protected/internal> class <class name>{
    //    //data fields, constructors, functions all inside.
    //    //functions are called as methods in Java.
    // }

    public class Bicycle
    {
        // Bicycle's Fields/Variables
        public int Cadence // Public: Can be accessed from anywhere
        {
            get // get - define a method to retrieve the property
            {
                return _cadence;
            }
            set // set - define a method to set a property
            {
                _cadence = value; // Value is the value passed in to the setter
            }
        }
        private int _cadence;

        protected virtual int Gear // Protected: Accessible from the class and subclasses
        {
            get; // creates an auto property so you don't need a member field
            set;
        }

        internal int Wheels // Internal: Accessible from within the assembly
        {
            get;
            private set; // You can set modifiers on the get/set methods
        }

        int _speed; // Everything is private by default: Only accessible from within this class.
                    // can also use keyword private
        public string Name { get; set; }

        // Properties also have a special syntax for when you want a readonly property
        // that simply returns the result of an expression
        public string LongName => Name + " " + _speed + " speed";

        // Enum is a value type that consists of a set of named constants
        // It is really just mapping a name to a value (an int, unless specified otherwise).
        // The approved types for an enum are byte, sbyte, short, ushort, int, uint, long, or ulong.
        // An enum can't contain the same value twice.
        public enum BikeBrand
        {
            AIST,
            BMC,
            Electra = 42, //you can explicitly set a value to a name
            Gitane // 43
        }
        // We defined this type inside a Bicycle class, so it is a nested type
        // Code outside of this class should reference this type as Bicycle.Brand

        public BikeBrand Brand; // After declaring an enum type, we can declare the field of this type

        // Decorate an enum with the FlagsAttribute to indicate that multiple values can be switched on
        // Any class derived from Attribute can be used to decorate types, methods, parameters etc
        // Bitwise operators & and | can be used to perform and/or operations

        [Flags]
        public enum BikeAccessories
        {
            None = 0,
            Bell = 1,
            MudGuards = 2, // need to set the values manually!
            Racks = 4,
            Lights = 8,
            FullPackage = Bell | MudGuards | Racks | Lights
        }

        // Usage: aBike.Accessories.HasFlag(Bicycle.BikeAccessories.Bell)
        // Before .NET 4: (aBike.Accessories & Bicycle.BikeAccessories.Bell) == Bicycle.BikeAccessories.Bell
        public BikeAccessories Accessories { get; set; }

        // Static members belong to the type itself rather than specific object.
        // You can access them without a reference to any object:
        // Console.WriteLine("Bicycles created: " + Bicycle.bicyclesCreated);
        public static int BicyclesCreated { get; set; }

        // readonly values are set at run time
        // they can only be assigned upon declaration or in a constructor
        readonly bool _hasCardsInSpokes = false; // read-only private

        // Constructors are a way of creating classes
        // This is a default constructor
        public Bicycle()
        {
            this.Gear = 1; // you can access members of the object with the keyword this
            Cadence = 50;  // but you don't always need it
            _speed = 5;
            Name = "Bontrager";
            Brand = BikeBrand.AIST;
            BicyclesCreated++;
        }

        // This is a specified constructor (it contains arguments)
        public Bicycle(int startCadence, int startSpeed, int startGear,
                       string name, bool hasCardsInSpokes, BikeBrand brand)
            : base() // calls base first
        {
            Gear = startGear;
            Cadence = startCadence;
            _speed = startSpeed;
            Name = name;
            _hasCardsInSpokes = hasCardsInSpokes;
            Brand = brand;
        }

        // Constructors can be chained
        public Bicycle(int startCadence, int startSpeed, BikeBrand brand) :
            this(startCadence, startSpeed, 0, "big wheels", true, brand)
        {
        }

        // Function Syntax:
        // <public/private/protected> <return type> <function name>(<args>)

        // classes can implement getters and setters for their fields
        // or they can implement properties (this is the preferred way in C#)

        // Method parameters can have default values.
        // In this case, methods can be called with these parameters omitted
        public void SpeedUp(int increment = 1)
        {
            _speed += increment;
        }

        public void SlowDown(int decrement = 1)
        {
            _speed -= decrement;
        }

        // properties get/set values
        // when only data needs to be accessed, consider using properties.
        // properties may have either get or set, or both
        private bool _hasTassles; // private variable
        public bool HasTassles // public accessor
        {
            get { return _hasTassles; }
            set { _hasTassles = value; }
        }

        // You can also define an automatic property in one line
        // this syntax will create a backing field automatically.
        // You can set an access modifier on either the getter or the setter (or both)
        // to restrict its access:
        public bool IsBroken { get; private set; }

        // Properties can be auto-implemented
        public int FrameSize
        {
            get;
            // you are able to specify access modifiers for either get or set
            // this means only Bicycle class can call set on Framesize
            private set;
        }

        // It's also possible to define custom Indexers on objects.
        // All though this is not entirely useful in this example, you
        // could do bicycle[0] which returns "chris" to get the first passenger or
        // bicycle[1] = "lisa" to set the passenger. (of this apparent quattrocycle)
        private string[] passengers = { "chris", "phil", "darren", "regina" };

        public string this[int i]
        {
            get
            {
                return passengers[i];
            }

            set
            {
                passengers[i] = value;
            }
        }

        // Method to display the attribute values of this Object.
        public virtual string Info()
        {
            return "Gear: " + Gear +
                    " Cadence: " + Cadence +
                    " Speed: " + _speed +
                    " Name: " + Name +
                    " Cards in Spokes: " + (_hasCardsInSpokes ? "yes" : "no") +
                    "\n------------------------------\n"
                    ;
        }

        // Methods can also be static. It can be useful for helper methods
        public static bool DidWeCreateEnoughBicycles()
        {
            // Within a static method, we only can reference static class members
            return BicyclesCreated > 9000;
        } // If your class only needs static members, consider marking the class itself as static.


    } // end class Bicycle

    // PennyFarthing is a subclass of Bicycle
    class PennyFarthing : Bicycle
    {
        // (Penny Farthings are those bicycles with the big front wheel.
        // They have no gears.)

        // calling parent constructor
        public PennyFarthing(int startCadence, int startSpeed) :
            base(startCadence, startSpeed, 0, "PennyFarthing", true, BikeBrand.Electra)
        {
        }

        protected override int Gear
        {
            get
            {
                return 0;
            }
            set
            {
                //throw new InvalidOperationException("You can't change gears on a PennyFarthing");
            }
        }

        public static PennyFarthing CreateWithGears(int gears)
        {
            var penny = new PennyFarthing(1, 1);
            penny.Gear = gears; // Oops, can't do this!
            return penny;
        }

        public override string Info()
        {
            string result = "PennyFarthing bicycle ";
            result += base.ToString(); // Calling the base version of the method
            return result;
        }
    }

    // Interfaces only contain signatures of the members, without the implementation.
    interface IJumpable
    {
        void Jump(int meters); // all interface members are implicitly public
    }

    interface IBreakable
    {
        bool Broken { get; } // interfaces can contain properties as well as methods & events
    }

    // Classes can inherit only one other class, but can implement any amount of interfaces,
    // however the base class name must be the first in the list and all interfaces follow
    class MountainBike : Bicycle, IJumpable, IBreakable
    {
        int damage = 0;

        public void Jump(int meters)
        {
            damage += meters;
        }

        public bool Broken
        {
            get
            {
                return damage > 100;
            }
        }
    }

    /// <summary>
    /// Used to connect to DB for LinqToSql example.
    /// EntityFramework Code First is awesome (similar to Ruby's ActiveRecord, but bidirectional)
    /// https://docs.microsoft.com/ef/ef6/modeling/code-first/workflows/new-database
    /// </summary>
    public class BikeRepository : DbContext
    {
        public BikeRepository()
            : base()
        {
        }

        public DbSet<Bicycle> Bikes { get; set; }
    }

    // Classes can be split across multiple .cs files
    // A1.cs
    public partial class A
    {
        public static void A1()
        {
            Console.WriteLine("Method A1 in class A");
        }
    }

    // A2.cs
    public partial class A
    {
        public static void A2()
        {
            Console.WriteLine("Method A2 in class A");
        }
    }

    // String interpolation by prefixing the string with $
    // and wrapping the expression you want to interpolate with { braces }
    // You can also combine both interpolated and verbatim strings with $@
    public class Rectangle
    {
        public int Length { get; set; }
        public int Width { get; set; }
    }

    /*public*/ class Program
    {
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle { Length = 5, Width = 3 };
            Console.WriteLine($"The length is {rect.Length} and the width is {rect.Width}");

            string username = "User";
            Console.WriteLine($@"C:\Users\{username}\Desktop");
            Console.WriteLine("Hello world");
            
            // Program using the partial class "A"
            //A.A1();
            //A.A2();
            //Simpleserver.PrintYieldCounterToConsole();
            
            Simpleserver.Classes();
            DelegateTest delegateTest = new DelegateTest();
            DelegateTest.NotMain(new string[]{ "a", "b", "c" });

        }
    }
/*
    // New C# 6 features
    class GlassBall : IJumpable, IBreakable
    {
        // Autoproperty initializers
        public int Damage { get; private set; } = 0;

        // Autoproperty initializers on getter-only properties
        public string Name { get; } = "Glass ball";

        // Getter-only autoproperty that is initialized in constructor
        public string GenieName { get; }

        public GlassBall(string genieName = null)
        {
            GenieName = genieName;
        }

        public void Jump(int meters)
        {
            if (meters < 0)
                // New nameof() expression; compiler will check that the identifier exists
                // nameof(x) == "x"
                // Prevents e.g. parameter names changing but not updated in error messages
                throw new ArgumentException("Cannot jump negative amount!", nameof(meters));

            Damage += meters;
        }

        // Expression-bodied properties ...
        public bool Broken
            => Damage > 100;

        // ... and methods
        public override string ToString()
            // Interpolated string
            => $"{Name}. Damage taken: {Damage}";

        public string SummonGenie()
            // Null-conditional operators
            // x?.y will return null immediately if x is null; y is not evaluated
            => GenieName?.ToUpper();
    }

    static class MagicService
    {
        private static bool LogException(Exception ex)
        {
            // log exception somewhere
            return false;
        }

        public static bool CastSpell(string spell)
        {
            try
            {
                // Pretend we call API here
                throw new MagicServiceException("Spell failed", 42);

                // Spell succeeded
                return true;
            }
            // Only catch if Code is 42 i.e. spell failed
            catch (MagicServiceException ex) when (ex.Code == 42)
            {
                // Spell failed
                return false;
            }
            // Other exceptions, or MagicServiceException where Code is not 42
            catch (Exception ex) when (LogException(ex))
            {
                // Execution never reaches this block
                // The stack is not unwound
            }
            return false;
            // Note that catching a MagicServiceException and rethrowing if Code
            // is not 42 or 117 is different, as then the final catch-all block
            // will not catch the rethrown exception
        }
    }

    public class MagicServiceException : Exception
    {
        public int Code { get; }

        public MagicServiceException(string message, int code) : base(message)
        {
            Code = code;
        }
    }

    public static class PragmaWarning
    {
        // Obsolete attribute
        [Obsolete("Use NewMethod instead", false)]
        public static void ObsoleteMethod()
        {
            // obsolete code
        }

        public static void NewMethod()
        {
            // new code
        }

        public static void Main()
        {
            ObsoleteMethod(); // CS0618: 'ObsoleteMethod is obsolete: Use NewMethod instead'
#pragma warning disable CS0618
            ObsoleteMethod(); // no warning
#pragma warning restore CS0618
            ObsoleteMethod(); // CS0618: 'ObsoleteMethod is obsolete: Use NewMethod instead'
        }
    }*/
} // End Namespace

/*
using System;
// C# 6, static using
using static System.Math;

namespace Learning.More.CSharp
{
    class StaticUsing
    {
        static void Main()
        {
            // Without a static using statement..
            Console.WriteLine("The square root of 4 is {}.", Math.Sqrt(4));
            // With one
            Console.WriteLine("The square root of 4 is {}.", Sqrt(4));
        }
    }
}*/

/*
// New C# 7 Feature
// Install Microsoft.Net.Compilers Latest from Nuget
// Install System.ValueTuple Latest from Nuget
using System;
namespace Csharp7
{
    // TUPLES, DECONSTRUCTION AND DISCARDS
    class TuplesTest
    {
        public (string, string) GetName()
        {
            // Fields in tuples are by default named Item1, Item2...
            var names1 = ("Peter", "Parker");
            Console.WriteLine(names1.Item2);  // => Parker

            // Fields can instead be explicitly named
            // Type 1 Declaration
            (string FirstName, string LastName) names2 = ("Peter", "Parker");

            // Type 2 Declaration
            var names3 = (First: "Peter", Last: "Parker");

            Console.WriteLine(names2.FirstName);  // => Peter
            Console.WriteLine(names3.Last);  // => Parker

            return names3;
        }

        public string GetLastName()
        {
            var fullName = GetName();

            // Tuples can be deconstructed
            (string firstName, string lastName) = fullName;

            // Fields in a deconstructed tuple can be discarded by using _
            var (_, last) = fullName;
            return last;
        }

        // Any type can be deconstructed in the same way by
        // specifying a Deconstruct method
        public int randomNumber = 4;
        public int anotherRandomNumber = 10;

        public void Deconstruct(out int randomNumber, out int anotherRandomNumber)
        {
            randomNumber = this.randomNumber;
            anotherRandomNumber = this.anotherRandomNumber;
        }

        static void Main(string[] args)
        {
            var tt = new TuplesTest();
            (int num1, int num2) = tt;
            Console.WriteLine($"num1: {num1}, num2: {num2}");  // => num1: 4, num2: 10

            Console.WriteLine(tt.GetLastName());
        }
    }

    // PATTERN MATCHING
    class PatternMatchingTest
    {
        public static (string, int)? CreateLogMessage(object data)
        {
            switch (data)
            {
                // Additional filtering using when
                case System.Net.Http.HttpRequestException h when h.Message.Contains("404"):
                    return (h.Message, 404);
                case System.Net.Http.HttpRequestException h when h.Message.Contains("400"):
                    return (h.Message, 400);
                case Exception e:
                    return (e.Message, 500);
                case string s:
                    return (s, s.Contains("Error") ? 500 : 200);
                case null:
                    return null;
                default:
                    return (data.ToString(), 500);
            }
        }
    }

    // REFERENCE LOCALS
    // Allow you to return a reference to an object instead of just its value
    class RefLocalsTest
    {
        // note ref in return
        public static ref string FindItem(string[] arr, string el)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == el)
                {
                    // return the reference
                    return ref arr[i];
                }
            }
            throw new Exception("Item not found");
        }

        public static void SomeMethod()
        {
            string[] arr = { "this", "is", "an", "array" };

            // note refs everywhere
            ref string item = ref FindItem(arr, "array");
            item = "apple";
            Console.WriteLine(arr[3]);  // => apple
        }
    }

    // LOCAL FUNCTIONS
    class LocalFunctionTest
    {
        private static int _id = 0;
        public int id;
        public LocalFunctionTest()
        {
            id = generateId();

            // This local function can only be accessed in this scope
            int generateId()
            {
                return _id++;
            }
        }

        public static void AnotherMethod()
        {
            var lf1 = new LocalFunctionTest();
            var lf2 = new LocalFunctionTest();
            Console.WriteLine($"{lf1.id}, {lf2.id}");  // => 0, 1

            int id = generateId();
            // error CS0103: The name 'generateId' does not exist in the current context
        }
    }
}
*/

