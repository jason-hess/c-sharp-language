using NUnit.Framework;

namespace CSharpLang130;

/// <summary>
/// 
/// </summary>
/// <see href="https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#implicit-index-access" />
public class IndexAccessInObjectInitializer
{
    public class Initilizable
    {
        public int[] Value = new int[10];
    }

    /// <summary>
    /// The implicit "from the end" index operator, ^, is now allowed in an object initializer expression.
    /// </summary>
    [Test]
    public void Should()
    {
        _ = new Initilizable()
        {
            Value =
            {
                [^1] = 0,
                [^2] = 1,
                [^3] = 2,
                [^4] = 3,
                [^5] = 4,
                [^6] = 5,
                [^7] = 6,
                [^8] = 7,
                [^9] = 8,
                [^10] = 9
            }
        };

        // See: https://sharplab.io/#v2:CYLg1APgAgTAjAWAFBQMwAJboMLoN7LpGYZYCSAdgJYAuVANlQF4CGARvQKaHEFLECS6KhRoBtALroAai3oBXTugC86CpwDuw0WLgAGCQG4eRAL7IT6APQ30AHgDO8gLbOWAJwCeAPks2r6AAqABZKVM4ADowAxrToAEQAZu4A9s7oNKHonBTA8drAnAAe6CkRnO4sNCnuADToAHr1VA5qKVpy9O2cwNroLBSlbABWnNE02rRUcswV2UUR7pwODlQpFAB0frZ2Vk6uHj6WaJgALOgAysEp8vTAABQAlJZ8gsQA+ipqmuiUU4ysDicJ6WASvN6CWQKJTKUEQ8EQiFiBpwKSqPS1OGIojImBo9BwTH8bFvZGofEwIkk0kNU741BU6kCZEAVnxp0ZTJxDQAbPiWZyuciAOz4nmCpnIgAc+OFEupyIAnPipfKScj9PjFVjBOZiW9TMZ9eg9aYgA=

        // The above transpiles to the following:
        //     public class Initilizable
        //{
        //    [Nullable(1)]
        //public int[] Value = new int[10];
        //}

        //public void Should()
        //{
        //    Initilizable initilizable = new Initilizable();
        //    int num = initilizable.Value.Length - 1;
        //    initilizable.Value[num] = 0;
        //    int num2 = initilizable.Value.Length - 2;
        //    initilizable.Value[num2] = 1;
        //    int num3 = initilizable.Value.Length - 3;
        //    initilizable.Value[num3] = 2;
        //    int num4 = initilizable.Value.Length - 4;
        //    initilizable.Value[num4] = 3;
        //    int num5 = initilizable.Value.Length - 5;
        //    initilizable.Value[num5] = 4;
        //    int num6 = initilizable.Value.Length - 6;
        //    initilizable.Value[num6] = 5;
        //    int num7 = initilizable.Value.Length - 7;
        //    initilizable.Value[num7] = 6;
        //    int num8 = initilizable.Value.Length - 8;
        //    initilizable.Value[num8] = 7;
        //    int num9 = initilizable.Value.Length - 9;
        //    initilizable.Value[num9] = 8;
        //    int num10 = initilizable.Value.Length - 10;
        //    initilizable.Value[num10] = 9;
        //}
    }

}
