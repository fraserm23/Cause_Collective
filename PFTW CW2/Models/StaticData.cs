using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PFTW_CW2.Models
{
    public class StaticData
    {
        static StaticData instance = null;
        List<User> exampleSigList { get; set; }
        List<Cause> exampleCauseList { get; set; }

        StaticData()
        {
            User admin = new User
            {
                id = 1,
                firstName = "Fraser",
                lastName = "McMillan",
                email = "40086074@live.napier.ac.uk",
                password = "password1234",
                isAdmin = true,
                isActive = true
            };

            User userTwo = new User
            {
                id = 1,
                firstName = "John",
                lastName = "Smith",
                email = "john@smith.com",
                password = "password5678",
                isAdmin = false,
                isActive = true
            };

            User userThree = new User
            {
                id = 1,
                firstName = "Mary",
                lastName = "Smith",
                email = "mary@smith.com",
                password = "password2468",
                isAdmin = false,
                isActive = true
            };

            exampleSigList = new List<User>();

            exampleSigList.Add(admin);
            exampleSigList.Add(userTwo);
            exampleSigList.Add(userThree);

            Cause causeOne = new Cause
            {
                id = 1,
                title = "Save the Amazon",
                description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc nisl mauris, tincidunt nec lorem in, maximus porta arcu. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Nam porttitor, nulla vel vulputate interdum, orci sapien aliquet ex, quis vehicula dui orci vel nisl. Ut condimentum enim eget dolor pharetra condimentum. Duis vitae arcu ut leo imperdiet eleifend. Nunc venenatis, quam vel lobortis sagittis, eros justo gravida est, id vulputate leo nulla et orci. In vitae porta odio. Suspendisse blandit, sapien ut luctus molestie, diam arcu feugiat risus, in euismod ligula lacus et mi. Vestibulum feugiat a elit non finibus. Nulla commodo urna id eleifend congue. Donec in quam eu nulla dictum tincidunt a faucibus enim. Duis in blandit diam. In dictum libero in imperdiet malesuada. Proin lacinia auctor turpis at dignissim. Mauris egestas ante et luctus vestibulum.",
                signatureCount = 1000,
                signatureList = exampleSigList,
                imageURL = "#",
                isActive = true
            };

            exampleCauseList = new List<Cause>();

            exampleCauseList.Add(causeOne);
         }

        public static StaticData Instance
        {
            get
            {
                if (instance == null) instance = new StaticData();
                return instance;
            }
        }
    }
}