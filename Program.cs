using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SBW;
using WatiN.Core;

namespace SBMLValidator
{
    public class SBMLValidator
    {
      public static void OpenValidator()
      {
        new IE("http://sbml.org/validator");
      }

        private static string _lastSBML;
        public static void ValidateSBML(string sbml)
        {
            var browser = new IE("http://sbml.org/validator");
            browser.TextField(Find.ByName("rawSBML")).Value =
                            sbml;

            var link = 
                browser.
                    Links.FirstOrDefault(s => s.Url.Contains("fromPaste"));
            if (link != null)
                link.Click();
        }

        public static void doAnalysis(string sbml)
        {
            _lastSBML = sbml;
            var thread = new Thread(StartSBML);
            thread.SetApartmentState(ApartmentState.STA); //Set the thread to STA
            thread.Start();
        }

        private static void StartSBML()
        {
            ValidateSBML(_lastSBML);
        }
    }

    static class Program
    {
        private static object _instance;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _instance = new SBMLValidator();

            if (args.Length == 1 && File.Exists(args[0]))
            {
                SBMLValidator.ValidateSBML(File.ReadAllText(args[0]));
                return;
            }

          if (args.Length == 0)
            SBMLValidator.OpenValidator();

            try
            {
                DefaultModule.Run(ref _instance,
                    "SBMLValidator",
                    "SBML Online Validator",
                    "Sends the model to the SBML online validator",
                    LowLevel.ModuleManagementType.UniqueModule,
                    "validate",
                    "SBML Online Validator",
                    "/Analysis",
                    "Sends the model to the SBML online validator",
                    args);

            }
            catch
            {
                
            }
        }
    }
}
