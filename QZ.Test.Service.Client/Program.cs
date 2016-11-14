using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QZ.Instrument.LogClient;
using QZ.Foundation.Model;

namespace QZ.Test.Service.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var log_M = new Log_M() {
                Id = 0,
                Analysis = "analysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysisanalysis",
                Location = Location.Enter,
                Message = "messagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessagemessage",
                Uri = "uri" };
            var proxy = new Proxy();
            while(true)
            {
                Console.WriteLine("please press 'q' to exit...");
                var key = Console.Read();
                if (key == 'q' || key == 'Q')
                    break;

                log_M.Id++;

                proxy.Log_Info(log_M);
            }
        }
    }
}
