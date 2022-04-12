using Moq;
using NUnit.Framework;
using System;

using Trace.BLL;
using Trace.Model;
using Trace.Repository;

namespace Trace.UnitTest
{
    [TestFixture]
    public class TraceApiTests
    {
        

        [Test]
        [TestCase("testAccount", "")]
        [TestCase("", "testpassword")]
        [TestCase("", "")]
        public void Register_LoginArgs_ReturnFail(string account, string password) 
        {
            // Arrange
            var mockUser = new Mock<IUserRepository>();
            var mockUserFriend = new Mock<IUserFriendRepository>();
            UserCenter userCenter = new UserCenter(mockUser.Object, mockUserFriend.Object);
            // Act
            var task = userCenter.Register(new LoginArgs { Account = account, Password = password });
            // Assert
            Assert.AreEqual(EnumStatusCode.Fail,task.Result.StatusCode );
        }
    }
}
