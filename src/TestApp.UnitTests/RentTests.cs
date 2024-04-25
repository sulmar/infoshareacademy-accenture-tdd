namespace TestApp.UnitTests
{
    [TestClass]
    public class RentTests
    {
        // Method_Scenario_ExpectedBehavior

        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Arrange           
            User rentee = new User();
            Rent rent = new Rent { Rentee = rentee };

            // Act
            var result = rent.CanReturn(rentee);

            // Assert            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent { Rentee = rentee };

            // Act
            var result = rent.CanReturn(new User());

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent {  Rentee = rentee };

            // Act
            var result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]   // Assert
        public void CanReturn_UserIsEmpty_ShouldThrowArgumentNullException()
        {
            // Arrange
            Rent rent = new Rent();

            // Act
            rent.CanReturn(null);

            // Assert
        }
    }
}