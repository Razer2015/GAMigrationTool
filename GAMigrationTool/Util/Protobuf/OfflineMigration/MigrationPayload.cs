using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleAuthenticator.Util.Protobuf.OfflineMigration
{
    [ProtoContract(SkipConstructor = true)]
    public class MigrationPayload
    {
        [ProtoMember(1)]
        public List<OtpParameters> OtpParameters { get; set; }

        [ProtoMember(2)]
        public int Version { get; set; }

        [ProtoMember(3)]
        public int BatchSize { get; set; }

        [ProtoMember(4)]
        public int BatchIndex { get; set; }

        [ProtoMember(5)]
        public int BatchId { get; set; }

        public override string ToString()
        {
            var maxSecret = Math.Max(OtpParameters.Max(x => Base32String.Encode(x.Secret).Length), 6);
            var maxName = Math.Max(OtpParameters.Max(x => x.Name?.Length) ?? 0, 4);
            var maxIssuer = Math.Max(OtpParameters.Max(x => x.Issuer?.Length) ?? 0, 6);

            var rowWidth = maxSecret + maxName + maxIssuer + 10 + 6 + 6 + 8 + 18;
            return $@"
| {new String('-', rowWidth)} |
| {$"{new String(' ', (int)Math.Ceiling((double)(rowWidth - 63) / 2))} GoogleAuthenticator - OfflineMigration Visualiser by xfileFIN {new String(' ', (rowWidth - 63) / 2)}"} |
| {new String('-', rowWidth)} |
| {$"Version: {Version}".PadRight(rowWidth)} |
| {$"Batch Size: {BatchSize}".PadRight(rowWidth)} |
| {$"Batch Index: {BatchIndex}".PadRight(rowWidth)} |
| {$"Batch Id: {BatchId}".PadRight(rowWidth)} |
| {new String('-', rowWidth)} |
| {$"{new String(' ', (int)Math.Ceiling((double)(rowWidth - 10) / 2))} Accounts {new String(' ', (rowWidth - 10) / 2)}"} |
| {new String('-', rowWidth)} |
| {"Secret".PadRight(maxSecret)} | {"Name".PadRight(maxName)} | {"Issuer".PadRight(maxIssuer)} | {"Algorithm",-10} | {"Digits",-6} | {"Type",-6} | {"Counter",-8} |
| {new String('-', maxSecret)} | {new String('-', maxName)} | {new String('-', maxIssuer)} | {new String('-', 10)} | {new String('-', 6)} | {new String('-', 6)} | {new String('-', 8)} |
{string.Join(Environment.NewLine, OtpParameters.Select(x => x.ToString(maxSecret, maxName, maxIssuer)))}
| {new String('-', rowWidth)} |
";
        }
    }
}
