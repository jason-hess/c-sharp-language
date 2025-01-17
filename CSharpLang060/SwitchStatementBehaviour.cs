using System;
using NUnit.Framework;

namespace CSharpLang60
{
    /// <summary>
    /// C# 6.0 (and prior) switch statement behaviour
    /// </summary>
    class SwitchStatementBehaviour
    {
        [Test]
        [TestCase(null)]
        public void Should(Thing theThing)
        {
            Thing aThing = new Thing();
            //switch (aThing) // Error CS0151 A switch expression or case label must be a bool, char, string, integral, enum, or corresponding nullable type in C# 6 and earlier.
            {
            }

            var aRealThing = aThing is Thing;
            if (aRealThing != null)
            {
                return;
            }
        }

        [Test]
        public void SwitchCanBeEmpty()
        {
            int x = 10;
            switch (x)
            {

            }
        }

        [Test]
        public void SwitchMustBailOutOfEachMatch()
        {
            var x = new Random().Next();

            switch (x)
            {
                // case 1: // Error CS0163  Control cannot fall through from one case label('case 1:') to another
                // x = 10;
                // case 2: // Error CS8070  Control cannot fall out of switch from final case label('case 2:')
                // x = 13;
            }


            switch (x)
            {
                // default: // Error CS8070  Control cannot fall out of switch from final case label('default:') 
            }
        }

        [Test]
        public void SwitchMustBeOneOfIntegralLikeShortIntLongBoolStringCharOrEnum()
        {
            float x = 10;
            // switch(x) // Error CS0151 A switch expression or case label must be a bool, char, string, integral, enum, or corresponding nullable type in C# 6 and earlier.
            {
            }
        }

        [Test]
        public void SwitchSectionCanHaveTwoLabels()
        {
            switch (10)
            {
                case 1:
                case 2:
                    var result = "1 or 2";
                    return; // good to specify here although not needed?
            }
        }

        [Test]
        public void OnlyOneCaseWillEverMatchInCSharp60()
        {
            int x = 10;
            string word;
            switch (x)
            {
                case 1:
                    word = "One";
                    break;
                default:
                    word = "Unknown";
                    break;
            }
        }

        [Test]
        public void InCSharp60OnlyTheConstantPatternExists()
        {
            object x = 10;
            //switch (x) // Error CS0151 A switch expression or case label must be a bool, char, string, integral, enum, or corresponding nullable type in C# 6 and earlier.
            {
                //case 1:
                //case "10":
            }
        }

        [Test]
        public void InCSharp60OnlyTheConstantPatternExists2()
        {
            int x = 10;
            switch (x)
            {
                case 1:
                    break;
                    //case "10": // Error CS0029  Cannot implicitly convert type 'string' to 'int' 

                    break;
            }
        }

        [Test]
        public void CanReferToConstants()
        {
            int x = 10;
            const int SUPER_THING = 1;
            switch (x)
            {
                case SUPER_THING:
                    break;
                    //case "10": // Error CS0029  Cannot implicitly convert type 'string' to 'int' 

                    break;
            }
        }

        /// <summary>
        /// A good example of C# 6.0 switching on an enum
        /// </summary>
        public void CanSwitchOnEnum()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    Console.WriteLine("The weekend");
                    break;
                case DayOfWeek.Monday:
                    Console.WriteLine("The first day of the work week.");
                    break;
                case DayOfWeek.Friday:
                    Console.WriteLine("The last day of the work week.");
                    break;
                default:
                    Console.WriteLine("The middle of the work week.");
                    break;
            }
        }


        public bool IsWeekend(string dayOfWeek)
        {
            if (dayOfWeek == "Sunday")
            {
                return true;
            }
            else if (dayOfWeek == "Saturday")
            {
                return true;
            }
            return false;
        }

        public class Thing
        {

        }
    }
}
