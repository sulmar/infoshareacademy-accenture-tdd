namespace TestApp.UnitTests
{
    [TestClass]
    public class RentTests
    {
        private User rentee;
        private Rent sut;   // SUT = System Under Test

        [TestInitialize]
        public void Setup()
        {
            // Arrange          
            rentee = new User();
            sut = new Rent { Rentee = rentee };
        }

        // Method_Scenario_ExpectedBehavior

        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Arrange           

            // Act
            var result = sut.CanReturn(rentee);

            // Assert            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = sut.CanReturn(new User());

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = sut.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]   // Assert
        public void CanReturn_UserIsEmpty_ShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            sut.CanReturn(null);

            // Assert
        }
    }
}