using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MathLib.Tests
{
    public class AssertionStudy
    {
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
        public void PropertyChangedAssertion()
        {
            var account = new BankAccount();

            //  No code for event registration and deregistration
            //  This method doesn't return any object unlike the event assetion
            Assert.PropertyChanged(account, nameof(account.Balance), () => account.Deposit(1452));
        }
    }

    //  INotifyPropertyChanged is a general purpose event pattern
    //  https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged.propertychanged?view=net-7.0
    class BankAccount : INotifyPropertyChanged
    {
        private double _balance = 0;
        public double Balance => _balance;

        public void Deposit(double amount)
        {
            SetField(ref _balance, amount, nameof(Balance));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
