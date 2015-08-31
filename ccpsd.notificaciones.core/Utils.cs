using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace ccpsd.notificaciones.core
{
    /// <summary>
    /// Clase para colocar utilidades estaticas
    /// </summary>
    public static class Utils
    {




        public static string GetAuthToken()
        {
            return GetMd5Hash(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// para encriptar una cadena usando el algoritmo md5, tomado de : 
        /// http://msdn.microsoft.com/en-us/library/s02tk69a(v=vs.110).aspx
        /// </summary>
        /// <param name="input">La cadena a encriptar</param>
        /// <returns>cadena encriptada</returns>
        private static string GetMd5Hash(string input)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        // Verify a hash against a string.
        private static bool VerifyMd5Hash(string input, string hash)
        {

            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static List<string> GetDomainGroups()
        {
            List<string> listGroups = new List<string>();
            try
            {

                PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
                GroupPrincipal qbeGroup = new GroupPrincipal(ctx);
                PrincipalSearcher srch = new PrincipalSearcher(qbeGroup);

                // find all matches
                foreach (var found in srch.FindAll())
                {
                    GroupPrincipal foundGroup = found as GroupPrincipal;

                    if (foundGroup != null)
                    {
                        listGroups.Add(foundGroup.Name);
                    }
                }

                return listGroups;
            }
            catch (Exception ex)
            {

            }

            return listGroups;

        }

        ///// <summary> 
        ///// Gets the base principal context 
        ///// </summary> 
        ///// <returns>Returns the PrincipalContext object which contains Domain</returns> 
        public static PrincipalContext GetPrincipalContext()
        {

            PrincipalContext oPrincipalContext = new PrincipalContext(ContextType.Domain);
            return oPrincipalContext;
        }


   
        public static List<DomainUsers> GetDomainUsers(string group = null)
        {
            List<DomainUsers> listUsers = new List<DomainUsers>();
            try
            {
                //PrincipalContext AD = new PrincipalContext(ContextType.Domain, "LDAP://172.16.10.2");
                PrincipalContext AD = new PrincipalContext(ContextType.Domain, ConfigReader.GetLdapServer());

                UserPrincipal u = new UserPrincipal(AD);
                PrincipalSearcher search = new PrincipalSearcher(u);
                PrincipalSearchResult<Principal> searchResult = null;

                searchResult = search.FindAll();

                var usersList = searchResult.Where(s => string.IsNullOrEmpty(group)
                                                        || s.GetGroups().Any(t => t.Name == group)).ToList();

                foreach (var result in usersList)
                {
                    listUsers.Add(new DomainUsers
                    {
                        UserName = result.SamAccountName,
                        FullName = result.Name
                    });
                }

                return listUsers;
            }
            catch (Exception ex)
            {

            }

            return listUsers;

        }


        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestarCurrentAppAsAdmin(string arg = "")
        {
            if (IsAdministrator() == false)
            {
                // Restart program and run as admin
                Process.GetCurrentProcess().WaitForExit();
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                startInfo.Arguments = arg;
                System.Diagnostics.Process.Start(startInfo);

            }
        }


        public static void RestarCurrentApp(string arg = "")
        {
            if (IsAdministrator() == false)
            {
                // Restart program and run as admin

                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Arguments = arg;
                System.Diagnostics.Process.Start(startInfo);
                Thread.Sleep(2000);
                Environment.Exit(-1);
            }
        }

        public static string GetCurrentUser()
        {
            return Environment.UserName;
        }


        public static string CmdProc(string cmd)
        {
            //which process tho start with which command
            Process.Start("CMD.exe", cmd);

            //just to hide the black window and get any errors from the cmd.exe       
            Process proc = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.FileName = "cmd.exe";
            info.Arguments = cmd;
            proc.StartInfo = info;
            //now the process starts
            proc.Start();
            string res = proc.StandardOutput.ReadToEnd();

            return res;
        }

    
    }

}
