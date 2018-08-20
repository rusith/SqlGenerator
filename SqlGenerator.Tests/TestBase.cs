using SqlGenerator.Abstract;
using SqlGenerator.Concrete;

namespace SqlGenerator.Tests
{
    public class TestBase
    {
        public TestBase()
        {
            Converter = new ExpressionToSqlConverter();
        }


        public IExpressionToSqlConverter Converter { get; set; }
    }
}
