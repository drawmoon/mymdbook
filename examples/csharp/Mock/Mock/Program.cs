using System;
using System.Collections.Generic;
using Mock;
using Moq;
using System.Diagnostics;

var userServiceMock = new Mock<IUserService>();

userServiceMock.Setup(p => p.Current)
    .Returns(new UserContextForTest());

userServiceMock.Setup(p => p.GetOnlineUser())
    .Returns(new List<IUserContext> { new UserContext(Guid.NewGuid().ToString(), "HeHuan") });

userServiceMock.Setup(p => p.Switch(It.IsNotNull<string>()))
    .Returns(new UserContext(Guid.NewGuid().ToString(), "KangJia"));

userServiceMock.Setup(p => p.FindUser(It.IsIn<string>("TanMu"), It.IsAny<string>()))
    .Returns(new UserContext(Guid.NewGuid().ToString(), "TanMu"));

var userService = userServiceMock.Object;

var currentUser = userService.Current;
Debug.Assert(currentUser.Name == "admin");

var onlineUsers = userService.GetOnlineUser();
Debug.Assert(onlineUsers.Count == 1);
Debug.Assert(onlineUsers[0].Name == "HeHuan");

var switchUser = userService.Switch(Guid.NewGuid().ToString());
Debug.Assert(switchUser.Name == "KangJia");

var findUser = userService.FindUser("TanMu", "ko");
Debug.Assert(findUser.Name == "TanMu");
