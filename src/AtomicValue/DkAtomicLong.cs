namespace Tool.Compet.Core;

using System.Threading;

public class DkAtomicLong {
	private long rawValue;

	public long value => Interlocked.Read(ref this.rawValue);

	public long Increment() {
		return Interlocked.Increment(ref this.rawValue);
	}

	public long Decrement() {
		return Interlocked.Decrement(ref this.rawValue);
	}

	public long Add(long more) {
		return Interlocked.Add(ref this.rawValue, more);
	}
}
