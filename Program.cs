using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Kbackup
{
    class Program
    {
        public string searchPatten;
        public string target;
        public int counter = 0;

        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine( "args length {0}", args.Length );
            if(args.Length != 3)
                p.DisplayUsage();
            else
            {
                p.searchPatten = args[1];
                p.target = args[2];
                Console.WriteLine( "Source = {0}", args[0] );
                Console.WriteLine( "Search = {0} ", p.searchPatten );
                Console.WriteLine( "Target = {0}", p.target );
            }

            Console.WriteLine( "Hit Enter to Continue!" );
            Console.ReadLine();

            if(!Directory.Exists( p.target ))
                Directory.CreateDirectory( p.target );

            p.DirSearch( args[0] );
            p.FileSearch( args[0] );
        }//end of Main

        /// <summary>
        /// Search files in sub-directory. Files in current directory will not search.
        /// </summary>
        /// <param name="sDir"></param>
        public void DirSearch(string sDir)
        {
            string fn, fn2;
            try
            {
                foreach(string d in Directory.GetDirectories( sDir ))
                {
                    //foreach(string f in Directory.GetFiles( d, searchPatten ))
                    //{
                    //    fn = Path.GetFileName( f );
                    //    Console.WriteLine( f );
                    //    if(File.Exists( target + "\\" + fn ))
                    //    {
                    //        counter++;
                    //        fn2 = Path.GetFileNameWithoutExtension( fn ) + counter + Path.GetExtension( fn );
                    //        //Console.WriteLine( target + "\\" + fn2 );
                    //        File.Copy( f, target + "\\" + fn2 );
                    //    }
                    //    else
                    //    {
                    //        //Console.WriteLine( "fn> " + fn );
                    //        File.Copy( f, target + "\\" + fn );
                    //    }
                    //}
                    FileSearch( d );
                    DirSearch( d );
                }
            }
            catch(System.Exception excpt)
            {
                Console.WriteLine( excpt.Message );
            }
        }//end of DirSearch

        /// <summary>
        /// Search files in the current directory.
        /// </summary>
        /// <param name="cDir"></param>
        public void FileSearch(string cDir)
        {
            string fn;
            string fn2;

            try
            {
                foreach(string f in Directory.GetFiles( cDir, searchPatten ))
                {
                    fn = Path.GetFileName( f );
                    Console.WriteLine( f );
                    if(File.Exists( target + "\\" + fn ))
                    {
                        counter++;
                        fn2 = Path.GetFileNameWithoutExtension( fn ) + counter + Path.GetExtension( fn );
                        //Console.WriteLine( target + "\\" + fn2 );
                        File.Copy( f, target + "\\" + fn2 );
                    }
                    else
                    {
                        //Console.WriteLine( "fn> " + fn );
                        File.Copy( f, target + "\\" + fn );
                    }
                }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine( ex.Message );
            }
        }//end of fileSearch

        public void DisplayUsage()
        {
            Console.WriteLine( "Usage:   Kbankup.exe <source> <extension> <target>" );
            Console.WriteLine( "Example: Kbankup.exe c:\\tmp *.doc f:" );            
        }//end of DisplayUsage
    }
}
