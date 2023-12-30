namespace Common.Domain;
public static class SequentialGuidGenerator
{
    private static readonly object LockObject = new();
    private static long _lastTimestamp = DateTime.UtcNow.Ticks;

    public static Guid GenerateNewGuid()
    {
        lock (LockObject)
        {
            var timestamp = DateTime.UtcNow.Ticks;
            if (timestamp <= _lastTimestamp)
            {
                timestamp = _lastTimestamp + 1;
            }

            _lastTimestamp = timestamp;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            var guidBytes = Guid.NewGuid().ToByteArray();
            Array.Copy(timestampBytes, timestampBytes.Length - 6, guidBytes, guidBytes.Length - 6, 6);

            var sequentialGuid = new Guid(guidBytes);
            return sequentialGuid;
        }
    }
}
