using System;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
using ClassLibrary1;

namespace schedule
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Class1 schedule = new Class1();
            schedule.sched();
        }
    }
}