using SAR0083_LogFiles;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

// Settings
string rootPath = "../../../";
string logFilePath = rootPath + "LogFile.txt";
string errorLogFilePath = rootPath + "ErrorsLogFile.txt";
string excelFilePath = rootPath + "Users.xlsx";
string textFilePath = rootPath + "Data.txt";
Dictionary<string, Dictionary<string, List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>>> excelColRules = new Dictionary<string, Dictionary<string, List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>>>() {
    { "List 1", new Dictionary<string, List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>>() {
        { "ID", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.Required, new Dictionary<string, object>()),
            (Logger.Rule.Int, new Dictionary<string, object>())
        } },
        { "Jmeno", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.Required, new Dictionary<string, object>()),
            (Logger.Rule.Encoding, new Dictionary<string, object>() {
                { "Language", "cs_CZ" }
            })
        } },
        { "Prijmeni", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.Required, new Dictionary<string, object>()),
            (Logger.Rule.Encoding, new Dictionary<string, object>() {
                { "Language", "cs_CZ" }
            })
        } },
        { "Vek", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.Required, new Dictionary<string, object>()),
            (Logger.Rule.Int, new Dictionary<string, object>())
        } },
        { "Web", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.URL, new Dictionary<string, object>())
        } },
        { "Mail", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.Required, new Dictionary<string, object>()),
            (Logger.Rule.Email, new Dictionary<string, object>())
        } },
        { "Telefon", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
            (Logger.Rule.Required, new Dictionary<string, object>()),
            (Logger.Rule.Num, new Dictionary<string, object>()),
            (Logger.Rule.Length, new Dictionary<string, object>() {
                { "Eq", 9 }
            })
        } }
    } }
};
Dictionary<string, List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>> textColRules = new Dictionary<string, List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>>() {
    { "ID", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
        (Logger.Rule.Required, new Dictionary<string, object>()),
        (Logger.Rule.Int, new Dictionary<string, object>())
    } },
    { "UserID", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
        (Logger.Rule.Required, new Dictionary<string, object>()),
        (Logger.Rule.Int, new Dictionary<string, object>()),
        (Logger.Rule.JoinSourceTableColumn, new Dictionary<string, object>() {
            { "Table", "List 1" },
            { "Column", "ID" }
        })
    } },
    { "Time", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
        (Logger.Rule.Required, new Dictionary<string, object>()),
        (Logger.Rule.TimeFormat, new Dictionary<string, object>())
    } },
    { "Data", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
        (Logger.Rule.Required, new Dictionary<string, object>()),
        (Logger.Rule.AllowedCharacters, new Dictionary<string, object>() {
            { "Regex", new List<string>() {
                "A-F",
                "a-f",
                "0-5",
                "+",
                "-"
            } }
        }),
        (Logger.Rule.Length, new Dictionary<string, object>() {
            { "Min", 20 },
            { "Max", 50 }
        })
    } },
    { "MD5hash", new List<(Logger.Rule rule, Dictionary<string, object> ruleArgs)>() {
        (Logger.Rule.Required, new Dictionary<string, object>()),
        (Logger.Rule.MD5Hash, new Dictionary<string, object>())
    } }
};

// Run
new Logger(logFilePath, errorLogFilePath, excelFilePath, textFilePath, excelColRules, textColRules);