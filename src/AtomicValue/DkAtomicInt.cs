namespace Tool.Compet.Core;

using System.Threading;

public class DkAtomicInt {
	private long rawValue;

	public int value => (int)Interlocked.Read(ref this.rawValue);

	public int Increment() {
		return (int)Interlocked.Increment(ref this.rawValue);
	}

	public int Decrement() {
		return (int)Interlocked.Decrement(ref this.rawValue);
	}

	public int Add(int more) {
		return (int)Interlocked.Add(ref this.rawValue, more);
	}
}
