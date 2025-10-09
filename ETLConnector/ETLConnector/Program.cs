using ETLConnector.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    public static void Main(String[] args)
    {
        //auth
        /*var body = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", "3MVG9gtjsZa8aaSX9642R9nl_tEKDkuvns6jjhkHI.j441iYmOiY62rBfIhxDys1kkHXgFmbs9obAx2gXtOui" },
            { "client_secret", "8CD65078B95A562DF79D1F29C448CAB611E586CBFD5574B87573869C06054C5D" },
            { "username", "it@qualtech.ca.pilot" },
            { "password", "ot72XLQHxWjWyS" }
        };

        ETLConnector.Connector.SFConnector sfConnector = new ETLConnector.Connector.SFConnector("https://platform-flow-1867--pilot.sandbox.my.salesforce-setup.com", "services/oauth2/token", "");
        
        
        ETLConnector.Model.ConnectorOperationResult connectorOpRes = sfConnector.AuthenticateAsync(body, "POST",  new Dictionary<string, string>() { }).Result;


        string test = "test";

        //get api metadata
        Connector Connector = new Connector(
            "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
            "services/data/v59.0/sobjects",
            "v59.0",
            connectorOpRes.RawResponse["access_token"].ToString()
            );
       // var data = Connector.SendObjectAsync(new { }, "GET", "Opportunity/describe","",new Dictionary<string,string>(),100).Result;

        

        //create
        Connector Connector2 = new Connector(
            "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
            "services/data/v59.0/sobjects",
            "v59.0",
            connectorOpRes.RawResponse["access_token"].ToString()
            );


        //var data2 = Connector2.SendObjectAsync(new { Name = "TEST AF 10-09", AccountId = "001Aq00000dhuAGIAY", TECH_Company__c = "QS" }, "POST", "Opportunity","", new Dictionary<string, string>(),100).Result;


        //update 
        Connector Connector3 = new Connector(
            "https://platform-flow-1867--pilot.sandbox.my.salesforce.com",
            "services/data/v59.0/sobjects",
            "v59.0",
            connectorOpRes.RawResponse["access_token"].ToString()
            );

        var data3 = Connector3.SendObjectAsync(new { Description = "TEST" } , "PATCH", "Opportunity", "006Aq00000SDkwGIAT", new Dictionary<string, string>(), 100).Result;*/



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
                        Description = "test antoine 10-09 1 2",
  //                      CurrencyCode= "CAD",
  //                      ConRevMethod= "MAN",
 //                      RateGrpCode = "MAIN",
//                        SysRevID    = 0x00000000850122F5, 
//                        SysRowID    = "83C4FC16-C6AD-4E7F-B35E-4AAB99EC2AE2",
//                        RowMod      = "U"
                    }
                }
            },
            continueProcessingOnError = true,
            rollbackParentOnChildError = true
        };

        //auth
        EpicorConnector epiConnector = new EpicorConnector(
            "https://qbcdeverpapp.qualtech.int",
            "ERPPilot/TokenResource.svc",
            "v2"
        );
        Dictionary<string,string> map = new Dictionary<string,string>();
        map["Username"] = "TASK";
        map["Password"] = "LuK^d6swSwj4";
        var content = new StringContent("", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        var data = epiConnector.AuthenticateAsync(new Dictionary<string, string>(), "POST",  map).Result;

        //Get meta data
        Connector epiConnector1 = new Connector(
            "https://qbcdeverpapp.qualtech.int",
            "ERPPilot/api/v2/odata/QS",
            "v2",
            data.RawResponse["AccessToken"].ToString()
         );

        Dictionary<string, string> headersAdd = new Dictionary<string, string>();
        headersAdd["X-API-Key"] = "kEcTYTqXn4sKi20uctfbqsTEETbrqCyEUfmXiYbA8RPbJ";

        var data1 = epiConnector1.SendObjectAsync(request, "GET", "Erp.BO.ProjectSvc/$metadata?$format=json", "", headersAdd, 100).Result;

        //update 
        Connector epiConnector2 = new Connector(
            "https://qbcdeverpapp.qualtech.int",
            "ERPPilot/api/v2/odata/QS",
            "v2",
            data.RawResponse["AccessToken"].ToString()
         );



        var data2 = epiConnector2.SendObjectAsync(request, "POST", "Erp.BO.ProjectSvc/UpdateExt", "", headersAdd, 100).Result;*/
    }

}

