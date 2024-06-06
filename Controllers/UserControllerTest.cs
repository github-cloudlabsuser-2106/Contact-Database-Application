using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _userList;

        [TestInitialize]
        public void Setup()
        {
            _userList = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" }
            };

            UserController.userlist = _userList;
            _controller = new UserController();
        }

        [TestMethod]
        public void Index_ReturnsCorrectView()
        {
            var result = _controller.Index() as ViewResult;
            Assert.AreEqual(_userList, result.Model);
        }

        [TestMethod]
        public void Details_ReturnsCorrectView()
        {
            var result = _controller.Details(1) as ViewResult;
            Assert.AreEqual(_userList[0], result.Model);
        }

        [TestMethod]
        public void Create_AddsUserToList()
        {
            var newUser = new User { Name = "New User", Email = "newuser@example.com" };
            _controller.Create(newUser);
            Assert.IsTrue(UserController.userlist.Contains(newUser));
        }

        [TestMethod]
        public void Edit_UpdatesUserInList()
        {
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updateduser@example.com" };
            _controller.Edit(1, updatedUser);
            Assert.AreEqual(updatedUser.Name, UserController.userlist[0].Name);
            Assert.AreEqual(updatedUser.Email, UserController.userlist[0].Email);
        }

        [TestMethod]
        public void Delete_RemovesUserFromList()
        {
            _controller.Delete(1, new FormCollection());
            Assert.IsFalse(UserController.userlist.Exists(u => u.Id == 1));
        }
    }
}
