namespace Ardalis.SmartEnum.JsonNet.UnitTests
{
    public sealed class TestEnumBoolean : SmartEnum<TestEnumBoolean, bool>
    {
        public static readonly TestEnumBoolean Instance = new TestEnumBoolean(nameof(Instance), true);

        TestEnumBoolean(string name, bool value) : base(name, value) { }
    }

    public sealed class TestEnumInt16 : SmartEnum<TestEnumInt16, short>
    {
        public static readonly TestEnumInt16 Instance = new TestEnumInt16(nameof(Instance), 1);

        TestEnumInt16(string name, short value) : base(name, value) { }
    }

    public sealed class TestEnumInt32 : SmartEnum<TestEnumInt32, int>
    {
        public static readonly TestEnumInt32 Instance = new TestEnumInt32(nameof(Instance), 1);
        public static readonly TestEnumInt32 Instance2 = new TestEnumInt32(nameof(Instance2), 2);
        public static readonly TestEnumInt32 Instance3 = new TestEnumInt32(nameof(Instance3), 3);

        TestEnumInt32(string name, int value) : base(name, value) { }
    }

    public sealed class TestEnumDouble : SmartEnum<TestEnumDouble, double>
    {
        public static readonly TestEnumDouble Instance = new TestEnumDouble(nameof(Instance), 1.0);

        TestEnumDouble(string name, double value) : base(name, value) { }
    }

    public sealed class TestEnumString : SmartEnum<TestEnumString, string>
    {
        public static readonly TestEnumString Instance = new TestEnumString(nameof(Instance), "string");

        TestEnumString(string name, string value) : base(name, value) { }
    }
}
