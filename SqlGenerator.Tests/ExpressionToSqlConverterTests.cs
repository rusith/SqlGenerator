using System;
using Shouldly;
using Xunit;

namespace SqlGenerator.Tests
{
    public class TestModel
    {
        public string StringProperty { get; set; }
        public int IntProperty { get; set; }
        public double DoubleProperty { get; set; }
        public long LongProperty { get; set; }
        public short ShortProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public TimeSpan TimeSpanProperty { get; set; }

    }

    public class ExpressionToSqlConverterTests : TestBase
    {
        #region object.prop == somethig

        [Fact]
        public void Constant_Equal_To_Property_String()
        {
            Converter.GetWhere<TestModel>((m) => m.StringProperty == "something")
                .ShouldBe("[StringProperty] = 'something'");
        }

        [Fact]
        public void Constant_Equal_To_Property_Null()
        {
            Converter.GetWhere<TestModel>((m) => m.StringProperty == null)
                .ShouldBe("[StringProperty] IS NULL");
        }

        [Fact]
        public void Constant_Equal_To_Property_NullableInt()
        {
            int? integer = null;
            Converter.GetWhere<TestModel>((m) => m.IntProperty == integer)
                .ShouldBe("[IntProperty] IS NULL");
        }

        [Fact]
        public void Constant_Equal_To_Property_NullableDouble()
        {
            double? d = null;
            Converter.GetWhere<TestModel>((m) => m.DoubleProperty == d)
                .ShouldBe("[DoubleProperty] IS NULL");
        }

        [Fact]
        public void Constant_Equal_To_Property_NullableDateTime()
        {
            DateTime? dateTime = null;
            Converter.GetWhere<TestModel>((m) => m.DateTimeProperty == dateTime)
                .ShouldBe("[DateTimeProperty] IS NULL");
        }

        [Fact]
        public void Constant_Equal_To_Property_Int()
        {
            Converter.GetWhere<TestModel>((m) => m.IntProperty == 122)
                .ShouldBe("[IntProperty] = 122");
        }

        [Fact]
        public void Constant_Equal_To_Property_()
        {
            Converter.GetWhere<TestModel>((m) => m.StringProperty == "something")
                .ShouldBe("[StringProperty] = 'something'");
        }

        #endregion

        #region object.prop == something.somethingElse

        public void EqualToObjectProperty()
        {
            var obj = new TestModel { StringProperty = "abc" };
            Converter.GetWhere<TestModel>(m => m.StringProperty == obj.StringProperty);
        }

        #endregion

        #region object.prop == object.somethign
        #endregion
    }
}
