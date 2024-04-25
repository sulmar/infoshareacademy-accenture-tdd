namespace TestApp.UnitTests
{
    [TestClass]
    public class RentTests
    {
        // Method_Scenario_ExpectedBehavior

        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void CanReturn_UserIsEmpty_ShouldThrowArgumentNullException()
        {
            Assert.Fail();
        }
    }
}