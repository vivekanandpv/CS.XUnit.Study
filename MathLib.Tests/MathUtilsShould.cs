using Xunit.Abstractions;

namespace MathLib.Tests
{
    public class MathUtilsShould
    {
        //  Console.WriteLine doesn't work with test adapters
        //  Therefore, the following steps are to be followed
        //  1. create a field of type ITestOutputHelper type (usually private readonly)
        //  2. invert this dependency in the constructor, so that XUnit can inject it
        //  3. use WriteLine on the helper
        
        private readonly ITestOutputHelper _helper; //  private readonly fields have _ prefix by convention

        public MathUtilsShould(ITestOutputHelper helper)
        {
            _helper = helper;
        }


        [Fact]
        public void CalculateProductOfIntegers()
        {
            //  Arrange
            var utils = new MathUtils();

            //  Act
            var result = utils.GetProduct(5, 10);

            //  Assert
            Assert.Equal(50, result);

            //  You can use _helper to log the output.
            //  This can be used multiple times within a test.
            _helper.WriteLine($"Got the result: {result}");
        }
    }
}