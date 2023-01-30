namespace MathLib.Tests
{
    public class MathUtilsShould
    {
        [Fact]
        public void CalculateProductOfIntegers()
        {
            //  Arrange
            var utils = new MathUtils();

            //  Act
            var result = utils.GetProduct(5, 10);

            //  Assert
            Assert.Equal(50, result);
        }
    }
}