namespace MathLib.Tests
{
    public class MathUtilsShould
    {
        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(-7, 7, -49)]
        [InlineData(-5, 0, 0)]
        [InlineData(5, 0, 0)]
        [InlineData(0, 0, 0)]
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
}