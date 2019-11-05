using System;

namespace CSharpLangX
{
    public class Car
    {
        public int Seats { get; set; }
    }

    public class PatternMatchingBehaviour
    {
        public decimal CalculateToll(object dayOfWeek) =>
            dayOfWeek switch
            {
                Car c => c switch
                {
                    { Seats: 1 } => 1.0M
                },
                _ => 2.0M
            };
    }
}
