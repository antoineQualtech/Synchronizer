using ETLConnector.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    public static void Main(String[] args)

    //notes 
    /* {
       "ds": {
         "Project": [
           {
             "Company": "QS",
             "ProjectID": "250458",
             "Description": "test antoine",
             "CurrencyCode": "CAD",
             "ConRevMethod": "MAN",
             "RateGrpCode": "MAIN",
             "SysRevID": 0x0000000084B260A2, 
             "SysRowID": "83C4FC16-C6AD-4E7F-B35E-4AAB99EC2AE2",
             "RowMod": "U"
           }
         ]
       }
     }*/
    //pour update un projet ces champs sont obligatoire et aussi utiliser UpdateExt pour update les champs voulu uniquement

    {
        //soql : select id,Name, Motif__c,CreatedDate from Opportunity where name = 'TEST' and accountid = '001Aq00000dhuAGIAY'
        // D:\Epicor\Erp11\localclients\ErpPilot
        // D:\websites\ERPPilot\Server\Assemblies
        // reset app pool et recycle iis


        //get api metadata
        /*Connector sfConnector = new Connector(
            "00DHp0000033JkA!AQEAQNdQEY4sNQkAc64G2D.Qr5kYfj45bmVTXF5CYN0vF_s8tRwLL6dMX5CdMjRzR1FVQoEXXM4EUaN5OxmFZ8r1IYkj6YIW",
            "https://platform-flow-1867.my.salesforce.com",
            "/services/data/v59.0/sobjects/",
            "v59.0",
            "Opportunity/describe"
            );*/
        //var data = sfConnector.SendSObjectAsync(new { }, "GET", "").Result;

        //update 
        /*Connector sfConnector = new Connector(
            "00DAq000007wKLi!AQEAQGWpKdOOJKu1udQM8Rb2VMnM7kaYfYErcpz97PETV39gGZRHZCVCz_YIzu6M.I8quhS3.jcXFIIqIP0FBv3agMvlWzM0",
            "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
            "/services/data/v59.0/sobjects/",
            "v59.0",
            "Opportunity"
            );

        var data =  sfConnector.SendSObjectAsync(new { Name = "TEST" } , "PATCH", "006Aq00000Ru5LeIAJ").Result;*/

        //create
        /*Connector sfConnector = new Connector(
            "00DAq000007wKLi!AQEAQGWpKdOOJKu1udQM8Rb2VMnM7kaYfYErcpz97PETV39gGZRHZCVCz_YIzu6M.I8quhS3.jcXFIIqIP0FBv3agMvlWzM0",
            "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
            "/services/data/v59.0/sobjects/",
            "v59.0",
            "Opportunity"
            );

        var data = sfConnector.SendSObjectAsync(new { Name = "TEST", AccountId = "001Aq00000dhuAGIAY", TECH_Company__c = "QS" }, "POST", "").Result;*/

        //delete
        /*Connector sfConnector = new Connector(
                "00DAq000007wKLi!AQEAQGWpKdOOJKu1udQM8Rb2VMnM7kaYfYErcpz97PETV39gGZRHZCVCz_YIzu6M.I8quhS3.jcXFIIqIP0FBv3agMvlWzM0",
                "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
                "/services/data/v59.0/sobjects/",
                "v59.0",
                "Opportunity"
                );

            var data = sfConnector.SendSObjectAsync(new {  }, "DELETE", "006Aq00000RtyTrIAJ").Result;*/

        /*Connector sfConnector = new Connector(
        "00DAq000007wKLi!AQEAQPG4crz7yR3zbRFio0jFjs0FjqwGx_tCJXKkS62RTp44z3hzmay5.W4Z1Q2RiKruYc1G.zFY7T9grIN6lulSGWD0y6na",
        "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
        "/services/data/v59.0/sobjects/",
        "v59.0",
        "Account"
        );

        var data = sfConnector.SendSObjectAsync(new { Name = "TESTaccountetl" }, "POST", "", new Dictionary<string, string>()).Result;*/
        //select id,Name, Motif__c,CreatedDate from Opportunity where id = '006Aq00000Ru5LeIAJ' 
        /* Connector sfConnector = new Connector(
              "00DAq000007wKLi!AQEAQAdi8EtD0hf3V27bRg1kn304RTNaA_oa8N84A14bS5gJJPjKhf_P75VtYLCvndnly0jXULVe5f1l5cZgyJ9saYO8myph",
              "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
              "/services/data/v59.0/sobjects/",
             "v59.0",
             "Opportunity"
             );

        var data = sfConnector.SendSObjectAsync(new { Name = "TEST AF 10-08", AccountId = "001Aq00000dhuAGIAY", TECH_Company__c = "QS" }, "POST", "", new Dictionary<string, string>()).Result;*/

        /*var request = new
        {
            ds = new
            {
                Project = new[]
                {
                    new
                    {
                        Company     = "QS",
                        ProjectID   = "250458",
                        Description = "test antoine ttt666",
                        CurrencyCode= "CAD",
                        ConRevMethod= "MAN",
                        RateGrpCode = "MAIN",
                        SysRevID    = 0x0000000085011C22, 
                        SysRowID    = "83C4FC16-C6AD-4E7F-B35E-4AAB99EC2AE2",
                        RowMod      = "U"
                    }
                }
            },
            continueProcessingOnError = true,
            rollbackParentOnChildError = true
        };

        Connector epiConnector = new Connector(
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOiIxNzU5Mjc1OTYxIiwiaWF0IjoiMTc1OTIzOTk2MSIsImlzcyI6ImVwaWNvciIsImF1ZCI6ImVwaWNvciIsInVzZXJuYW1lIjoiVEFTSyJ9.mNdC1B_slxjAuAnwUoMaZfPCOkFwlDYt3C-Qu2V3jlg",
        "https://qbcdeverpapp.qualtech.int/erppilot",
        "api/v2/odata/QS",
        "v2",
        "Erp.BO.ProjectSvc"
        );
        Dictionary<string,string> map = new Dictionary<string,string>();
        map["X-API-Key"] = "kEcTYTqXn4sKi20uctfbqsTEETbrqCyEUfmXiYbA8RPbJ";
        var data = epiConnector.SendSObjectAsync(request, "POST", "UpdateExt", map).Result;*/
    }

}

