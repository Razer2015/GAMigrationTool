using System;
using System.IO;
using System.Runtime.InteropServices;
using GoogleAuthenticator.Util;
using GoogleAuthenticator.Util.Protobuf;
using GoogleAuthenticator.Util.Protobuf.OfflineMigration;

namespace GoogleAuthenticator
{
    class Program
    {
        static void PrintUsage()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            Console.WriteLine($@"
#################################################################
# GoogleAuthenticator - OfflineMigration Visualiser by xfileFIN #
# Version: {version}                                              #
#################################################################
Usage: {(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "dotnet GAMigrationTool.dll" : "GAMigrationTool.exe")} <otpData>

Instructions:
1. Scan the export accounts QR Code from the Google Authenticator app
2. Give the whole string as in input to this application.
3. Check the generated accounts.txt or console for results.

Example:
{(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "dotnet GAMigrationTool.dll" : "GAMigrationTool.exe")} otpauth-migration://offline/?data=CjAKCrlQshUNlIgoknISEnhmaWxlRklOJ3MgQWNjb3VudBoIeGZpbGVGSU4gASgBMAIQARgBKNXJ3tcC");

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1)
                {
                    PrintUsage();
                    return;
                }

                var payload = ParsePayload(args[0]);

                WriteAccounts(payload, "accounts.txt");
                PrintAccounts(payload);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to parse the given data. More detailed exception below.\n\n");
                Console.WriteLine(ex.Message);
                return;
            }
        }

        static MigrationPayload ParsePayload(string data)
        {
            return ProtobufSerializer.ProtoDeserialize<MigrationPayload>(Base64.Decode(OtpAuth.GetData(data)));
        }

        static string WritePayload(MigrationPayload payload)
        {
            return OtpAuth.WriteData(Base64.Encode(ProtobufSerializer.ProtoSerialize<MigrationPayload>(payload)));
        }

        static void WriteAccounts(MigrationPayload payload, string fileName)
        {
            File.WriteAllText(fileName, payload.ToString());
        }

        static void PrintAccounts(MigrationPayload payload)
        {
            Console.WriteLine(payload.ToString());
        }
    }
}
