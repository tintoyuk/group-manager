using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Group_Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    //structs

    public struct groupmembers
    {
        public int Id { get; set; }
        //[Display(Name = "Display Name")]
        public string samaccountname { get; set; }
        public string displayname { get; set; }

    }

    public struct groupsdata
    {
        public int Id { get; set; }
        //[Display(Name = "Group Name")]
        public string samgroupname { get; set; }
        public string displayname { get; set; }

        public List<groupmembers> groupmembers { get; set; }
    }


    //classes

    public class addata
    {
        public string getsamaccountname()
        {
            var impersonatedUser = WindowsIdentity.GetCurrent();
            var getsamaccountname = impersonatedUser.Name;
            return getsamaccountname;
        }

        public static List<groupsdata> getgroupdata(string domain, string OUpath)
        {
            /*
            Get groups from ad into struc
		    For each group
		    Get members 
            Return struct groups 
            */

            //get ad groups into an instance of groupsdata called ADgroups

            List<groupsdata> ADgroups = new List<groupsdata>();
            var ctx = new PrincipalContext(ContextType.Domain, domain, OUpath);
            GroupPrincipal _groupPrincipal = new GroupPrincipal(ctx);

            PrincipalSearcher srch = new PrincipalSearcher(_groupPrincipal);

            foreach (var found in srch.FindAll())
            {
                ADgroups.Add(new groupsdata { samgroupname = found.ToString() });
            }

            //itterate ADgroups adding group members
            foreach (string searchgroup in ADgroups)
            {

            }
                                 
            return null;
        }

        public static void updategroupdata( )
        {

        }

        public static string getloginsamname()
        {
            var impersonatedUser = WindowsIdentity.GetCurrent().Name;
            return impersonatedUser;
        }


    }

}
