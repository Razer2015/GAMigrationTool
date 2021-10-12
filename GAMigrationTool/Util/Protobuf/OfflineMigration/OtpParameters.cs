using ProtoBuf;

namespace GoogleAuthenticator.Util.Protobuf.OfflineMigration
{
    [ProtoContract(SkipConstructor = true)]
    public class OtpParameters
    {
        [ProtoMember(1)]
        public byte[] Secret { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public string Issuer { get; set; }

        [ProtoMember(4)]
        public int Algorithm { get; set; }

        [ProtoMember(5)]
        public int Digits { get; set; }

        [ProtoMember(6)]
        public int Type { get; set; }

        [ProtoMember(7)]
        public long Counter { get; set; }

        public override string ToString()
        {
            return $@"| {Base32String.Encode(Secret),-32} | {Name,-32} | {Issuer,-32} | {Algorithm,-10} | {Digits,-6} | {Type,-6} | {Counter,-8} |";
        }

        public string ToString(int secretPad, int namePad, int issuerPad)
        {
            return $@"| {Base32String.Encode(Secret).PadRight(secretPad)} | {Name.PadRight(namePad)} | {(Issuer ?? "").PadRight(issuerPad)} | {Algorithm,-10} | {Digits,-6} | {Type,-6} | {Counter,-8} |";
        }
    }
}
