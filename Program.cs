using System;
using System.Text;
using System.IO;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using iTextSharp.text;
using System.Linq;
using System.Text.RegularExpressions;

namespace PdfToText
{
    class Student
    {
        public string Fam { get; set; }
        public string Name { get; set; }
        public string Oth{ get; set; }

        public Student(string fam,string nam,string oth)
        {
            Fam=fam;
            Name=nam;
            Oth=oth;
        }
    }
    /// <summary>
    /// The main entry point to the program.
    /// </summary>
    class Program

    {
        static List<Student> texIsTXT;
        static List<string> texIsPDF;

        static void Main(string[] args)
        {

            try
            {
                string file = @"Proceeding_IPMT_2024.pdf";

                if (!File.Exists(file))
                {

                    if (!File.Exists(file))
                    {
                        Console.WriteLine("Please give in the path to the PDF file.");
                    }


                }
                texIsPDF = ExtractTextFromPdf(file);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            try
            {
                string file = @"spisokMag.txt";

                if (!File.Exists(file))
                {

                    if (!File.Exists(file))
                    {
                        Console.WriteLine("Please give in the path to the PDF file.");
                    }


                }
                texIsTXT = ExtractTextFromStudentT(file);

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                Console.ReadLine();
                return;
            }

            using (StreamWriter fstream = new StreamWriter(new FileStream("out.txt", FileMode.OpenOrCreate)))
            {
            
            foreach (Student student in texIsTXT)
            {
                for (int i = 0; i < texIsPDF.Count; i++)
                {
                    if (texIsPDF[i].Contains(student.Fam))
                    {
                            fstream.WriteLine($"{student.Fam} на странице {i}");
                    }    
                        

                }
            }
                
            }

        }

        public static List<Student> ExtractTextFromStudentT(string path)
        {

            List<Student> texIsTXT = new List<Student>();


            string[] lines = File.ReadAllLines(path);
            foreach (string str in lines)
            {
   
                if (!str.Contains("#"))
                {
                    string[] Stud = str.Split(' ');
                    switch (Stud.Length)
                        {

                        case 1:
                            texIsTXT.Add(new Student(Stud[0], "", ""));
                            break;
                        case 2:
                            texIsTXT.Add(new Student(Stud[0], Stud[1], ""));
                            break;
                        case 3:
                            texIsTXT.Add(new Student(Stud[0], Stud[1], Stud[2]));
                            break;
                        default: Console.WriteLine(" ошибка в фале ");
                            break;

                    }



                }
                


            }


            return texIsTXT;
        }

 
        public static List<string> ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                List<string> strings = new List<string>();
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    strings.Add (PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return strings;
            }
        }
    }
}
