using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace Eclipse.UI.Tests.Integration.ExcelDataAccess.UserCredentials
{
    public class UserDataAccess
    {
        private const string fileName = "UserCredentials.xlsx";

        public static UserData GetUserCredentails(string userKey)
        {
            var usersDetails = AccessUserDetails();
            return usersDetails.FirstOrDefault(user => user.Key == userKey);
        }

        private static List<UserData> AccessUserDetails()
        {
            string projectPath = Directory.GetParent(
            Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).ToString()
            ).ToString();

            string filePath = string.Concat(projectPath, "\\Integration\\ExcelDataAccess\\UserCredentials\\",
                fileName);

            var dataTable = ExcelDataAccess.DataAccess.ImportExcelData(filePath);
            var userList = (from DataRow dataRow in dataTable.Rows
                            select new UserData()
                            {
                                Key = dataRow["Key"].ToString(),
                                Username = dataRow["Username"].ToString(),
                                Password = dataRow["Password"].ToString()
                            }).ToList();

            return userList;
        }

        public class UserData
        {
            public string Key { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}