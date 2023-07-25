using System.Data;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SAR0083_LogFiles
{
    public class Logger
    {
        public enum Rule { Required, Length, Int, Email, TimeFormat, Num, URL, Encoding, AllowedCharacters, MD5Hash, JoinSourceTableColumn }
        private enum logType { Info, Error }
        
        private string logFilePath;
        private string errorLogFilePath;
        private string excelFilePath;
        private string textFilePath;
        private Dictionary<string, Dictionary<string, List<(Rule rule, Dictionary<string, object> ruleArgs)>>> excelColRules;
        private Dictionary<string, List<(Rule rule, Dictionary<string, object> ruleArgs)>> textColRules;
        
        private FileLoader fileLoader = new FileLoader();

        public Logger(string logFilePath, string errorLogFilePath, string excelFilePath, string textFilePath, Dictionary<string, Dictionary<string, List<(Rule rule, Dictionary<string, object> ruleArgs)>>> excelColRules, Dictionary<string, List<(Rule rule, Dictionary<string, object> ruleArgs)>> textColRules)
        {
            this.logFilePath = logFilePath;
            this.errorLogFilePath = errorLogFilePath;
            this.excelFilePath = excelFilePath;
            this.textFilePath = textFilePath;
            this.excelColRules = excelColRules;
            this.textColRules = textColRules;

            CreateLogFiles();
        }

        private void CreateLogFiles()
        {
            File.Delete(logFilePath);
            File.Delete(errorLogFilePath);
            File.Create(logFilePath).Close();
            File.Create(errorLogFilePath).Close();

            WriteLog("Started at " + GetTimestamp() + "\n\n", true);

            WriteLog(Path.GetFileName(excelFilePath), true, logType.Error);
            DataTableCollection excelData = null;
            if (!File.Exists(excelFilePath))
            {
                WriteLog(" (file not found)\n", true, logType.Error);
            }
            else
            {
                excelData = fileLoader.LoadExcelFile(excelFilePath); // ID, Jmeno, Prijmeni, Vek, Web, Mail, Telefon

                WriteLog(" (" + excelData.Count + " table" + (excelData.Count != 1 ? "s" : "") + " found)", true);
                WriteLog("\n", true, logType.Error);
                for (int i = 0; i < excelData.Count; i++)
                {
                    DataTable table = excelData[i];
                    
                    WriteLog(Path.GetFileName(excelFilePath) + " / " + table.ToString() + " (" + table.Rows.Count + " row" + (table.Rows.Count != 1 ? "s" : "") + " found)\n", true);

                    ValidateTableColumns(table, 2, excelColRules[table.ToString()]);
                }
            }

            WriteLog("\n" + Path.GetFileName(textFilePath), true, logType.Error);
            if (!File.Exists(textFilePath))
            {
                WriteLog(" (file not found)\n", true, logType.Error);
            }
            else
            {
                DataTable textData = fileLoader.LoadTextFile(textFilePath, new List<string>() { "ID", "UserID", "Time", "Data", "MD5hash" });

                WriteLog(textData.ToString() + " (" + textData.Rows.Count + " row" + (textData.Rows.Count != 1 ? "s" : "") + " found)", true);
                WriteLog("\n", true, logType.Error);

                ValidateTableColumns(textData, 1, textColRules, excelData);
            }
        }

        private void ValidateTableColumns(DataTable table, int rowStart, Dictionary<string, List<(Rule rule, Dictionary<string, object> ruleArgs)>> colRulesByTable, DataTableCollection sourceData = null)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                int rowID = i + rowStart;
                bool rowErrors = false;

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    DataColumn col = table.Columns[j];
                    List<(Rule rule, Dictionary<string, object> ruleArgs)> colRulesByCol = colRulesByTable[col.ToString()];

                    for (int k = 0; k < colRulesByCol.Count; k++)
                    {
                        (Rule rule, Dictionary<string, object> ruleArgs) colRule = colRulesByCol[k];

                        switch (colRule.rule)
                        {
                            case Rule.Required:
                                if (row[col.ToString()].ToString().Length == 0)
                                {
                                    rowErrors = true;
                                    WriteLog(col.ToString() + " is required but is not set.", false, logType.Error, null, rowID);
                                }
                                break;
                            case Rule.Length:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    if (colRule.ruleArgs.ContainsKey("Eq") && (int)colRule.ruleArgs["Eq"] != row[col.ToString()].ToString().Length)
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " should have length of <" + (int)colRule.ruleArgs["Eq"] + "> but the given value <" + row[col.ToString()].ToString() + "> has length of <" + row[col.ToString()].ToString().Length + ">.", false, logType.Error, null, rowID);
                                    }
                                    else if (colRule.ruleArgs.ContainsKey("Min") && colRule.ruleArgs.ContainsKey("Max") && ((int)colRule.ruleArgs["Min"] > row[col.ToString()].ToString().Length || (int)colRule.ruleArgs["Max"] < row[col.ToString()].ToString().Length))
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " should have length between <" + (int)colRule.ruleArgs["Min"] + "> and <" + (int)colRule.ruleArgs["Max"] + "> but the given value <" + row[col.ToString()].ToString() + "> has length of <" + row[col.ToString()].ToString().Length + ">.", false, logType.Error, null, rowID);
                                    }
                                    else if (colRule.ruleArgs.ContainsKey("Min") && (int)colRule.ruleArgs["Min"] > row[col.ToString()].ToString().Length)
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " should have length at least <" + (int)colRule.ruleArgs["Min"] + "> but the given value <" + row[col.ToString()].ToString() + "> has length of <" + row[col.ToString()].ToString().Length + ">.", false, logType.Error, null, rowID);
                                    }
                                    else if (colRule.ruleArgs.ContainsKey("Max") && (int)colRule.ruleArgs["Max"] < row[col.ToString()].ToString().Length)
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " should have length at most <" + (int)colRule.ruleArgs["Max"] + "> but the given value <" + row[col.ToString()].ToString() + "> has length of <" + row[col.ToString()].ToString().Length + ">.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.Int:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    if (!int.TryParse(row[col.ToString()].ToString(), out _))
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> does not contain a valid integer.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.Email:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    try
                                    {
                                        MailAddress emailToCheck = new MailAddress(row[col.ToString()].ToString());
                                    }
                                    catch
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> does not contain a valid email address.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.TimeFormat:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    string timeFormatToCheck = row[col.ToString()].ToString();
                                    try
                                    {
                                        timeFormatToCheck = row[col.ToString()].ToString().Split("<")[1].Split(">")[0];
                                        if (!DateTime.TryParse(timeFormatToCheck, out _))
                                        {
                                            rowErrors = true;
                                            WriteLog(col.ToString() + " <" + timeFormatToCheck + "> does not contain a valid time format.", false, logType.Error, null, rowID);
                                        }
                                    }
                                    catch
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " <" + timeFormatToCheck + "> does not contain a valid time format.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.Num:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    string pattern = @"^[0-9]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    if (!regex.IsMatch(row[col.ToString()].ToString()))
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " < " + row[col.ToString()].ToString() + "> does not contain a valid number.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.URL:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    string pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    if (!regex.IsMatch(row[col.ToString()].ToString()))
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> does not contain a valid url.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.Encoding:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    if (colRule.ruleArgs.ContainsKey("Language"))
                                    {
                                        switch (colRule.ruleArgs["Language"])
                                        {
                                            case "cs_CZ":
                                                string language = "Czech (" + colRule.ruleArgs["Language"] + ")";
                                                string pattern = @"^[a-zA-ZáčďéěíľňóřšťúůýžÁČĎÉĚÍĽŇÓŘŠŤÚŮÝŽ\\s]*$";
                                                Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                                if (!regex.IsMatch(row[col.ToString()].ToString()))
                                                {
                                                    rowErrors = true;
                                                    WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> does not meet the valid encoding of language <" + language + ">.", false, logType.Error, null, rowID);
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            case Rule.AllowedCharacters:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    if (colRule.ruleArgs.ContainsKey("Regex"))
                                    {
                                        string allowedCharacters = string.Join("", ((List<string>) colRule.ruleArgs["Regex"]).ToArray());
                                        string pattern = @"^[" + allowedCharacters + "]*$";
                                        Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                        if (!regex.IsMatch(row[col.ToString()].ToString()))
                                        {
                                            rowErrors = true;
                                            WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> contains invalid characters outside the set of allowed characters <" + allowedCharacters + ">.", false, logType.Error, null, rowID);
                                        }
                                    }
                                }
                                break;
                            case Rule.MD5Hash:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    string expectedHash = GetMD5Hash(row["Data"].ToString());
                                    if (row[col.ToString()].ToString() != expectedHash)
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> is not equal to the expected hash <" + expectedHash + ">.", false, logType.Error, null, rowID);
                                    }
                                }
                                break;
                            case Rule.JoinSourceTableColumn:
                                if (colRulesByCol.Any(colRule => colRule.rule == Rule.Required) || row[col.ToString()].ToString().Length > 0)
                                {
                                    if (sourceData == null)
                                    {
                                        rowErrors = true;
                                        WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> is not found in source data table <" + (string)colRule.ruleArgs["Table"] + "> because the source data file <" + Path.GetFileName(excelFilePath) + "> is not found.", false, logType.Error, null, rowID);
                                    }
                                    else
                                    {
                                        if (colRule.ruleArgs.ContainsKey("Table") && colRule.ruleArgs.ContainsKey("Column"))
                                        {
                                            DataTable sourceTable = null;
                                            try
                                            {
                                                sourceTable = sourceData[(string)colRule.ruleArgs["Table"]];
                                            }
                                            catch
                                            {
                                                rowErrors = true;
                                                WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> is not found in source data table <" + (string)colRule.ruleArgs["Table"] + "> because the source data file <" + Path.GetFileName(excelFilePath) + "> is not found.", false, logType.Error, null, rowID);
                                            }
                                            
                                            if (sourceTable != null)
                                            {
                                                DataColumn sourceCol = null;
                                                try
                                                {
                                                    sourceCol = sourceTable.Columns[(string)colRule.ruleArgs["Column"]];
                                                }
                                                catch
                                                {
                                                    rowErrors = true;
                                                    WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> is not found in source data column <" + (string)colRule.ruleArgs["Column"] + "> because the source data table <" + (string)colRule.ruleArgs["Table"] + "> is not found.", false, logType.Error, null, rowID);
                                                }

                                                if (sourceCol != null)
                                                {
                                                    bool joinSuccessful = false;

                                                    for (int l = 0; l < sourceTable.Rows.Count; l++)
                                                    {
                                                        if (row[col.ToString()].ToString().Length > 0 && int.Parse(sourceTable.Rows[l][sourceCol.ToString()].ToString()) == int.Parse(row[col.ToString()].ToString()))
                                                        {
                                                            joinSuccessful = true;
                                                            break;
                                                        }
                                                    }

                                                    if (!joinSuccessful)
                                                    {
                                                        rowErrors = true;
                                                        WriteLog(col.ToString() + " <" + row[col.ToString()].ToString() + "> is not found in source data column <" + (string)colRule.ruleArgs["Column"] + ">.", false, logType.Error, null, rowID);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                if (!rowErrors)
                {
                    WriteLog("", false, logType.Info, int.Parse(row["ID"].ToString()), rowID);
                }
            }
        }

        private string GetMD5Hash(string data)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] input = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hash = md5.ComputeHash(input);
            return Convert.ToHexString(hash);
        }

        private string GetLog(string logText, bool clearLogText = false, logType logType = logType.Info, int? id = null, int? row = null)
        {
            string logFullText = "";

            if(clearLogText)
            {
                logFullText = logText;
            }
            else
            {
                logFullText += "{" + GetTimestamp();

                switch (logType)
                {
                    case logType.Error:
                        logFullText += " Error";
                        if (row != null) { logFullText += " at row <" + row + ">"; }
                        break;
                    default:
                        if (id != null) { logFullText += " <" + id + ">"; }
                        if (row != null) { logFullText += " <" + row + ">"; }
                        break;
                }

                logFullText += (logText.Length > 0 ? ": " + logText : "") + "}\n";
            }

            return logFullText;
        }

        private void WriteLog(string logFullText, logType logType)
        {
            File.AppendAllText(logFilePath, logFullText);
            switch (logType)
            {
                case logType.Error:
                    File.AppendAllText(errorLogFilePath, logFullText);
                    break;
            }
        }

        private void WriteLog(string logText, bool clearLogText = false, logType logType = logType.Info, int? id = null, int? row = null)
        {
            WriteLog(GetLog(logText, clearLogText, logType, id, row), logType);
        }

        private string GetTimestamp()
        {
            return "<" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff") + ">";
        }
    }
}

/*
 void CreateLogFile()
{
    string logFileContent = "Application launched at " + DateTime.Now + "\n";
    string errorLogFileContent = "";

    logFileContent += "Users.xlsx log (" + usersList.Count + " entries found)" + "\n";
    errorLogFileContent += "Users.xlsx error log\n";
    for (int i = 0; i < usersList.Count; i++)
    {
        bool isIdNumber = int.TryParse(usersList[i].ID, out _);
        if(!isIdNumber)
        {
            logFileContent += "<" + DateTime.Now + "> Error: ID <" + usersList[i].ID + "> is not a number." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: ID <" + usersList[i].ID + "> is not a number." + " (row: " + usersList[i].row + ")\n";
        }

        bool isNameCzech = Regex.IsMatch(usersList[i].jmeno, "^[a-zA-ZáčďéěíňóřšťůúýžÁČĎÉĚÍŇÓŘŠŤŮÚÝŽ\\s]*$");
        if(!isNameCzech)
        {
            logFileContent += "<" + DateTime.Now + "> Error: Name <" + usersList[i].jmeno + "> includes non-Czech characters." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Name <" + usersList[i].jmeno + "> includes non-Czech characters." + " (row: " + usersList[i].row + ")\n";
        }

        bool isSurnameCzech = Regex.IsMatch(usersList[i].prijmeni, "^[a-zA-ZáčďéěíňóřšťůúýžÁČĎÉĚÍŇÓŘŠŤŮÚÝŽ\\s]*$");
        if (!isSurnameCzech)
        {
            logFileContent += "<" + DateTime.Now + "> Error: Surname <" + usersList[i].prijmeni + "> includes non-Czech characters." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Surname <" + usersList[i].prijmeni + "> includes non-Czech characters." + " (row: " + usersList[i].row + ")\n";
        }

        bool isAgeNumber = int.TryParse(usersList[i].vek, out _);
        if (!isAgeNumber)
        {
            logFileContent += "<" + DateTime.Now + "> Error: Age <" + usersList[i].vek + "> is not a number." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Age <" + usersList[i].vek + "> is not a number." + " (row: " + usersList[i].row + ")\n";
        }

        bool isEmailValid = Regex.IsMatch(usersList[i].mail, "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        if (!isEmailValid)
        {
            logFileContent += "<" + DateTime.Now + "> Error: Mail address <" + usersList[i].mail + "> format is not valid." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Mail address <" + usersList[i].mail + "> format is not valid." + " (row: " + usersList[i].row + ")\n";
        }

        bool isPhoneNumber = usersList[i].telefon.All(char.IsDigit);
        if(usersList[i].telefon.Length == 0 || !isPhoneNumber)
        {
            logFileContent += "<" + DateTime.Now + "> Error: Phone <" + usersList[i].telefon + "> is not a number." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Phone <" + usersList[i].telefon + "> is not a number." + " (row: " + usersList[i].row + ")\n";
        }

        bool isPhoneNineCharsLong = true;
        if (usersList[i].telefon.Length != 9)
        {
            isPhoneNineCharsLong = false;
            logFileContent += "<" + DateTime.Now + "> Error: Phone <" + usersList[i].telefon + "> does not have 9 characters." + " (row: " + usersList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Phone <" + usersList[i].telefon + "> does not have 9 characters." + " (row: " + usersList[i].row + ")\n";
        }

        if (isIdNumber && isNameCzech && isSurnameCzech && isAgeNumber && isEmailValid && isPhoneNumber && isPhoneNineCharsLong)
        {
            logFileContent += "{<" + DateTime.Now + "> <" + usersList[i].ID + "> <" + usersList[i].row + ">}" + "\n";
        }
        else
        {
            userIdErrorList.Add(usersList[i].ID);
        }
    }

    logFileContent += "\nData.txt log (" + dataList.Count + " entries found)" + "\n";
    errorLogFileContent += "\nData.txt error log\n";
    for (int i = 0; i < dataList.Count; i++)
    {
        if(dataList[i].ID == "Not enough arguments provided")
        {
            logFileContent += "<" + DateTime.Now + "> Error: Not enough arguments provided." + " (row: " + dataList[i].row + ")\n";
            errorLogFileContent += "<" + DateTime.Now + "> Error: Not enough arguments provided." + " (row: " + dataList[i].row + ")\n";
        }
        else
        {
            bool isIdNumber = int.TryParse(dataList[i].ID, out _);
            if (!isIdNumber)
            {
                logFileContent += "<" + DateTime.Now + "> Error: ID <" + dataList[i].ID + "> is not a number." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: ID <" + dataList[i].ID + "> is not a number." + " (row: " + dataList[i].row + ")\n";
            }

            bool isUserIdNumber = int.TryParse(dataList[i].userID, out _);
            if (!isUserIdNumber)
            {
                logFileContent += "<" + DateTime.Now + "> Error: UserID <" + dataList[i].userID + "> is not a number." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: UserID <" + dataList[i].userID + "> is not a number." + " (row: " + dataList[i].row + ")\n";
            }

            bool userIdExists = true;
            if(!userIdList.Contains(dataList[i].userID))
            {
                userIdExists = false;
                logFileContent += "<" + DateTime.Now + "> Error: UserID <" + dataList[i].userID + "> doesn't exist in the Users.xlsx file." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: UserID <" + dataList[i].userID + "> doesn't exist in the Users.xlsx file." + " (row: " + dataList[i].row + ")\n";
            }

            bool userIdNoError = true;
            if (userIdErrorList.Contains(dataList[i].userID))
            {
                userIdNoError = false;
                logFileContent += "<" + DateTime.Now + "> Error: UserID <" + dataList[i].userID + "> exists in the Users.xlsx file, but there is an error with this user." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: UserID <" + dataList[i].userID + "> exists in the Users.xlsx file, but there is an error with this user." + " (row: " + dataList[i].row + ")\n";
            }

            DateTime dateTime;
            bool isTimeValid = DateTime.TryParse(dataList[i].time, out dateTime);
            if (!isTimeValid)
            {
                logFileContent += "<" + DateTime.Now + "> Error: Time <" + dataList[i].time + "> format is not valid." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: Time <" + dataList[i].time + "> format is not valid." + " (row: " + dataList[i].row + ")\n";
            }

            bool isTimeInCurrentYear = true;
            if (isTimeValid)
            {
                if(dateTime.Year != 2022)
                {
                    isTimeInCurrentYear = false;
                    logFileContent += "<" + DateTime.Now + "> Error: Time <" + dataList[i].time + "> has invalid year (" + dateTime.Year + "). (row: " + dataList[i].row + ")\n";
                    errorLogFileContent += "<" + DateTime.Now + "> Error: Time <" + dataList[i].time + "> has invalid year (" +dateTime.Year + "). (row: " + dataList[i].row + ")\n";
                }
            }

            bool isDataContentValid = Regex.IsMatch(dataList[i].data, "^[a-fA-F0-5+-]*$");
            if (!isDataContentValid)
            {
                logFileContent += "<" + DateTime.Now + "> Error: Data <" + dataList[i].data + "> contains forbidden characters." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: Data <" + dataList[i].data + "> contains forbidden characters." + " (row: " + dataList[i].row + ")\n";
            }

            bool isDataLengthValid = true;
            if (dataList[i].data.Length < 20 || dataList[i].data.Length > 50)
            {
                isDataLengthValid = false;
                logFileContent += "<" + DateTime.Now + "> Error: Data <" + dataList[i].data + "> length (" + dataList[i].data.Length + ") is not valid." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: Data <" + dataList[i].data + "> length (" + dataList[i].data.Length + ") is not valid." + " (row: " + dataList[i].row + ")\n";
            }

            bool isHashCorrect = true;
            if (dataList[i].hash != CreateMD5(dataList[i].data))
            {
                isHashCorrect = false;
                logFileContent += "<" + DateTime.Now + "> Error: Hash <" + dataList[i].hash + "> is not correct." + " (row: " + dataList[i].row + ")\n";
                errorLogFileContent += "<" + DateTime.Now + "> Error: Hash <" + dataList[i].hash + "> is not correct." + " (row: " + dataList[i].row + ")\n";
            }

            if (isIdNumber && isUserIdNumber && userIdExists && userIdNoError && isTimeValid && isTimeInCurrentYear && isDataContentValid && isDataLengthValid && isHashCorrect)
            {
                logFileContent += "{<" + DateTime.Now + "> <" + dataList[i].ID + "> <" + dataList[i].row + ">}" + "\n";
            }
        }
    }

    File.WriteAllText(workspace + "\\LogFile.txt", logFileContent);
    File.WriteAllText(workspace + "\\ErrorsLogFile.txt", errorLogFileContent);
}

string CreateMD5(string input)
{
    using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
    {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes); 
    }
}
 */