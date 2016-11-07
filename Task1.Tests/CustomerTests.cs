using System.Collections;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        private static readonly Component component1 = new Component();
        private static readonly Component component2 = new Component();
        private static readonly Component component3 = new Component();
        private static readonly Component component4 = new Component();

        private static readonly DecoratorName decorator1 = new DecoratorName(component1);
        private static readonly DecoratorContactPhone decorator2 = 
            new DecoratorContactPhone(component2);
        private static readonly DecoratorRevenue decorator3 = 
            new DecoratorRevenue(component3, "F0");
        private static readonly DecoratorRevenue decorator4 =
            new DecoratorRevenue(component4);

        private static readonly Customer customer1 = new Customer
            ("Jeffrey Richter", "+1 (425) 555-0100", 1000000);

        private const string result1 = 
            "Customer record: Jeffrey Richter ";
        private const string result2 = 
            "Customer record: +1 (425) 555-0100 ";
        private const string result3 = 
            "Customer record: 1000000 ";
        private const string result4 = 
            "Customer record: Jeffrey Richter 1,000,000.00 +1 (425) 555-0100 ";

        [Test, TestCaseSource(typeof(CustomerTest), nameof(CustomerTest.CustomerTestCases))]
        public string DecoratorTests(IComponent iComponent)
        {
            return iComponent.Operation();
        }

        public class CustomerTest
        {
            public static IEnumerable CustomerTestCases
            {
                get
                {
                    decorator1.Info = customer1.Name;
                    decorator2.Info = customer1.ContactPhone;
                    decorator3.Info = customer1.Revenue;
                    decorator4.Info = customer1.Revenue;

                    var decorator5 = (DecoratorName) decorator1.Clone();
                    decorator5.Info += " " + decorator4.Info.ToString(decorator4.Format);
                    decorator5.Info += " " + decorator2.Info;

                    yield return new TestCaseData(decorator1).Returns(result1);
                    yield return new TestCaseData(decorator2).Returns(result2);
                    yield return new TestCaseData(decorator3).Returns(result3);
                    yield return new TestCaseData(decorator5).Returns(result4);
                }
            }
        }
    }
}
