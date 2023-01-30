namespace MathLib.Tests
{
    public class MathUtilsShould
    {
        [Theory]
        //  We use MemberData attribute to provide external data
        //  prefer nameof() operator to hardcoded string values
        //  The static method can reside any class, so long as it is public
        
        [MemberData(nameof(GetTestDataLocal))]
        //  [MemberData(nameof(DataProvider.GetTestDataExternal))]    //  also works
        public void CalculateProductOfIntegers(int op1, int op2, int product)
        {
            //  Arrange
            var utils = new MathUtils();

            //  Act
            var result = utils.GetProduct(op1, op2);

            //  Assert
            Assert.Equal(product, result);
        }

        //  a public static method that can return IEnumerable<object[]> can provide the data to the theory
        public static IEnumerable<object[]> GetTestDataLocal()
        {
            //  compiler builds a state machine (iterator) when you use yield return 
            //  more info: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield
            //  https://stackoverflow.blog/2022/06/15/c-ienumerable-yield-return-and-lazy-evaluation/
            yield return new object[] { 5, 10, 50 };
            yield return new object[] { -7, 7, -49 };
            yield return new object[] { -5, 0, 0 };
            yield return new object[] { 5, 0, 0 };
            yield return new object[] { 0, 0, 0 };
        }
    }

    public class DataProvider
    {
        public static IEnumerable<object[]> GetTestDataExternal()
        {
            //  compiler builds a state machine (iterator) when you use yield return 
            //  more info: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield
            //  https://stackoverflow.blog/2022/06/15/c-ienumerable-yield-return-and-lazy-evaluation/
            yield return new object[] { 5, 10, 50 };
            yield return new object[] { -7, 7, -49 };
            yield return new object[] { -5, 0, 0 };
            yield return new object[] { 5, 0, 0 };
            yield return new object[] { 0, 0, 0 };
        }
    }
}