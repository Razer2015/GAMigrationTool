using ProtoBuf;
using System.IO;

namespace GoogleAuthenticator.Util.Protobuf
{
    public static class ProtobufSerializer
    {
        public static byte[] ProtoSerialize<T>(T record) where T : class
        {
            if (null == record) return null;

            using var stream = new MemoryStream();
            Serializer.Serialize(stream, record);
            return stream.ToArray();
        }

        public static T ProtoDeserialize<T>(byte[] data) where T : class
        {
            if (null == data) return null;

            using var stream = new MemoryStream(data);
            return Serializer.Deserialize<T>(stream);
        }
    }
}
