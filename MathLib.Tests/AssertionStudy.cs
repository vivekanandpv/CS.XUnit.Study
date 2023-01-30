using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace MathLib.Tests
{
    //  If the SUT requires disposing, the test suite can implement IDisposable
    public class AssertionStudy : IDisposable
    {
        private ITestOutputHelper _helper;
        
        //  The test suite is created once per test (before a test runs)
        //  Common initialization logic, such as arrange phase in some cases
        //  can be moved to the constructor (making the SUT a field)
        //  SUT: System Under Test (or Class Under Test - CUT) is the name of the unit
        public AssertionStudy(ITestOutputHelper helper)
        {
            _helper = helper;
            _helper.WriteLine($"{nameof(AssertionStudy)} is created");
        }

        public void Dispose()
        {
            _helper.WriteLine($"{nameof(AssertionStudy)} is disposed");
        }

        [Fact]
        public void NumericAssertions()
        {
            Assert.Equal<int>(41, 41);
            Assert.NotEqual<int>(41, 45);
            Assert.InRange(14, 10, 18);
            Assert.InRange<double>(1.666, 1.660, 1.667);
            Assert.Equal(1.66667,1.66668, 3);
        }

        [Fact]
        public void StringAssertions()
        {
            Assert.Equal("Hello", "Hello");
            Assert.Equal("Hello", "HeLlO", ignoreCase:true);
            Assert.NotEqual("Hello", "hello");
            Assert.Empty(string.Empty);
            Assert.NotEmpty("Hello");
            Assert.Contains("ll", "Hello");
            Assert.Matches(new Regex("^[A-Z]"), "Joy");
            Assert.DoesNotMatch(new Regex("^[A-Z]"), "joy");
        }

        [Fact]
        public void BooleanAssertions()
        {
            Assert.True(9 == 9);
            Assert.False(5 > 8);
        }

        [Fact]
        public void NullAssertions()
        {
            string s = null;
            Assert.Null(s);
            Assert.NotNull(new StringBuilder());
        }

        [Fact]
        public void CollectionAssertions()
        {
            //  For working with IEnumerable<T>
            Assert.Contains(4, new int[] { 1, 4, 5, 8 });
            Assert.DoesNotContain(-4, new int[] { 1, 4, 5, 8 });
            Assert.All(new int[] { 1, 4, 5, 8 }, n =>
            {
                Assert.InRange(n, 0, 100);
            });
        }

        [Fact]
        public void TypeAssertions()
        {
            Assert.IsType<string>("hello"); //  returns the object
            Assert.IsType(typeof(string), "hello"); //  older
            Assert.IsNotType<string>(DateTime.Now);
            Assert.IsNotType(typeof(string), DateTime.Now);

            //  this is strict, not polymorphic!
            Assert.IsNotType<object>("Hello");

            //  For polymorphic check
            Assert.IsAssignableFrom<object>("Hello");
        }

        [Fact]
        public void InstanceAssertions()
        {
            var sb1 = new StringBuilder();
            var sb2 = sb1;

            var sb3 = new StringBuilder();

            Assert.Same(sb1, sb2);
            Assert.NotSame(sb1, sb3);
        }

        [Fact]
        public void ExceptionAssertions()
        {
            string str = null;

            var ex = Assert.Throws<NullReferenceException>(() =>
            {
                string s2 = str.ToLower();
            });

            Assert.Contains("reference", ex.Message);
        }

        [Fact]
        public void EventAssertions()
        {
            var account = new BankAccount();

            var result = Assert.Raises<string>(
                handler => account.OnDeposit += handler,
                handler => account.OnDeposit -= handler,
                () =>
                {
                    account.Deposit(1455);
                });
        }
    }

    class BankAccount
    {
        public event EventHandler<string> OnDeposit;

        public void Deposit(double amount)
        {
            OnDeposit?.Invoke(this, "deposited");
        }
    }
}
