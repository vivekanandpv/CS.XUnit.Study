namespace MathLib.Tests
{
    public class MathUtilsShould
    {
        [Theory]
        //  If a static method in another method is referenced to MemberData use this overload
        [MemberData(nameof(DataProvider.GetTestDataExternal), MemberType = typeof(DataProvider))]
        public void CalculateProductOfIntegers(int op1, int op2, int product)
        {
            //  Arrange
            var utils = new MathUtils();

            //  Act
            var result = utils.GetProduct(op1, op2);

            //  Assert
            Assert.Equal(product, result);
        }
    }

    public static class DataProvider
    {
        public static IEnumerable<object[]> GetTestDataExternal()
        {
            //  Make sure the csv file's Copy to Output Directory property is set to Copy Always
            var lines = File.ReadAllLines("test-data.csv");
            return lines
                .Select(l => l.Split(','))
                .Select(sa => sa.Select(s => Convert.ToInt32(s.Trim()) as object).ToArray());
        }
    }
}